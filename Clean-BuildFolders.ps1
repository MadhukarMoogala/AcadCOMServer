<#
.SYNOPSIS
    Cleans Visual Studio and .NET build folders (bin, obj, Debug, Release, etc.)

.DESCRIPTION
    Recursively searches the specified path for common build output directories
    and removes them or their contents.

.PARAMETER Path
    The root directory to start searching from. Defaults to current directory.

.PARAMETER KeepFolders
    If specified, only deletes the contents inside build folders (keeps the folder structure).

.PARAMETER WhatIf
    Runs the script in preview mode (shows what would be deleted without actually deleting).

.EXAMPLE
    ./Clean-BuildFolders.ps1

.EXAMPLE
    ./Clean-BuildFolders.ps1 -Path "D:\Projects\MySolution" -KeepFolders

.EXAMPLE
    ./Clean-BuildFolders.ps1 -WhatIf
#>

param(
    [string]$Path = ".",
    [switch]$KeepFolders,
    [switch]$WhatIf
)

Write-Host "=== Cleaning build folders in: $((Resolve-Path $Path).Path) ===" -ForegroundColor Cyan

# Common build directories
$targets = Get-ChildItem -Path $Path -Recurse -Directory -Force |
    Where-Object { $_.Name -in 'bin', 'obj', 'Binaries', 'Debug', 'Release' }

if (-not $targets) {
    Write-Host "No build folders found." -ForegroundColor DarkGray
    exit
}

foreach ($folder in $targets) {
    if ($KeepFolders) {
        Write-Host "Cleaning contents of: $($folder.FullName)" -ForegroundColor Yellow
        Get-ChildItem -Path $folder.FullName -Recurse -Force -File |
            ForEach-Object {
                if ($WhatIf) {
                    Write-Host "  [WhatIf] Would delete file: $($_.FullName)" -ForegroundColor DarkGray
                } else {
                    Remove-Item $_.FullName -Force -ErrorAction SilentlyContinue
                }
            }
    } else {
        Write-Host "Deleting folder: $($folder.FullName)" -ForegroundColor Red
        if ($WhatIf) {
            Write-Host "  [WhatIf] Would remove folder and all contents" -ForegroundColor DarkGray
        } else {
            Remove-Item -Path $folder.FullName -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
}

Write-Host "=== Clean complete! ===" -ForegroundColor Green
