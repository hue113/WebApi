using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
  [Table("Photos")] // create table Photos & set up relationship bw AppUser-Photo
  public class Photo
  {
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }

    // Need to add these 2 relationship properties to define the relationship between Photo-AppUser
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }
  }
}