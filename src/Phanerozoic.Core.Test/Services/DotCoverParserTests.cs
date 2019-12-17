using Phanerozoic.Core.Entities;
using Xunit;

namespace Phanerozoic.Core.Services.Tests
{
    public class DotCoverParserTests
    {
        public DotCoverParserTests()
        {
        }

        [Fact(DisplayName = "載入檔案")]
        public void ParserTest()
        {
            //// arrange
            var reportEntity = new ReportEntity
            {
                FilePath = ""
            };

            //// act
            var target = GetTarget();
            target.Parser(reportEntity);

            Assert.True(false, "This test needs an implementation");
        }

        private DotCoverParser GetTarget()
        {
            return new DotCoverParser();
        }
    }
}