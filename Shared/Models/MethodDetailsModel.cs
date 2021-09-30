using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Shared.Models
{
    public class MethodDetailsModel
    {
        public string Name { get; set; }
        public Type ReturnType { get; set; }
        public MethodInfo Details { get; set; }
        public List<ParamDetailsModel> Params { get; set; }
        public string ParamsAsString
        {
            get
            {
                return Params?.Count > 0 ? Params?.Select(e => e.Name).Aggregate((x, y) => string.Format("{0}, {1}", x, y)) : string.Empty;
            }
        }
        public string Accessifier { get; set; }
        public string Description { get; set; }
    }

    public class ParamDetailsModel
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Desciption { get; set; }
        public string Accessifier { get; set; }
        public ParameterInfo Info { get; set; }
    }
}
