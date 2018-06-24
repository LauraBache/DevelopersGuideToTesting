using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopersGuideToTesting
{
    public class EvaluationService
    {
        private ILogService _logService;
        private IEducationMinistryApiHandler _educationMinistryApi;

        public EvaluationService(ILogService logService, IEducationMinistryApiHandler educationMinistryApi)
        {
            this._logService = logService;
            this._educationMinistryApi = educationMinistryApi;
        }

        public void SendClassEvaluation(ClassEvaluation classEvaluation)
        {

        }
    }
}
