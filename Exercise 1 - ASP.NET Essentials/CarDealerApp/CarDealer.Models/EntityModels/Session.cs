using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Models.EntityModels
{
    public class Session
    {
        [Key]
        public string SessionId { get; set; }

        public bool IsActive { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public override string ToString()
        {
            return $"{this.SessionId}\t{this.User.Id}";
        }
    }
}
