using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<string, int> valuesToReplace { get; set; }

        public IncDecExpressionVisitor()
        {
            valuesToReplace = new Dictionary<string, int>();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.Right.NodeType == ExpressionType.Constant)
            {
                int rightValue = (int)((node.Right as ConstantExpression).Value);
                if (rightValue == 1)
                {
                    if (node.NodeType == ExpressionType.Add)
                    {
                        return Expression.Increment(node.Left);
                    }
                    else if (node.NodeType == ExpressionType.Subtract)
                    {
                        return Expression.Decrement(node.Left);
                    }
                }               
            }
            return base.VisitBinary(node);
        }

        internal Expression VisitAndConvert<T>(Expression<T> root)
        {
            return VisitLambda(root);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            // Leave all parameters alone except the one we want to replace.
            var parameters = node.Parameters
                                 .Where(p => valuesToReplace.TryGetValue(p.Name, out int value));

            return Expression.Lambda(Visit(node.Body), parameters);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        { 
            if(valuesToReplace.TryGetValue(node.Name, out int value))
            {
                return Expression.Constant(value);
            }
            return base.VisitParameter(node); 
        }
    }
}
