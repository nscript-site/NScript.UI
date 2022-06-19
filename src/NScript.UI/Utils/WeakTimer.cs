using System;
using System.Collections.Generic;
using System.Timers;
using System.Text;

namespace NScript.UI.Utils
{
    public class WeakTimer
    {
        public interface IWeakTimerSubscriber
        {
            bool Tick();
        }

        private readonly WeakReference<IWeakTimerSubscriber> _subscriber;
        private System.Timers.Timer _timer;

        public WeakTimer(IWeakTimerSubscriber subscriber)
        {
            _subscriber = new WeakReference<IWeakTimerSubscriber>(subscriber);
            _timer = new System.Timers.Timer();
            _timer.Elapsed += OnTick;
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            IWeakTimerSubscriber subscriber;
            bool v1 = _subscriber.TryGetTarget(out subscriber);
            //Console.WriteLine("v1:" + v1.ToString());

            bool v2 = subscriber == null ? false : subscriber.Tick();
            //Console.WriteLine("v2:" + v2.ToString());

            if (!v1 || !v2)
                Stop();
        }

        public TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(_timer.Interval); }
            set { _timer.Interval = value.TotalMilliseconds; }
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();


        public static WeakTimer StartWeakTimer(WeakTimer.IWeakTimerSubscriber subscriber, TimeSpan interval)
        {
            return null;
            //var timer = new WeakTimer(subscriber)
            //{
            //    Interval = interval
            //};
            //timer.Start();
            //return timer;
        }

    }
}
