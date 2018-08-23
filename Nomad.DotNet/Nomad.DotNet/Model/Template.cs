namespace Nomad.DotNet.Model
{
    public class Template : ApiObject<Template>
    {
        public string SourcePath { get; set; }
        public string DestPath { get; set; }
        public string EmbeddedTmpl { get; set; }
        public string ChangeMode { get; set; }
        public string ChangeSignal { get; set; }
        public long Splay { get; set; }
        public string Perms { get; set; }
        public string LeftDelim { get; set; }
        public string RightDelim { get; set; }
        public bool Envvars { get; set; }
        public long VaultGrace { get; set; }
    }
}