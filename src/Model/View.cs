namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    public class View : Table, IDatabaseObject
    {
        public string Definition { get; set; }

        public new List<string[]> ToArrayList()
        {
            return base.ToArrayList();
        }
    }
}
