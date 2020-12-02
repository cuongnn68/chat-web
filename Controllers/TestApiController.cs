using System.Net.Security;
using System.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DiscordRipoff.Entities;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;
using DiscordRipoff.Data;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TestApiController : Controller {
        private JWTService jwtService;
        private AppDbContext dbContext;

        public TestApiController(
            JWTService jwtService,
            AppDbContext dbContext) 
        {
            this.jwtService = jwtService;
            this.dbContext = dbContext;
        }
        
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

        [HttpGet]
        [Route("jwt")]
        public string GetJWT([FromBody] JWTModel model)
        {
            return jwtService.CreateToken(model.Id, model.Username);
        }

        [HttpGet]
        [Route("validate-jwt")]
        public object ValidateJWT() {
            var token = HttpContext.Request.Headers["Test-Header"].FirstOrDefault()?.Split(" ").Last();
            return jwtService.ValidateTokent(dbContext, token); 
        }
    } 

    public class JWTModel {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}