using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Services;
using Xunit;

namespace Phanerozoic.Core.Test.Services
{
    public class CoverageProcessorTest
    {
        [Fact]
        public void Test_Process_Flow()
        {
            var reportEntity = new ReportEntity
            {
                FilePath = "report.json"
            };
            var target = new CoverageProcessor();

            //// act
            target.Process(reportEntity);

            //// assert
            Assert.True(true);
        }
    }
}