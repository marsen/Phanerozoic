using Phanerozoic.Core.Entities;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public interface ICoverageUpdater
    {
        IList<MethodEntity> Update(CoverageEntity coverageEntity, IList<MethodEntity> methodList);
    }
}