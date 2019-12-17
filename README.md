# Phanerozoic

## Naming
> [顯生宙](https://zh.wikipedia.org/wiki/%E6%98%BE%E7%94%9F%E5%AE%99)

## Coverage Tool
### dotcover
```
../../../../../lib/JetBrains.dotCover.CommandLineTools.2019.2.3/dotcover cover /TargetExecutable="C:\Program Files\dotnet\dotnet.exe" /TargetArguments="test ./Phanerozoic.Core.Test.dll" /Output="AppCoverageReport.html" /ReportType="HTML"
```
- ReportType  
[HTML|JSON|XML|DetailedXML|NDependXML]. A type of the report. XML by default

> [Download dotCover](https://www.jetbrains.com/dotcover/download/#section=commandline)
> [Coverage Analysis from the Command Line](https://www.jetbrains.com/help/dotcover/Running_Coverage_Analysis_from_the_Command_LIne.html)
> [report](https://www.jetbrains.com/help/dotcover/dotCover__Console_Runner_Commands.html#report)

### coverlet
```
coverlet "dotnet" --target "Phanerozoic.Core.Test.dll"
```

> [GitHub](https://github.com/tonerdo/coverlet)
> [Docker 教學 - .NET Core 測試報告 (Coverlet + ReportGenerator)](https://blog.johnwu.cc/article/docker-dotnet-coverage-report-generator.html)

### ReportGenerator

> [GitHub](https://github.com/danielpalme/ReportGenerator)