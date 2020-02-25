using System.Collections.Generic;
using Phanerozoic.Core.Entities;
using Xunit;

namespace Phanerozoic.Core.Services.Notifications.Tests
{
    public class GmailNotifyerTests
    {
        [Fact()]
        public void NotifyTest()
        {
            //// Assert
            string repository = "Phanerozoic";
            string project = "Phanerozoic.Core";
            var coverageEntity = new CoverageEntity
            {
                Repository = repository,
                Project = project,
            };
            var methodList = new List<MethodEntity>
            {
                new MethodEntity
                {
                    Repository = repository,
                    Project = project,
                    Class = "SomeClass",
                    Method = "SomeMethod",
                    Coverage = 12,
                }
            };

            //// Act
            EmailNotifyer target = this.GetTarget();
            target.Notify(coverageEntity, methodList);

            //// Arrange

        }

        private EmailNotifyer GetTarget()
        {
            return new EmailNotifyer(null);
        }
    }
}