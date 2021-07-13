using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using MicroOrm.Pocos.SqlGenerator;
using Dapper;
using System.Data;

namespace Sale.Data
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        #region Variables
        string MsgCode, MsgMessage, MsgErrorCode;
        public string Msg { get; set; }
        protected ConnectionFactory IConnect;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        protected Repository()
        {
            if (IConnect == null)
            {
                IConnect = new ConnectionFactory(new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString));
            }
        }

        #endregion

        #region Get Data

        /// <summary>
        /// Get List object
        /// </summary>
        /// <param name="navigationProperties">parameter</param>
        /// <returns>Return IList objects</returns>
        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    List<T> list;
                    IQueryable<T> dbQuery = db.Set<T>();

                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    list = dbQuery
                        .AsNoTracking()
                        .ToList<T>();
                    //this.TotalRows = list.Count();
                    return list;
                }
            }
            catch
            {
                throw;
            }
        }

        public IList<T> GetAll(int startRow, int maxRow, params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    List<T> list;
                    IQueryable<T> dbQuery = db.Set<T>();

                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    list = dbQuery
                        .AsNoTracking()
                        .ToList<T>();
                    //this.TotalRows = list.Count();
                    return list.Skip(startRow).Take(maxRow).ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get list object by some conditions 
        /// </summary>
        /// <param name="where">Where conditions</param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    List<T> list;
                    IQueryable<T> dbQuery = db.Set<T>();

                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    list = dbQuery
                        .AsNoTracking()
                        .Where(where)
                        .ToList<T>();
                    //this.TotalRows = list.Count();
                    return list;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
        public IList<T> GetList(int startRow, int maxRow, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    List<T> list;
                    IQueryable<T> dbQuery = db.Set<T>();

                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    list = dbQuery
                        .AsNoTracking()
                        .Where(where)
                        .ToList<T>();
                    //this.TotalRows = list.Count();
                    return list.Skip(startRow).Take(maxRow).ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get single object
        /// </summary>
        /// <param name="where">Where some conditions</param>
        /// <param name="navigationProperties"></param>
        /// <returns>Return Object </returns>
        public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    T item = null;
                    IQueryable<T> dbQuery = db.Set<T>();

                    //Apply eager loading
                    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                        dbQuery = dbQuery.Include<T, object>(navigationProperty);

                    item = dbQuery
                        .AsNoTracking() //Don't track any changes for the selected item
                        .FirstOrDefault(where); //Apply where clause
                    //this.TotalRows = 1;
                    return item;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get object by object ID
        /// </summary>
        /// <param name="ID">object ID</param>
        /// <returns>Return only object</returns>
        public T GetByID(object ID)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    return db.Set<T>().Find(ID);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get message error by msgCode
        /// </summary>
        /// <param name="Code">Message Code</param>
        /// <returns>Return message</returns>
        public string GetMessage(string Code, string Lang)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    return "";
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get current message
        /// </summary>
        /// <returns>Return message</returns>
        //public string GetMessage()
        //{
        //    try
        //    {
        //        using (var db = new DBEntities())
        //        {
        //            return db.Messages.Find(MsgCode).Messages;
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// Add message
        /// </summary>
        /// <param name="Code">Message Code</param>
        /// <param name="Message">Message Text</param>
        public void AddMessage(string Code, string Message, string ErrorCode = "")
        {
            MsgCode = Code;
            MsgMessage = Message;
            MsgErrorCode = ErrorCode;
        }

        public string getMsgCode()
        {
            return MsgCode;
        }

        public string getMessage()
        {
            return MsgMessage;
        }

        public string getErrorCode()
        {
            return MsgErrorCode;
        }
        #endregion

        #region Insert, Update, Delete

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="item">Object</param>
        /// <returns>True/False</returns>
        public bool Add(T item)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add news multi items
        /// </summary>
        /// <param name="items">list items</param>
        /// <returns>True/False</returns>
        public bool Add(List<T> items)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    foreach (T item in items)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>True/False</returns>
        public bool Update(T item)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DbSet dbSet = db.Set<T>();
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update list items
        /// </summary>
        /// <param name="items">List items</param>
        /// <returns>True/False</returns>
        public bool Update(List<T> items)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    foreach (T item in items)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete item by ID
        /// </summary>
        /// <param name="ID">Item ID</param>
        /// <returns>True/False</returns>
        public bool Delete(object ID)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DbSet<T> dbSet;
                    dbSet = db.Set<T>();
                    T obj = db.Set<T>().Find(ID);
                    if (db.Entry(obj).State == EntityState.Detached)
                    {
                        dbSet.Attach(obj);
                    }

                    dbSet.Remove(obj);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;
                // Exception occur when having other table relation with this table
                if (sqlException != null && sqlException.Number == 547)
                {
                    this.AddMessage("DEL-000002", ex.Message);
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                this.AddMessage("DEL-000001", "Delete dữ liệu không thành công");
                throw;
            }
        }

        /// <summary>
        /// Delete multi items
        /// </summary>
        /// <param name="items">List items</param>
        /// <returns>True/False</returns>
        public bool Delete(List<T> items)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DbSet<T> dbSet;
                    dbSet = db.Set<T>();
                    dbSet.RemoveRange(items);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;
                // Exception occur when having other table relation with this table
                if (sqlException != null && sqlException.Number == 547)
                {
                    this.AddMessage("DEL-000002", ex.Message);
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

       
    }
}
