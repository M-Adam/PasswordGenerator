using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PasswordGenerator.Test
{
    [TestClass]
    public class PasswordGeneratorTest
    {
        private const int PasswordLength = 100;

        [TestMethod]
        public void SimplestPasswordIsWorking()
        {
            var pass = PasswordCode.CreatePassword(5, false, false, new char[0] );

            Assert.IsNotNull(pass);
            Assert.IsInstanceOfType(pass, typeof(string));
            Assert.IsTrue(pass.Length == 5);
        }

        [TestMethod]
        public void PasswordHasOnlyLowerLetters()
        {
            var pass = PasswordCode.CreatePassword(PasswordLength, false, false, new char[0]);

            Assert.IsTrue(pass.All(char.IsLower));
        }

        [TestMethod]
        public void PasswordHasUpperLetters()
        {
            var pass = PasswordCode.CreatePassword(PasswordLength, true, false, new char[0]);

            Assert.IsTrue(pass.Any(char.IsUpper));
        }


        [TestMethod]
        public void PasswordHasDigits()
        {
            var pass = PasswordCode.CreatePassword(PasswordLength, false, true, new char[0]);

            Assert.IsTrue(pass.Any(char.IsDigit));
        }

        [TestMethod]
        public void PasswordHasDigitsAndUpperLetters()
        {
            var pass = PasswordCode.CreatePassword(PasswordLength, true, true, new char[0]);

            Assert.IsTrue(pass.Any(char.IsDigit) && pass.Any(char.IsUpper));
        }

        [TestMethod]
        public void PasswordHasSymbolsAndLowerLetters()
        {
            var symbols = new[] {'!', '@', '#'};
            var pass = PasswordCode.CreatePassword(PasswordLength, false, false, symbols);

            Assert.IsTrue(pass.All(x=> char.IsLower(x) || symbols.Contains(x)));
        }

        [TestMethod]
        public void PasswordHasEverything()
        {
            var symbols = new[] { '!', '@', '#' };
            var pass = PasswordCode.CreatePassword(PasswordLength, true, true, symbols);

            Assert.IsTrue(pass.Any(char.IsLower));
            Assert.IsTrue(pass.Any(char.IsDigit));
            Assert.IsTrue(pass.Any(char.IsUpper));
            Assert.IsTrue(pass.Any(x=>symbols.Contains(x)));
        }
    }
}