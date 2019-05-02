using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GMToolsWeb.Models;

namespace GMToolsWeb.Controllers
{
    public class UserTablesController : ApiController
    {
        private DnDatabaseContext db = new DnDatabaseContext();

        // GET: api/UserTables
        public IQueryable<UserTable> GetUserTable()
        {
            return db.UserTable;
        }

        // GET: api/UserTables/5
        [ResponseType(typeof(UserTable))]
        public IHttpActionResult GetUserTable(int id)
        {
            UserTable userTable = db.UserTable.Find(id);
            if (userTable == null)
            {
                return NotFound();
            }

            return Ok(userTable);
        }

        // PUT: api/UserTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserTable(int id, UserTable userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTable.UserId)
            {
                return BadRequest();
            }

            db.Entry(userTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserTables
        [ResponseType(typeof(UserTable))]
        public IHttpActionResult PostUserTable(UserTable userTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTable.Add(userTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userTable.UserId }, userTable);
        }

        // DELETE: api/UserTables/5
        [ResponseType(typeof(UserTable))]
        public IHttpActionResult DeleteUserTable(int id)
        {
            UserTable userTable = db.UserTable.Find(id);
            if (userTable == null)
            {
                return NotFound();
            }

            db.UserTable.Remove(userTable);
            db.SaveChanges();

            return Ok(userTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTableExists(int id)
        {
            return db.UserTable.Count(e => e.UserId == id) > 0;
        }
    }
}