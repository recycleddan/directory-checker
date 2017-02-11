using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            }

            var archivePath = args[0];
            var candidatePath = args[1];

            var sha = new SHA256Managed();

            foreach(var file in Directory.EnumerateFiles(archivePath, "*.*", SearchOption.AllDirectories))
            {
                using (var fs = File.OpenRead(file))
                {
                    var checksum = sha.ComputeHash(fs);
                    archiveHashes.Add(BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower());
                }
            }

            foreach(var file in Directory.EnumerateFiles(candidatePath, "*.*", SearchOption.AllDirectories))
            {
                using (var fs = File.OpenRead(file))
                {
                    var checksum = sha.ComputeHash(fs);
                    if (!archiveHashes.Contains(BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower()))
                    {
                        Console.WriteLine(file);
                    }
                }
            }
        }
    }
}
