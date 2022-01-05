using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace ParentSideApp
{
    public class CTime : INotifyPropertyChanged
    {
        public CTime(string inputLine)
        {
            string[] time = inputLine.Split(' ');
            string[] from = time[0].Split(':');
            string[] to = time[1].Split(':');
            from[0] = from[0].Remove(0, 1);
            to[0] = to[0].Remove(0, 1);
            To = new Interval(Int32.Parse(to[0]), Int32.Parse(to[1]));
            From = new Interval(Int32.Parse(from[0]), Int32.Parse(from[1]));
            Duration = 0;
            Sum = 0;
            Interupt = 0;
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

        public CTime()
        {
            From = new Interval();
            To = new Interval();
        }
        public override string ToString()
        {
            return From.Hour + "h" + From.Minute + " - " + To.Hour + "h" + To.Minute;
        }

        public string TextToSave()
        {
            string result = "";
            result += "F" + From.Hour.ToString("00") + ":" + From.Minute.ToString("00");
            result += " ";
            result += "T" + To.Hour.ToString("00") + ":" + To.Minute.ToString("00");
            if (Duration != 0)
            {
                result += " D" + Duration;
            }
            if (Interupt != 0)
            {
                result += " I" + Interupt;
            }
            if (Sum != 0)
            {
                result += " S" + Sum;
            }
            return result;
        }
        public CTime Clone()
        {
            var result = new CTime
            {
                From =
                {
                    Hour = From.Hour,
                    Minute = From.Minute
                },
                To =
                {
                    Hour = To.Hour,
                    Minute = To.Minute
                },
                Duration = Duration,
                Interupt = Interupt,
                Sum = Sum
            };
            return result;
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

        public static bool InTime(List<CTime> timeLines, CTime newTime, int startedIndex = -1)
        {
            int i = 0;
            foreach (var timeLine in timeLines)
            {
                if (i == startedIndex) continue;
                if (newTime.From.IsGreaterOrEqual(timeLine.From) && !newTime.From.IsGreaterOrEqual(timeLine.To))
                    return true;
                if (newTime.To.IsGreaterOrEqual(timeLine.From) && !newTime.To.IsGreaterOrEqual(timeLine.To))
                    return true;
                if (timeLine.From.IsGreaterOrEqual(newTime.From) && newTime.To.IsGreaterOrEqual(timeLine.To))
                    return true;
                i++;
            }
            return false;
        }
        public static bool WriteDownTheSchedule(string path, List<CTime> list)
        {
            int i = 0;
            StreamWriter sw = new StreamWriter(path);
            foreach (var item in list)
            {
                string tempLine = item.TextToSave();
                i++;
                if (i != list.Count)
                {
                    tempLine += "\n";
                }
                sw.Write(tempLine);
            }
            sw.Close();
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class Interval : INotifyPropertyChanged
    {
        public Interval(int hour, int minute)
        {
            this.Hour = hour;
            this.Minute = minute;
        }

        public Interval()
        {

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

        public bool IsGreaterOrEqual(Interval secondInterval)
        {
            if (Hour > secondInterval.Hour) return true;
            if (Hour == secondInterval.Hour)
            {
                if (Minute >= secondInterval.Minute) return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
