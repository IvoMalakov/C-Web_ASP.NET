using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Models.EntityModels
{
    public partial class User
    {
        private string username;
        private string password;
        private string email;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                if (!CheckIsUsernameValid(value))
                {
                    throw new ArgumentException("username is not valid");
                }

                this.username = value;
            }
        }

        [Required]
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (!CheckIsPasswordValid(value))
                {
                    throw new ArgumentException("Password is not valid");
                }

                this.password = value;
            }
        }

        [Required]
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (!CheckIsEmailValid(value))
                {
                    throw new ArgumentException("Email is not valid");
                }

                this.email = value;
            }
        }
    }
}
