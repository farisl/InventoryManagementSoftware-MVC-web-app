using System.Collections.Generic;

namespace InventoryManagementSoftware.Service.DTO
{
    public class DataTableOptions
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Search search { get; set; }
        public List<Column> columns { get; set; }
        public List<Order> order { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public bool regex { get; set; }

    }

    public class Order
    {
        public string dir { get; set; }
        public int column { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public ColumnSearch search { get; set; }
    }

    public class ColumnSearch
    {
        public string value { get; set; }
        public string regex { get; set; }
    }
}

