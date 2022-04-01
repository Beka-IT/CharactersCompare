using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAllCharactersAsync();
        Task<Character> AddCharacterAsync(Character character);
        Task<Character> DeleteCharacterAsync(int id);
        Task<List<Character>> GetOrderedListOfCharactersAsync();
        Task<List<Character>> GetRandomTwoCharactersAsync();
        Task<Character> UpdatedRatingOfCharactersAsync(int id1, int id2);
        Task<List<Character>> SearchAndGetCharactersAsync(string name);
    }
}