using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class PointTransfer
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int FromUserId { get; set; }
        //public User FromUser { get; set; }
        public int ToUserId { get; set; }
        //public User ToUser { get; set; }
        public int ReasonId { get; set; }
        public int Points { get; set; }
        public string Note { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
