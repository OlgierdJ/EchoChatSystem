using System.ComponentModel.DataAnnotations;

namespace Echo.Chat.API.Model;

public class CatalogBrand
{
    public int Id { get; set; }

    [Required]
    public string Brand { get; set; }
}
