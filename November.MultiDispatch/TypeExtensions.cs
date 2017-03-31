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

            var parents = typeInfo.ImplementedInterfaces.ToList();
            if (null!=typeInfo.BaseType)
                parents.Add(typeInfo.BaseType);

            var result = parents.SelectMany(p => p.GetAssignmentTargetTypes()).ToList();
            result.Add(self);
            return result.Distinct();
        }
        public static int GetTypeDistanceFrom(this Type self, Type other)
        {
            return 0;
        }
    }
}