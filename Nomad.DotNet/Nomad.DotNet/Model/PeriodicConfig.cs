namespace Nomad.DotNet.Model
{
    public class PeriodicConfig
    {
        public bool Enabled { get; set; }
        public string Spec { get; set; }
        public string SpecType { get; set; }
        public bool ProhibitOverlap { get; set; }
        public string TimeZone { get; set; }
    }
}