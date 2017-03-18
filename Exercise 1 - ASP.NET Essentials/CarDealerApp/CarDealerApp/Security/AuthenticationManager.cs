using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealer.Data;
using CarDealer.Models.EntityModels;

namespace CarDealerApp.Security
{
    public static class AuthenticationManager
    {
        private static CarDealerContext dbContext = Data.Context;

        public static bool IsAuthenticated(string sessionId)
        {
            return dbContext.Sessions.Any(s => s.IsActive && s.SessionId == sessionId);
        }

        public static User GetAuthenticatedUser(string sessionId)
        {
            User user = dbContext.Sessions.FirstOrDefault(s => s.IsActive && s.SessionId == sessionId)?.User;

            return user;
        }

        public static void Logout(string sessionId)
        {
            Session session = dbContext.Sessions.FirstOrDefault(s => s.IsActive && s.SessionId == sessionId);
            session.IsActive = false;
            dbContext.SaveChanges();
        }
    }
}