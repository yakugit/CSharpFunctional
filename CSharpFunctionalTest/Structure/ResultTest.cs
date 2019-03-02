using CSharpFunctional.Extention;
using CSharpFunctional.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFunctionalTest.Structure
{
    [TestClass]
    public class ResultInstanceTest
    {
        private const int testThreshold = 100;
        private const int testThresholdOverOne = testThreshold + 1;
        private const int testThresholdAnderOne = testThreshold - 1;
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
                var overSuccess = GetResult(testThresholdOverOne);
                Assert.IsTrue(overSuccess.IsOK);
                Assert.IsFalse(overSuccess.IsError);
                Assert.IsTrue(overSuccess.Value > testThreshold);
                Assert.AreNotEqual(overSuccess.ErrorValue, errorMessage);
            }

            {
                var error = GetResult(testThresholdAnderOne);
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
                var bindedError = (testThresholdAnderOne).Bind(GetResult);
                Assert.IsFalse(bindedError.IsOK);
                Assert.IsTrue(bindedError.IsError);
                Assert.AreNotEqual(bindedError.Value, testThreshold);
                Assert.AreEqual(bindedError.ErrorValue, errorMessage);
            }

            {
                var mapedSuccess = testThreshold.Bind(GetResult).Map(v => okMessage);
                Assert.IsTrue(mapedSuccess.IsOK);
                Assert.IsFalse(mapedSuccess.IsError);
                Assert.AreEqual(mapedSuccess.Value, okMessage);
                Assert.AreNotEqual(mapedSuccess.ErrorValue, errorMessage);
            }

            {
                var mapedErrorValue = testThresholdAnderOne.Bind(GetResult).Map(v => okMessage);
                Assert.IsFalse(mapedErrorValue.IsOK);
                Assert.IsTrue(mapedErrorValue.IsError);
                Assert.AreNotEqual(mapedErrorValue.Value, okMessage);
                Assert.AreEqual(mapedErrorValue.ErrorValue, errorMessage);
            }

            {
                var mapedErrorSuccessValue = testThreshold.Bind(GetResult).mapError(s => errorCode);
                Assert.IsTrue(mapedErrorSuccessValue.IsOK);
                Assert.IsFalse(mapedErrorSuccessValue.IsError);
                Assert.AreEqual(mapedErrorSuccessValue.Value, testThreshold);
                Assert.AreNotEqual(mapedErrorSuccessValue.ErrorValue, errorCode);
            }

            {
                var mapedError = testThresholdAnderOne.Bind(GetResult).mapError(s => errorCode);
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

