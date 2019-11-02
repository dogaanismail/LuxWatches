using MvcApplication1.Entities.Messages;
using System.Collections.Generic;

namespace MvcApplication1.Business.BusinessResult
{
    public class BusinessResults<T> where T:class
    {
        public List<ErrorMessageObj> Errors { get; set; }
        public T result { get; set; }  // T has to be a class. Such as, Users.

        public BusinessResults()
        {
            Errors = new List<ErrorMessageObj>();
        }

        public void AddError(ErrorMessageCode code, string messsage)
        {
            Errors.Add(new ErrorMessageObj() { Code = code, Message = messsage });
        }
    }
}
