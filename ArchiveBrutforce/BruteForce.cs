using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiveBrutforce
{
    class BruteForce
    {
        /// <summary>
        /// Method is used to check is archive location null or not
        /// </summary>
        public static bool checkNullPath()
        {
            if(string.IsNullOrEmpty(Uploader.Path))
            {
                MessageBox.Show($"Nie ma podłączonego archiwum", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Method is used to bruteforce archive password
        /// </summary>
        public static void Brute()
        {
            if(checkNullPath())
            {
                ZipFile archive = new ZipFile(Uploader.Path);
                MessageBox.Show(ZipFile.CheckZipPassword(archive.Name,"volodya").ToString());
            }
        }
    }
}
