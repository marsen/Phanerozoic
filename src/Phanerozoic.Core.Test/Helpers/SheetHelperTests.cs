using Xunit;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Phanerozoic.Core.Helpers.Tests
{
    public class SheetHelperTests
    {
        [Fact()]
        public void CodeToIndexTest()
        {
            //// Arrange
            var code = "Z";

            var exptectd = 26;

            //// Target
            var actual = SheetHelper.CodeToIndex(code);

            //// Assert
            actual.Should().Be(exptectd);
        }

        private object GetTarget()
        {
            throw new NotImplementedException();
        }
    }
}