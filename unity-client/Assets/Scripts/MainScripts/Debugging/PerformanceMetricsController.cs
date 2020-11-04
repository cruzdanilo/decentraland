using System.Text;
using DCL.Interface;
using DCL.FPSDisplay;
using UnityEngine;

namespace DCL
{
    public class PerformanceMetricsController
    {
        private LinealBufferHiccupCounter tracker = new LinealBufferHiccupCounter();
        private const int SAMPLES_SIZE = 1000;
        private char[] encodedSamples = new char[SAMPLES_SIZE];
        private int currentIndex = 0;

        public void Update()
        {
            var deltaInMs = Time.deltaTime * 1000;

            tracker.AddDeltaTime(Time.deltaTime);

            encodedSamples[currentIndex++] = (char) deltaInMs;

            if (currentIndex == SAMPLES_SIZE)
            {
                currentIndex = 0;
                Report(new string(encodedSamples));
            }
        }

        private void Report(string encodedSamples)
        {
            WebInterface.SendPerformanceReport(encodedSamples);
        }
    }
}