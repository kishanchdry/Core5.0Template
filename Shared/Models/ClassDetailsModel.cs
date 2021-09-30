using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public class ClassDetailsModel
    {
        public string Name { get; set; }
        public string Accessifier { get; set; }
        public Type Details { get; set; }
        public List<MethodDetailsModel> Methods { get; set; }
        public string Description { get; set; }
    }
}
