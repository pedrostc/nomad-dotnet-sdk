namespace Nomad.DotNet.Model
{
    public class LogConfig : ApiObject<LogConfig>
    {
        public int MaxFiles { get; set; }
        public int MaxFileSizeMb { get; set; }
    }
}