using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DevelopersGuideToTesting.Tests
{
    [TestClass]
    public class Tests_StudentEvaluation
    {
        [TestMethod]
        public void Tests_HasPassed_Passed()
        {
            //Arrange
            int grade = 10;
            string expectedResult = "Passed";
            var mockLog = new Mock<ILogService>();

            //Act
            StudentEvaluation evaluation = new StudentEvaluation(mockLog.Object);
            string actualResult = evaluation.HasPassed(grade);

            //Assert
            Assert.IsTrue(expectedResult.Equals(actualResult), "The student didn't pass as expected.");
            mockLog.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Tests_HasPassed_Not_Passed()
        {
            //Arrange
            int grade = 0;
            string expectedResult = "Failed";
            var mockLog = new Mock<ILogService>();

            //Act
            StudentEvaluation evaluation = new StudentEvaluation(mockLog.Object);
            string actualResult = evaluation.HasPassed(grade);

            //Assert
            Assert.IsTrue(expectedResult.Equals(actualResult), "The student didn't fail as expected.");
            mockLog.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Tests_HasPassed_Invalid_Grade()
        {
            //Arrange
            int invalidGrade = 13;
            string expectedResult = string.Empty;
            var mockLog = new Mock<ILogService>();
            string expectedLogMessage = $"An attempt to evaluate the invalid grade {invalidGrade} was made.";

            //Act
            StudentEvaluation evaluation = new StudentEvaluation(mockLog.Object);
            string actualResult = evaluation.HasPassed(invalidGrade);

            //Assert
            Assert.IsTrue(expectedResult.Equals(actualResult), "Invalid grade wasn't handled as expected.");
            mockLog.Verify(log => log.Log(expectedLogMessage), "The expected message wasn't logged.");
            mockLog.VerifyNoOtherCalls();
        }
    }
}
