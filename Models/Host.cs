namespace ms_evva_core.Models
{
    public class Host
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string OperatingSystem { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string Architecture { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int Port { get; set; } = 5643;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
} 