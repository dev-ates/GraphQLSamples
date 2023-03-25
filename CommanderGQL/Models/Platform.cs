using System.ComponentModel.DataAnnotations;

namespace CommanderGQL.Models;

public class Platform
{
    [Key]
    public Int32 Id { get; set; }
    [Required]
    public String Name { get; set; }
    public String LicenseKey { get; set; }
    public ICollection<Command> Commands { get; set; } = new List<Command>();
}
