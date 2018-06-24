using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopersGuideToTesting
{
    public class StudentEvaluation
    {
        private ILogService _logService;

        public StudentEvaluation(ILogService logService)
        {
            this._logService = logService;
        }

        public string HasPassed(int grade)
        {
            return string.Empty;
        }
    }
}
