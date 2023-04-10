namespace BuberBreakfast.Models
{
    public class BreakfastEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public DateTime LastModifiedDateTime { get; set; }

        public List<string>? Savory { get; set; }

        public List<string>? Sweet { get; set; }
    }
}
