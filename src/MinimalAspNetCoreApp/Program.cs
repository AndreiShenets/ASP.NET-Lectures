using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class Program
{
    static void Main() => WebHost.Start(c => c.Response.WriteAsync("Hello World!")).WaitForShutdown();
}

/*

Short:

namespace Microsoft.AspNetCore
{
    using Hosting;
    using Http;
    class P
    {
        static void Main() => WebHost.Start(c => c.Response.WriteAsync("Hello World!")).WaitForShutdown();
    }
}

Even shorter (160 characters long):

namespace Microsoft.AspNetCore{using Hosting;using Http;class P{static void Main()=>WebHost.Start(c=>c.Response.WriteAsync("Hello World!")).WaitForShutdown();}}

 */
