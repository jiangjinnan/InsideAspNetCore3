using Microsoft.AspNetCore.Mvc;

namespace helloworld
{
public class HelloController
{
    [HttpGet("/hello")]
    public string SayHello() => "Hello World";
}
}
