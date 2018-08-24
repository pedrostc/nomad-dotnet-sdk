namespace Nomad.DotNet.Model
{
    public class RaftServer : ApiObject<RaftServer>
    {
        public string Id { get; set; }
        public string Node { get; set; }
        public string Address { get; set; }
        public bool Leader { get; set; }
        public bool Voter { get; set; }
    }
}