namespace TestApi.Models
{
    public class Patient
    {
        public int id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? phone_num { get; set; }
        public string? address { get; set; }
        public DateTime? admitted { get; set; }
        public bool? discharged { get; set; }
        public Staff? staff { get; set; }

    }
}
