using Fclp;
using System;

namespace EncryptStringSample
{
    public class ApplicationArguments
    {
        public ActionType Action { get; set; }
        public string EncryptionKey { get; set; }
        public string StringValue { get; set; }

    }
    [Flags]
    public enum ActionType
    {
        Encrypt,
        Decrypt
    }
    class Program
    {
        static void Main(string[] args)
        {
            var p = new FluentCommandLineParser<ApplicationArguments>();
            // specify which property the value will be assigned too.
            p.Setup(arg => arg.Action)
             .As('a', "action") // define the short and long option name
             .Required(); // using the standard fluent Api to declare this Option as required.
            p.Setup(arg => arg.StringValue)
              .As('v', "stringvalue")
              .Required();
            p.Setup(arg => arg.EncryptionKey)
              .As('k', "encryptionkey");

            var result = p.Parse(args);

            if (result.HasErrors == false)
            {
                string password = p.Object.EncryptionKey;
                if (string.IsNullOrEmpty(password))
                {
                    if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ENCRYPTION_KEY")))
                    {
                        Console.WriteLine("There is no encryption key in the arguments neither environment variable ENCRYPTION_KEY");
                        return;
                    }
                    password = Environment.GetEnvironmentVariable("ENCRYPTION_KEY");
                    Console.WriteLine("Using environment variable to encrypt");
                }

                if (p.Object.Action.HasFlag(ActionType.Encrypt))
                {
                    Console.WriteLine("we are inside encrypted block");
                    string encryptedstring = StringCipher.Encrypt(p.Object.StringValue, password);
                    Console.WriteLine("Encrypted value: " + encryptedstring);
                }

                if (p.Object.Action.HasFlag(ActionType.Decrypt))
                {
                    string decryptedstring = StringCipher.Decrypt(p.Object.StringValue, password);
                    Console.WriteLine("Decrypted value: " + decryptedstring);
                }
                return;
            }

            Console.WriteLine("Wrong parameters");
        }
    }
}