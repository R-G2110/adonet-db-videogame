namespace adonet_db_videogame
{
    public class Videogame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string SoftwareHouseId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public Videogame(int id, string name, string overview, string releaseDate, string softwareHouseId, string createdAt, string updatedAt)
        {
            Id = id;
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
            SoftwareHouseId = softwareHouseId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
