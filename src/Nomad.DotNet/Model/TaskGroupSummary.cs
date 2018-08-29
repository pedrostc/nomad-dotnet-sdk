namespace Nomad.DotNet.Model
{
    public class TaskGroupSummary : ApiObject<TaskGroupSummary>
    {
        public int Queued { get; set; }
        public int Complete { get; set; }
        public int Failed { get; set; }
        public int Running { get; set; }
        public int Starting { get; set; }
        public int Lost { get; set; }
    }
}