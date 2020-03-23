using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public delegate Task RequestDelegate(HttpContext httpContext);
}
