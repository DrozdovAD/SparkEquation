using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using CustomDataAnnotations;
using SparkEquation.Trial.WebAPI.Data.Configuration;

namespace SparkEquation.Trial.WebAPI.Data.Models
{
    public class ProductMessage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Featured { get; set; }

        [DateNotLessThen(DateRange = Constants.DATE_RANGE_DAY_COUNT, ErrorMessage = "Should expire not less than 30 days since now")]
        public DateTime? ExpirationDate { get; set; }

        public int ItemsInStock { get; set; }

        public DateTime? ReceiptDate { get; set; }

        private double rating;
        public double Rating {
            get { return rating; }
            set {
                rating = value;

                if(value > Constants.RATING_DEFAULT_VALUE) {
                    Featured = true;
                }
            }
        }

        [Required]
        public int BrandId { get; set; }

        [Required]
        [MinLength(Constants.CATEGORIES_MIN_LENGTH)]
        [MaxLength(Constants.CATEGORIES_MAX_LENGTH)]
        public IList<int> CategoryIds { get; set; }
    }
}
