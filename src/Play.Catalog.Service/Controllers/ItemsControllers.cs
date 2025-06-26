using System.Security.Cryptography.X509Certificates;
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

    private static readonly List<ItemDto> Items = new List<ItemDto>([
        new ItemDto(Guid.NewGuid(), "Potion", "Description for item 1", 100, DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(), "Antidote", "Description for item 2", 200, DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(), "Sword", "Description for item 3", 300, DateTimeOffset.UtcNow)
    ]);

    [HttpGet]
    public IEnumerable<ItemDto> GetItems()
    {
        return Items;
    }

    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItemById(Guid id)
    {
        var item = Items.SingleOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
    {
        var newItem = new ItemDto(
            Guid.NewGuid(),
            createItemDto.Name,
            createItemDto.Description,
            createItemDto.Price,
            DateTimeOffset.UtcNow
        );

        Items.Add(newItem);

        return CreatedAtAction(nameof(GetItemById), new { id = newItem.Id }, newItem);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(Guid id)
    {
        if (!Items.Exists(i => i.Id == id))
        {
            return NotFound();
        }


        Items.RemoveAll(item => item.Id == id);

        IEnumerable<object> objects = from i in Items orderby i.Id select i;
        return NoContent();
    }
}