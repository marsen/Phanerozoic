using Microsoft.Extensions.DependencyInjection;
using Phanerozoic.Core.Entities;
using Phanerozoic.Core.Helpers;
using System;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public class FileUpdater
    {
        private IFileHelper _fileHelper;

        public FileUpdater(IServiceProvider serviceProvider)
        {
            this._fileHelper = serviceProvider.GetService<IFileHelper>();
        }

        public void Update(List<CoverageEntity> coverageList)
        {
            string contents = null;

            this._fileHelper.WriteAllText("", contents);
        }
    }
}