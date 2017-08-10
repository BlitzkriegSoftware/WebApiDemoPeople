using System;
using System.Collections.Generic;
using System.Web.Http;
using System.IO;
using System.Web.Http.Description;
using Demo.People.Repository;
using Demo.People.Repository.Models;
using System.Web;

namespace Demo.People.WebAPI.Controllers
{
    /// <summary>
    /// Controller: Person
    /// </summary>
    [RoutePrefix("people")]
    public class PersonController : ApiController
    {
        private string _dbpath = null;

        /// <summary>
        /// Database Path
        /// </summary>
        public string DbPath
        {
            get
            {
                if(string.IsNullOrEmpty(_dbpath))
                {
                    var path = "~/SqlData";
                    var directory = HttpContext.Current.Server.MapPath(path);
                    var filename = "People.db3";
                    _dbpath = Path.Combine(directory, filename);
                }
                return _dbpath;
            }
        }

        private PeopleRepository _repository = null;

        /// <summary>
        /// People Repository
        /// </summary>
        public PeopleRepository Repository
        {
            get
            {
                if(_repository == null)
                {
                    _repository = new PeopleRepository(this.DbPath);
                }
                return _repository;
            }
        }

        /// <summary>
        /// Do a search using contains on First, Last, Email
        /// </summary>
        /// <param name="searchString">Thing to search for</param>
        /// <returns>Search results</returns>
        [HttpGet]
        [Route("search")]
        [ResponseType(typeof(IEnumerable<Person>))]
        public IHttpActionResult Search(string searchString)
        {
            var list = Repository.Search(searchString);
            return Ok(list);
        }

        /// <summary>
        /// Add/Update a Person
        /// </summary>
        /// <param name="model">Person</param>
        /// <returns>PK</returns>
        [HttpPost]
        [Route("addupdate")]
        [ResponseType(typeof(int))]
        public IHttpActionResult AddUpdate(Person model)
        {
            var id = Repository.AddUpdate(model);
            return Ok(id);
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            Repository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Does person exist?
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns>True if so</returns>
        [HttpGet]
        [Route("exists")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Exists(int id)
        {
            var ok = Repository.Exists(id);
            return Ok(ok);
        }
        
        /// <summary>
        /// Get Person by Id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns>Model</returns>
        [HttpGet]
        [Route("byid")]
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetById(int id)
        {
            var model = Repository.GetById(id);
            return Ok(model);
        }
    
    }
}
