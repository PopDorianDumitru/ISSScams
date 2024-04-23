using ISSProject.Common.DataEncryption;
using System.Diagnostics;
using System;

namespace ISSProject.Common.Test
{
    internal class EncrypterTest
    {
        public static void ShiftEncrypterTest()
        {
            EncrypterBase shiftEnc = new ShiftEncrypter(4);

            // test encryption & decryption of alphabetic character
            Debug.Assert(shiftEnc.encryptCharacter('a') == 'e');
            Debug.Assert(shiftEnc.decryptCharacter('e') == 'a');

            // test encryption & decryption of non-alphabetic character
            Debug.Assert(shiftEnc.encryptCharacter('\\') == '\\');
            Debug.Assert(shiftEnc.decryptCharacter('\\') == '\\');

            // test encryption & decryption of strings
            Debug.Assert(shiftEnc.encryptString("aBc dEf?") == "eFg hIj?");
            Debug.Assert(shiftEnc.decryptString("eFg hIj?") == "aBc dEf?");

            // test encryption for large shift keys
            EncrypterBase bigShiftEnc = new ShiftEncrypter(30);
            Debug.Assert(bigShiftEnc.encryptString("aBc dEf?") == "EfG HiJ?");
            Debug.Assert(bigShiftEnc.decryptString("EfG HiJ?") == "aBc dEf?");

            EncrypterBase bigShiftEnc2 = new ShiftEncrypter(290);
            Debug.Assert(bigShiftEnc2.encryptString("aBc dEf?") == "EfG HiJ?");
            Debug.Assert(bigShiftEnc2.decryptString("EfG HiJ?") == "aBc dEf?");
            
            Debug.WriteLine("Shift encrypter test completed successfully.");
        }

        public static void SubstitutionEncrypterTest()
        {
            EncrypterBase substitutionEnc = new SubstitutionEncrypter("aBc?f", "?facB");

            // test encryption & decryption of character present in cipher
            Debug.Assert(substitutionEnc.encryptCharacter('a') == '?');
            Debug.Assert(substitutionEnc.decryptCharacter('?') == 'a');

            // test encryption & decryption of unspecified character
            Debug.Assert(substitutionEnc.encryptCharacter('\\') == '\\');
            Debug.Assert(substitutionEnc.decryptCharacter('\\') == '\\');

            // test encryption & decryption of strings
            Debug.Assert(substitutionEnc.encryptString("aBc dEf?") == "?fa dEBc");
            Debug.Assert(substitutionEnc.decryptString("?fa dEBc") == "aBc dEf?");

            //  ------------------------------------------  //
            // test usage of invalid substitution ciphers

            // multiple values for one key
            try
            {
                EncrypterBase substitutionEncErr = new SubstitutionEncrypter("xyx", "yyx");
                Debug.Assert(false);

            }
            catch (Exception) { }

            // invalid map
            try
            {
                EncrypterBase substitutionEncErr = new SubstitutionEncrypter("1", "ab3");
                Debug.Assert(false);

            }
            catch (Exception) { }
            //  ------------------------------------------  //

            Debug.WriteLine("Substitution encrypter test completed successfully.");
        }

        public static void Test()
        {
            ShiftEncrypterTest();
            SubstitutionEncrypterTest();
        }
    }
}