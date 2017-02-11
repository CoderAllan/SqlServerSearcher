namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    interface IDatabaseObject
    {
        string Name { get; set; }
        List<string[]> ToArrayList();
    }
}
