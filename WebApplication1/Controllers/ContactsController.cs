﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly UsersService _service;

        public ContactsController(UsersService service)
        {
            _service = service;
        }

        // GET: api/Contacts
        // returns list of contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetContacts()
        {
            List<User>? users = await _service.GetContacts(HttpContext.User.Claims.First().Value);
            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // GET: api/Contacts/5
        // get contact details 
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetContact(string id)
        {
            User? contact = await _service.GetContact(id, HttpContext.User.Claims.First().Value);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // GET: api/Contacts/5/messages
        // get messages
        [HttpGet("{id}/messages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(string id)
        {
            List<Message>? messages = await _service.GetMessages(id, HttpContext.User.Claims.First().Value);

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

        // Pst: api/Contacts/5/messages
        // post message
        [HttpPost("{id}/messages")]
        public async Task<ActionResult<Message>> AddMessage(string id, [FromBody] Message msg)
        {
            bool messages = await _service.AddMessage(id, msg.Content, HttpContext.User.Claims.First().Value);

            if (messages == false)
            {
                return NotFound();
            }

            return Created(id + "messages", msg);
        }

        [HttpDelete("{id}/messages/{id2}")]
        public async Task<IActionResult> DeleteMessage(string id, int id2)
        {
            bool? res = await _service.DeleteMessage(id2, id);
            if (res == null || res == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}/messages/{id2}")]
        public async Task<ActionResult<Message>> GetMessage(string id, int id2)
        {
            Message? res = await _service.GetMessage(id2, id);
            if (res == null)
            {
                return NotFound();
            }

            return res;
        }

        [HttpPut("{id}/messages/{id2}")]
        public async Task<IActionResult> PutMessage(string id, int id2, Message msg)
        {
            if (msg == null)
            {
                return BadRequest();
            }
            bool? res = await _service.PutMessage(id2, id, msg.Content);
            if (res == null || res == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // update details of contact with the given id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(string id, User contact)
        {
            contact.Id = id;
            bool res = await _service.UpdateContact(contact, HttpContext.User.Claims.First().Value);
            if (res == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // add new contact to current user
        [HttpPost]
        public async Task<ActionResult<User>> PostContact(User contact)
        {
            if (contact.Server == null && contact.Name == null)
            {
                return ValidationProblem("Server field is requierd.\n Name field is requierd.");
            } else if (contact.Server == null)
            {
                return ValidationProblem("Server field is requierd.");
            } else if (contact.Name == null)
            {
                return ValidationProblem("Name field is requierd.");
            }
            if (contact.Server == HttpContext.Request.Host.Value)
            {
                User? c = await _service.GetUser(contact.Id);
                if (c == null)
                {
                    return BadRequest();
                }
            }

            bool? res = await _service.AddContact(contact, HttpContext.User.Claims.First().Value);
            if (res == null)
            {
                return Problem("Entity set 'WebApplication1Context.User'  is null.");
            }

            if (res == false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(GetContact), new { id = contact.Id });
        }

        // DELETE: api/Contacts/5
        // delete id from contacts
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {

            bool? res = await _service.DeleteContact(id, HttpContext.User.Claims.First().Value);
            if (res == null || res == false)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}