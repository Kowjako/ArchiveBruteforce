using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ArchiveBrutforce
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime now;
        TimeSpan estimatedTime;
        public bool isPasswordCracked = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void dropPanel_Drop(object sender, DragEventArgs e)
        {
            Uploader.upload(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            now = DateTime.Now;
            Thread bruteForceThread = new Thread(new ParameterizedThreadStart(BruteForce.Brute));
            bruteForceThread.Start(this);
            Thread timeUpdaterThread = new Thread(new ThreadStart(UpdateEstimatedTime));
            timeUpdaterThread.Start();
        }

        private void UpdateEstimatedTime()
        {
            while(!isPasswordCracked)
            {
                estimatedTime = DateTime.Now.Subtract(now);
                this.Dispatcher.Invoke(() => timeLabel.Text = string.Format("{0:D1}:{1:D2}:{2:D2}", estimatedTime.Hours, estimatedTime.Minutes, estimatedTime.Seconds));
            }
        }
    }
}

