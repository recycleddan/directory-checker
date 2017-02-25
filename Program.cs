using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace directory_checker
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> archiveHashes = new HashSet<string>();

            if(args.Length < 2)
            {
                // TODO: Print usage
                Console.WriteLine("directory-checker <digest> <directory>");
                return;
            }

            var digestPath = args[0];
            var candidatePath = args[1];

            var md5 = MD5.Create();

            using (var fs = new StreamReader(digestPath))
            {
                string line;
                while((line = fs.ReadLine()) != null)
                {
                    archiveHashes.Add(line);
                }
            }

            foreach(var file in Directory.EnumerateFiles(candidatePath, "*.*", SearchOption.AllDirectories))
            {
                using (var fs = File.OpenRead(file))
                {
                    var checksum = md5.ComputeHash(fs);
                    if (!archiveHashes.Contains(BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower()))
                    {
                        Console.WriteLine(file);
                    }
                }
            }
        }
    }
}
