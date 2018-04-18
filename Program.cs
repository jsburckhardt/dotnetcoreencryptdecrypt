using Fclp;
using System;

namespace EncryptStringSample
{
    public class ApplicationArguments
    {
        public int RecordId { get; set; }
        public bool Silent { get; set; }
        public string NewValue { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var p = new FluentCommandLineParser<ApplicationArguments>();

            // specify which property the value will be assigned too.
            p.Setup(arg => arg.RecordId)
             .As('r', "record") // define the short and long option name
             .Required(); // using the standard fluent Api to declare this Option as required.

            p.Setup(arg => arg.NewValue)
             .As('v', "value")
             .Required();

            p.Setup(arg => arg.Silent)
             .As('s', "silent")
             .SetDefault(false); // use the standard fluent Api to define a default value if non is specified in the arguments

            var result = p.Parse(args);
            Console.WriteLine(p.Object.NewValue);

            if (result.HasErrors == false)
            {
                //Console.WriteLine(result.NewValue)
                // use the instantiated ApplicationArguments object from the Object property on the parser.
                //application.Run(p.Object);
            }














            //Console.WriteLine("Please enter a password to use:");
            //string password = Console.ReadLine();
            //Console.WriteLine("Please enter a string to encrypt:");
            //string plaintext = Console.ReadLine();
            //Console.WriteLine("");
//
            //Console.WriteLine("Your encrypted string is:");
            //string encryptedstring = StringCipher.Encrypt(plaintext, password);
            //Console.WriteLine(encryptedstring);
            //Console.WriteLine("");
//
            //Console.WriteLine("Your decrypted string is:");
            //string decryptedstring = StringCipher.Decrypt(encryptedstring, password);
            //Console.WriteLine(decryptedstring);
            //Console.WriteLine("");
//
            //Console.WriteLine("Press any key to exit...");
            //Console.ReadLine();
        }
    }
}