using ISSProject.Common.DataEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon
{
    [TestClass]
    public class TestSubstitutionEncrypter
    {
        private EncrypterBase encrypter;
        private string replacedCharacters = "abcdefghijklmnopqrstuvwxyz";
        private string replacementCharacters = "bcdefghijklmnopqrstuvwxyza";

        [TestInitialize]
        public void TestInitialize()
        {
            encrypter = new SubstitutionEncrypter(replacedCharacters, replacementCharacters);
        }

        [TestMethod]
        public void EncryptString_ShouldEncryptStringUsingSubstitutionCipher()
        {
            string encryptedString = encrypter.EncryptString("hello");

            Assert.AreEqual("ifmmp", encryptedString);
        }

        [TestMethod]
        public void DecryptString_ShouldDecryptStringUsingSubstitutionCipher()
        {
            string decryptedString = encrypter.DecryptString("ifmmp");

            Assert.AreEqual("hello", decryptedString);
        }

        [TestMethod]
        public void EncryptCharacter_ShouldEncryptCharacterUsingSubstitutionCipher()
        {
            char encryptedChar = encrypter.EncryptCharacter('a');

            Assert.AreEqual('b', encryptedChar);
        }

        [TestMethod]
        public void DecryptCharacter_ShouldDecryptCharacterUsingSubstitutionCipher()
        {
            char decryptedChar = encrypter.DecryptCharacter('b');

            Assert.AreEqual('a', decryptedChar);
        }
    }
}
