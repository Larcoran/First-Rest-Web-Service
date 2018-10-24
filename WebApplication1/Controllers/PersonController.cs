using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Collections;

namespace WebApplication1.Controllers
{
    public class PersonController : ApiController
    {
        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns></returns>
        // GET: api/Person
        public ArrayList Get()
        {
            PersonPersistence pp = new PersonPersistence();
            return pp.getPersons();
        }

        /// <summary>
        /// Get specific person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Person/6
        public Person Get(long id)
        {
            PersonPersistence pp = new PersonPersistence();
            Person person = pp.getPerson(id);
            if(person == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return person;
        }

        /// <summary>
        /// Adds new person to DB
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person value)
        {
            PersonPersistence pp = new PersonPersistence();
            long id;
            id = pp.savePerson(value);
            value.ID = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("person/{0}", id));
            return response;
        }

        /// <summary>
        /// Changes person specified by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        // PUT: api/Person/5
        public HttpResponseMessage Put(long id, [FromBody]Person person)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;
            recordExisted = pp.updatePerson(id,person);
            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        /// <summary>
        /// Removes person with specified id from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Person/5
        public HttpResponseMessage Delete(long id)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;

            recordExisted = pp.deletePerson(id);

            HttpResponseMessage response;

            if(recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
