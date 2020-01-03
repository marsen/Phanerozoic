using System.Collections.Generic;

namespace Phanerozoic.Core.Entities
{
    public class MethodEntity
    {
        public string Repository { get; set; }
        public string Project { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
        public int Coverage { get; set; }

        public CoverageStatus Status { get; set; }
        public int RawIndex { get; set; }
        public IList<object> RawData { get; set; }

        public override string ToString()
        {
            return $"{Class}.{Method}:{Coverage}";
        }

        public void UpdateCoverage(MethodEntity method)
        {
            if (this.Coverage == method.Coverage)
            {
                this.Status = CoverageStatus.Unchange;
            }
            else if (this.Coverage > method.Coverage)
            {
                this.Status = CoverageStatus.Down;
            }
            else
            {
                this.Status = CoverageStatus.Up;
            }
            this.Coverage = method.Coverage;
        }
    }
}