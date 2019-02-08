namespace Nomad.DotNet.Model
{
    public class AutopilotConfiguration : ApiObject<AutopilotConfiguration>
    {
        public bool? CleanupDeadServers { get; set; }
        public string LastContactThreshold { get; set; }
        public int? MaxTrailingLogs { get; set; }
        public string ServerStabilizationTime { get; set; }
        public bool? EnableRedundancyZones { get; set; }
        public bool? DisableUpgradeMigration { get; set; }
        public bool? EnableCustomUpgrades { get; set; }
        public int? CreateIndex { get; set; }
        public int? ModifyIndex { get; set; }
    }
}
