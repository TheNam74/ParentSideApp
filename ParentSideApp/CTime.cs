using System;

namespace ParentSideApp
{
    public class CTime
    {
        public CTime(int hour, int minute, int hour2, int minute2, int duration, int interupt, int sum)
        {
            To = new Interval(hour2, minute2);
            From = new Interval(hour, minute);
            this.Duration = duration;
            this.Interupt = interupt;
            this.Sum = sum;
        }

        public CTime(string inputLine)
        {
            string[] time = inputLine.Split(' ');
            string[] from = time[0].Split(':');
            string[] to = time[1].Split(':');
            from[0] = from[0].Remove(0, 1);
            to[0] = to[0].Remove(0, 1);
            To = new Interval(Int32.Parse(to[0]), Int32.Parse(to[1]));
            From = new Interval(Int32.Parse(from[0]), Int32.Parse(from[1]));
            if (time.Length == 2) return;
            int i = 2;
            if (time[i].StartsWith("D"))
            {
                time[i] = time[i].Remove(0, 1);
                Duration = Int32.Parse(time[i]);
                i++;
            }

            if (i >= time.Length) return;
            if (time[i].StartsWith("I"))
            {
                time[i] = time[i].Remove(0, 1);
                Interupt = Int32.Parse(time[i]);
                i++;
            }

            if (i >= time.Length) return;
            time[i] = time[i].Remove(0, 1);
            Sum = Int32.Parse(time[i]);

        }
        public override string ToString()
        {
            return From.Hour + "h" + From.Minute + " - " + To.Hour + "h" + To.Minute;
        }

        public Interval From { get; set; }
        public Interval To { get; set; }
        public int Duration { get; set; }
        public int Interupt { get; set; }
        public int Sum { get; set; }

        public static bool InTime(CTime time, string fileName)
        {
            var interval = new Interval(fileName);
            if (interval.Hour > time.From.Hour && interval.Hour < time.To.Hour)
            {
                return true;
            }
            else if (interval.Hour == time.From.Hour)
            {
                if (time.From.Hour == time.To.Hour)
                {
                    if (time.From.Minute <= interval.Minute && interval.Minute <= time.To.Minute)
                    {
                        return true;
                    }
                    else return false;
                }

                return true;
            }
            else if (interval.Hour == time.To.Hour)
            {
                if (interval.Minute < time.To.Minute) return true;
            }
            return false;
        }
    }
    public class Interval
    {
        public Interval(int hour, int minute)
        {
            this.Hour = hour;
            this.Minute = minute;
        }

        public Interval(string fileName)
        {
            string[] time = fileName.Split('-');
            Hour = Int32.Parse(time[0]);
            time[1] = time[1].Remove(time[1].IndexOf('.'));
            Minute = Int32.Parse(time[1]);
        }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public override string ToString()
        {
            return Hour + "h" + Minute;
        }
    }
}
