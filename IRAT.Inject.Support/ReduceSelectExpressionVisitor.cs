using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IRAT.Inject.Support
{
    public class ReduceSelectExpressionVisitor : ExpressionVisitor
    {
        private readonly IEnumerable<string> _memberNames;

        private readonly bool _inclusive;

        private readonly Type _type;

        public ReduceSelectExpressionVisitor(IEnumerable<string> memberNames, bool inclusive, Type type)
        {
            _memberNames = memberNames;
            _inclusive = inclusive;
            _type = type;
        }

        protected override Expression VisitMemberInit(MemberInitExpression memberInit)
        {
            if (memberInit.Type == _type)
            {
                List<MemberBinding> bindings = memberInit.Bindings.Where(delegate (MemberBinding binding)
                {
                    bool flag = _memberNames.Contains(binding.Member.Name);
                    if (!_inclusive)
                    {
                        return !flag;
                    }
                    return flag;
                }).ToList();
                return Expression.MemberInit(memberInit.NewExpression, bindings);
            }
            return base.VisitMemberInit(memberInit);
        }
    }
}
