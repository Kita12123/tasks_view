# セットアップ

セットアップ手順を記載する。

## Agents skills

```shell
# 図・デザイン関連スキル (architecture-diagrams, database-schema-design, database-schema-documentation)
npx skills add aj-geddes/useful-ai-prompts --agent github-copilot
# API関連スキル (writing-openapi-specs)
npx skills add speakeasy-api/skills --agent github-copilot
# フロントエンドコーディング関連スキル
npx skills add shadcn/ui --agent github-copilot
# 自作スキル (dev-ctx)
npx skills add https://github.com/Kita12123/dev-ctx.git --agent github-copilot --all
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