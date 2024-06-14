using kolos2.Data;
using kolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace kolos2.Services;

public class DataBaseService : IDataBaseService
{
    private readonly DatabaseContext _context;

    public DataBaseService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Characters?> GetCharacter(int characterId)
    {
        return await _context.characters
            .Include(ch => ch.CharacterTitles)
                .ThenInclude(ct => ct.Title)
            .Include(ch => ch.Backpacks)
                .ThenInclude(b => b.Item)
            .FirstOrDefaultAsync(ch => ch.Id == characterId);
    }

    public async Task<bool> IfCharacterExist(int characterId)
    {
        return await _context.characters.AnyAsync(c => c.Id == characterId);
    }

    public async Task<bool> IfItemExist(int itemId)
    {
        return await _context.items.AnyAsync(i => i.Id == itemId);
    }

    public async Task<int> CharacterCurrentWeight(int characterId)
    {
        var character = await _context.characters
            .Where(c => c.Id == characterId)
            .Select(c => new { c.MaxWeight, c.CurrentWei })
            .FirstAsync();

        return character.MaxWeight - character.CurrentWei;
    }

    public async Task<int> GetItemWeight(int itemId)
    {
        var item = await _context.items
            .Where(i => i.Id == itemId)
            .Select(i => i.Weight)
            .FirstAsync();

        return item;
    }
    
    public async Task UpdateWeight(int characterId, int weight)
    {
        var character = await _context.characters
            .Where(c => c.Id == characterId)
            .FirstAsync();

        character.CurrentWei += weight;
        _context.Update(character);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Backpacks>> GetBackpack(int characterId)
    {
        return await _context.backpacks
            .Where(b => b.CharacterId == characterId)
            .ToListAsync();
    }

    public async Task AddItems(int[] itemIds, int characterId)
    {
        foreach (var itemId in itemIds)
        {
            var backpack = await _context.backpacks
                .FirstOrDefaultAsync(e => e.ItemId == itemId && e.CharacterId == characterId);

            if (backpack != null)
            {
                await IncrementBackpackAmount(backpack);
            }
            else
            {
                await AddBackpackNewItem(characterId, itemId);
            }
        }
        await _context.SaveChangesAsync();
    }

    private async Task AddBackpackNewItem(int characterId, int itemId)
    {
        await _context.backpacks.AddAsync(new Backpacks
        {
            CharacterId = characterId,
            ItemId = itemId,
            Amount = 1
        });
    }

    private async Task IncrementBackpackAmount(Backpacks backpack)
    {
        backpack.Amount++;
        _context.Update(backpack);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBackpack(int characterId, int itemId)
    {
        var backpack = await _context.backpacks
            .Where(e => e.CharacterId == characterId && e.ItemId == itemId)
            .FirstOrDefaultAsync();

        if (backpack != null)
        {
            backpack.Amount++;
            _context.Entry(backpack).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}
