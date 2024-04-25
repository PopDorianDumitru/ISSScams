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
        private EncrypterBase encrypter;
        private int key = 3;

        [TestInitialize]
        public void TestInitialize()
        {
            encrypter = new ShiftEncrypter(key);
        }

        [TestMethod]
        public void EncryptCharacter_ShouldEncryptCharacterUsingShiftCipher()
        {
            char encryptedChar = encrypter.EncryptCharacter('a');

            Assert.AreEqual('d', encryptedChar);
        }

        [TestMethod]
        public void DecryptCharacter_ShouldDecryptCharacterUsingShiftCipher()
        {

            char decryptedChar = encrypter.DecryptCharacter('d');

            Assert.AreEqual('a', decryptedChar);
        }

        [TestMethod]
        public void EncryptString_ShouldEncryptStringUsingShiftCipher()
        {
            string inputString = "hello";

            string encryptedString = encrypter.EncryptString(inputString);

            Assert.AreEqual("khoor", encryptedString);
        }

        [TestMethod]
        public void DecryptString_ShouldDecryptStringUsingShiftCipher()
        {
            string encryptedString = "khoor";

            string decryptedString = encrypter.DecryptString(encryptedString);

            Assert.AreEqual("hello", decryptedString);
        }
    }
}
