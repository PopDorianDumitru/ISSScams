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
                throw new Exception("Arguments must be a 1:1 map");

            for(int i = 0; i < replacedCharacters.Length; i++)
            {
                if (!replacementCharacters.Contains(replacedCharacters[i]))
                    throw new Exception("A character must be present in both arguments.");
            }

            for (int i = 0; i < replacedCharacters.Length; i++)
            {
                if (encryptionMap.ContainsKey(replacedCharacters[i]) || decryptionMap.ContainsKey(replacementCharacters[i]))
                    throw new Exception("A character cannot be mapped to multiple characters");

                encryptionMap.Add(replacedCharacters[i], replacementCharacters[i]);
                decryptionMap.Add(replacementCharacters[i], replacedCharacters[i]);
            }
        }
    }
}
