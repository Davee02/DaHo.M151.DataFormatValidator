﻿using System.ComponentModel.DataAnnotations;

namespace DaHo.M151.DataFormatValidator.Models.ApiModels
{
    public class ConvertFormatRequest
    {
        [Required]
        public DataFormat From { get; set; }

        [Required]
        public DataFormat To { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
