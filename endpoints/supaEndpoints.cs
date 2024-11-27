// [Table("cities")]
// class City : BaseModel
// {
//     [PrimaryKey("id", false)]
//     public int Id { get; set; }

//     [Column("name")]
//     public string Name { get; set; }

//     [Column("country_id")]
//     public int CountryId { get; set; }
// }

// var model = new City
// {
//   Name = "The Shire",
//   CountryId = 554
// };

// await supabase.From<City>().Insert(model);
