﻿using System.Collections.Generic;

public partial class DotCoverReport
{
    public string DotCoverVersion { get; set; }
    public string Kind { get; set; }
    public long? CoveredStatements { get; set; }
    public long? TotalStatements { get; set; }
    public long? CoveragePercent { get; set; }
    public List<DotCoverReportChild> Children { get; set; }
}

public partial class DotCoverReportChild
{
    public Kind? Kind { get; set; }
    public string Name { get; set; }
    public long? CoveredStatements { get; set; }
    public long? TotalStatements { get; set; }
    public long? CoveragePercent { get; set; }
    public List<DotCoverReportChild> Children { get; set; }
}

public enum Kind { AnonymousMethod, Assembly, AutoProperty, Constructor, InternalCompiledMethod, Method, Namespace, OwnCoverage, Property, PropertyGetter, PropertySetter, Type };