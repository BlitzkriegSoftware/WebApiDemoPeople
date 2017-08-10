using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Demo.People.Repository;
using System.Collections.Generic;

namespace Demo.People.Repository.Test
{
    [TestClass]
    public class T_People
    {
        #region "BoilerPlate"
        public TestContext TestContext { get; set; }
        #endregion

        #region "People Maker"

        public Models.Person Make()
        {
            var person = new Models.Person()
            {
                Birthday = Faker.Date.Birthday(),
                EMail = Faker.User.Email(),
                Id = 0,
                NameLast = Faker.Name.LastName()
            };

            var gender = Faker.Name.Gender();
            if (gender.ToLowerInvariant().StartsWith("f"))
            {
                person.NameFirst = Faker.Name.FemaleFirstName();
            }
            else
            {
                person.NameFirst = Faker.Name.MaleFirstName();
            }

            person.Company = person.EMail.Substring(person.EMail.IndexOf('@') + 1);

            person.EMail = string.Format("{0}.{1}@{2}", person.NameFirst, person.NameLast, person.Company);

            return person;
        }


        private string _dbname = null;
        public string DbName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_dbname))
                {
                    var directory = TestContext.TestRunResultsDirectory;
                    _dbname = "UnitTest.db3";
                    _dbname = Path.Combine(directory, _dbname);
                }
                return _dbname;
            }
        }

        #endregion


        [TestMethod]
        public void T_Create_1()
        {
            if (File.Exists(this.DbName)) File.Delete(this.DbName);
            var pr = new PeopleRepository(this.DbName);
            Assert.IsTrue(File.Exists(this.DbName));
        }


        [TestMethod]
        public void T_CRUD_1()
        {
            var pr = new PeopleRepository(this.DbName);

            TestContext.WriteLine("{0}", pr.PathToSQLLiteDBFile);

            var model = Make();

            TestContext.WriteLine("{0}", model.ToString());

            var id = pr.AddUpdate(model);
            Assert.AreNotEqual(0, id, "Should not be zero");

            Assert.IsTrue(pr.Exists(id), "Should exist");

            model.NameLast = "Smith";
            model.Id = id;

            var id2 = pr.AddUpdate(model);

            var model2 = pr.GetById(id);
            TestContext.WriteLine("{0}", model2.ToString());

            Assert.IsNotNull(model2, "Model should not be null");

            Assert.AreEqual(model2.Id, id, "ID");
            Assert.AreEqual(model.Birthday.Year, model2.Birthday.Year, "Birthday");
            Assert.AreEqual(model.Company, model2.Company, "Company");
            Assert.AreEqual(model.EMail, model2.EMail, "EMail");
            Assert.AreEqual(model.NameFirst, model2.NameFirst, "NameFirst");
            Assert.AreEqual(model.NameLast, model2.NameLast, "NameLast");

            pr.Delete(id);

            var model3 = pr.GetById(id);

            Assert.IsNull(model3,"Should be null post delete");
        }

        [TestMethod]
        public void T_Collection_1()
        {
            var pr = new PeopleRepository(this.DbName);

            var model = Make();
            model.NameLast = "Ede";
            pr.AddUpdate(model);

            pr.AddUpdate(Make());
            pr.AddUpdate(Make());
            pr.AddUpdate(Make());
            pr.AddUpdate(Make());
            pr.AddUpdate(Make());
            pr.AddUpdate(Make());

            var result =  pr.Search("e");
            var list = new List<Models.Person>();
            list.AddRange(result);

            Assert.IsTrue((list.Count > 0), "Should have at least one result");
        }

    }
}
