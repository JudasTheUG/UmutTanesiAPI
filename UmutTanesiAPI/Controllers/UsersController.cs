using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UmutTanesiAPI.Models;

namespace UmutTanesiAPI.Controllers
{
    public class UsersController : ApiController
    {
        private UmutTanesiEntities db = new UmutTanesiEntities();
        Mail mail = new Mail();

        public string newPass()
        {
            string temp = "";
            Random rastgele = new Random();
            int sayi = rastgele.Next(100000, 999999);
            temp = "UT" + sayi.ToString();
            return temp;
        }
        public int getUserId(string email)
        {
            foreach (var user in db.Users1)
            {
                if (user.email==email)
                {
                    return user.userId;
                }
            }

            return -1;
        }

        [ResponseType(typeof(User))]
        [HttpGet]
        public User getUserInfo(int id)
        {
            User tempUser = new User();
            foreach (var user in db.Users1)
            {
                if (user.userId==id)
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


        [ResponseType(typeof(List<Users>))]
        [HttpGet]
        public List<User> ChattingPeople(int id)
        {

            List<User> people = new List<User>();

            ObjectResult<int?> temp = db.sp_chatters(id);

            var peopleOnChat = temp.ToList();

            foreach (var item in peopleOnChat)
            {

                User users = new User();

                users = getUserInfo(Convert.ToInt32(item));          

                people.Add(users);
            }

            return people;
        }


        // GET: api/Users
        public IQueryable<Users> GetUsers1()
        {
            return db.Users1;
        }

        //// GET: api/Users/5
        //[ResponseType(typeof(Users))]
        //public IHttpActionResult GetUsers(int id)
        //{
        //    Users user = db.Users1.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}

        // GET: api/Users/5
        [ResponseType(typeof(bool))]
        [HttpGet]
        public bool Login(string email, string password)
        {
            foreach (var user in db.Users1)
            {
                if (user.email == email && user.userPassword == password)
                {
                    return true;
                }
            }

            return false;
        }

        [ResponseType(typeof(bool))]
        [HttpGet]
        public bool forgotPassword(string email)
        {
            Users tmpUser = new Users();

            string tempPass = "";
            foreach (var user in db.Users1)
            {
                if (user.email == email)
                {
                    tmpUser = user;
                    break;
                }
                else
                    return false;
            }

            tempPass = newPass();
            tmpUser.userPassword = tempPass;
            UpdateUser(tmpUser.userId, tmpUser);
            mail.sendMail(email, tempPass);

            return true;
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.userId)
            {
                return BadRequest();
            }

            db.Entry(users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(Users))]
        [HttpPost]
        public IHttpActionResult Register(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users1.Add(users);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = users.userId }, users);
        }

        [ResponseType(typeof(int))]
        [HttpPost]
        public int UpdateUserPost(Users users)
        {
            UpdateUser(users.userId, users);

            return 1;
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(Users))]
        [HttpDelete]
        public IHttpActionResult DeleteUsers(int id)
        {
            Users users = db.Users1.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            db.Users1.Remove(users);
            db.SaveChanges();

            return Ok(users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(int id)
        {
            return db.Users1.Count(e => e.userId == id) > 0;
        }
    }
}