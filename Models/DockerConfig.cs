namespace ms_evva_core.Models
{
    public class DockerConfig
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HostPorts { get; set; } = string.Empty;
        public string? RunParams { get; set; }
        public string ContainerName { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
    }
} 