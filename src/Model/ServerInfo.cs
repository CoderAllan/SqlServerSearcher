namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class ServerInfo : IDatabaseObject
    {
        public DateTime StartTime { get; set; }
        public long PhysicalMemory { get; set; }
        public long AvailablePhysicalMemory { get; set; }
        public int CPUCount { get; set; }

        public List<string[]> ToArrayList()
        {
            var result = new List<string[]>
            {
                new[] { "Start time:", StartTime.ToString(CultureInfo.CurrentCulture) },
                new[] { "CPU count:", CPUCount.ToString(CultureInfo.InvariantCulture) },
                new[] { "Physical memory:", PhysicalMemory.ToString(CultureInfo.InvariantCulture) },
                new[] { "Available memory:", AvailablePhysicalMemory.ToString(CultureInfo.InvariantCulture) },
            };
            return result;
        }
    }
}
