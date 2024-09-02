
using System.Text.RegularExpressions;

namespace RoutingExample.CustomConstrains
{
    public class MonthCustomConstraints : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey))
            {
                return false; //not a match
            }

            Regex regex = new Regex("^(apr|jul|oct|jan)$");
            string? monthValue = Convert.ToString(values[routeKey]);

            if(regex.IsMatch(monthValue!))
            {
                return true; //it's a match
            }
            return false; //not a match
        }
    }
}
