using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Wiki: https://github.com/praeclarum/sqlite-net/wiki

namespace Demo.People.Repository.Models
{
    /// <summary>
    /// Entity: Person
    /// </summary>
    public class Person
    {
        /// <summary>
        /// MemberId
        /// </summary>
        [PrimaryKey, Indexed, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Name: Last
        /// </summary>
        [Indexed]
        public string NameLast { get; set; }

        /// <summary>
        /// Name: First
        /// </summary>
        [Indexed]
        public string NameFirst { get; set; }

        /// <summary>
        /// E-Mail
        /// </summary>
        [Indexed]
        public string EMail { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime Birthday { get; set; }


        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}", this.Id, this.NameFirst, this.NameLast, this.EMail, this.Company, this.Birthday);
        }
    }

}
