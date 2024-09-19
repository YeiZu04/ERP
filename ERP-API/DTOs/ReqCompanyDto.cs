﻿using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class ReqCompanyDto
    {
        [Required]
        [StringLength(100)]
        public string NameCompany { get; set; }

        [Required]
        [StringLength(100)]
        public string CodeCompany { get; set; }

        [StringLength(500)]
        public string? DescriptionCompany { get; set; }

        [StringLength(200)]
        public string? LocationCompany { get; set; }

        [StringLength(200)]
        public string? UrlCompany { get; set; }
    }
}
