using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearningASPdotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // cái controller là cái tên class cụ thể trước Controller
    public class CharacterController : ControllerBase
    {
        // cái trong dấu { ... } là 2 cái đối tướng mới khởi tạo trong list á
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character {Id = 1, Name = "Sam"}
        };
        [HttpGet]
        public ActionResult<List<Character>> Get() {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id) {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }
        
        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return Ok(characters);
        }
    }
}