using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CSharpFunctional.Extention;
using CSharpFunctional.Structure;

namespace CSharpFunctionalTest.Extention
{
    [TestClass]
    public class DisposeResultExtentionTest
    {

        private const int testCapacity = 1024;
        private readonly byte[] overCapacityBuffer = new byte[testCapacity + 1];
        private readonly Func<MemoryStream> testStreamFunc = GetTestStream;
        private const string notMatch = "Not Match";

        [TestCategory("CSharpFunctional_Extention_DisposeResultExtention")]
        [TestMethod]
        public void DisposeResultTest()
        {
            {
                var result = testStreamFunc.Run(ExceptionToString, m =>
                {
                    if (m.Capacity == testCapacity)
                    {
                        return Result<bool, string>.Success(true);
                    }
                    else
                    {
                        return Result<bool, string>.Error("Not Match");
                    }
                });
                Assert.IsTrue(result.Value);
                Assert.IsTrue(result.IsOK);
                Assert.IsFalse(result.IsError);
            }

            {
                var result = testStreamFunc.Run(ExceptionToString, m =>
                {
                    if (m.Capacity == 0)
                    {
                        return Result<bool, string>.Success(true);
                    }
                    else
                    {
                        return Result<bool, string>.Error(notMatch);
                    }
                });
                Assert.IsFalse(result.IsOK);
                Assert.IsTrue(result.IsError);
                Assert.AreEqual(result.ErrorValue, notMatch);
            }

            {
                var result = testStreamFunc.Run<MemoryStream, bool ,string>(ExceptionToString, m =>
                {
                    throw new ArgumentOutOfRangeException();
                });
                Assert.IsFalse(result.IsOK);
                Assert.IsTrue(result.IsError);
                Assert.AreEqual(result.ErrorValue, typeof(ArgumentOutOfRangeException).ToString());
            }
        }

        private static MemoryStream GetTestStream()
        {
            return new MemoryStream(testCapacity);
        }

        private static string ExceptionToString(Exception ex)
        {
            return ex.GetType().ToString();
        }
    }
}
