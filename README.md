# Phanerozoic

## Naming
> [顯生宙](https://zh.wikipedia.org/wiki/%E6%98%BE%E7%94%9F%E5%AE%99)

## Coverage Tool
### dotcover
```
dotcover cover /TargetExecutable="D:\Program Files\NUnit 2.6\bin\nunit-console.exe"
  /TargetArguments="D:\Projects\TheApplication\bin\Debug\AppTests.dll"
  /Output="AppCoverageReport.html"
  /ReportType="HTML"
```
- ReportType  
[HTML|JSON|XML|DetailedXML|NDependXML]. A type of the report. XML by default

> [Coverage Analysis from the Command Line](https://www.jetbrains.com/help/dotcover/Running_Coverage_Analysis_from_the_Command_LIne.html)
> [report](https://www.jetbrains.com/help/dotcover/dotCover__Console_Runner_Commands.html#report)

### coverlet
```
coverlet "D:\Code\Taiwan\NineYi.Sms\Test\BL.Services.Test.Xunit\bin\Debug\NineYi.Sms.Validators.Test.Xunit.dll" --target "D:\Code\Taiwan\NineYi.Sms\packages\xunit.runner.console.2.2.0\tools\xunit.console.exe"
```

> [GitHub](https://github.com/tonerdo/coverlet)