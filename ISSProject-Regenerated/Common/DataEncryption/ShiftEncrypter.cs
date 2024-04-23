using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.DataEncryption
{
    internal class ShiftEncrypter : EncrypterBase
    {
        static string allLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // A shift cipher encrypter and decrypter that encodes a letter by shifting
        // its alphabet position by a certain value.
        public ShiftEncrypter(int key)
        {
            for (int i = 0; i < allLetters.Length; i++)
            {
                encryptionMap.Add(allLetters[i], allLetters[(i + key % allLetters.Length + allLetters.Length) % allLetters.Length]);
                decryptionMap.Add(allLetters[i], allLetters[(i - key % allLetters.Length + allLetters.Length) % allLetters.Length]);
            }
        }
    }
}
