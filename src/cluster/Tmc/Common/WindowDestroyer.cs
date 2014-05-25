using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Tmc.Common
{
    public class WindowDestroyer
    {
        public string WindowName { get; set; }
        public double RetryInterval
        {
            get { return this._timer.Interval; }
            set { this._timer.Interval = value; }
        }
        public int MaxRetries { get; set; }
        private Timer _timer;
        private int _numRetries;

        public WindowDestroyer(string windowName, double retryInterval = 1000, int maxRetries = 1)
        {
            _numRetries = 0;
            this.WindowName = windowName;
            this.MaxRetries = maxRetries;
            this._timer = new Timer(retryInterval);
            this._timer.Elapsed += OnTimerTickDestroyWindow;
        }

        public void DestroyWindow()
        {
            _numRetries = 0;
            this._timer.Start();
        }

        private void Reset()
        {
            this._timer.Stop();
            _numRetries = 0;
        }

        private void OnTimerTickDestroyWindow(object sender, ElapsedEventArgs e)
        {
            var windows = Win32Helpers.GetOpenWindows();
            var matchingWindows = windows.Where(x => x.Value.Contains(this.WindowName)).Select(x => x.Key);

            if (matchingWindows == null || matchingWindows.Count() == 0)
            {
                _numRetries++;
                if (_numRetries > MaxRetries)
                {
                    this.Reset();
                }
                return;
            }

            try
            {
                foreach (var w in matchingWindows)
                {
                    Win32Helpers.CloseWindow(w);
                }
                this.Reset();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to close window", ex);
            }
        }
    }
}
