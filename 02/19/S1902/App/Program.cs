using System;
using System.Security.Claims;
using System.Security.Principal;

namespace App
{
    class Program
    {
        static void Main()
        {
            var identity1 = new ClaimsIdentity(authenticationType: "Password");
            var identity2 = new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Name, "Foobar") });
            var identity3 = new GenericIdentity(name: "", type: "Password");
            var identity4 = new GenericIdentity(name: "Foobar");

            Console.WriteLine("{0,-20}{1,-20}{2,-10}{3,-10}", "Type", "AuthenticationType", "Name", "IsAuthenticated");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var identity in new IIdentity[] {identity1, identity2, identity3, identity4 })
            {
                string name = string.IsNullOrEmpty(identity.Name) ? "-" : identity.Name;
                string authenticationType = string.IsNullOrEmpty(identity.AuthenticationType)? "-" : identity.AuthenticationType;
                Console.WriteLine("{0,-20}{1,-20}{2,-10}{3,-10}", identity.GetType().Name,authenticationType, name, identity.IsAuthenticated);
            }
        }
    }
}
