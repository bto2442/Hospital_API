namespace TestApi.Models
{
    public class Diagnosis
    {
        public int? id { get; set; }
        public Patient? patient { get; set; }
        public string? cause { get; set; }
        public string? symptoms { get; set; }
    }
}
