using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static PasswordGenerator.PeselCode;

namespace PasswordGenerator.Test
{
    [TestClass]
    public class PeselCode
    {
        [TestMethod]
        public void PeselCodeTest()
        {
            Assert.IsTrue(TestPeselForTimes(100000));
        }

        private bool TestPeselForTimes(int times)
        {
            for (int i = 0; i < times; i++)
            {
                if (!ValidatePesel(CreatePesel()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
