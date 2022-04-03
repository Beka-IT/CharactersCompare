

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private ICharacterService characterService;
        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }
        [HttpGet]
        public async Task<List<Character>> GetAllCharacters()
        {
            var characters = await characterService.GetAllCharactersAsync();
            return characters;
        }
        [HttpGet("{id}")]
        public async Task<Character> DeleteCharacter(int id)
        {
            var characters = await characterService.DeleteCharacterAsync(id);
            return characters;
        }
        [HttpPost]
        public async Task<ActionResult<Character>> AddCharacter(Character character)
        {
            if(character == null)
            {
                return BadRequest();
            }
            var addedCharacter = await characterService.AddCharacterAsync(character);
            return Ok();
        }

    }
}