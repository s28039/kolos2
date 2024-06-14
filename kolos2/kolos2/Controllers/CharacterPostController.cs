
using kolos2.DTOs;
using kolos2.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolos2.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterPostController : ControllerBase
{
    private readonly IDataBaseService _dataBaseService;

    public CharacterPostController(IDataBaseService dataBaseService)
    {
        _dataBaseService = dataBaseService;
    }
    
    
    [HttpPost("{characterID}/backpacks")]
    public async Task<IActionResult> PostItems(int characterID, int[] itemIds)
    {
        if (!await _dataBaseService.IfCharacterExist(characterID))
            return NotFound($"Brak danych o {characterID}");

        int sumWeightOfItems = 0;
        
        foreach (var itemId in itemIds)
        {
            if (!await _dataBaseService.IfItemExist(itemId))
                return NotFound($"Brak itemu o id {itemId}");
            sumWeightOfItems += await _dataBaseService.GetItemWeight(itemId);
        }

        int availableSpace = await _dataBaseService.CharacterCurrentWeight(characterID);

        if (sumWeightOfItems > availableSpace)
            return BadRequest($"Za duża waga itemow {sumWeightOfItems}");

        await _dataBaseService.AddItems(itemIds, characterID);
        await _dataBaseService.UpdateWeight(characterID, sumWeightOfItems);

        var backpackItems = await _dataBaseService.GetBackpack(characterID);
        
        var backpackItemsDto = backpackItems.Select(b => new BackpackItemDTO
        {
            amount = b.Amount,
            itemId = b.ItemId,
            characterId = b.CharacterId
        });

        return Ok(backpackItemsDto);
    }
}
