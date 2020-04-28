using System;
using System.ComponentModel.DataAnnotations;

namespace CustomDataAnnotations
{
    public class DateNotLessThenAttribute : ValidationAttribute
    {
        public int DateRange {get; set;}

        public DateNotLessThenAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dateForCheck = (DateTime)value;
            var validDate = DateTime.Now;
            validDate.AddDays(DateRange);

            return dateForCheck >= validDate;
        }
    }
}