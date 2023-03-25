namespace CommanderGQL.Models;

using System.ComponentModel.DataAnnotations;

public class Command
{
    [Key]
    public Int32 Id { get; set; }
    [Required]
    public String HowTo { get; set; }
    [Required]
    public String CommandLine { get; set; }
    [Required]
    public Int32 PlatformId { get; set; }
    public Platform Platform { get; set; }
}
