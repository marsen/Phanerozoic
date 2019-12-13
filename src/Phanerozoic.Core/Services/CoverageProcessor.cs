using Phanerozoic.Core.Entities;
using System;
using System.IO;

namespace Phanerozoic.Core.Services
{
    public class CoverageProcessor : ICoverageProcessor
    {
        public void Process(ReportEntity reportEntity)
        {
            if (File.Exists(reportEntity.FilePath))
            {
                Console.WriteLine("Not Found");
            }
            Console.WriteLine("Run");
        }
    }
}