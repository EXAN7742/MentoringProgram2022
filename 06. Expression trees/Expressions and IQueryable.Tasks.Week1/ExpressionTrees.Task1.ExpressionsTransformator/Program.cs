/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using AgileObjects.ReadableExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Visitor for increment/decrement.");
            Console.WriteLine();

            //part1
            increment();
            decrement();

            //part2
            replacementParameter();
            

            Console.ReadLine();
        }

        private static void replacementParameter()
        {
            var valuesToReplace = new Dictionary<string, int>();
            valuesToReplace.Add("num", 5);
            
            var translator = new IncDecExpressionVisitor();
            translator.valuesToReplace = valuesToReplace;

            Expression<Func<int, int>> sourceExpression
                = num => num * 3;
            Console.WriteLine("Initial expression: {0}", sourceExpression.ToReadableString());

            Expression translated = translator.Visit(sourceExpression);
            Console.WriteLine("Transformed expression: {0}", translated.ToReadableString());
        }

        private static void increment()
        {
            var translator = new IncDecExpressionVisitor();
            Expression<Func<int, int>> expression
                = num => num + 1;
            Console.WriteLine("Initial expression: {0}", expression.ToReadableString());


            Expression translated = translator.Visit(expression);
            Console.WriteLine("Transformed expression: {0}", translated.ToReadableString());
        }

        private static void decrement()
        {
            var translator = new IncDecExpressionVisitor();
            Expression<Func<int, int>> expression
                = num => num - 1 + 3;
            Console.WriteLine("Initial expression: {0}", expression.ToReadableString());


            Expression translated = translator.Visit(expression);
            Console.WriteLine("Transformed expression: {0}", translated.ToReadableString());
        }
    }
}
