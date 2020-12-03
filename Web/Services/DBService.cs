using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class DBService
    {
        protected DatabaseContext Database { get; set; }
        public DBService(DatabaseContext db)
        {
            Database = db;
        }

        public IEnumerable<T> SelectRandom<T>(IQueryable<T> list, Func<T, bool> filter, int count) where T : class
        {
            var random = new Random();
            var filtered = list
                                .Where(filter)
                                .OrderBy(r => random.Next()); //shuffle
            var countToTake = count < filtered.Count() ? count : filtered.Count();
            return filtered.Take(countToTake);
        }
    }
}
