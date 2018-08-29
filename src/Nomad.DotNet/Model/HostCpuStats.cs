namespace Nomad.DotNet.Model
{
    public class HostCpuStats : ApiObject<HostCpuStats>
    {
        public string Cpu { get; set; }
        public double User { get; set; }
        public double System { get; set; }
        public double Idle { get; set; }
    }
}
