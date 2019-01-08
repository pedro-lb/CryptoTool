namespace CryptoTool.Models
{
    public enum AnimationType
    {
        Animation_OUT,
        Animation_IN,
        Animation_HIDE
    }

    public enum Screen
    {
        EncodingScreen,
        EncryptionScreen,
        HashingScreen,
        ObfuscationScreen,
        SettingsScreen,
        AboutScreen
    }

    public enum OperationType
    {
        Encoding,
        Encryption,
        Hashing,
        Obfuscation
    }

    public enum EncodingType
    {
        Base64_Decode,
        Base64_Encode,
        ASCII_Decode,
        ASCII_Encode,
        Binary_Decode,
        Binary_Encode
    }

    public enum EncryptionType
    {
        Blowfish,
        AES,
        Serpent
    }

    public enum HashingType
    {
        SHA128,
        SHA256,
        SHA512,
        MD4,
        MD5,
        HMAC_SHA128,
        HMAC_SHA256,
        HMAC_SHA512,
        HMAC_MD4,
        HMAC_MD5
    }

    public enum ObfuscationType
    {
        //nothing here yet
    }
}