using System;
using Quartz;

namespace QuartzDocker {


    public class EchoJob : IJob {

        public void Execute(IJobExecutionContext context) {
            Console.WriteLine($"{DateTime.Now} Hello, world!");
        }

    }

}
