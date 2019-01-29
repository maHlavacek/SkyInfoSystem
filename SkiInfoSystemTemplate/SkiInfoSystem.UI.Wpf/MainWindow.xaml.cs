using SkiInfoSystem.Core;
using SkiInfoSystem.Utils;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkiInfoSystem.UI.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckBoxClockRuns_Click(object sender, RoutedEventArgs e)
        {
            FastClock.Instance.IsRunning = CheckBoxClockRuns.IsChecked.Value;
        }

        private void InitSkiInfoSystem_Click(object sender, RoutedEventArgs e)
        {
            FastClock.Instance.Time = Convert.ToDateTime("01.01.2019 07:25");
            FastClock.Instance.Factor = Convert.ToInt32(SliderFactor.Value);
            FastClock.Instance.OneMinuteIsOver += TimeChange;
            Controller.Instance.CreateSlops(ConditionChanged);
            CheckBoxClockRuns.IsEnabled = true;
            CheckBoxClockRuns.IsChecked = true;
            FastClock.Instance.IsRunning = CheckBoxClockRuns.IsChecked.Value;
        }

        private void ConditionChanged(object sender, string message)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(TextBoxLogging.Text);
            txt.Insert(0, "\n" + message);
            TextBoxLogging.Text = txt.ToString();
        }

        private void TimeChange(object sender, DateTime dateTime)
        {
            this.Title = "Ski Info System: [" + dateTime + "]";
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    SliderFactor.Value = 300;
        //}

        //private void SliderFactor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    FastClock.Instance.Factor = Convert.ToInt32(SliderFactor.Value);
        //}
    }
}
