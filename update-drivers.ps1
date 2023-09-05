Write-Host "Installing Chrome Selenium driver..." -ForegroundColor Cyan

$destPath = 'C:\Tools\WebDriver'

if(-not (Test-Path $destPath)) {
    New-Item $destPath -ItemType directory -Force | Out-Null
}

$zipPath = "$env:TEMP\chromedriver_win32.zip"
(New-Object Net.WebClient).DownloadFile('https://chromedriver.storage.googleapis.com/100.0.4896.20/chromedriver_win32.zip', $zipPath)
7z x $zipPath -aoa -o"$destPath"
del $zipPath

Add-Path $destPath

Write-Host "Installed Chrome Selenium driver" -ForegroundColor Green
