using Phanerozoic.Core.Entities;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public interface ICoverageUpdater
    {
        void Update(CoverageEntity coverageEntity, List<MethodEntity> methodList);
    }
}