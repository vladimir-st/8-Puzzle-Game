using System;
using System.Diagnostics;

namespace _8PuzzleGame
{
    public class PerformanceMeasurer
    {
        public PerformanceMeasurer()
        {
            this.StopWatch = new Stopwatch();
        }

        public Stopwatch StopWatch { get; private set; }
        public Process Process { get; private set; }

        public void StartMeasuring()
        {
            this.StopWatch.Start();
            this.Process = Process.GetCurrentProcess();
        }

        public void StopMeasuring()
        {
            this.StopWatch.Stop();
        }

        public void PrintResults()
        {
            Console.WriteLine($"Running time: {this.StopWatch.Elapsed.TotalSeconds} s");
            Console.WriteLine($"Max ram usage: {this.Process.PeakWorkingSet64} kb");
        }
    }
}
