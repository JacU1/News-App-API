﻿using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News_App_API.Context;
using News_App_API.Handlers;
using News_App_API.Interfaces;
using News_App_API.Models;
using News_App_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly NewsAPIContext _appContext;
        public UserController(NewsAPIContext appContext)
        {
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                Name = userForRegistration.Name,
                LastName = userForRegistration.LastName,
                Email = userForRegistration.Email,
                UserTag = userForRegistration.UserTag,
            };

            var userAuth = new UserAuthDto
            {
                Id = user.Id,
                Email = user.Email,
                Password = userForRegistration.Password
            };

            if(user != null && userAuth != null)
            {
                _appContext.Add<UserDto>(user);
                _appContext.SaveChanges();

                _appContext.Add<UserAuthDto>(userAuth);
                _appContext.SaveChanges();

                return Ok(user);
            }

            return BadRequest();
        }

        // GET: api/<UserController>
        [HttpGet, Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}"), Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost, Authorize]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}"), Authorize]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}"), Authorize]
        public void Delete(int id)
        {
        }
    }
}
