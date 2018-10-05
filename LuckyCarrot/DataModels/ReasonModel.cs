using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataModels
{
    public class ReasonModel
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
