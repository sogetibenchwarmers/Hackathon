using HackathonApi.Models.Options;
using System;

namespace HackathonApi.Mediator
{
    public static class OptionsExtensions
    {
        public static string BuildAuthHeader(this ServiceNowOptions options)
        {
            return Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1")
                .GetBytes($"{options.ServiceNowUser}:{options.ServiceNowSecret}"));
        }
    }
}
