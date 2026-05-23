Param(
    [string]$RepoPath = (Get-Location).Path
)

# Convert Windows path to WSL path (try wslpath, fall back to manual conversion)
$wslPath = $null
try {
    $wslPathResult = & wsl wslpath -a "$RepoPath" 2>&1
} catch {
    $wslPathResult = $null
}
if ($LASTEXITCODE -eq 0 -and $wslPathResult) {
    $wslPath = $wslPathResult.Trim()
} else {
    # Fallback: convert C:\path\to\repo -> /mnt/c/path/to/repo
    $win = $RepoPath.Trim()
    if ($win -match '^[A-Za-z]:') {
        $drive = $win.Substring(0,1).ToLower()
        $rest = $win.Substring(2) -replace '\\','/'
        $wslPath = "/mnt/$drive$rest"
    } else {
        Write-Error "Cannot convert path to WSL path: $RepoPath"
        exit 1
    }
}

# Build bash command safely (expand $wslPath, keep sed $ literal by escaping)
$bashCmd = @"
cd '$wslPath'
if [ -f ./scripts/docker-up-wsl.sh ]; then
  sed -i 's/\r`$//' ./scripts/docker-up-wsl.sh || true
fi
./scripts/docker-up-wsl.sh
"@

# Remove CR characters to avoid carriage return issues when passing to bash
$bashCmd = $bashCmd -replace "`r", ""

# Execute in WSL
& wsl bash -lc $bashCmd
