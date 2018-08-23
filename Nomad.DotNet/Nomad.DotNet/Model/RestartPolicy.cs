namespace Nomad.DotNet.Model
{
    public class RestartPolicy : ApiObject<RestartPolicy>
    {
        public long Interval { get; set; }
        public int Attempts { get; set; }
        public long Delay { get; set; }
        public string Mode { get; set; }
    }
}