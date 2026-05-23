#!/usr/bin/env bash
set -euo pipefail

# Determine repository root (script is located in scripts/)
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

echo "Starting Docker Compose in WSL at $REPO_ROOT"
# Normalize line endings of this script in case of CRLF (idempotent)
if command -v sed >/dev/null 2>&1; then
  sed -i 's/\r$//' ./scripts/docker-up-wsl.sh || true
fi

# Build and start services
docker compose up --build -d

echo "--- docker compose ps ---"
docker compose ps

echo "--- recent logs ---"
docker compose logs --no-color --tail 200
