using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var template = @"weather/{city:regex(^0\d{{2,3}}$)=010}/{days:int:range(1,4)=4}/{detailed?}";
            var pattern = RoutePatternFactory.Parse(
                pattern: template,
                defaults: null,
                parameterPolicies: null,
                requiredValues: new { city = "010", days = 4 });

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder .Configure(app => app.Run(context => context.Response.WriteAsync(Format(pattern)))))
                .Build()
                .Run();
        }

        private static string Format(RoutePattern pattern)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"RawText:{pattern.RawText}");
            builder.AppendLine($"InboundPrecedence:{pattern.InboundPrecedence}");
            builder.AppendLine($"OutboundPrecedence:{pattern.OutboundPrecedence}");
            var segments = pattern.PathSegments;
            builder.AppendLine("Segments");
            foreach (var segment in segments)
            {
                foreach (var part in segment.Parts)
                {
                    builder.AppendLine($"\t{ToString(part)}");
                }
            }
            builder.AppendLine("Defaults");
            foreach (var @default in pattern.Defaults)
            {
                builder.AppendLine($"\t{@default.Key} = {@default.Value}");
            }

            builder.AppendLine("ParameterPolicies ");
            foreach (var policy in pattern.ParameterPolicies)
            {
                builder.AppendLine($"\t{policy.Key} = {string.Join(',', policy.Value.Select(it => it.Content))}");
            }

            builder.AppendLine("RequiredValues");
            foreach (var required in pattern.RequiredValues)
            {
                builder.AppendLine($"\t{required.Key} = {required.Value}");
            }

            return builder.ToString();

            static string ToString(RoutePatternPart part)
            {
                if (part is RoutePatternLiteralPart literal)
                {
                    return $"Literal: {literal.Content}";
                }
                if (part is RoutePatternSeparatorPart separator)
                {
                    return $"Separator: {separator.Content}";
                }
                else
                {
                    var parameter = (RoutePatternParameterPart)part;
                    return $"Parameter: Name = {parameter.Name}; Default = {parameter.Default}; IsOptional = { parameter.IsOptional}; IsCatchAll = { parameter.IsCatchAll}; ParameterKind = { parameter.ParameterKind}";
                }
            }
        }
    }
}