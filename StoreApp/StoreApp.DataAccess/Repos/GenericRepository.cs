using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.DataAccess.Model;
using System.Collections.Generic;
using StoreApp.Library.Interfaces;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{

    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static readonly DbContextOptions<_2006StoreApplicationContext> Options = new DbContextOptionsBuilder<_2006StoreApplicationContext>()
            //.UseLoggerFactory(MyLoggerFactory)
            .UseSqlServer(SecretConfiguration.ConnectionString)
            .Options;

        private _2006StoreApplicationContext _context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new _2006StoreApplicationContext();
            table = _context.Set<T>();
        }
        public GenericRepository(_2006StoreApplicationContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table;
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Add(T obj)
        {
            table.Add(obj);
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        
    }
}
