﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            //asymmetric encryption
            string RSAprivateKey, RSApublicKey;
            string contentToBeEncrypted="654789321";
            RSAEncryption rsaCryption = new RSAEncryption();

            rsaCryption.RSAKey(out RSAprivateKey, out RSApublicKey);
            Console.WriteLine("RSAprivateKey:"+ RSAprivateKey);
            Console.WriteLine("\r\n" );
            Console.WriteLine("RSApublicKey:" + RSApublicKey);
            Console.WriteLine("\r\n");

            //encrypt1
            string encryptedStr1 = rsaCryption.RSAEncrypt(RSApublicKey, contentToBeEncrypted);
            Console.WriteLine("encryptedStr1:" + encryptedStr1);
            Console.WriteLine("\r\n");

            //encrypt2
            string encryptedStr2 = rsaCryption.RSAEncrypt(RSApublicKey, contentToBeEncrypted);
            Console.WriteLine("encryptedStr2:" + encryptedStr2);
            Console.WriteLine("\r\n");

            //decrypt1
            string decryptedStr1 = rsaCryption.RSADecrypt(RSAprivateKey, encryptedStr1);
            Console.WriteLine("decryptedStr1:" + decryptedStr1);
            Console.WriteLine("\r\n");

            //decrypt2
            string decryptedStr2 = rsaCryption.RSADecrypt(RSAprivateKey, encryptedStr2);
            Console.WriteLine("decryptedStr2:" + decryptedStr2);
            Console.WriteLine("\r\n");

            Console.ReadKey();
        }
    }
}