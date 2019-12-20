using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace Phanerozoic.Core.Services.Tests
{
    public class DotCoverParserTests
    {
        private readonly IServiceProvider _subServiceProvider;
        private readonly IFileHelper _subFileHelper;

        public DotCoverParserTests()
        {
            this._subFileHelper = Substitute.For<IFileHelper>();

            this._subServiceProvider = Substitute.For<IServiceProvider>();
            this._subServiceProvider.GetService<IFileHelper>().Returns(this._subFileHelper);
        }

        [Fact(DisplayName = "取得涵蓋率資料")]
        public void ParserTest()
        {
            //// arrange
            var report = new DotCoverReport
            {
                DotCoverVersion = "2019.2.3",
                Kind = Kind.Root,
                CoveredStatements = 11937,
                TotalStatements = 160364,
                CoveragePercent = 7,
                Children = new List<DotCoverReportChild>
                {
                    new DotCoverReportChild
                    {
                        Kind = Kind.Assembly,
                        Name = "Phanerozoic.Core",
                        CoveredStatements = 11937,
                        TotalStatements = 160364,
                        CoveragePercent = 7,
                        Children = new List<DotCoverReportChild>
                        {
                            new DotCoverReportChild
                            {
                                Kind = Kind.Assembly,
                                Name = "Phanerozoic.Core",
                                CoveredStatements = 11937,
                                TotalStatements = 160364,
                                CoveragePercent = 7,
                                Children = new List<DotCoverReportChild>
                                {
                                    new DotCoverReportChild
                                    {
                                        Kind = Kind.Namespace,
                                        Name = "Phanerozoic.Core.Services",
                                        CoveredStatements = 22,
                                        TotalStatements = 11,
                                        CoveragePercent = 7,
                                        Children = new List<DotCoverReportChild>
                                        {
                                            new DotCoverReportChild
                                            {
                                                Kind = Kind.Type,
                                                Name = "DotCoverParser",
                                                CoveredStatements = 22,
                                                TotalStatements = 11,
                                                CoveragePercent = 7,
                                                Children = new List<DotCoverReportChild>
                                                {
                                                    new DotCoverReportChild
                                                    {
                                                        Kind = Kind.Method,
                                                        Name = "Parser(ReportEntity):List<CoverageEntity>",
                                                        CoveredStatements = 22,
                                                        TotalStatements = 11,
                                                        CoveragePercent = 7,
                                                        Children = new List<DotCoverReportChild>
                                                        {
                                                        }
                                                    }
                                                }
                                            },
                                        }
                                    },
                                }
                            },
                        }
                    },
                }
            };
            string reportJson = JsonSerializer.Serialize(report);

            this._subFileHelper.ReadAllText(Arg.Any<string>()).Returns(reportJson);

            var reportEntity = new ReportEntity
            {
                FilePath = "report.json"
            };

            //// act
            var target = GetTarget();
            var actual = target.Parser(reportEntity);

            actual.Count.Should().Be(1);
        }

        private DotCoverParser GetTarget()
        {
            return new DotCoverParser(this._subServiceProvider);
        }
    }
}