Write-Host "Installing Chrome Selenium driver..." -ForegroundColor Cyan

$destPath = 'C:\Tools\WebDriver'

if(-not (Test-Path $destPath)) {
    New-Item $destPath -ItemType directory -Force | Out-Null
}


Remove-Item -Path C:\Tools\WebDriver\chromedriver.exe -Force
Remove-Item -Path C:\Tools\WebDriver\LICENSE.chromedriver -Force
$zipPath = "$env:TEMP\chromedriver_win32.zip"
(New-Object Net.WebClient).DownloadFile('https://storage.googleapis.com/chrome-for-testing-public/140.0.7339.81/win32/chromedriver-win32.zip', $zipPath)
7z x $zipPath -aoa -o"$destPath"
del $zipPath
Get-ChildItem $destPath
Add-Path $destPath

Write-Host "Installed Chrome Selenium driver" -ForegroundColor Green
