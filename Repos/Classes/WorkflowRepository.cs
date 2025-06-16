using ms_evva_core.Base;
using ms_evva_core.Models;
using ms_evva_core.Repos.Interfaces;

namespace ms_evva_core.Repos.Classes;

public class WorkflowRepository : GenericRepository<Workflow>, IWorkflowRepository
{
    public WorkflowRepository() : base("workflows")
    {
    }
} 