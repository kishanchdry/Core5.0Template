using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface IExceptionLogginService// : IGenericService<ExceptionModel>
    {
        bool SaveException(Exception model);
        bool SaveException(ExceptionModel model);
    }
}
