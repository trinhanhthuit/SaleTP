using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sale.Data;

namespace WebApplication1.Utils
{
    public interface ISession
    {
        void ClearSession();
        string Language { get; set; }
        bool IsLogin { get; set; }
        User User { get; set; }

    }
}