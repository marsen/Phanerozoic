Write-Host 'coverlet' -ForegroundColor Green
# --format
coverlet ..\src\Phanerozoic.Core.Test\bin\Debug\netcoreapp3.1\Phanerozoic.Core.Test.dll --target "dotnet" --targetargs "test ..\src\Phanerozoic.Core.Test --no-build" --output .\output\coverlet.xml --format opencover

Write-Host 'ReportGenerator' -ForegroundColor Green
# -reporttypes: Html Cobertura CsvSummary
reportgenerator -reports:./output/coverlet.xml -reporttypes:Html -targetdir:./output/ReportGenerator