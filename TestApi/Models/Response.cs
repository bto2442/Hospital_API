namespace TestApi.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public List<Patient> patients { get; set; } = new();
        public List<Staff> staffs { get; set; } = new();
        public List<Diagnosis> diagnoses { get; set; } = new();
        public List<Prescription> prescriptions { get; set; } = new();
    }
}
