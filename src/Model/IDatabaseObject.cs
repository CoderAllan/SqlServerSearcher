namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    interface IDatabaseObject
    {
        List<string[]> ToArrayList();
    }
}
