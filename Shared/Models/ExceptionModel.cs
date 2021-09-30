using Shared.Models;
using Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ExceptionModel : BaseModel
    {
        //public ExceptionEntity GetEntity()
        //{
        //    return new ExceptionEntity()
        //    {
        //        APISource = this.APISource,
        //        CreatedDate = this.CreatedDate ?? DateTime.UtcNow.GetLocal(),
        //        ExceptionMessage = this.ExceptionMessage,
        //        ExceptionType = this.ExceptionType,
        //        FunctionName = this.FunctionName,
        //        Id = this.Id,
        //        SourceFileName = this.SourceFileName,
        //        StackTrace = this.StackTrace,
        //        ModelValues = this.ModelValues,

        //        ModifiedDate = this.ModifiedDate ?? DateTime.UtcNow.GetLocal(),
        //        IsActive = this.IsActive,
        //        IsDeleted = this.IsDeleted

        //    };
        //}
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string APISource { get; set; }
        public string FunctionName { get; set; }
        public string SourceFileName { get; set; }
        public string ModelValues { get; set; }
    }
}