
using kolos2.DTOs;
using kolos2.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolos2.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterGetController : ControllerBase
{
    private readonly IDataBaseService _dataBaseService;

    public CharacterGetController(IDataBaseService dataBaseService)
    {
        _dataBaseService = dataBaseService;
    }

    [HttpGet("{characterID}")]
    public async Task<IActionResult> GetCharacter(int characterID)
    {
        if (!await _dataBaseService.IfCharacterExist(characterID))
            return NotFound($"Brak danych o {characterID}");

        var character = await _dataBaseService.GetCharacter(characterID);
        
        var characterDto = new GetCharacterDTO
        {
            firstName = character.FirstName,
            lastName = character.LastName,
            currentWeight = character.CurrentWei,
            maxWeight = character.MaxWeight,
            backpackItems = character.Backpacks.Select(b => new CharacterItemDTO
            {
                itemName = b.Item.Name,
                itemWeight = b.Item.Weight,
                amount = b.Amount
            }).ToList(),
            
            titles = character.CharacterTitles.Select(ct => new TitleDTO
            {
                title = ct.Title.Name,
                aquiredAt = ct.AcquireAt
            }).ToList()
        };

        return Ok(characterDto);
    }
}
