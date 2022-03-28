

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Services
{
    public class CharacterService : ICharacterService
    {
        private AppDbContext db;
        public CharacterService(AppDbContext context)
        {
            db = context;
        }
        public async Task<List<Character>> GetAllCharactersAsync()
        {
            var characters = await db.Characters.ToListAsync();
            return characters;
        }
        public async Task<Character> AddCharacterAsync(Character character)
        {
            await db.Characters.AddAsync(character);
            await db.SaveChangesAsync();
            return character;

        }
    }
}