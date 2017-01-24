using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        // take in input from the file and generate tokens for each one
        static void Main(string[] args)
        {
            new Program().TokenizeTest();
        }

        public void TokenizeTest()
        {
            string contents = File.ReadAllText(@"test.txt");
            Tokenizer tokenizer = new Tokenizer();
            tokenizer.Tokenize(contents);
            tokenizer.ListAllTokens();
            Console.ReadLine();
        }
    }
}
