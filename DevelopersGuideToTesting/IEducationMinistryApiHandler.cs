using System.Net.Http;
using System.Threading.Tasks;

namespace DevelopersGuideToTesting
{
    public interface IEducationMinistryApiHandler
    {
        Task<HttpResponseMessage> PostClassEvaluation(ClassEvaluation classEvaluation);
    }
}
