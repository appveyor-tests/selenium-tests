Write-Host "Installing Chrome Selenium driver..." -ForegroundColor Cyan

$destPath = 'C:\Tools\WebDriver'

if(-not (Test-Path $destPath)) {
    New-Item $destPath -ItemType directory -Force | Out-Null
}


Remove-Item -Path C:\Tools\WebDriver\chromedriver.exe -Force
Remove-Item -Path C:\Tools\WebDriver\LICENSE.chromedriver -Force
$zipPath = "$env:TEMP\chromedriver_win32.zip"
(New-Object Net.WebClient).DownloadFile('https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/116.0.5845.96/win32/chromedriver-win32.zip', $zipPath)
7z x $zipPath -aoa -o"$destPath"
del $zipPath

Add-Path $destPath

Write-Host "Installed Chrome Selenium driver" -ForegroundColor Green
