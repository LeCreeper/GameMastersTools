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
    public class CampaignTablesController : ApiController
    {
        private DnDatabaseContext db = new DnDatabaseContext();

        // GET: api/CampaignTables
        public IQueryable<CampaignTable> GetCampaignTable()
        {
            return db.CampaignTable;
        }

        // GET: api/CampaignTables/5
        [ResponseType(typeof(CampaignTable))]
        public IHttpActionResult GetCampaignTable(int id)
        {
            CampaignTable campaignTable = db.CampaignTable.Find(id);
            if (campaignTable == null)
            {
                return NotFound();
            }

            return Ok(campaignTable);
        }

        // PUT: api/CampaignTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCampaignTable(int id, CampaignTable campaignTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != campaignTable.CampaignId)
            {
                return BadRequest();
            }

            db.Entry(campaignTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignTableExists(id))
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

        // POST: api/CampaignTables
        [ResponseType(typeof(CampaignTable))]
        public IHttpActionResult PostCampaignTable(CampaignTable campaignTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CampaignTable.Add(campaignTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = campaignTable.CampaignId }, campaignTable);
        }

        // DELETE: api/CampaignTables/5
        [ResponseType(typeof(CampaignTable))]
        public IHttpActionResult DeleteCampaignTable(int id)
        {
            CampaignTable campaignTable = db.CampaignTable.Find(id);
            if (campaignTable == null)
            {
                return NotFound();
            }

            db.CampaignTable.Remove(campaignTable);
            db.SaveChanges();

            return Ok(campaignTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CampaignTableExists(int id)
        {
            return db.CampaignTable.Count(e => e.CampaignId == id) > 0;
        }
    }
}