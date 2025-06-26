using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItems()
    {
        _logger.LogInformation("Getting items...");

        await Task.Delay(10);

        // return Enumerable.Range(1, 10).Select(index => new ItemDto(
        //     Guid.NewGuid(),
        //     $"Item {index}",
        //     $"Description for item {index}",
        //     Random.Shared.Next(10, 1000),
        //     DateTimeOffset.UtcNow));

        return Enumerable.Range(1, 10).Select(i => new ItemDto
        (
            Guid.NewGuid(),
            $"Item {i}", $"Description for item {i}",
            Random.Shared.Next(200, 5000),
            DateTimeOffset.UtcNow
        ));
    }

    [HttpGet("{id:guid}")]
    public async Task<ItemDto> GetItem(Guid id)
    {
        _logger.LogInformation("Getting item with id {Id}...", id);

        await Task.Delay(10);

        return new ItemDto
        (
            id,
            $"Item {id}",
            $"Description for item {id}",
            Random.Shared.Next(200, 5000),
            DateTimeOffset.UtcNow
        );
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> createItem(CreateItemDto createItemDto)
    {
        _logger.LogInformation("Creating item...");

        await Task.Delay(10);

        var itemDto = new ItemDto
        (
            Guid.NewGuid(),
            createItemDto.Name,
            createItemDto.Description,
            createItemDto.Price,
            DateTimeOffset.UtcNow
        );

        return CreatedAtAction(nameof(GetItem), new { id = itemDto.Id }, itemDto);
    }
    

}