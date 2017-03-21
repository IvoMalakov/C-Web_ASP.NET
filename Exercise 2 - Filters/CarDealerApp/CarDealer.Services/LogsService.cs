using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public class LogsService : Service
    {
        public LogsService(CarDealerContext context) : base(context)
        {
        }

        public IEnumerable<LogViewModel> GetAllLogs(int pageToDisplay)
        {
            var logViewModels = GetFiltredLogs("");
            LogViewModel.Page = pageToDisplay;

            return logViewModels;
        }

        public IEnumerable<LogViewModel> GetFiltredLogs(string username)
        {
            var allLogs = this.Context.Logs.Where(l => l.User.Username.Contains(username)).ToList();
            var logViewModels = new HashSet<LogViewModel>();

            foreach (var log in allLogs)
            {
                var logViewModel = new LogViewModel()
                {
                    Id = log.Id,
                    ModifiedTable = log.ModifiedTable,
                    Operation = log.Operation,
                    Time = log.Time,
                    Username = log.User.Username
                };

                logViewModels.Add(logViewModel);
            }

            return logViewModels;
        }

        public DeleteLogViewModel GetLogToDelition(int id)
        {
            Log log = this.Context.Logs.Find(id);

            DeleteLogViewModel deleteLogViewModel = new DeleteLogViewModel()
            {
                Id = log.Id,
                Time = log.Time,
                User = log.User.Username
            };

            return deleteLogViewModel;
        }

        public void DeleteLog(int id)
        {
            Log log = this.Context.Logs.Find(id);
            this.Context.Logs.Remove(log);
            this.Context.SaveChanges();
        }

        public void ClearAllLogs()
        {
            this.Context.Database.ExecuteSqlCommand("DELETE FROM LOGS");
            this.Context.SaveChanges();
        }

        public void GenerateLog(Operation operation, ModifiedTable modified, int userId)
        {
            Log log = new Log()
            {
                Operation = operation,
                ModifiedTable = modified,
                User = this.Context.Users.Find(userId),
                Time = DateTime.Now
            };

            this.Context.Logs.Add(log);
        }
    }
}
