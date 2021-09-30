using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common
{
    /// <summary>
    ///  Generic response
    /// </summary>
    public class TResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponsePacket { get; set; }
    }
}
