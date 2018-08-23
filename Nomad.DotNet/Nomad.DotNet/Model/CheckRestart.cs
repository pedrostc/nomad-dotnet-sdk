namespace Nomad.DotNet.Model
{
    public class CheckRestart : ApiObject<CheckRestart>
    {
        public int Limit { get; set; }
        public long Grace { get; set; }
        public bool IgnoreWarnings { get; set; }
    }
}