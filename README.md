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

開発の基本フロー: まずサーバを起動し、ブラウザで http://localhost:5000 を開いてください。

### Docker: 1コマンドで起動

Docker Compose を使って全サービス（DB, API, フロント, リバースプロキシ）を一括で起動できます。ビルドと起動:

```bash
docker compose up --build
```

起動後、ブラウザで http://localhost:8080 を開いてください。停止するには:

```bash
docker compose down
```

WSL で実行する方法（推奨）

- WSL 内で直接実行:
  1. WSL のターミナル（例: Ubuntu）を開く
  2. リポジトリのルートに移動してスクリプトを実行:

```bash
bash ./scripts/docker-up-wsl.sh
```

- Windows PowerShell から WSL 経由で実行（簡単）:

```powershell
.\scripts\run-docker-wsl.ps1
```

PowerShell スクリプトは現在のディレクトリを WSL パスに変換し、WSL 内でスクリプトを実行します。WSL 側で docker が利用可能（Docker Desktop の WSL 統合や WSL 内の Docker CLI）であることを確認してください。


