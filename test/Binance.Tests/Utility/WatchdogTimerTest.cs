﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Binance.Utility;
using Xunit;

namespace Binance.Tests.Utility
{
    public class WatchdogTimerTest
    {
        [Fact]
        public void Throws()
        {
            Assert.Throws<ArgumentNullException>("onTimeout", () => new WatchdogTimer(null));
        }

        [Fact]
        public void Properties()
        {
            var interval = TimeSpan.FromMinutes(5);

            var watchdog = new WatchdogTimer(() => { })
            {
                Interval = interval
            };

            Assert.Equal(interval, watchdog.Interval);
        }

        [Fact]
        public async Task Timeout()
        {
            var interval = TimeSpan.FromSeconds(1);

            var stopwatch = Stopwatch.StartNew();

            var watchdog = new WatchdogTimer(() => stopwatch.Stop())
            {
                Interval = interval
            };

            await Task.Delay(2000);

            Assert.True(stopwatch.ElapsedMilliseconds <= interval.TotalMilliseconds + 100);
            Assert.True(stopwatch.ElapsedMilliseconds >= interval.TotalMilliseconds - 100);
        }

        [Fact]
        public async Task Kick()
        {
            var interval = TimeSpan.FromSeconds(1);

            var stopwatch = Stopwatch.StartNew();

            var watchdog = new WatchdogTimer(() => stopwatch.Stop())
            {
                Interval = interval
            };

            const int count = 4;
            const int delay = 500;
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(delay);
                watchdog.Kick();
            }

            Assert.True(stopwatch.ElapsedMilliseconds <= (count * delay) + 100);
            Assert.True(stopwatch.ElapsedMilliseconds >= (count * delay) - 100);
        }
    }
}
