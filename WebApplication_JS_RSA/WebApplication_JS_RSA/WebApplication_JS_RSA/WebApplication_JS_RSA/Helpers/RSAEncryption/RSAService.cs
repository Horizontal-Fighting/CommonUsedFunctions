using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Authentication.Helpers.RSAEncryption
{
    public class RSAService
    {

        public static string GetPlainText(string encryptedText)
        {
            var privateKeyFilePath = AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["RSAPrivateKey"];
            var RSAprivateKey = System.IO.File.ReadAllText(privateKeyFilePath);
            var plainText = RSAEncryption.RSADecrypt(RSAprivateKey, encryptedText);
            return plainText;
        }

        public static string GetPulicKey(string publicKeyFullFilePath = null)
        {
            if (string.IsNullOrEmpty(publicKeyFullFilePath))
            {
                var publicKeyPathFile = AppDomain.CurrentDomain.BaseDirectory + System.Web.Configuration.WebConfigurationManager.AppSettings["RSAPublicKey"];
                return System.IO.File.ReadAllText(publicKeyPathFile);
            }
            else
            {
				if (File.Exists(publicKeyFullFilePath))
				{
					return System.IO.File.ReadAllText(publicKeyFullFilePath);
				}

				else
				{
					throw new FileNotFoundException();
				}
                  
            }
           
        }
    }
}