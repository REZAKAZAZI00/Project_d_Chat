using System.Security.Cryptography;

namespace Project_d_Chat.Core.Security
{
    public static class PasswordHelper
    {

        public static string EncodePasswordSHA1(string password) 
        {
            Byte[] originalBytes;
            Byte[] encodeedBytes;
            //Blake2 blake2;
            SHA1 sHA1;
            //HMACSHA256 HMACSHA256;
#pragma warning disable SYSLIB0021 // Type or member is obsolete
              //  hA256=new H HMACSHA256CryptoServiceProvider();
            sHA1 = new SHA1CryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete


            originalBytes =ASCIIEncoding.Default.GetBytes(password);

            encodeedBytes=sHA1.ComputeHash(originalBytes);

            return BitConverter.ToString(encodeedBytes);
        }   
    }
}
