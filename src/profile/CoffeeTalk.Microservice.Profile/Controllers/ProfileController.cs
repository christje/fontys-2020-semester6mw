using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoffeeTalk.Microservice.Profile.Models.RequestBody;

using CoffeeTalk.Microservice.Profile.Data.Repository;
using CoffeeTalk.Microservice.Profile.Models.Entities;

namespace CoffeeTalk.Microservice.Profile.Controllers
{
    [ApiController]
    [Produces("applicaiton/json")]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepo;

        public ProfileController(IProfileRepository profileRepo)
        {
            _profileRepo = profileRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(string profileId)
        {
            var result = await _profileRepo.GetProfile(profileId);
            if (result == null) return NotFound("This profile does not exist");
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestBody updateProfileRequestBody)
        {
            string id = updateProfileRequestBody.Id;
            List<string> interests = updateProfileRequestBody.Interests;
            List<Project> previousProjects = updateProfileRequestBody.PreviousProjects;

            var result = await _profileRepo.UpdateProfile(id, interests, previousProjects);
            return Ok(result);
        }

        [HttpPost]
        public void CreateProfile([FromBody] CreateProfileRequestBody createProfileRequestBody)
        {
            string firstName = createProfileRequestBody.FirstName;
            string lastName = createProfileRequestBody.LastName;
            int age = createProfileRequestBody.Age;

            _profileRepo.CreateProfile(firstName, lastName, age);
        }
    }
}