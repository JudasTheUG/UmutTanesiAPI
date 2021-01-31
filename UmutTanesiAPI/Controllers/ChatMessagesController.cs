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
    public class ChatMessagesController : ApiController
    {
        private UmutTanesiEntities db = new UmutTanesiEntities();

        public Chat messageFinder(Message mess)
        {
            Chat tempChat = new Chat();

            foreach (var item in db.ChatMessages)
            {
                if (item.senderId==mess.sender && item.reciverId==mess.reciver && item.currentMessage==mess.message)
                {
                    tempChat.messageId = item.messageId;
                    tempChat.senderId = item.senderId;
                    tempChat.reciverId = item.reciverId;
                    tempChat.currentMessage = item.currentMessage;
                    tempChat.messageTime = item.messageTime;
                    tempChat.otherPerson = item.reciverId;
                    break;
                }
            }

            return tempChat;
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

        [ResponseType(typeof(List<Chat>))]
        [HttpGet]
        public List<Chat> Chatters(int id, int secondId)
        {

            List<Chat> convos = new List<Chat>();

                ObjectResult<sp_getChat_Result> objectResults = db.sp_getChat(id, secondId);
                var holder = objectResults.ToList();

                for (int i = 0; i < holder.Count(); i++)
                {
                    Chat message = new Chat();

                    message.messageId = holder[i].messageId ;
                    message.senderId = holder[i].senderId;
                    message.reciverId = holder[i].reciverId;
                    message.currentMessage = holder[i].currentMessage;
                    message.messageTime = holder[i].messageTime;
                    message.otherPerson = secondId;

                    convos.Add(message);
                }

            return convos;
        }
        // GET: api/ChatMessages
        public IQueryable<ChatMessage> GetChatMessages()
        {
            return db.ChatMessages;
        }

        // GET: api/ChatMessages/5
        [ResponseType(typeof(ChatMessage))]
        [HttpGet]
        public IHttpActionResult GetChatMessage(int id)
        {
            ChatMessage chatMessage = db.ChatMessages.Find(id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            return Ok(chatMessage);
        }

        // PUT: api/ChatMessages/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutChatMessage(int id, ChatMessage chatMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chatMessage.messageId)
            {
                return BadRequest();
            }

            db.Entry(chatMessage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatMessageExists(id))
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

        // POST: api/ChatMessages
        [ResponseType(typeof(Chat))]
        [HttpPost]
        public Chat PostChatMessage(Message message)
        {
            db.sp_message(message.sender,message.reciver,message.message);
            db.SaveChanges();

            Chat chatMessage = new Chat();
            chatMessage = messageFinder(message);

            return chatMessage;
        }

        [ResponseType(typeof(Chat))]
        [HttpGet]
        public void deleteMessage(int id, int secondId)
        {
            db.sp_deleteChat(id, secondId);
            db.SaveChanges();

        }

        // DELETE: api/ChatMessages/5
        [ResponseType(typeof(ChatMessage))]
        public IHttpActionResult DeleteChatMessage(int id)
        {
            ChatMessage chatMessage = db.ChatMessages.Find(id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            db.ChatMessages.Remove(chatMessage);
            db.SaveChanges();

            return Ok(chatMessage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChatMessageExists(int id)
        {
            return db.ChatMessages.Count(e => e.messageId == id) > 0;
        }
    }
}