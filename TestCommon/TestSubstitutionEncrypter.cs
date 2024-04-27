using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.DataEncryption;

namespace TestCommon
{
    [TestClass]
    public class TestSubstitutionEncrypter
    {
        private EncrypterBase encryptor;
        private string replacedCharacters = "abcdefghijklmnopqrstuvwxyz";
        private string replacementCharacters = "bcdefghijklmnopqrstuvwxyza";

        [TestInitialize]
        public void TestInitialize()
        {
            encryptor = new SubstitutionEncrypter(replacedCharacters, replacementCharacters);
        }

        [TestMethod]
        public void EncryptString_ShouldEncryptStringUsingSubstitutionCipher()
        {
            string encryptedString = encryptor.EncryptString("hello");

            Assert.AreEqual("ifmmp", encryptedString);
        }

        [TestMethod]
        public void DecryptString_ShouldDecryptStringUsingSubstitutionCipher()
        {
            string decryptedString = encryptor.DecryptString("ifmmp");

            Assert.AreEqual("hello", decryptedString);
        }

        [TestMethod]
        public void EncryptCharacter_ShouldEncryptCharacterUsingSubstitutionCipher()
        {
            char encryptedChar = encryptor.EncryptCharacter('a');

            Assert.AreEqual('b', encryptedChar);
        }

        [TestMethod]
        public void DecryptCharacter_ShouldDecryptCharacterUsingSubstitutionCipher()
        {
            char decryptedChar = encryptor.DecryptCharacter('b');

            Assert.AreEqual('a', decryptedChar);
        }
    }
}
