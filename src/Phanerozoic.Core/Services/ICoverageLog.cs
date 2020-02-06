using System.Collections.Generic;
using Phanerozoic.Core.Entities;

namespace Phanerozoic.Core.Services
{
    public interface ICoverageLog
    {
        void Log(IList<MethodEntity> methodList);
    }
}