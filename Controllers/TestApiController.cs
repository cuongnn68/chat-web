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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/testapi")]
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
        
        // [HttpGet]
        // public IEnumerable<User> GetAll() {
        //     return UserStoreTest.GetUser();
        // }

        // [HttpGet("{id}")]
        // public User Get(int id) {
        //     var user = UserStoreTest.GetUser()
        //                         // .Where(user => user.Id == id)
        //                         .FirstOrDefault(user => user.Id == id);
        //     user ??= new User {
        //         Id = 69,
        //         Username = "null"
        //     };
        //     return user;
        // }

        [HttpPut]
        public IActionResult CallToTest([FromBody] LoginModel model) {
            if(!ModelState.IsValid) return BadRequest(); 
            Console.WriteLine(1);
            dbContext.Add(new User{
                Username = "uname Test",
                Password = "1",
                DateOfBirth = new DateTime(1998, 8, 6),
            });
            Console.WriteLine(2);

            dbContext.SaveChanges();

            Console.WriteLine(3);

            var date = dbContext.Users.FirstOrDefault(user => user.Username == "unameTest").DateOfBirth;
            Console.WriteLine(date);
            Console.WriteLine(date.GetType());
            return Ok();
        }

        [HttpGet]
        public IActionResult GetMultiRes([FromQuery]int[] id) { //RM: get array of praram from uri
            var users = UserStoreTest.GetUser()
                                    .Where(user => id.Contains(user.Id))
                                    .ToArray();
            foreach(var user in users) {
                Console.WriteLine(user);
            }
            return Ok(users);
        }

        [HttpGet]
        [Route("jwt")]
        public IActionResult GetJWT([FromQuery] RequestJWTModel model) {
            var token = new JWTModel {
                Token = jwtService.CreateToken(model.Id, model.Username)
            };
            return Ok(token);
        }

        [HttpGet]
        [Route("validate-jwt")]
        public IActionResult ValidateJWT() {
            var token = HttpContext.Request.Headers["Test-Header"].FirstOrDefault()?.Split(" ").Last();
            return Ok(jwtService.ValidateTokent(dbContext, token)); 
        }
    } 

    public class RequestJWTModel {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}