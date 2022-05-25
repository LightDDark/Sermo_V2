using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Services
{

    public class UsersService
    {
        private readonly WebApplication1Context _context;

        public UsersService(WebApplication1Context context)
        {
            _context = context;
        }

        public async Task<bool> UpdateContact(User contact, string user)
        {
            User? u = await UserWithContacts(user);
            if (u == null || u.Contacts == null)
            {
                return false;
            }
            User? c = u.Contacts.Find(c => c.Id == contact.Id);
            if (c == null)
            {
                return false;
            }
            if (contact.Server != null)
            {
                c.Server = contact.Server;
            }
            if (contact.Name != null)
            {
                c.Name = contact.Name;
            }

            _context.Entry(u).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return true;
        }
        public async Task<User?> GetUser(string id)
        {
            if (_context.User == null)
            {
                return null;
            }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return null;
            }
            User retuser = new User() { Id = user.Id, Name = user.Name, Server = user.Server, Last = user.Last, Lastdate = user.Lastdate };

            return retuser;
        }

        public async Task<List<User>?> GetContacts(string currentName)
        {
            
            User? user = await UserWithContacts(currentName);

            if (user == null)
            {
                return null;
            }

            if (user.Contacts == null)
            {
                return null;
            }

            List<User> contacts = new List<User>();
            user.Contacts.ForEach(x =>
            {
                contacts.Add(new User() { Id = x.Id, Name = x.Name,  Server = x.Server, Last = x.Last, Lastdate = x.Lastdate});
            });

            return contacts;
        }

        public async Task<User?> GetContact(string id, string currentName)
        {

            User? user = await UserWithContacts(currentName);

            if (!(await ContactExists(id, currentName)))
            {
                return null;
            }


            User? x = user.Contacts.Find(x => x.Id == id);

            return new User() { Id = x.Id, Name = x.Name, Server = x.Server, Last = x.Last, Lastdate = x.Lastdate };
        }

        public async Task<List<Message>?> GetMessages(string id, string currentName)
        {

            Log? log = await GetLog(id, currentName);
            User? user = await _context.User.FindAsync(currentName);

            if (log == null || log.Messages == null || user == null)
            {
                return null;
            }
            List<Message> msgs = new List<Message>();
            log.Messages.ForEach(x =>
            {
                msgs.Add(new Message() { Content = x.Content, Created = x.Created, Id = x.Id, Sent = x.Author.Id == user.Id });
            });
            return msgs;
        }

        public async Task<bool> AddMessage(string id, string msg, string currentName)
        {
            User? user = await UserWithLogs(currentName);
            
            if (user == null || !(await ContactExists(id, currentName)))
            {
                return false;
            }
            Log? log = await GetLog(id, currentName);

            if (log == null)
            {
                return false;
            }
            if (log.Messages == null)
            {
                log.Messages = new List<Message>();
            }
            int nextId = 0;
            if (_context.Message != null && _context.Message.Any())
            {
                nextId = _context.Message.Max(x => x.Id) + 1;
            }
            DateTime date = DateTime.Now;
            log.Messages.Add(new Message() { Author = user, Content = msg,
                            Log = log, Created = date, Id = nextId});
            user.Lastdate = date.ToString("o");
            user.Last = msg;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return true;
        }

        public async Task<bool?> DeleteContact(string id, string currentName)
        {
            if (_context.User == null)
            {
                return null;
            }
            User? user = await UserWithContacts(currentName);
            if (user == null)
            {
                return null;
            }
            if (user.Contacts == null)
            {
                return false;
            }
            int numRemoved = user.Contacts.RemoveAll(x =>
            {
                return x.Id == id;
            });

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            if (numRemoved == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<Message?> GetMessage(int id, string userId)
        {
            if (_context.Message == null)
            {
                return null;
            }
            Message? msg = await _context.Message.Include(m => m.Author).FirstOrDefaultAsync(m => m.Id == id);
            if (msg == null || msg.Author.Id != userId)
            {
                return null;
            }
            return new Message() { Id = msg.Id, Content = msg.Content, Created = msg.Created, Sent = false };
        }

        public async Task<bool?> PutMessage(int id, string userId, string content)
        {
            if (_context.Message == null)
            {
                return null;
            }
            Message? msg = await _context.Message.Include(m => m.Author).Include(m => m.Log).FirstOrDefaultAsync(m => m.Id == id);
            if (msg == null || msg.Author.Id != userId)
            {
                return false;
            }
            msg.Content = content;
            Log l = msg.Log;
            User user = msg.Author;
            if (l.Messages.Last() == msg)
            {
                user.Last = msg.Content;
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(msg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return true;
        }

        public async Task<bool?> Transfer(string from, string to, string content)
        {
            if (_context.User == null)
            {
                return null;
            }
            return await AddMessage(to, content, from);
        }

        public async Task<bool?> Invite(string from, string to, string server, bool here)
        {
            if (_context.User == null)
            {
                return null;
            }
            return await AddContact(new User() { Id = from, Name = from, Server = server }, to);
        }

        public async Task<bool?> DeleteMessage(int id, string userId)
        {
            if (_context.Message == null )
            {
                return null;
            }
            Message? msg = await _context.Message.Include(m => m.Author).Include(m => m.Log).FirstOrDefaultAsync(m => m.Id == id);
            if (msg == null || msg.Author.Id != userId)
            {
                return false;
            }
            Log l = msg.Log;
            User user = msg.Author;
            if (l == null || !l.Messages.Remove(msg))
            {
                return false;
            }
            Message m = l.Messages.Last();
            user.Lastdate = ((DateTime)m.Created).ToString("o");
            user.Last = m.Content;

            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(l).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return true;
        }
        // add contact for current
        public async Task<bool?> AddContact(User contact, string currentName)
        {
            if (_context.User == null)
            {
                return null;
            }
            User? user = await UserWithContacts(currentName);
            User?  toAdd = await _context.User.FindAsync(contact.Id);
            if (user == null || contact == null)
            {
                return false;
            }
            if (user.Contacts == null)
            {
                user.Contacts = new List<User>();
            }
            if (toAdd == null)
            {
                toAdd = contact;
            }
            user.Contacts.Add(toAdd);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return true;
        }


        public async Task<bool?> AddUser(User user)
        {
            if (_context.User == null)
            {
                return null;
            }
            _context.User.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool?> ValidateUser(User user)
        {
            if (_context.User == null)
            {
                return null;
            }

            if (user == null || !UserExists(user.Id))
            {
                return false;
            }
            var realUser = await _context.User.FindAsync(user.Id);
            if (realUser == null || realUser.Password != user.Password)
            {
                return false;
            }

            return true;
        }

        private bool UserExists(string id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async ValueTask<bool> ContactExists(string id, string currentName)
        {
            User? user = await UserWithContacts(currentName);
            if (user == null || user.Contacts == null)
            {
                return false;
            }
            return user.Contacts.Exists(x =>
            {
                return x.Id == id;
            });
        }


        private async ValueTask<User?> UserWithContacts(string currentName)
        {
            if (_context.User == null)
            {
                return null;
            }
            User? user = await _context.User.FindAsync(currentName);

            if (user == null)
            {
                return null;
            }

            return await _context.User.Include(x => x.Contacts).FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        private async ValueTask<User?> UserWithLogs(string currentName)
        {
            if (_context.User == null)
            {
                return null;
            }
            User? user = await _context.User.FindAsync(currentName);

            if (user == null)
            {
                return null;
            }
            return await _context.User.Include(x => x.Logs).FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        private async ValueTask<User?> UserWithAll(string currentName)
        {
            if (_context.User == null)
            {
                return null;
            }
            User? user = await _context.User.FindAsync(currentName);

            if (user == null)
            {
                return null;
            }
            return await _context.User.Include(x => x.Logs).Include(x => x.Contacts).FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        private async ValueTask<Log?> GetLog(string id, string currentName)
        {
            User? user = await UserWithAll(currentName);

            if (user == null || !(await ContactExists(id, currentName)))
            {
                return null;
            }
            string logId = Log.LogId(user.Id, id);
            User? contact = await _context.User.FindAsync(id);
            if (user.Logs == null)
            {
                user.Logs = new List<Log>();
            }
            Log? log = await _context.Log.Include(x => x.Messages).FirstOrDefaultAsync(l => l.stringId == logId);

            if (log == null || log.stringId == "")
            {
                log = new Log()
                {
                    stringId = logId,
                    Messages = new List<Message>(),
                    Users = new HashSet<User> { user, contact }
                };
                user.Logs.Add(log);
            }

            return log;
        }
    }
}