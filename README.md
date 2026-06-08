# タスク管理アプリ

タスク(TODOリスト)を管理するWebアプリケーションです。

## 開発環境

- AI駆動開発
	- `.agents/skills`下にスキルを格納します。 (npx skills add ...)
- ドキュメントファースト
	- `docs`下に仕様を定義します。
	- メタデータには`tags`を記述します。
		- 画面: 画面設計ドキュメント
		- 機能: 機能仕様ドキュメント

## 技術選定

- ドキュメント: Obsidian
	- 画面設計: json-canvas
- API設計: OpenAPI
	- 定義ファイル: openapi.yml
	- コード生成: NSwag
- 開発言語: C#, TypeScript
	- フロントエンド: React
		- UI構築: shadcn/ui
	- バックエンド: ASP. NET Core
		- CQRS: Mediator.SourceGenerator
		- ORM: EntityFramework
		- テスト: xUnit
- データベース: Postgres
	- ER図: Mermaid/C4
- AI: GitHub Copilot
	- スキル: `npx skills`
- バージョン管理: GitHub