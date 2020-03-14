using Phanerozoic.Core.Entities;
using System.Collections.Generic;

namespace Phanerozoic.Core.Services
{
    public interface INotifyer
    {
        void Notify(CoverageEntity coverageEntity, IList<MethodEntity> methodList);
    }
}