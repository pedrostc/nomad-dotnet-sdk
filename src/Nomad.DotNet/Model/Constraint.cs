namespace Nomad.DotNet.Model
{
    public class Constraint : ApiObject<Constraint>
    {
        public string LTarget { get; set; }
        public string RTarget { get; set; }
        public string Operand { get; set; }
    }
}