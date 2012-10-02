using System;

namespace nl.jbs.banktransactions.Models
{
    public class TimePeriodGroup : IComparable<TimePeriodGroup>
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public TimePeriodGroup(DateTime date)
        {
            this.Month = date.Month;
            this.Year = date.Year;
        }

        /**
         * Used to group this object
         *
         * TODO: Find proper way to do that
         **/

        public override string ToString()
        {
            return Year + "/" + string.Format("{0:00}", Month);
        }

        public bool Equals(TimePeriodGroup obj)
        {
            return Year == obj.Year && Month == obj.Month;
        }

        public override int GetHashCode()
        {
            return Month.GetHashCode() + Year.GetHashCode();
        }

        public int CompareTo(TimePeriodGroup obj)
        {
            int year = Year.CompareTo(obj.Year);
            return year == 0 ? Month.CompareTo(obj.Month) : year;
        }
    }
}