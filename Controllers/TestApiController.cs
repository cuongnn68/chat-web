using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DiscordRipoff.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TestApiController : Controller {
        
        [HttpGet]
        public IEnumerable<User> GetAll() {
            return UserStoreTest.GetUser();
        }

        [HttpGet("{id}")]
        public User Get(int id) {
            var user = UserStoreTest.GetUser()
                                // .Where(user => user.Id == id)
                                .FirstOrDefault(user => user.Id == id);
            user ??= new User {
                Id = 69,
                Username = "null"
            };
            return user;
        }
    } 
}