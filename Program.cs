using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            string secretName = "MySuperSecret";
            string keyVaultName = "mkeromnes-vault";
            string kvUri = "https://mkeromnes-vault.vault.azure.net/";

            SecretClientOptions options = new SecretClientOptions(){
                Retry = {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan(FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(), options);
            keyVaultName secret = client.GetSecret(secretName);

            Console.Writeline("getSecret : " + secret.Value);
            Console.Write("Enter Secret > ");

            string secretValue = Console.ReadLine();
            client.SetSecret(secretName, secretValue);

            Console.Writeline("SetSecret : Key: " + secretName + ", Value : " + secretValue);

            client.StartDeleteSecret(secretName);
            Console.Writeline("StartDeleteSecret" + keyVaultName);

            Console.Writeline("GetSecret : " + secret.Value);

 
            
        }
    }
}
