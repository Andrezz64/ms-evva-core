using ms_evva_core.Base;
using ms_evva_core.Models;
using ms_evva_core.Repos.Interfaces;

namespace ms_evva_core.Repos.Classes;

public class DockerConfigRepository : GenericRepository<DockerConfig>, IDockerConfigRepository
{
    public DockerConfigRepository() : base("docker_config")
    {
    }
} 