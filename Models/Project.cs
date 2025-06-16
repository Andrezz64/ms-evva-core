namespace ms_evva_core.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? RepositoryUrl { get; set; }
        public string Branch { get; set; } = "main";
        public string TargetPath { get; set; } = string.Empty;
        public int HostId { get; set; }
        public string? HostName { get; set; }
        public int Responsable { get; set; }
        public int Status { get; set; } = 0;
        public DateTime LastSync { get; set; }
        public DateTime? LastClone { get; set; }
        public DateTime? LastPull { get; set; }
        public DateTime? LastPush { get; set; }
        public bool AutoAsync { get; set; } = false;
        public bool IsDockerEnabled { get; set; } = false;
        public int? DockerConfig { get; set; }
    }
} 