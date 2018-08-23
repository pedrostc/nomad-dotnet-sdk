namespace Nomad.DotNet.Model
{
    public class EphemeralDisk
    {
        public bool Sticky { get; set; }
        public bool Migrate { get; set; }
        public int SizeMb { get; set; }
    }
}