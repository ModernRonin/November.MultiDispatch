using System;
using System.Collections.Generic;
using System.Linq;

namespace November.MultiDispatch
{
    public class TypesToContextMap : ITypesToHandlerMap
    {
        readonly List<Record> mRecords = new List<Record>();
        public CallContext GetFor(Type left, Type right)
        {
            // TODO: add caching
            var leftTypes = left.GetAssignmentTargetTypes();
            var rightTypes = right.GetAssignmentTargetTypes();
            var pairs = leftTypes.SelectMany(l => rightTypes.Select(r => new Pair(l, r)));
            var candidates =
                mRecords.Where(r => pairs.Any(p => p.Left == r.Left && p.Right == r.Right))
                    .Select(r => new {Record = r, TypeDistance = r.CalculateTypeDistanceFrom(left, right)})
                    .OrderBy(r => r.TypeDistance)
                    .Select(r => r.Record.Context);

            return candidates.FirstOrDefault();
        }
        public void Add(Type leftType, Type rightType, CallContext context)
            => mRecords.Add(new Record(leftType, rightType, context));

        class Pair
        {
            public Pair(Type left, Type right)
            {
                Left = left;
                Right = right;
            }
            public Type Left { get; }
            public Type Right { get; }
        }

        class Record : Pair
        {
            public Record(Type left, Type right, CallContext context) : base(left, right)
            {
                Context = context;
            }
            public CallContext Context { get; }
            public int CalculateTypeDistanceFrom(Type left, Type right)
                => 1000 * left.GetTypeDistanceFromAncestor(Left) + right.GetTypeDistanceFromAncestor(Right);
        }
    }
}