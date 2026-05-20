# Copilot Instructions for this Repository

## Code generate

- `openapi.yml`からコマンドを使用して、コードを生成すること。
```shell
# \tasks_view>
npx nswag run nswag.json
```

## High-level architecture

- このリポジトリは **ドキュメントファースト** で進める構成。仕様は `docs/` の Markdown に置く。
- プロジェクト構造
  - `src/Server/Server.csproj`: Backend
  - `src/Web/Web.csproj`: Frontend StartApp
  - `src/Web/Web.Client/Web.Client.csproj`: Frontend
- 技術方針（READMEベース）:
  - Frontend: Blazor + Blazor Blueprint
  - Frontend Test: Playwright
  - Backend: ASP.NET Core + CQRS（Mediator.SourceGenerator）
  - Backend Test: xUnit
  - DB: Postgres
  - API 契約: `openapi.yml`

## Key conventions

- `openapi.yml`には必ず`description`に処理内容を記載する。
- 仕様ドキュメントは `docs/*.md` に置き、先頭に YAML front matter を持つ:
  - `tags`
  - `name`
  - `description`
- ドキュメント間の関連は Obsidian 形式の `[[wiki-link]]` で接続する（例: `[[tasks-show]]`）。
- `tags` は現在 `画面`: 画面設計ドキュメント / `機能`: 画面に付随した機能仕様ドキュメント を使って分類しているため、新規仕様も同じ分類軸を優先する。
- 仕様記述は日本語ベース。既存ドキュメントと同じ言語・粒度で追記する。
- AI スキルは `.agents/skills` と `skills-lock.json` で管理される前提。 ## Agents skills を参照のこと。
- MCP サーバーは `.vscode/mcp.json` で管理し、現時点では Playwright を有効化している。

## Agents skills

- ドキュメント関連を操作する際は、以下のスキルを参照してください。
	- 記述ルール: `.agents/skills/obsidian-markdown`
	- 画面デザイン: `.agents/skills/json-canvas`
	- ER図: `.agents/skills/architecture-diagrams`
  - openapi.yml: `.agents/skills/writing-openapi-specs`
- コード関連を操作する際は、以下のスキルを参照してください。
	- コード規約: `.agents/skills/start-new-sdk-project`
- テスト
	- 画面制御: `.agents/skills/playwright-cli`