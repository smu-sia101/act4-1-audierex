using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace PetNameGenerated.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetGenerator : ControllerBase
    {
      
        private string[] dog = new string[] { "Cooper", "Canolord", "Doggy", "Ryan", "Rex" };
        private string[] cat = new string[] { "Kiffy", "Mittens", "PussyCat", "Simba", "Tiger" };
        private string[] bird = new string[] { "Birdy", "Junior", "Chirpy", "Raven", "Sunny" };

        [HttpPost("generate")]
        public IActionResult GeneratePetName([FromBody] PetRequest request)
        {
           
            if (string.IsNullOrEmpty(request.AnimalType))
            {
                return BadRequest(new { error = "The 'animalType' field is required." });
            }
            string[] PetNames;
            switch (request.AnimalType.ToLower())
            {
                case "dog":
                    PetNames = dog;
                    break;
                case "cat":
                    PetNames = cat;
                    break;
                case "bird":
                    PetNames = bird;
                    break;
                default:
                    return BadRequest(new { error = "Invalid animal type. Allowed values: dog, cat, bird." });
            }

          
            if (request.TwoPart.HasValue && !(request.TwoPart.Value is bool))
            {
                return BadRequest(new { error = "The 'twoPart' field must be a boolean (true or false)." });
            }

         
            Random rnd = new Random();
            string petAnimalName;

            if (request.TwoPart == true)
            {
               
                string name1 = PetNames[rnd.Next(PetNames.Length)];
                string name2 = PetNames[rnd.Next(PetNames.Length)];
                petAnimalName = $"{name1} {name2}";
            }
            else
            {
                petAnimalName = PetNames[rnd.Next(PetNames.Length)];
            }

           
            return Ok(new { name = petAnimalName });
        }
    }

    
    public class PetRequest
    {
        public string AnimalType { get; set; }
        public bool? TwoPart { get; set; } 
    }
}
