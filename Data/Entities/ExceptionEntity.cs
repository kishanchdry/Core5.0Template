using Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("Exceptions")]
    public partial class ExceptionEntity : BaseEntity
    {
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string APISource { get; set; }
        public string FunctionName { get; set; }
        public string SourceFileName { get; set; }
        public string ModelValues { get; set; }
    }
}