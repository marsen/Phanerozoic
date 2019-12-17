coverlet ..\src\Phanerozoic.Core.Test\bin\Debug\netcoreapp3.1\Phanerozoic.Core.Test.dll --target "dotnet" --targetargs "test ..\src\Phanerozoic.Core.Test --no-build" --output .\output\ --format opencover

# -reporttypes: Html Cobertura CsvSummary
reportgenerator -reports:./output/coverage.opencover.xml -reporttypes:Cobertura -targetdir:./output/report