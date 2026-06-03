# セットアップ

セットアップ手順を記載する。

## Agents skills

```shell
# ドキュメント関連スキル (json-canvas, obsidian-markdown)
npx skills add kepano/obsidian-skills
# 図・デザイン関連スキル (architecture-diagrams, database-schema-design, database-schema-documentation)
npx skills add aj-geddes/useful-ai-prompts
# API関連スキル (writing-openapi-specs)
npx skills add speakeasy-api/skills
# テスト関連スキル (playwright-cli)
npx skills add microsoft/playwright-cli
# コード規約スキル (modern-web-guidance)
npx skills add GoogleChrome/modern-web-guidance
# 自作スキル
npx skills add https://github.com/Kita12123/dev-ctx.git --agent github-copilot --all
```

## Front Project

```shell
# Initializing.
cd src
dotnet new install BlazorBlueprint.Templates
dotnet new blazorblueprint -n Web

# Install Nuget Package.
cd Web
dotnet add package BlazorBlueprint.Components
dotnet add package BlazorBlueprint.Icons.Lucide
cd Web.Client
# System.Text.Json is built into .NET; no package required
```

## Backend Project

```shell
# Initializing.
cd src
dotnet new webapi -o Server --no-https -n Server

# Install Nuget Package.
cd Server
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
# Microsoft.AspNetCore.Mvc.NewtonsoftJson is not required; use System.Text.Json (built-in)
dotnet add package Mediator.SourceGenerator
dotnet add package Mediator.Abstractions
```

## Test Tool

```shell
# Playwrightのインストール
npm install @playwright/cli@latest
npx playwright install
```

## Code Generate

```shell
# nswag.jsonは、NSwagStudio等で作成して、プロジェクトルートに配置すること。
npx nswag run "../../nswag.json"
```

## Docker

```shell
# WSL起動
wsl
# パッケージ更新
sudo apt-get update
# 依存パッケージをインストール
sudo apt-get install ca-certificates curl gnupg lsb-release
# GPG登録 (Dockerをインストールするために必要な公開鍵をインストールする)
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```