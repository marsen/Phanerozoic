using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phanerozoic.Core.Services
{
    public class FileUpdater : ICoverageUpdater
    {
        private IFileHelper _fileHelper;

        public FileUpdater(IServiceProvider serviceProvider)
        {
            this._fileHelper = serviceProvider.GetService<IFileHelper>();
        }

        public void Update(CoverageEntity coverageEntity, List<MethodEntity> methodList)
        {
            var stringBuilder = new StringBuilder();
            methodList.ForEach(i => stringBuilder.AppendLine(i.ToString()));

            string contents = stringBuilder.ToString();

            this._fileHelper.WriteAllText("a.txt", contents);
        }
    }
}