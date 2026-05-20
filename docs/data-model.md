# データ設計（Tasks）

このドキュメントは、`openapi.yml` に対応する Tasks（TODO）アプリのデータ設計を示します。Postgres を想定しています。

## 目的
- フロント（Blazor）／バックエンド（ASP.NET Core）で使う永続化スキーマを定義
- 検索（全文検索）、タグ絞り込み、ページングを効率よく実行できる設計
- CQRS を想定して、読み取り用に集約やビューを作りやすい正規化スキーマ

## 主要エンティティ
- tasks: タスク本体
- tags: タグ辞書（正規化）
- tasks_tags: tasks ⇄ tags の多対多マッピング

（OpenAPI フィールド → DB カラム）
- id (uuid) → id
- title (string) → title
- content (string) → content
- dueDate (date-time) → due_date (timestamptz)
- completed (boolean) → completed
- tags (array[string]) → tags は tags/taks_tags による正規化（下記）
- createdAt / updatedAt → created_at / updated_at

## 設計方針（要点）
- ID は UUID（DB 側で生成: gen_random_uuid()）を推奨
- タグは正規化（tags + tasks_tags）を標準とする（タグ検索やトラッキングに有利）
- 全文検索は to_tsvector を利用した GIN インデックスで高速化
- `updated_at` と `version` を用いた楽観的ロック（更新競合検出）を想定
- API の JSON 名前（camelCase） と DB カラム（snake_case）のマッピングは自動設定（System.Text.Json）か DTO レイヤで変換

## ファイル
- 実際の DDL は `docs/001_create_tasks.sql` を参照

## 読み取りクエリ例（Pagination + 検索 + タグ絞り込み）
-- 検索キーワード q、タグ filter_tag、completed フラグ、page/pageSize を受け取る例

-- サンプル（SQL: OFFSET/LIMIT）
SELECT
  t.id, t.title, t.content, t.due_date, t.completed, t.created_at, t.updated_at,
  COALESCE(array_agg(tags.name) FILTER (WHERE tags.name IS NOT NULL), ARRAY[]::text[]) AS tags
FROM tasks t
LEFT JOIN tasks_tags tt ON t.id = tt.task_id
LEFT JOIN tags ON tt.tag_id = tags.id
WHERE
  ( :q IS NULL OR to_tsvector('simple', coalesce(t.title,'') || ' ' || coalesce(t.content,'')) @@ plainto_tsquery('simple', :q) )
  AND ( :completed IS NULL OR t.completed = :completed )
  AND ( :filter_tag IS NULL OR EXISTS (
    SELECT 1 FROM tasks_tags tt2 JOIN tags tg2 ON tt2.tag_id = tg2.id WHERE tt2.task_id = t.id AND lower(tg2.name) = lower(:filter_tag)
  ))
GROUP BY t.id
ORDER BY CASE WHEN :sort = 'dueDate' THEN t.due_date END NULLS LAST, t.created_at DESC
LIMIT :pageSize OFFSET (:page - 1) * :pageSize;

（実アプリではプレースホルダと型を合わせてください）

## タグ格納の代替案
1. 正規化（推奨）: tags / tasks_tags ・タグ名にメタデータを付けやすい・タグ統計が容易
2. 配列（簡便）: tasks.tags TEXT[] + GIN index ・スキーマがシンプルだがタグ管理が難しい

## マテリアライズドビュー（オプション）
読み取り専用の高速 API 用に、タグ配列を含むマテリアライズドビューを作ると CQRS の read model に最適です。

## 運用メモ
- 大量データで全文検索を高速化するには、`pg_trgm` やカスタム辞書の導入を検討
- バックアップ: pg_dump / basebackup
- マイグレーション: EF Core Migrations または Flyway / Liquibase

---
参照: `docs/001_create_tasks.sql` に DDL とインデックス、トリガ定義を収録しています。