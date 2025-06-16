namespace ms_evva_core.Models
{
    public class Token
    {
        public int Id { get; set; }
        public int? HostId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsAtive { get; set; } = true;
        public DateTime? ExpiresIn { get; set; }
        public string Hash { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
} 