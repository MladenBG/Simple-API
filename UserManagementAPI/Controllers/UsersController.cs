using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // In-memory static list to simulate a database for CRUD operations
        private static readonly List<User> _users = new List<User>();
        private static int _nextId = 1;

        // GET: api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return Ok(_users);
            }
            catch (Exception ex)
            {
                // Rethrowing to be caught by the ErrorHandlingMiddleware
                throw new Exception("Error retrieving users.", ex);
            }
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                
                if (user == null)
                {
                    return NotFound();
                }
                
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user with ID {id}.", ex);
            }
        }

        // POST: api/user
        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User newUser)
        {
            try
            {
                if (newUser == null)
                {
                    return BadRequest("User data cannot be null.");
                }

                newUser.Id = _nextId++;
                _users.Add(newUser);

                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding a new user.", ex);
            }
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                var existingUser = _users.FirstOrDefault(u => u.Id == id);
                
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.FirstName = updatedUser.FirstName;
                existingUser.LastName = updatedUser.LastName;
                existingUser.Email = updatedUser.Email;

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user with ID {id}.", ex);
            }
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                
                if (user == null)
                {
                    return NotFound();
                }

                _users.Remove(user);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user with ID {id}.", ex);
            }
        }
    }
}