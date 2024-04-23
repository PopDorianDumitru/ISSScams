using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.DataEncryption
{
    internal class ShiftEncrypter : EncrypterBase
    {
        private static readonly string AllLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // A shift cipher encrypter and decrypter that encodes a letter by shifting
        // its alphabet position by a certain value.
        public ShiftEncrypter(int key)
        {
            for (int i = 0; i < AllLetters.Length; i++)
            {
                encryptionMap.Add(AllLetters[i], AllLetters[(i + (key % AllLetters.Length) + AllLetters.Length) % AllLetters.Length]);
                decryptionMap.Add(AllLetters[i], AllLetters[(i - (key % AllLetters.Length) + AllLetters.Length) % AllLetters.Length]);
            }
        }
    }
}
