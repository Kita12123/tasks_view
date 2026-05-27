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
- ドキュメントは Obsidian 形式で `docs/` に Markdown で置く。
- API 仕様は `openapi.yml` に OpenAPI 形式で置く。
- OpenAPI コード自動生成は NSwag を使用して `nswag.json` で管理する。
- AI スキルは `.agents/skills` と `skills-lock.json` で管理される前提。 ## Agents skills を参照のこと。

## Agents skills

- 設計
	- Markdown: `.agents/skills/obsidian-markdown`
	- 画面・デザイン: `.agents/skills/json-canvas`
	- ER図: `.agents/skills/database-schema-design`, `.agents/skills/database-schema-documentation`
	- openapi.yml: `.agents/skills/writing-openapi-specs`, `docs/skills/OpenAPI規約/SKILL.md`
- コーディング
	- コード規約: `.agents/skills/modern-web-guidance`, `docs/skills/コード規約/SKILL.md`
  - UI: `https://blazorblueprintui.com/llms.txt`, `docs/skills/UI規約/SKILL.md`
- テスト
	- E2E: `.agents/skills/playwright-cli`