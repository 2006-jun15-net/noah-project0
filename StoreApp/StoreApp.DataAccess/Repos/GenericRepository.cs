using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.DataAccess.Model;
using System.Collections.Generic;
using StoreApp.Library.Interfaces;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    /// <summary>
    /// A general repository that takes handles DML operations for a type T entity. Implements the generic methods provide by IRepository
    /// </summary>
    /// <typeparam name="T">The type of entity</typeparam>
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Logger that logs SQL Statements to the console
        /// </summary>
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        /// <summary>
        /// The options for the database named 2006StoreApplication. Requires a particular connection string for this database
        /// </summary>
        public static readonly DbContextOptions<_2006StoreApplicationContext> Options = new DbContextOptionsBuilder<_2006StoreApplicationContext>()
            //.UseLoggerFactory(MyLoggerFactory)
            .UseSqlServer(SecretConfiguration.ConnectionString)
            .Options;

        /// <summary>
        /// DbContext for communicating with the 2006StoreApplication database
        /// </summary>
        private _2006StoreApplicationContext _context = null;

        /// <summary>
        /// Contains the records of the DbSet dealing with entities of type T
        /// </summary>
        private DbSet<T> table = null;

        /// <summary>
        /// Initialize a repository and provide the context for the 2006StoreApplication database
        /// </summary>
        public GenericRepository()
        {
            this._context = new _2006StoreApplicationContext();
            table = _context.Set<T>();
        }
        /// <summary>
        /// Initialize a repository and take a context for the 2006StoreApplication database
        /// </summary>
        /// <param name="_context">The DbContext</param>
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
