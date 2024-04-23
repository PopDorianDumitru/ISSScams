using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.DataEncryption
{
    internal abstract class EncrypterBase
    {
        protected Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
        protected Dictionary<char, char> decryptionMap = new Dictionary<char, char>();

        // Encrypts a character and returns the result.
        // Parameter: character = character to be encrypted
        public char EncryptCharacter(char character)
        {
            if (encryptionMap.ContainsKey(character))
            {
                return encryptionMap[character];
            }
            else
            {
                return character;
            }
        }

        // Encrypts a string and returns the result
        // Parameter: inputString = string to be encrypted
        public string EncryptString(string inputString)
        {
            string result = string.Empty;
            for (int i = 0; i < inputString.Length; ++i)
            {
                result += EncryptCharacter(inputString[i]);
            }

            return result;
        }

        // Decrypts a character and returns the result.
        // Parameter: character = character to be decrypted
        public char DecryptCharacter(char character)
        {
            if (decryptionMap.ContainsKey(character))
            {
                return decryptionMap[character];
            }
            else
            {
                return character;
            }
        }

        // Decrypts a string and returns the result.
        // Parameter: encryptedString = string to be decrypted
        public string DecryptString(string encryptedString)
        {
            string result = string.Empty;
            for (int i = 0; i < encryptedString.Length; ++i)
            {
                result += DecryptCharacter(encryptedString[i]);
            }

            return result;
        }
    }
}
