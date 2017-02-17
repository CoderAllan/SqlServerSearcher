namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    public interface IDatabaseObject
    {
        string Name { get; set; }
        List<string[]> ToArrayList();
    }
}
