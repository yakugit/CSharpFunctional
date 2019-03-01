using CSharpFunctional.Extention;
using CSharpFunctional.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFunctionalTest.Structure
{
    [TestClass]
    public class ResultInstanceTest
    {
        private const int testThreshold = 100;
        private const string okMessage = "Value is OK";
        private const string errorMessage = "Value is Under";
        private const int errorCode = -1;

        [TestCategory("CSharpFunctional_Structure_Result")]
        [TestMethod]
        public void ResultReturnTest()
        {
            {
                var success = GetResult(testThreshold);
                Assert.IsTrue(success.IsOK);
                Assert.IsFalse(success.IsError);
                Assert.AreEqual(success.Value, testThreshold);
                Assert.AreNotEqual(success.ErrorValue, errorMessage);
            }

            {
                var overSuccess = GetResult(testThreshold + 1);
                Assert.IsTrue(overSuccess.IsOK);
                Assert.IsFalse(overSuccess.IsError);
                Assert.IsTrue(overSuccess.Value > testThreshold);
                Assert.AreNotEqual(overSuccess.ErrorValue, errorMessage);
            }

            {
                var error = GetResult(testThreshold - 1);
                Assert.IsFalse(error.IsOK);
                Assert.IsTrue(error.IsError);
                if (testThreshold != default(int))
                {
                    Assert.AreNotEqual(error.Value, testThreshold);
                }
                Assert.AreEqual(error.ErrorValue, errorMessage);
            }

            {
                var bindedSuccess = testThreshold.Bind(GetResult);
                Assert.IsTrue(bindedSuccess.IsOK);
                Assert.IsFalse(bindedSuccess.IsError);
                Assert.AreEqual(bindedSuccess.Value, testThreshold);
                Assert.AreNotEqual(bindedSuccess.ErrorValue, errorMessage);
            }

            {
                var mapedSuccess = GetResult(testThreshold).Map(v => okMessage);
                Assert.IsTrue(mapedSuccess.IsOK);
                Assert.IsFalse(mapedSuccess.IsError);
                Assert.AreEqual(mapedSuccess.Value, okMessage);
                Assert.AreNotEqual(mapedSuccess.ErrorValue, errorMessage);
            }

            {
                var mapedError = GetResult(testThreshold - 1).mapError<int, string, int>(s => errorCode);
                Assert.IsFalse(mapedError.IsOK);
                Assert.IsTrue(mapedError.IsError);
                Assert.AreNotEqual(mapedError.Value, testThreshold);
                Assert.AreEqual(mapedError.ErrorValue, errorCode);
            }
        }

        private static Result<int, string> GetResult(int value)
        {
            if(value >= testThreshold)
            {
                return value.ToSuccess<int, string>();
            }
            else
            {
                return errorMessage.ToError<int, string>();
            }
        }

    }
}

