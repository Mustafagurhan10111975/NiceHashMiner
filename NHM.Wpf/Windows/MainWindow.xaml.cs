﻿using NHM.Wpf.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace NHM.Wpf.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<DeviceInfo> _devs;

        public MainWindow()
        {
            InitializeComponent();

            _devs = new ObservableCollection<DeviceInfo>
            {
                new DeviceInfo(false, "CPU#1 Intel(R) Core(TM) i7-8700k CPU @ 3.70GHz", null, 10, null, "3 / 3 / 0"),
                new DeviceInfo(true, "GPU#1 EVGA GeForce GTX 1080 Ti", 64, 0, 1550, "36 / 27 / 5"),
                new DeviceInfo(true, "GPU#2 EVGA GeForce GTX 1080 Ti", 54, 0, 1150, "36 / 27 / 3"),

            };

            DevGrid.ItemsSource = _devs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _devs[1].Temp++;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _devs.Add(_devs[1]);
        }
    }

    public class DeviceInfo : INotifyPropertyChanged
    {
        public bool Enabled { get; set; }
        public string Device { get; }
        public string Status => Enabled ? "Stopped" : "Disabled";
        private int? _temp;
        public int? Temp
        {
            get => _temp;
            set
            {
                _temp = value;
                SetPropertyChanged();
            }
        }
        public int Load { get; }
        public int? RPM { get; }
        public string AlgoDetail { get; }
        public string ButtonText => Enabled ? "Start" : "N/A";

        public DeviceInfo(bool enabled, string dev, int? temp, int load, int? rpm, string detail)
        {
            Enabled = enabled;
            Device = dev;
            Temp = temp;
            Load = load;
            RPM = rpm;
            AlgoDetail = detail;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void SetPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
