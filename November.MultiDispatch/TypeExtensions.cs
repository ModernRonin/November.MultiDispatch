using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace November.MultiDispatch
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetAssignmentTargetTypes(this Type self)
        {
            var typeInfo = self.GetTypeInfo();
            var ancestors = typeInfo.ImplementedInterfaces.ToList();
            if (null != typeInfo.BaseType) ancestors.Add(typeInfo.BaseType);
            var result = ancestors.Distinct().SelectMany(p => p.GetAssignmentTargetTypes()).ToList();
            result.Add(self);
            return result.Distinct();
        }
    }
}