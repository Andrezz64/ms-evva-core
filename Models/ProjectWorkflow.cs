namespace ms_evva_core.Models
{
    public class ProjectWorkflow
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int WorkflowId { get; set; }
        public int ExecutionOrder { get; set; }
        public string? StageName { get; set; }
    }
} 