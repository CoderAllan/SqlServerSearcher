namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class View : Table, IDatabaseObject
    {
        public string Definition { get; set; }

        public new List<string[]> ToArrayList()
        {
            return base.ToArrayList().Where(p => !p[0].Equals("Row count:")).ToList();
        }
    }
}
