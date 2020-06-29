using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        /// <summary>
        /// Gets all the records from a DbSet dealing with entities of type T from the database
        /// </summary>
        /// <returns>The DbSet</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets a specific entity of type T from the database given its primary key id
        /// </summary>
        /// <param name="id">The id that corresponds to the primary key of the table</param>
        /// <returns>The entity of the table with the given id </returns>
        T GetById(object id);

        /// <summary>
        /// Tracks an object of type T corresponding to the DbSet dealing with entities of type T to be added to the database
        /// </summary>
        /// <param name="obj">The object to be added</param>
        void Add(T obj);

        /// <summary>
        /// Tracks an object of type T corresponding to the DbSet dealing with entities of type T to be removed from the database
        /// </summary>
        /// <param name="id">The primary key id of the object to be removed</param>
        void Delete(object id);

        /// <summary>
        /// Saves the tracked changes to persist them to the database
        /// </summary>
        void Save();
        
    }
}
