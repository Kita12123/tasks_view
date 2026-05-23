Param(
    [string]$RepoPath = $(Get-Location).Path
)

# Convert Windows path to WSL path
$wslPath = & wsl wslpath -a "$RepoPath"
if ($LASTEXITCODE -ne 0) {
    Write-Error "WSL path conversion failed. Ensure WSL is installed and wslpath is available."
    exit 1
}
$wslPath = $wslPath.Trim()

# Run the WSL helper script (normalize line endings then execute)
$cmd = "bash -lc 'cd ""$wslPath"" && if [ -f ./scripts/docker-up-wsl.sh ]; then sed -i \"s/\r$//\" ./scripts/docker-up-wsl.sh || true; fi && ./scripts/docker-up-wsl.sh'"
wsl $cmd
