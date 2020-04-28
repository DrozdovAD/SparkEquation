using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using CustomDataAnnotations;

namespace SparkEquation.Trial.WebAPI.Data.Models
{
    public class ProductMessage
    {
        private const int RATING_DEFAULT_VALUE = 8;
        private const int DATE_RANGE = 30;
        private const int CATEGORIES_MIN_LENGTH = 1;
        private const int CATEGORIES_MAX_LENGTH = 5;

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Featured { get; set; }

        [DateNotLessThen30(DateRange = DATE_RANGE, ErrorMessage = "Should expire not less than 30 days since now")]
        public DateTime? ExpirationDate { get; set; }

        public int ItemsInStock { get; set; }

        public DateTime? ReceiptDate { get; set; }

        private double rating;
        public double Rating {
            get { return rating; }
            set {
                rating = value;

                if(value > RATING_DEFAULT_VALUE) {
                    Featured = true;
                }
            }
        }

        [Required]
        public int BrandId { get; set; }

        [Required]
        [MinLength(CATEGORIES_MIN_LENGTH)]
        [MaxLength(CATEGORIES_MAX_LENGTH)]
        public IList<int> CategoryIds { get; set; }
    }
}
