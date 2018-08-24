namespace Nomad.DotNet.Model
{
    public class QuotaLimit : ApiObject<QuotaLimit>
    {
        public string Region { get; set; }
        public Resources RegionLimit { get; set; }
        public byte[] Hash { get; set; }
    }
}
