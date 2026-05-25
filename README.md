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
- 開発言語: C#
	- フロントエンド: Blazor
		- UI構築: Blazor Blueprint
		- テスト: Playwright
	- バックエンド: ASP. NET Core
		- CQRS: Mediator.SourceGenerator
		- ORM: EntityFramework
		- テスト: xUnit
- データベース: Postgres
	- ER図: Mermaid/C4
- AI: GitHub Copilot
	- スキル: `npx skills`
- バージョン管理: GitHub


## ビルドと実行

前提: .NET 10 SDK と Node.js (npx) がインストールされていること

- クライアント生成 (openapi 変更時):

```bash
npx nswag run nswag.json
```

- サーバ起動:

```bash
cd src\Server
dotnet run
```

- フロント起動 (ホスト経由):

```bash
cd src\Web
dotnet run
```

  またはクライアント単体でビルド/確認:

```bash
cd src\Web\Web.Client
dotnet build
```

- 全体ビルド:

```bash
dotnet build
```

- テスト実行:

```bash
dotnet test
```

開発の基本フロー: Docker Compose を使って全サービス（DB, API, フロント, リバースプロキシ）を一括起動します（推奨）。

必要条件: Docker がインストールされ、WSL 統合が有効であること（Windows の場合）。

起動（WSL 推奨）:

```bash
# WSL のシェル（例: Ubuntu）を開き、リポジトリのルートで実行
docker compose up --build -d
```

Windows の PowerShell から起動したい場合は補助スクリプトを利用できます（WSL を呼び出します）:

```powershell
.\scripts\run-docker-wsl.ps1
```

アクセス: ブラウザで http://localhost:8080 を開いてください。

停止:

```bash
docker compose down
```

補足: openapi.yml を編集した場合はクライアントを再生成してください（ホスト環境で実行）:

```bash
npx nswag run nswag.json
```
