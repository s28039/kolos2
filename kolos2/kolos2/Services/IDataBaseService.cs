using kolos2.DTOs;
using kolos2.Models;

namespace kolos2.Services;

public interface IDataBaseService
{
    Task<Characters> GetCharacter(int characterId);
    Task<bool> IfCharacterExist(int characterId);
    Task<bool> IfItemExist(int itemID);
    Task<int> CharacterCurrentWeight(int characterId);
    Task<int> GetItemWeight(int itemID);
    Task<ICollection<Backpacks>> GetBackpack(int characterId);
    Task AddItems(int[] itemIds, int characterId);
    Task UpdateWeight(int characterId, int weight);
}