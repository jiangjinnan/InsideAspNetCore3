using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
public class HttpContext
{
    public HttpRequest Request { get; }
    public HttpResponse Response { get; }

    public HttpContext(IFeatureCollection features)
    {
        Request = new HttpRequest(features);
        Response = new HttpResponse(features);
    }
}
}
