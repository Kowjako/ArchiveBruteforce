using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiveBrutforce
{
    class BruteForce
    {
        static MainWindow Window;

        private static string pass = null;
        public static bool isMatched = false;
        private static long computedKeys = 0;
        private static int charactersToTestLength = 0;

        static ZipFile archive = new ZipFile(Uploader.Path);

        private static char[] charactersToTest =
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
        'F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
        '6','7','8','9','0','!','$','#','@','-','+'
        };

        public static bool checkNullPath()
        {
            if(string.IsNullOrEmpty(Uploader.Path))
            {
                MessageBox.Show($"Nie ma podłączonego archiwum", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public static void Brute(object window)
        {
            Window = (MainWindow)window;
            if(checkNullPath())
            {
                int passLength = 0;
                charactersToTestLength = charactersToTest.Length;
                StartComputedThread();
                while(!isMatched)
                {
                    passLength++;
                    StartBruteForce(passLength);
                }
            }
        }

        private static void StartComputedThread()
        {
            Thread computedKeysThread = new Thread(new ThreadStart(ShowComputedKeys));
            computedKeysThread.Start();
        }

        private static void ShowComputedKeys()
        {
            while (!isMatched)
            {
                Window.Dispatcher.Invoke(() => Window.passCounter.Text = computedKeys.ToString());
            }
        }

        private static void StartBruteForce(int passLength)
        {
            char[] keyChars = createCharArray(passLength, charactersToTest[0]); 
            int indexOfLastChar = passLength - 1;
            CreateNewKey(0, keyChars, passLength, indexOfLastChar);
        }

        /* makes the first pass to test which depends only on length: aaa, aaaa, aaaaa */
        private static char[] createCharArray(int length, char defaultChar)
        {
            return (from c in new char[length] select defaultChar).ToArray();
        }

        private static void CreateNewKey(int currentCharPosition, char[] keyChars, int passLength, int indexOfLastChar)
        {
            int nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < charactersToTestLength; i++)
            {
                keyChars[currentCharPosition] = charactersToTest[i];

                if(currentCharPosition<indexOfLastChar)
                {
                    CreateNewKey(nextCharPosition, keyChars, passLength, indexOfLastChar);
                }
                else
                {
                    computedKeys++;
                    pass = new string(keyChars);
                    if (ZipFile.CheckZipPassword(archive.Name, pass))
                    {
                        if(!isMatched)
                        {
                            isMatched = true;
                            MessageBox.Show($"Password is {new string(keyChars)}");
                        }
                        return;
                    }
                }
            }
        }
    }
}
