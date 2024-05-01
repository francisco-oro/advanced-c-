using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming
{
    public abstract class Expression
    {
    }

    public class Literal : Expression
    {
        public double Value { get; set; }

        public Literal(double value)
        {
            Value = value;
        }
    }


    public class Addition : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public Addition(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }

    public class ExpressionPrinter
    {
        public static void Print(Literal literal, StringBuilder sb)
        {
            sb.Append(literal.Value);
        }

        public static void Print(Addition addition, StringBuilder sb)
        {
            sb.Append('(');
            Print((dynamic)addition.Left, sb);
            sb.Append('+');
            Print((dynamic)addition.Right, sb);
            sb.Append(')');
        }
    }

    internal class VisitorPatternDemo
    {
        static void Main(string[] args)
        {
            // 1+2+3
            Expression e = new Addition(
                new Addition(
                    new Literal(1),
                    new Literal(2)),
                new Literal(3));

            var sb = new StringBuilder();
            ExpressionPrinter.Print((dynamic)e, sb);
            Console.WriteLine(sb);
        }
    }
}
