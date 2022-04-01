

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Services
{
    public class CharacterService : ICharacterService
    {
        private AppDbContext db;
        static List<Character> allCharacters;
        static int counter = 0;
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
        public async Task<Character> DeleteCharacterAsync(int id)
        {
            var hero = await db.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if(hero == null)
            {
                return null;
            }
            db.Characters.Remove(hero);
            await db.SaveChangesAsync();
            return hero;
        }
        public async Task<List<Character>> SearchAndGetCharactersAsync(string name)
        {
            var heroes = await db.Characters.ToListAsync();
            var searchedHeroes = heroes.Where(c => c.Fullname.Contains(name)).ToList();
            return searchedHeroes;
        }
        public async Task<List<Character>> GetOrderedListOfCharactersAsync()
        {
            var characters = await db.Characters.OrderByDescending(c=> c.Rating).ToListAsync();
            return characters;
        }
        public async Task<Character> UpdatedRatingOfCharactersAsync(int id1, int id2)
        {
            
            if(counter >= 12)
            {
                allCharacters =  db.Characters.ToList();
                counter = 0;
            }
            var winner = await db.Characters.FirstOrDefaultAsync(c=> c.Id == id1);
            var loser = await db.Characters.FirstOrDefaultAsync(c=> c.Id == id2);
            
            
            if(winner.Rating > loser.Rating)
            {
                winner.Rating += loser.Rating/winner.Rating*10;
                loser.Rating -= loser.Rating/winner.Rating*10;
            }
            else
            {
                winner.Rating += winner.Rating/loser.Rating*10;
                loser.Rating -= winner.Rating/loser.Rating*10;
            }
            db.Characters.UpdateRange(winner,loser);
            await db.SaveChangesAsync();
            return await GetRandomCharacterAsync();
        }

        public async Task<Character> GetRandomCharacterAsync()
        {
            Character character;
            Random indexGenerator = new Random();
            
            int indexOfFirstCharacter = indexGenerator.Next(allCharacters.Count);
            character = allCharacters[indexOfFirstCharacter];
            allCharacters.Remove(character);
            counter++;
            return character;    

        }
        public async Task<List<Character>> GetRandomTwoCharactersAsync()
        {
            counter = 0;
            allCharacters =  db.Characters.ToList();
            var characters = new List<Character>();
            Random indexGenerator = new Random();
            int indexOfFirstCharacter = indexGenerator.Next(allCharacters.Count);
            characters.Add(allCharacters[indexOfFirstCharacter]);
            allCharacters.Remove(characters[0]);
            int indexOfSecondCharacter = indexGenerator.Next(allCharacters.Count);   
            characters.Add(allCharacters[indexOfSecondCharacter]);
            allCharacters.Remove(characters[1]);
            counter++;
            return characters; 
                
        }
    }
}