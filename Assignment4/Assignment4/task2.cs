using System;
using System.Timers;

namespace AlarmClockDemo
{
    // 自定义事件参数类
    public class TickEventArgs : EventArgs
    {
        public DateTime CurrentTime { get; set; }
    }

    public class AlarmEventArgs : EventArgs
    {
        public DateTime AlarmTime { get; set; }
    }

    // 闹钟类
    public class AlarmClock
    {
        private readonly System.Timers.Timer _timer;
        private DateTime? _alarmTime;

        // 声明事件
        public event EventHandler<TickEventArgs> Tick;
        public event EventHandler<AlarmEventArgs> Alarm;

        public AlarmClock()
        {
            _timer = new System.Timers.Timer(1000); // 1秒间隔
            _timer.Elapsed += OnTimerElapsed;
        }

        // 设置闹钟时间
        public void SetAlarm(DateTime alarmTime)
        {
            _alarmTime = alarmTime;
            Console.WriteLine($"\n闹钟已设置为 {alarmTime:HH:mm:ss}");
        }

        // 启动闹钟
        public void Start()
        {
            _timer.Start();
            Console.WriteLine("闹钟开始运行...");
        }

        // 停止闹钟
        public void Stop()
        {
            _timer.Stop();
            Console.WriteLine("闹钟已停止");
        }

        // 定时器触发事件
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var currentTime = DateTime.Now;

            // 触发Tick事件
            Tick?.Invoke(this, new TickEventArgs { CurrentTime = currentTime });

            // 检查是否到达闹钟时间
            if (_alarmTime.HasValue && currentTime >= _alarmTime.Value)
            {
                // 触发Alarm事件
                Alarm?.Invoke(this, new AlarmEventArgs { AlarmTime = _alarmTime.Value });
                _alarmTime = null; // 重置闹钟时间防止重复触发
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var alarmClock = new AlarmClock();

            // 订阅Tick事件
            alarmClock.Tick += (sender, e) =>
                Console.WriteLine($"[Tick] 当前时间: {e.CurrentTime:HH:mm:ss}");

            // 订阅Alarm事件
            alarmClock.Alarm += (sender, e) =>
                Console.WriteLine($"\n>>> [Alarm] 叮铃铃！ {e.AlarmTime:HH:mm:ss} 到啦！ <<<");

            // 设置10秒后的闹钟
            alarmClock.SetAlarm(DateTime.Now.AddSeconds(10));

            alarmClock.Start(); // 启动闹钟

            Console.WriteLine("\n按任意键停止闹钟...");
            Console.ReadKey();
            alarmClock.Stop();
        }
    }
}
