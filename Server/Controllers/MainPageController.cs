

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainPageController : ControllerBase
    {
        ICharacterService characterService;
        public MainPageController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }
        [HttpGet]
        public async Task<List<Character>> GetAllCharacters()
        {
            var characters = await characterService.GetOrderedListOfCharactersAsync();
            return characters;
        }   
        [HttpGet("vs")]
        public async Task<List<Character>> GetRandomTwoCharacters()
        {
            var characters = await characterService.GetRandomTwoCharactersAsync();
            return characters;
        }    
        [HttpPost]
        public async Task<Character> UpdatedRatingOfCharacters(int[] idOfCharacters)
        {
            var characters = await 
                characterService.UpdatedRatingOfCharactersAsync(idOfCharacters[0],idOfCharacters[1]);
            return characters;
        }
        [HttpPost]
        [Route("search/")]
        public async Task<List<Character>> SearchAndGetCharacters(SearchForm form)
        {   
             var name = form.Name;
            if(name != null)
            {
                name = char.ToUpper(name[0]) + name.Substring(1);
                Console.WriteLine(name);
            }
            var heroes = await characterService.SearchAndGetCharactersAsync(name);
            return heroes;
        }

    }
}