using System;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace WebApi.Infrastructure
{
    public static class BCEngine
    {
        private const string KEY = "ABCDEFGHIJKLMNOP";
        private static readonly Encoding _encoding = Encoding.ASCII;
        private static readonly IBlockCipher _blockCipher = new AesEngine();
        private static PaddedBufferedBlockCipher _cipher;
        private static readonly IBlockCipherPadding _padding = new Pkcs7Padding();

        public static string Encrypt(string plain)
        {
            byte[] result = BouncyCastleCrypto(true, _encoding.GetBytes(plain), KEY);
            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string cipher)
        {
            byte[] result = BouncyCastleCrypto(false, Convert.FromBase64String(cipher), KEY);
            return _encoding.GetString(result);
        }
        private static byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, string key)
        {
            try
            {
                _cipher = _padding == null ? new PaddedBufferedBlockCipher(_blockCipher) : new PaddedBufferedBlockCipher(_blockCipher, _padding);
                byte[] keyByte = _encoding.GetBytes(key);
                _cipher.Init(forEncrypt, new KeyParameter(keyByte));
                return _cipher.DoFinal(input);
            }
            catch (Org.BouncyCastle.Crypto.CryptoException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }
    }
}