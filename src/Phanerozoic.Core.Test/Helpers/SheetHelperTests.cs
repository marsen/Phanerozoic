using FluentAssertions;
using Xunit;

namespace Phanerozoic.Core.Helpers.Tests
{
    public class SheetHelperTests
    {
        [Fact(DisplayName = "Sheet 索引值與欄位轉換測試")]
        public void ColumnToLetterTest()
        {
            this.ColumnLetterAssert(1, "A");
            this.ColumnLetterAssert(26, "Z");
            this.ColumnLetterAssert(27, "AA");
            this.ColumnLetterAssert(52, "AZ");
        }

        private void ColumnLetterAssert(int column, string letter)
        {
            //// Target
            var actual = SheetHelper.ColumnToLetter(column);

            //// Assert
            actual.Should().Be(letter);
        }
    }
}