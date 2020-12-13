using System;
using System.Collections.Generic;
using System.Linq;
namespace Advent2020
{
    public class Bus
    {
        private int ArrivalTime;
        private List<int> Buses;
        
        public Bus()
        {
            ArrivalTime = 0;
            Buses = new List<int>(); 
        }
        public void ParseSchedule(string schedule)
        {
            foreach (string busid in schedule.Split(’,’))
            {
                if (busid == ”x”)
                {
                    Buses.Add(null);
                    continue; 
                }
                Console.WriteLine($”found bus ’{busid}’”);
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
            foreach (int bus in Buses.Where(bus != null))
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
            Console.WriteLine($”parsing ArrivalTime ’{file[0]}’”);
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
        public long StartTimestamp()
        {
            int x = 0;
            long timestamp;
            bool found = false;
            while (!found)
            {
                found = true;
                timestamp = x * Buses[0]
                for (int i = 1; i < Buses.Count; i++)
                {
                    if (Buses[i] != null && (timestamp + i) % Buses[i] != 0)
                    {
                        found = false;
                        break;
                    }
                }
                x++;
            }
            return timestamp;
        }
    }
}