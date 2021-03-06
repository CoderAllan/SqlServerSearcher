namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class ServerInfo : IDatabaseObject
    {
        public string Name { get; set; }
        public string ServerVersion { get; set; }
        public DateTime StartTime { get; set; }
        public long PhysicalMemory { get; set; }
        public long AvailablePhysicalMemory { get; set; }
        public int CPUCount { get; set; }

        public List<string[]> ToArrayList()
        {
            var result = new List<string[]>
            {
                new[] { "Server version:", ServerVersion },
                new[] { "Start time:", StartTime == DateTime.MinValue ? "N/A" : StartTime.ToString(CultureInfo.CurrentCulture) },
                new[] { "CPU count:", CPUCount == -1 ? "N/A" : CPUCount.ToString(CultureInfo.InvariantCulture) },
                new[] { "Physical memory:", PhysicalMemory == -1 ? "N/A" : PhysicalMemory.ToString(CultureInfo.InvariantCulture) + " Kb" },
                new[] { "Available memory:", AvailablePhysicalMemory == -1 ? "N/A" : AvailablePhysicalMemory.ToString(CultureInfo.InvariantCulture)  + " Kb"},
            };
            return result;
        }
    }
}
