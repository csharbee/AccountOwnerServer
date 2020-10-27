using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public abstract class BaseAPIResponseModel<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public T Data { get; set; }
    }
    public class BaseModel : BaseAPIResponseModel<object>
    {
    }
}
