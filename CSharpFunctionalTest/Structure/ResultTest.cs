using CSharpFunctional.Extention;
using CSharpFunctional.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFunctionalTest.Structure
{
    [TestClass]
    public class ResultInstanceTest
    {
        private const int testThreshold = 100;
        private const string errorMessage = "Value is Under";

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

