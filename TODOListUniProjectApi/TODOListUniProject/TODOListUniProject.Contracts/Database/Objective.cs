using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODOListUniProject.Contracts.Database;

[Table("tbl_objectives", Schema = "public")]
public class Objective
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("is_completed")]
    public bool IsCompleted { get; set; }

    [Required]
    [Column("title")]
    [MaxLength(200)]
    public string Title { get; set; }
}