using System;
using System.Security.Cryptography;
using System.Text;

namespace Campus.UI.Helpers
{
    public static class Encriptacion
    {
        public static string Encriptar(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] protectedBytes = ProtectedData.Protect(
                bytes,
                null,
                DataProtectionScope.LocalMachine
            );

            return Convert.ToBase64String(protectedBytes);
        }

        public static string Desencriptar(string protectedBase64)
        {
            if (string.IsNullOrEmpty(protectedBase64))
                return protectedBase64;

            byte[] protectedBytes = Convert.FromBase64String(protectedBase64);
            byte[] bytes = ProtectedData.Unprotect(
                protectedBytes,
                null,
                DataProtectionScope.LocalMachine
            );

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
