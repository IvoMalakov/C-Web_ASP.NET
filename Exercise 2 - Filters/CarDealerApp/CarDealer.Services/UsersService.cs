using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;

namespace CarDealer.Services
{
    public class UsersService : Service
    {
        public UsersService(CarDealerContext context) : base(context)
        {
        }

        public void RegisterUser(RegisterUserBindingModel bindingModel)
        {
            User userForCheck = this.Context.Users.FirstOrDefault(u => u.Username == bindingModel.Username);

            if (userForCheck == null && bindingModel.Password == bindingModel.ConfirmPassword)
            {
                User user = new User()
                {
                    Email = bindingModel.Email,
                    Username = bindingModel.Username,
                    Password = bindingModel.Password
                };

                this.Context.Users.Add(user);
                this.Context.SaveChanges();
            }
        }

        public void LoginUser(LoginUserBindingModel bindingModel, string sessionId)
        {
            User user =
                this.Context.Users.FirstOrDefault(
                    u => u.Username == bindingModel.Username && u.Password == bindingModel.Password);

            if (user != null)
            {
                if (!this.Context.Sessions.Any(s => s.SessionId == sessionId))
                {
                    this.Context.Sessions.Add(new Session()
                    {
                        IsActive = true,
                        SessionId = sessionId,
                        User = user
                    });

                    this.Context.SaveChanges();
                }

                Session session = this.Context.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
                session.IsActive = true;
                session.User = user;

                this.Context.SaveChanges();
            }
        }

        public bool UserExists(LoginUserBindingModel bindingModel)
        {
            if (this.Context.Users.Any(u => u.Username == bindingModel.Username && u.Password == bindingModel.Password))
            {
                return true;
            }

            return false;
        }
    }
}