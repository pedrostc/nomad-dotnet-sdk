namespace Nomad.DotNet.Model
{
    public class Port : ApiObject<Port>
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }
}