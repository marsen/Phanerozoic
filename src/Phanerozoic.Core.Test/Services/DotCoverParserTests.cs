using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Text;
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
            var report = new DotCoverReport();
            string json = JsonSerializer.Serialize(report);
            this._subFileHelper.ReadAllText(Arg.Any<string>()).Returns(json);

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