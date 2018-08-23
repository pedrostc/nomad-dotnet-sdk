namespace Nomad.DotNet.Model
{
    public class TaskEvent
    {
        public string Type { get; set; }
        public long Time { get; set; }
        public bool FailsTask { get; set; }
        public string RestartReason { get; set; }
        public string SetupError { get; set; }
        private string DriverError { get; set; }
        public string DriverMessage { get; set; }
        public int ExitCode { get; set; }
        public int Signal { get; set; }
        public string Message { get; set; }
        public string KillReason { get; set; }
        public long KillTimeout { get; set; }
        public string KillError { get; set; }
        public long StartDelay { get; set; }
        public string DownloadError { get; set; }
        public string ValidationError { get; set; }
        public long DiskLimit { get; set; }
        public long DiskSize { get; set; }
        public string FailedSibling { get; set; }
        public string VaultError { get; set; }
        public string TaskSignalReason { get; set; }
        public string TaskSignal { get; set; }
        public string GenericSource { get; set; }
    }
}