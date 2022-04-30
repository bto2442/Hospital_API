namespace TestApi.Models
{
    public class Prescription
    {
        public int? id { get; set; }
        public Patient? patient { get; set; }
        public string? presc_name { get; set; }
        public int? quantity { get; set; }

    }
}
