using System;

namespace FilterPage
{
    public class Filter
    {
        public string Field { get; set; }
        public FilterOperator Operator { get; set; }
        public object Value { get; set; }
    }

    public class Filter<T> where T : new()
    {
        
    }


    public class Filters
    {
        public FilterLogic Logic { get; set; }
        public Filter Filter { get; set; }
    }
}
