namespace SurvivorApi.Entities
{
    public class Competitor : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; } // Foreign key
        public Category Category { get; set; } // Navigasyon özelliği
    }
}
