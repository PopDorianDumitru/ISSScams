using System;
using System.Diagnostics;
using ISSProject.Common.DataEncryption;

namespace ISSProject.Common.Test
{
    internal class EncrypterTest
    {
        public static void ShiftEncrypterTest()
        {
            EncrypterBase shiftEnc = new ShiftEncrypter(4);

            // test encryption & decryption of alphabetic character
            Debug.Assert(shiftEnc.EncryptCharacter('a') == 'e');
            Debug.Assert(shiftEnc.DecryptCharacter('e') == 'a');

            // test encryption & decryption of non-alphabetic character
            Debug.Assert(shiftEnc.EncryptCharacter('\\') == '\\');
            Debug.Assert(shiftEnc.DecryptCharacter('\\') == '\\');

            // test encryption & decryption of strings
            Debug.Assert(shiftEnc.EncryptString("aBc dEf?") == "eFg hIj?");
            Debug.Assert(shiftEnc.DecryptString("eFg hIj?") == "aBc dEf?");

            // test encryption for large shift keys
            EncrypterBase bigShiftEnc = new ShiftEncrypter(30);
            Debug.Assert(bigShiftEnc.EncryptString("aBc dEf?") == "EfG HiJ?");
            Debug.Assert(bigShiftEnc.DecryptString("EfG HiJ?") == "aBc dEf?");

            EncrypterBase bigShiftEnc2 = new ShiftEncrypter(290);
            Debug.Assert(bigShiftEnc2.EncryptString("aBc dEf?") == "EfG HiJ?");
            Debug.Assert(bigShiftEnc2.DecryptString("EfG HiJ?") == "aBc dEf?");

            Debug.WriteLine("Shift encrypter test completed successfully.");
        }

        public static void SubstitutionEncrypterTest()
        {
            EncrypterBase substitutionEnc = new SubstitutionEncrypter("aBc?f", "?facB");

            // test encryption & decryption of character present in cipher
            Debug.Assert(substitutionEnc.EncryptCharacter('a') == '?');
            Debug.Assert(substitutionEnc.DecryptCharacter('?') == 'a');

            // test encryption & decryption of unspecified character
            Debug.Assert(substitutionEnc.EncryptCharacter('\\') == '\\');
            Debug.Assert(substitutionEnc.DecryptCharacter('\\') == '\\');

            // test encryption & decryption of strings
            Debug.Assert(substitutionEnc.EncryptString("aBc dEf?") == "?fa dEBc");
            Debug.Assert(substitutionEnc.DecryptString("?fa dEBc") == "aBc dEf?");

            // ------------------------------------------  //
            // test usage of invalid substitution ciphers

            // multiple values for one key
            try
            {
                EncrypterBase substitutionEncErr = new SubstitutionEncrypter("xyx", "yyx");
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }

            // invalid map
            try
            {
                EncrypterBase substitutionEncErr = new SubstitutionEncrypter("1", "ab3");
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }

            // ------------------------------------------
            Debug.WriteLine("Substitution encrypter test completed successfully.");
        }

        public static void Test()
        {
            ShiftEncrypterTest();
            SubstitutionEncrypterTest();
        }
    }
}