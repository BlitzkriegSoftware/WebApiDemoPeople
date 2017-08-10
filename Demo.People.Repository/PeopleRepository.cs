using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLitePCL;
using SQLite;

namespace Demo.People.Repository
{
    /// <summary>
    /// Repository: People
    /// </summary>
    public class PeopleRepository
    {
        private PeopleRepository() { }

        /// <summary>
        /// Path to SQL Lite DB File
        /// </summary>
        public string PathToSQLLiteDBFile { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="pathToSQLLiteDBFile">Path to SQL Lite DB File</param>
        public PeopleRepository(string pathToSQLLiteDBFile)
        {
            this.PathToSQLLiteDBFile = pathToSQLLiteDBFile;
            CreateDBIfNotExist();
        }

        /// <summary>
        /// Create DB if it does not exist
        /// <para>Callled by CTOR</para>
        /// </summary>
        public void CreateDBIfNotExist()
        {
            if (string.IsNullOrWhiteSpace(this.PathToSQLLiteDBFile)) throw new InvalidOperationException("PeopleRepository: A valid path to a sqllite3 file must be specified");
            if (!File.Exists(this.PathToSQLLiteDBFile))
            {
                using (var db = new SQLiteConnection(this.PathToSQLLiteDBFile))
                {
                    db.CreateTable<Models.Person>();
                }
            }
        }

        /// <summary>
        /// Does a person exist (by id)
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>True if so</returns>
        public bool Exists(int id)
        {
            bool flag = false;
            using (var db = new SQLiteConnection(this.PathToSQLLiteDBFile))
            {
                try
                {
                    var x = db.ExecuteScalar<int>("Select Id from Person where Id = ?", id);
                    if (x != 0) flag = true;
                }
                catch
                {
                }
            }
            return flag;
        }

        /// <summary>
        /// Add/Update a person
        /// </summary>
        /// <param name="model">Person</param>
        /// <returns>PK Id</returns>
        public int AddUpdate(Models.Person model)
        {
            int id = model.Id;
            if (model == null) throw new ArgumentNullException("AddUpdate: Model can not be null");

            using (var db = new SQLiteConnection(this.PathToSQLLiteDBFile))
            {
                if (id > 0)
                {
                    var result = db.Update(model);
                }
                else
                {
                    id = db.Insert(model);
                }
            }
            return id;
        }

        /// <summary>
        /// Delete Person by Id
        /// </summary>
        /// <param name="id">Id</param>
        public void Delete(int id)
        {
            using (var db = new SQLiteConnection(this.PathToSQLLiteDBFile))
            {
                db.Delete<Models.Person>(id);
            }
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Person</returns>
        public Models.Person GetById(int id)
        {
            Models.Person model = null;
            using (var db = new SQLiteConnection(this.PathToSQLLiteDBFile))
            {
                try
                {
                    model = db.Get<Models.Person>(id);
                }
                catch {
                    model = null;
                }
            }
            return model;
        }

        /// <summary>
        /// Search: Last, First, Email with contains
        /// </summary>
        /// <param name="searchString">thing to search for</param>
        /// <returns>Search results</returns>
        public IEnumerable<Models.Person> Search(string searchString)
        {
            using (var db = new SQLiteConnection(this.PathToSQLLiteDBFile))
            {
                var sql = string.Format("Select * from Person where (NameLast like '%{0}%') or (NameLast like '%{1}%') or (EMail like '%{2}%')", searchString, searchString, searchString);
                var list = db.Query<Models.Person>(sql);
                return list;
            }
        }

    }
}
