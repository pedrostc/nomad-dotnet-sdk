using System;

namespace Nomad.DotNet.Model
{
    public class AllocFileInfo : ApiObject<AllocFileInfo>
    {
        public string Name { get; set; }
        public bool IsDir { get; set; }
        public long Size { get; set; }
        public string FileMode { get; set; }
        public DateTime ModTime { get; set; }
    }
}
