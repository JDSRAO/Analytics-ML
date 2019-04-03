using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Alcohol.Consumption.Models
{
    public class ReflectionExtension
    {
        public static List<string> GetParameterNames(Type type)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var properties = type.GetProperties(flags);
            List<string> names = new List<string>();
            foreach (var property in properties)
            {
                if(property.CanRead)
                {
                    var name = property.Name;
                    names.Add(property.Name);
                }
            }

            return names;
        }
    }
}
