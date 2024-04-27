using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Cache;
using ISSProject.Common.DataEncryption;

namespace TestCommon
{
    [TestClass]
    public class TestShiftEncrypter
    {
        private EncrypterBase encryptor;
        private int key = 3;

        [TestInitialize]
        public void TestInitialize()
        {
            encryptor = new ShiftEncrypter(key);
        }

        [TestMethod]
        public void EncryptCharacter_WhenEncryptingOneCharacter_ShouldReturnEncryptedCharacterUsingShiftCipher()
        {
            char encryptedChar = encryptor.EncryptCharacter('a');

            Assert.AreEqual('d', encryptedChar);
        }

        [TestMethod]
        public void DecryptCharacter_WhenDecryptingOneCharacter_ShouldDecryptCharacterUsingShiftCipher()
        {
            char decryptedChar = encryptor.DecryptCharacter('d');

            Assert.AreEqual('a', decryptedChar);
        }

        [TestMethod]
        public void EncryptString_WhenEncryptingString_ShouldEncryptStringUsingShiftCipher()
        {
            string inputString = "hello";

            string encryptedString = encryptor.EncryptString(inputString);

            Assert.AreEqual("khoor", encryptedString);
        }

        [TestMethod]
        public void DecryptString_WhenDecryptingString_ShouldDecryptStringUsingShiftCipher()
        {
            string encryptedString = "khoor";

            string decryptedString = encryptor.DecryptString(encryptedString);

            Assert.AreEqual("hello", decryptedString);
        }
    }
}
