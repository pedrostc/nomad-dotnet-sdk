namespace Nomad.DotNet.Model
{
    public class ResourceUsage : ApiObject<ResourceUsage>
    {
        public MemoryStats MemoryStats { get; set; }
        public CpuStats CpuStats { get; set; }
    }
}