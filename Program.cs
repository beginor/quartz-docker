using System;
using Mono.Unix;
using Mono.Unix.Native;
using Quartz;
using Quartz.Impl;

namespace QuartzDocker {

    class MainClass {

        private static IScheduler scheduler = null;

        public static void Main(string[] args) {
            StartupQuartz();
            WaitForExit();
            ShutdownQuartz();
        }

        private static void StartupQuartz() {
            Console.WriteLine("Start Quartz");
            var factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler();
            scheduler.Start();
            var job = JobBuilder.Create<EchoJob>()
                .WithIdentity("EchoJob", "EchoJob")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("EchoJob-Trigger", "EchoJob")
                .WithSimpleSchedule(
                    x => x.WithInterval(TimeSpan.FromSeconds(5))
                          .RepeatForever()
                )
                .StartNow()
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }

        private static void ShutdownQuartz() {
            Console.WriteLine("Shutdown Quartz");
            scheduler.Shutdown();
        }

        private static void WaitForExit() {
            var signals = new UnixSignal[] {
                new UnixSignal(Signum.SIGINT),
                new UnixSignal(Signum.SIGTERM)
            };
            var index = UnixSignal.WaitAny(signals);
            var signal = signals[index].Signum;
            Console.WriteLine($"Received Signal: {signal}");
        }

    }

}
