using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.DataEncryption
{
    internal class SubstitutionEncrypter : EncrypterBase
    {
        // A substitution cipher encrypter and decrypter that encodes a letter by replacing
        // it with a specified character.
        public SubstitutionEncrypter(string replacedCharacters, string replacementCharacters)
        {
            if (replacedCharacters.Length != replacementCharacters.Length)
            {
                throw new Exception("Arguments must be a 1:1 map");
            }

            for (int index = 0; index < replacedCharacters.Length; index++)
            {
                if (!replacementCharacters.Contains(replacedCharacters[index]))
                {
                    throw new Exception("A character must be present in both arguments.");
                }
            }

            for (int index = 0; index < replacedCharacters.Length; index++)
            {
                if (encryptionMap.ContainsKey(replacedCharacters[index]) || decryptionMap.ContainsKey(replacementCharacters[index]))
                {
                    throw new Exception("A character cannot be mapped to multiple characters");
                }

                encryptionMap.Add(replacedCharacters[index], replacementCharacters[index]);
                decryptionMap.Add(replacementCharacters[index], replacedCharacters[index]);
            }
        }
    }
}
