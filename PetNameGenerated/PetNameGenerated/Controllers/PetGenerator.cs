using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PetNameGenerated.Controllers
{
    [ApiController]
    [Route(" [controller]")]
    public class PetGenerator : ControllerBase
    {
        private string[] dog = new string[] { "Cooper", "Canolord", "Doggy", "Ryan", "Rex" };
        private string[] cat = new string[] { "Kiffy", "Mittens", "PussyCat", "Simba", "Tiger" };
        private string[] bird = new string[] { "Birdy", "Junior", "Chirpy", "Raven", "Sunny" };

        [HttpGet("/generate")]
        public IActionResult GeneratePetName([FromBody] PetRequest request)
        {

            if (string.IsNullOrEmpty(request.AnimalType))
            {
                return BadRequest(new { error = "animalType is required" });

            }
            string[] selectedNames = request.AnimalType.ToLower() switch
            {"dog" => dog, "cat" => cat, "bird" => bird,_ => null};

            if (selectedNames == null)
            {
                return BadRequest(new { error = "Invalid animalType. Use 'dog', 'cat', or 'bird'." });
            }

           
            Random rnd = new Random();
            string petAnimalName;

            if (request.TwoPart == true)
            {
                
                string name1 = selectedNames[rnd.Next(selectedNames.Length)];
                string name2 = selectedNames[rnd.Next(selectedNames.Length)];
                petAnimalName = $"{name1} {name2}";
            }
            else
            {
                
                petAnimalName = selectedNames[rnd.Next(selectedNames.Length)];
            }

            return Ok(new { petName = petAnimalName });
        }
    }

    





































    public class PetRequest
    {
        public string AnimalType { get; set; }
        public bool? TwoPart { get; set; } 
    }
}
    
