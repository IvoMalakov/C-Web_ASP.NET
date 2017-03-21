using System;
using CarDealer.Models.EntityModels;

namespace CarDealer.Models.ViewModels
{
    public class LogViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public Operation Operation { get; set; }

        public ModifiedTable ModifiedTable { get; set; }

        public DateTime? Time { get; set; }

        public static int Page { get; set; }
    }
}
