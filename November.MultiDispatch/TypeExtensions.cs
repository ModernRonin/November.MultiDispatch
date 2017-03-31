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
            var parents = self.GetDirectParents();
            var result = parents.SelectMany(p => p.GetAssignmentTargetTypes()).ToList();
            result.Add(self);
            return result.Distinct();
        }
        public static IEnumerable<Type> GetDirectParents(this Type self)
        {
            var typeInfo = self.GetTypeInfo();
            var parents = typeInfo.ImplementedInterfaces.ToList();
            if (null != typeInfo.BaseType) parents.Add(typeInfo.BaseType);
            return parents.Distinct();
        }
        public static int GetTypeDistanceFromAncestor(this Type self, Type other)
        {
            if (self == other) return 0;

            var parents = self.GetDirectParents().ToArray();
            if (!parents.Any())
                return int.MaxValue;

            var minParentDistance = parents.Select(p => p.GetTypeDistanceFromAncestor(other)).Min();
            return minParentDistance == int.MaxValue ? int.MaxValue : 1 + minParentDistance;
        }
    }
}