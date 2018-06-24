using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DevelopersGuideToTesting.Tests
{
    [TestClass]
    public class Tests_EvaluationService
    {
        [TestMethod]
        public void Tests_SendClassEvaluation_1_API_Call()
        {
            //Arrange
            ClassEvaluation classEvaluation = new ClassEvaluation
            {
                Class = "7b",
                StudentEvaluations = new List<string> { "Passed", "Passed", "Failed", "Passed" }
            };
            
            var mockLog = new Mock<ILogService>();

            var mockApi = new Mock<IEducationMinistryApiHandler>();
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Accepted) { Content = new StringContent("") };
            mockApi.Setup(api => api.PostClassEvaluation(It.IsAny<ClassEvaluation>())).Returns(Task.FromResult<HttpResponseMessage>(responseMessage));

            int expectedNoOfApiCalls = 1;

            //Act
            EvaluationService evaluationService = new EvaluationService(mockLog.Object, mockApi.Object);
            evaluationService.SendClassEvaluation(classEvaluation);

            //Assert
            mockApi.Verify(api => api.PostClassEvaluation(classEvaluation), Times.Exactly(expectedNoOfApiCalls),
                "The API wasn't called the expected number of times.");
            mockApi.VerifyNoOtherCalls();

            mockLog.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Tests_SendClassEvaluation_3_API_Calls()
        {
            //Arrange
            ClassEvaluation classEvaluation = new ClassEvaluation
            {
                Class = "7b",
                StudentEvaluations = new List<string> { "Passed", "Passed", "Failed", "Passed" }
            };

            var mockLog = new Mock<ILogService>();

            var mockApi = new Mock<IEducationMinistryApiHandler>();

            HttpResponseMessage responseRequestTimeout1 = new HttpResponseMessage(HttpStatusCode.RequestTimeout) { Content = new StringContent("") };
            HttpResponseMessage responseRequestTimeout2 = new HttpResponseMessage(HttpStatusCode.RequestTimeout) { Content = new StringContent("") };
            HttpResponseMessage responseAccepted = new HttpResponseMessage(HttpStatusCode.Accepted) { Content = new StringContent("") };

            mockApi.SetupSequence(api => api.PostClassEvaluation(It.IsAny<ClassEvaluation>()))
                .Returns(Task.FromResult<HttpResponseMessage>(responseRequestTimeout1))
                .Returns(Task.FromResult<HttpResponseMessage>(responseRequestTimeout2))
                .Returns(Task.FromResult<HttpResponseMessage>(responseAccepted));

            int expectedNoOfApiCalls = 3;

            //Act
            EvaluationService evaluationService = new EvaluationService(mockLog.Object, mockApi.Object);
            evaluationService.SendClassEvaluation(classEvaluation);

            //Assert
            mockApi.Verify(api => api.PostClassEvaluation(classEvaluation), Times.Exactly(expectedNoOfApiCalls),
                "The API wasn't called the expected number of times.");
            mockApi.VerifyNoOtherCalls();

            mockLog.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Tests_SendClassEvaluation_4_API_Calls()
        {
            //Arrange
            ClassEvaluation classEvaluation = new ClassEvaluation
            {
                Class = "7b",
                StudentEvaluations = new List<string> { "Passed", "Passed", "Failed", "Passed" }
            };

            var mockLog = new Mock<ILogService>();

            var mockApi = new Mock<IEducationMinistryApiHandler>();

            HttpResponseMessage responseRequestTimeout1 = new HttpResponseMessage(HttpStatusCode.RequestTimeout) { Content = new StringContent("") };
            HttpResponseMessage responseRequestTimeout2 = new HttpResponseMessage(HttpStatusCode.RequestTimeout) { Content = new StringContent("") };
            HttpResponseMessage responseRequestTimeout3 = new HttpResponseMessage(HttpStatusCode.RequestTimeout) { Content = new StringContent("") };
            HttpResponseMessage responseRequestTimeout4 = new HttpResponseMessage(HttpStatusCode.RequestTimeout) { Content = new StringContent("") };

            mockApi.SetupSequence(api => api.PostClassEvaluation(It.IsAny<ClassEvaluation>()))
                .Returns(Task.FromResult<HttpResponseMessage>(responseRequestTimeout1))
                .Returns(Task.FromResult<HttpResponseMessage>(responseRequestTimeout2))
                .Returns(Task.FromResult<HttpResponseMessage>(responseRequestTimeout3))
                .Returns(Task.FromResult<HttpResponseMessage>(responseRequestTimeout4));

            int expectedNoOfApiCalls = 3;
            string expectedLogMessage = "Could not send class evaluation because of request timeout.";

            //Act
            EvaluationService evaluationService = new EvaluationService(mockLog.Object, mockApi.Object);
            evaluationService.SendClassEvaluation(classEvaluation);

            //Assert
            mockApi.Verify(api 8u=> api.PostClassEvaluation(classEvaluation), Times.Exactly(expectedNoOfApiCalls),
                "The API wasn't called the expected number of times.");
            mockApi.VerifyNoOtherCalls();

            mockLog.Verify(log => log.Log(expectedLogMessage), "The expected message wasn't logged.");
            mockLog.VerifyNoOtherCalls();
        }
    }
}
