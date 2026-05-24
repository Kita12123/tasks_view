---
tags: ["テスト","E2E","Playwright"]
name: "Playwright E2E テスト設計"
description: "リポジトリ内の Playwright E2E テストの目的、構成、実行方法、アーティファクト管理を定義するドキュメント。"
---

# Playwright E2E テスト設計

## 目的
- ユーザー操作に近い形で重要な画面フローを検証し、回帰を防止する。
- UI の主要操作（タスク追加、編集、保存など）が期待どおり動作することを確認する。
- 不具合発生時にスクリーンショットを残し、原因特定を容易にする。

## 範囲
- テスト実装言語: Playwright (TypeScript)
- カバレッジ: 主要な CRUD 操作、モーダル表示、フォーム入力、保存フロー

## ファイル構成と配置
- 全ての E2E テストは `tests/` ディレクトリに配置。
  - 例: `tests/add-task.spec.ts`, `tests/debug-task.spec.ts`, `tests/inspect.spec.ts`
- テストが出力するアーティファクト（スクリーンショット）は `tests/` 内に保存する。
  - 例: `tests/page-before-click.png` 等

## スクリーンショット / テストアーティファクト管理
- スクリーンショットはデバッグ用でテスト失敗時に確認する。必要に応じて CI のアーティファクトとして保存。
- 不要なスクリーンショットをリポジトリにコミットしない（既にある場合は移動済み）。`tests/` を .gitignore に追加して CI のみ保存する運用も検討。

## 実行手順（ローカル）
1. 開発サーバ起動（例: `dotnet run` などアプリを http://localhost:8080 で公開）
2. テスト実行: `npx playwright test` または `npx playwright test tests/add-task.spec.ts`
3. 失敗時は `tests/` 内のスクリーンショットと Playwright のレポートを確認する。

## CI 統合の指針
- CI ではヘッドレスモードで `npx playwright test --reporter=list` を実行。
- 失敗時のみスクリーンショットやレコード動画をワークフローのアーティファクトとして保存する設定を推奨。

## 命名規約／タイムアウト
- spec ファイル名は `<feature>-<action>.spec.ts`（例: `add-task.spec.ts`）。
- テスト全体タイムアウトは長め（既定: 120000ms）。個別待機は `waitForTimeout` を最小化し、`locator` の `toBeVisible` 等を使用する。

## テストデータと環境
- テストは可能な限り UI テスト用のスタブ API またはテスト DB を使用する。実 DB を直接叩く場合はクリーンアップ手順を明記する。
- 日付や時刻を扱う場合はテスト内で固定化（例: 明日の正午を指定）して再現性を担保する。

## 失敗時の調査フロー
1. スクリーンショットを確認
2. テストログ（console.log）を参照
3. 該当 UI コンポーネントの変更履歴を確認

## 更新手順
- 新しい E2E テストを追加する際は `docs/playwright-test-design.md` を更新し、目的・前提・副作用（外部 API の使用など）を記載する。

---

作成者: test-maintainer
