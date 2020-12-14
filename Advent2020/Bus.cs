using System;
using System.Collections.Generic;
using System.Linq;
namespace Advent2020
{
    public class Bus
    {
        private int ArrivalTime;
        private List<long?> Buses;
        
        public Bus()
        {
            ArrivalTime = 0;
            Buses = new List<long?>(); 
        }
        public void ParseSchedule(string schedule)
        {
            foreach (string busid in schedule.Split(','))
            {
                if (busid == "x")
                {
                    Buses.Add(null);
                    continue; 
                }
                Buses.Add(int.Parse(busid));
            }
        }
        public static int MinutesToBus(int bus, int arrival)
        {
            return bus - (arrival % bus);
        }
        public int NextBus(int arrival)
        {
            int minWait = 10000;
            int nextBus = 0;
            foreach (int bus in Buses.Where(bus => bus != null))
            {
                int wait = MinutesToBus(bus, arrival);
                if (wait < minWait)
                {
                    minWait = wait;
                    nextBus = bus;
                }
            }
            return nextBus;
        }
        public void ReadFile(string filename)
        {
            List<string> file = FileReader.ReadFileOfStrings(filename);
            ArrivalTime = int.Parse(file[0]);
            ParseSchedule(file[1]);
        }
        public int NextBusTimesWaitFromFile(string filename)
        {
            int bus;
            int wait;
            ReadFile(filename);
            bus = NextBus(ArrivalTime);
            wait = MinutesToBus(bus, ArrivalTime);
            return bus * wait;
        }
        public static long ExtendedGCD(long a, long b, out long x, out long y)
        {
            long oldR = a;
            long r = b;
            long oldS = 1;
            long s = 0;
            long oldT = 0;
            long t = 1;
            while (r != 0)
            {
                long quotient = oldR / r;
                long tmp = r;
                r = oldR - quotient * r;
                oldR = tmp;
                tmp = s;
                s = oldS - quotient * s;
                oldS = tmp;
                tmp = t;
                t = oldT - quotient * t;
                oldT = tmp;
            }
            x = oldS;
            y = oldT;
            return oldR;
        }
        public long StartTimestamp()
        {
            long N = Buses.Where(bus => bus != null).Aggregate(1L, (a, b) => (long)a * (long)b, a => a);
            long x = 0;
            foreach (int bus in Buses.Where(bus => bus != null).OrderByDescending(x => x))
            {
                int n1 = bus;
                long N1 = N / n1;
                long m1;
                long M1;
                int a1 = n1 - Buses.IndexOf(n1);
                ExtendedGCD(n1, N1, out m1, out M1);
                x += a1 * M1 * N1;
            }
            while (x < 0)
            {
                x += N;
            }
            while (x > N)
            {
                x -= N;
            }
            return x;
        }
        public long StartTimestampFromFile(string filename)
        {
            ReadFile(filename);
            return StartTimestamp();
        }
    }
}
