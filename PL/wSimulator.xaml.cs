﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using static Simulator.Simulator;
using System.Windows.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for wSimulator.xaml
    /// </summary>
    public partial class wSimulator : Window
    {
       
        BlApi.IBl bl;
        string? nextStatus;
        string? previousStatus;
        BackgroundWorker? worker;
        Tuple<BO.Order, int, string, string>? dataContextT;
        //====== disable the option of closing the window =======
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        //=====================================================
        private Stopwatch? stopWatch;
        private bool isTimerRun;
        //=======progressBar variables
        Duration duration;
        DoubleAnimation? doubleanimation;
        ProgressBar? ProgressBar;
        //=======countdown timer variables
        DispatcherTimer? timer;
        TimeSpan time;
        //=======
        public wSimulator(BlApi.IBl Bl)
        {
            InitializeComponent();
            bl = Bl;
            Loaded += ToolWindow_Loaded;
            TimerStart();
        }

        void countDownTimer(int sec)
        {
            time = TimeSpan.FromSeconds(sec);

            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = time.ToString("c");
                if (time == TimeSpan.Zero) timer?.Stop();
                time = time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            timer.Start();
        }
        void ProgressBarStart(int sec)
        {
            if (ProgressBar != null)
            {
                pBar.Items.Remove(ProgressBar);
            }
            ProgressBar = new ProgressBar();
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Orientation = Orientation.Horizontal;
            ProgressBar.Width = 500;
            ProgressBar.Height = 30;
            duration = new Duration(TimeSpan.FromSeconds(sec * 2));
            doubleanimation = new DoubleAnimation(200.0, duration);
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            pBar.Items.Add(ProgressBar);
        }
        void TimerStart()
        {
            stopWatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += TimerDoWork!;
            worker.ProgressChanged += TimerProgressChanged!;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            stopWatch.Restart();
            isTimerRun = true;
            worker.RunWorkerAsync();
        }
        void TimerDoWork(object sender, DoWorkEventArgs e)
        {
            ProgressChange += changeOrder!;
            StopSimulator += Stop!;
            run();
            while (isTimerRun)
            {
                worker?.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        private void changeOrder(object sender, EventArgs e)
        {
            if (!(e is Details))
                return;

            Details? details = e as Details;
            previousStatus = (details?.order.ShipDate == null) ? BO.Enums.EStatus.Done.ToString() : BO.Enums.EStatus.Sent.ToString();
            nextStatus = (details?.order.ShipDate == null) ? BO.Enums.EStatus.Sent.ToString() : BO.Enums.EStatus.Provided.ToString();
            dataContextT = new Tuple<BO.Order, int, string, string>(details!.order, details.seconds / 1000, previousStatus, nextStatus);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            else
            {
                DataContext = dataContextT;
                countDownTimer(details.seconds / 1000);
                ProgressBarStart(details.seconds / 1000);
            }
        }
        void TimerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch!.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            SimulatorTXTB.Text = timerText;
        }
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopWatch!.Stop();
                isTimerRun = false;
            }
            Simulator.Simulator.DoStop();
            this.Close();
        }
        public void Stop(object sender, EventArgs e)
        {
            ProgressChange -= changeOrder!;
            StopSimulator -= Stop!;
            
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(Stop, sender, e);
            }
            else
            {
                MessageBox.Show("complete updating");
                this.Close();
            }
        }
    }
}
