namespace Nomad.DotNet.Model
{
    public class UpdateStrategy : ApiObject<UpdateStrategy>
    {
        public long Stagger { get; set; }
        public int MaxParallel { get; set; }
        public string HealthCheck { get; set; }
        public long MinHealthyTime { get; set; }
        public long HealthyDeadLine { get; set; }
        public bool AutoRevert { get; set; }
        public int Canary { get; set; }
    }
}