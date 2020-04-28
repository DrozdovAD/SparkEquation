using System;
using System.ComponentModel.DataAnnotations;

namespace CustomDataAnnotations
{
    public class DateNotLessThen30Attribute : ValidationAttribute
    {
        public int DateRange {get; set;}

        public DateNotLessThen30Attribute()
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