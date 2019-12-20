Write-Host 'coverlet' -ForegroundColor Green
# --format
#coverlet "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\WebStore\Frontend\BLV2.Test.Xunit\bin\Debug\NineYi.WebStore.Frontend.BLV2.Test.Xunit.dll" --target "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\packages\xunit.runner.console.2.2.0\tools\xunit.console.exe" --output .\output\coverlet.xml --format opencover
#coverlet "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\WebStore\Frontend\BLV2.Test.Xunit\bin\Debug\NineYi.WebStore.Frontend.BLV2.Test.Xunit.dll" --target "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\packages\xunit.runner.console.2.2.0\tools\xunit.console.exe" --targetargs "test" --output .\output\coverlet.xml --format opencover
#coverlet "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\WebStore\Frontend\BLV2.Test.Xunit\bin\Debug\NineYi.WebStore.Frontend.BLV2.Test.Xunit.dll" --target "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\packages\xunit.runner.console.2.2.0\tools\xunit.console.x86.exe" --output .\output\coverlet.xml --format opencover
#dotnet test "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\WebStore\Frontend\BLV2.Test.Xunit\bin\Debug\NineYi.WebStore.Frontend.BLV2.Test.Xunit.dll" /p:CollectCoverage=true

dotnet test "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\WebStore\Frontend\BLV2.Test.Xunit\NineYi.WebStore.Frontend.BLV2.Test.Xunit.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
#dotnet xunit "D:\Code\Taiwan\NineYi.WebStore.MobileWebMall\WebStore\Frontend\BLV2.Test.Xunit\bin\Debug\NineYi.WebStore.Frontend.BLV2.Test.Xunit.dll"

Write-Host 'ReportGenerator' -ForegroundColor Green
# -reporttypes: Html Cobertura CsvSummary
#reportgenerator -reports:./output/coverlet.xml -reporttypes:Html -targetdir:./output/ReportGenerator