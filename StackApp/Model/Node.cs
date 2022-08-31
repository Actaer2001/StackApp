using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackApp.Model
{
    public class Node
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int StackId { get; set; }
        [MaxLength(50)]
        public string Value { get; set; }
        public int Index { get; set; }
    }
}
