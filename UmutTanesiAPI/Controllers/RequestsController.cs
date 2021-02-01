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
using UmutTanesiAPI.Models;

namespace UmutTanesiAPI.Controllers
{
    public class RequestsController : ApiController
    {
        private UmutTanesiEntities db = new UmutTanesiEntities();


        public string reqTypeFinder(int id)
        {
            string temp = "";

            if (id == 1)
                temp = "Barınma";
            else if (id == 2)
                temp = "Yiyecek-İçecek";
            else if (id == 3)
                temp = "Giyecek";
            else if (id == 4)
                temp = "Ulaşım";
            else if (id == 5)
                temp = "Sağlık";
            else
                temp = "hatalı giriş";

            return temp;
        }
        public User getUserInfo(int id)
        {
            User tempUser = new User();
            foreach (var user in db.Users1)
            {
                if (user.userId == id)
                {
                    tempUser.userId = user.userId;
                    tempUser.userName = user.userName;
                    tempUser.userSurname = user.userSurname;
                    tempUser.userPassword = user.userPassword;
                    tempUser.iban = user.iban;
                    tempUser.mobile = user.mobile;
                    tempUser.email = user.email;
                    tempUser.birthdate = user.birthdate;
                    break;
                }
            }
            return tempUser;
        }
        // GET: api/Requests
        public IQueryable<Request> GetRequests()
        {
            return db.Requests;
        }

        // GET: api/Requests/5
        [ResponseType(typeof(Request))]
        [HttpGet]
        public IHttpActionResult GetRequest(int id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        [ResponseType(typeof(List<reRequest>))]
        [HttpGet]
        public List<reRequest> MyRequests(int id)
        {
            List<reRequest> myRequestList = new List<reRequest>();

            foreach (var item in db.Requests)
            {
                if (item.userId == id)
                {
                    reRequest req = new reRequest();

                    req.requestId = item.requestId ;
                    req.reqTypeId = item.reqTypeId ;
                    req.reqType = reqTypeFinder(item.reqTypeId);
                    req.helpId = item.helpId ;
                    req.userId = item.userId;
                    req.personCount = item.personCount;
                    req.moneyStatus = item.moneyStatus;
                    req.ibanStatus = item.ibanStatus;
                    req.mobileStatus = item.mobileStatus;
                    req.beginDate = item.beginDate;
                    req.endDate = item.endDate;

                    myRequestList.Add(req);
                }
            }


            return myRequestList;
        }

        [ResponseType(typeof(List<matchRequest>))]
        [HttpGet]
        public List<matchRequest> MatchingRequests(int id)
        {
            List<matchRequest> matchRequestList = new List<matchRequest>();
            List<reRequest> myRequestList = new List<reRequest>();
            myRequestList = MyRequests(id);

            foreach(var member in myRequestList)
            {
                foreach (var item in db.Requests)
                {
                    if (member.helpId == 1)
                    {
                        if (item.userId != member.userId && item.reqTypeId == member.reqTypeId &&
    item.helpId != member.helpId && item.personCount <= member.personCount &&
    item.beginDate >= member.beginDate && item.endDate <= member.endDate)
                        {
                            if (member.helpId == 2 && member.moneyStatus == true && item.moneyStatus == true)
                            {
                                matchRequest req = new matchRequest();
                                User user = new User();
                                user = getUserInfo(item.userId);

                                req.requestId = item.requestId;
                                req.reqTypeId = item.reqTypeId;
                                req.reqType = reqTypeFinder(item.reqTypeId);
                                req.helpId = item.helpId;
                                req.userId = item.userId;
                                req.userName = user.userName;
                                req.userSurname = user.userSurname;
                                req.personCount = item.personCount;
                                req.moneyStatus = item.moneyStatus;
                                req.ibanStatus = item.ibanStatus;
                                req.mobileStatus = item.mobileStatus;
                                req.beginDate = item.beginDate;
                                req.endDate = item.endDate;

                                matchRequestList.Add(req);
                            }
                            else if (member.helpId == 2 && member.moneyStatus == true && item.moneyStatus == false)
                            {
                                continue;
                            }
                            else
                            {
                                matchRequest req = new matchRequest();

                                req.requestId = item.requestId;
                                req.reqTypeId = item.reqTypeId;
                                req.reqType = reqTypeFinder(item.reqTypeId);
                                req.helpId = item.helpId;
                                req.userId = item.userId;
                                req.personCount = item.personCount;
                                req.moneyStatus = item.moneyStatus;
                                req.ibanStatus = item.ibanStatus;
                                req.mobileStatus = item.mobileStatus;
                                req.beginDate = item.beginDate;
                                req.endDate = item.endDate;

                                matchRequestList.Add(req);

                            }
                        }
                    }
                    else
                    {
                        if (item.userId != member.userId && item.reqTypeId == member.reqTypeId &&
    item.helpId != member.helpId && item.personCount >= member.personCount &&
    item.beginDate <= member.beginDate && item.endDate >= member.endDate)
                        {
                            if (member.helpId == 2 && member.moneyStatus == true && item.moneyStatus == true)
                            {
                                matchRequest req = new matchRequest();
                                User user = new User();
                                user = getUserInfo(item.userId);

                                req.requestId = item.requestId;
                                req.reqTypeId = item.reqTypeId;
                                req.reqType = reqTypeFinder(item.reqTypeId);
                                req.helpId = item.helpId;
                                req.userId = item.userId;
                                req.userName = user.userName;
                                req.userSurname = user.userSurname;
                                req.personCount = item.personCount;
                                req.moneyStatus = item.moneyStatus;
                                req.ibanStatus = item.ibanStatus;
                                req.mobileStatus = item.mobileStatus;
                                req.beginDate = item.beginDate;
                                req.endDate = item.endDate;

                                matchRequestList.Add(req);
                            }
                            else if (member.helpId == 2 && member.moneyStatus == true && item.moneyStatus == false)
                            {
                                continue;
                            }
                            else
                            {
                                matchRequest req = new matchRequest();

                                req.requestId = item.requestId;
                                req.reqTypeId = item.reqTypeId;
                                req.reqType = reqTypeFinder(item.reqTypeId);
                                req.helpId = item.helpId;
                                req.userId = item.userId;
                                req.personCount = item.personCount;
                                req.moneyStatus = item.moneyStatus;
                                req.ibanStatus = item.ibanStatus;
                                req.mobileStatus = item.mobileStatus;
                                req.beginDate = item.beginDate;
                                req.endDate = item.endDate;

                                matchRequestList.Add(req);

                            }
                        }
                    }

                }
            }

            return matchRequestList;
        }

        // PUT: api/Requests/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.requestId)
            {
                return BadRequest();
            }

            db.Entry(request).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        [ResponseType(typeof(Request))]
        [HttpPost]
        public IHttpActionResult PostRequest(Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Requests.Add(request);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = request.requestId }, request);
        }

        // DELETE: api/Requests/5
        [ResponseType(typeof(Request))]
        [HttpDelete]
        public IHttpActionResult DeleteRequest(int id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            db.Requests.Remove(request);
            db.SaveChanges();

            return Ok(request);
        }

        [ResponseType(typeof(Request))]
        [HttpGet]
        public void GetDeleteRequest(int id)
        {
            DeleteRequest(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(int id)
        {
            return db.Requests.Count(e => e.requestId == id) > 0;
        }
    }
}