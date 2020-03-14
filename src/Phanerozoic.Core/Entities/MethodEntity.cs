using System;
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
        public int? LastCoverage { get; private set; }
        public int RawIndex { get; set; }
        public IList<object> RawData { get; set; }
        public string Team { get; set; }
        public string UpdatedDate { get; set; }

        public override string ToString()
        {
            return $"{Class}.{Method}:{Coverage}";
        }

        public void UpdateCoverage(MethodEntity method)
        {
            if (this.Equals(method) == false)
            {
                throw new ApplicationException($"MethodEntity Not Match! {this.ToString()} vs {method.ToString()}");
            }

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
            this.LastCoverage = this.Coverage;
            this.Coverage = method.Coverage;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is MethodEntity)
            {
                var target = (MethodEntity)obj;
                return (this.Repository == target.Repository &&
                    this.Project == target.Project &&
                    this.Class == target.Class &&
                    this.Method == target.Method);
            }
            return false;
        }
    }
}