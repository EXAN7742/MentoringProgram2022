using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Expressions.Task3.E3SQueryProvider.Models.Request;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }
            else if ((new[] { "Contains", "StartsWith", "EndsWith", "Equals" }).Contains(node.Method.Name))
            {
                createStringExpression(node.Object, node.Arguments[0], node.Method.Name);
                return node;
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if ((node.Left.NodeType == ExpressionType.MemberAccess) && (node.Right.NodeType == ExpressionType.Constant))
                    {
                        createStringExpression(node.Left, node.Right, "");
                    }
                    else
                    {
                        createStringExpression(node.Right, node.Left, "");
                    }
                    break;
                case ExpressionType.AndAlso:
                    var ftsQuery = new FtsQueryRequest
                    {
                        Statements = new List<Statement>()
                    };
                    
                    Visit(node.Left);
                    ftsQuery.Statements.Add(new Statement
                    {
                        Query = _resultStringBuilder.ToString()
                    });
                    _resultStringBuilder.Clear();
                    Visit(node.Right);
                    ftsQuery.Statements.Add(new Statement
                    {
                        Query = _resultStringBuilder.ToString()
                    });
                    _resultStringBuilder.Clear();

                    _resultStringBuilder.Append(JsonConvert.SerializeObject(ftsQuery));
                    break;

                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        private void createStringExpression(Expression left, Expression right, string methodName)
        {
            Visit(left);
            _resultStringBuilder.Append("(");
            if (methodName == "Contains" || methodName == "EndsWith")
            {
                _resultStringBuilder.Append("*");
            }
            Visit(right);
            if (methodName == "Contains" || methodName == "StartsWith")
            {
                _resultStringBuilder.Append("*");
            }
            _resultStringBuilder.Append(")");
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        #endregion
    }
}
