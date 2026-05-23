# セットアップ

セットアップ手順を記載する。

## Agents skills

```shell
npx skills add kepano/obsidian-skills --all
npx skills add aj-geddes/useful-ai-prompts --all
npx skills add speakeasy-api/skills --all
npx skills add microsoft/playwright-cli --all
npx skills add GoogleChrome/modern-web-guidance --all
npx @blazorblueprint/mcp@latest
npx @playwright/mcp@latest
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
dotnet add package NewtonsoftJson
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
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package Mediator.SourceGenerator
dotnet add package Mediator.Abstractions

# Code Generate.
# create nswag.json
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