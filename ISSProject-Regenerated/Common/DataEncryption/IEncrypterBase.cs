namespace ISSProject.Common.DataEncryption
{
    internal interface IEncrypterBase
    {
        char DecryptCharacter(char character);
        string DecryptString(string encryptedString);
        char EncryptCharacter(char character);
        string EncryptString(string inputString);
    }
}