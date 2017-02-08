namespace SQLServerSearcher.Model
{
    using System.Collections.Generic;

    public class Function : ProcedureObject, IDatabaseObject
    {
        public new List<string[]> ToArrayList()
        {
            return base.ToArrayList();
        }
    }
}
