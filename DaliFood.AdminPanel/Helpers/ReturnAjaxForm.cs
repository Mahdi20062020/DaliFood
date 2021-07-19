using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.AdminPanel.Helpers
{
    public class ReturnAjaxForm
    {
        public string html { get; set; }
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
    }
    public enum ResultType
    {
        Success,
        Failure,
        Update,
        Redirect,
    }
}
