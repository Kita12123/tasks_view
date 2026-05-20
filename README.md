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
	- 設計図: Mermaid/C4
- API設計: OpenAPI
	- 定義ファイル: openapi.yml
	- コード生成: NSwag
- 開発言語: C#
	- フロントエンド: Blazor
		- UI構築: Blazor Blueprint
		- テスト: Playwright
	- バックエンド: ASP. NET Core
		- CQRS: Mediator.SourceGenerator
		- ORM: EntityFramework
		- テスト: xUnit
- データベース: Postgres
- AI: GitHub Copilot
	- スキル: `npx skills`
- バージョン管理: GitHub


## Agents skills

- 設計
	- ドキュメント: `.agents/skills/obsidian-markdown`
	- 画面デザイン: `.agents/skills/json-canvas`
	- ER図: `.agents/skills/architecture-diagrams`
- API
	- openapi.yml: `.agents/skills/writing-openapi-specs`
- コーディング
	- バックエンド: `.agents/skills/start-new-sdk-project`
- テスト
	- 画面制御: `.agents/skills/playwright-cli`
