using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveBrutforce
{
    class Uploader
    {
        public static string Path { get; set; }

        public static void upload(string path)
        {
            Path = path;
        }
    }
}
