using System;

namespace ParentSideApp
{
    public class CDate
    {
        public int Date { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public CDate()
        {
            Date = 0;
            Month = 0;
            Year = 0;
        }

        public CDate(string folderName)
        {
            string[] listString = folderName.Split('-');
            Date = Int32.Parse(listString[0]);
            Month = Int32.Parse(listString[1]);
            Year = Int32.Parse(listString[2]);
        }
    }
}
