using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AssortedTopics
{
    public enum WorkflowResult
    {
        Success, Failure
    }
    public class QuadraticEquationSolver
    {
        // ax^2 + bx + c = 0 
        public WorkflowResult Start(double a, double b, double c, out Tuple<Complex, Complex> result)
        {
            var disc = b * b - 4 * a * c;
            if (disc < 0)
            {
                result = null;
                return WorkflowResult.Failure;
            }
            //return SolveComplex(a, b, c, disc);
            else
            {
                return SolveSimple(a, b, c, disc, out result);
            }
        }

        private WorkflowResult SolveSimple(double d, double d1, double d2, double disc, out Tuple<Complex, Complex> result)
        {
            var rootDisc = Math.Sqrt(disc);
            result = Tuple.Create(
                new Complex((-d2 + rootDisc) / (2 * d1), 0),
                new Complex((-d2 - rootDisc) / (2 * d1), 0));

            return WorkflowResult.Success;
        }


        private Tuple<Complex, Complex> SolveComplex(double d, double d1, double d2, double disc)
        {
            var rootDisc = Complex.Sqrt(new Complex(disc, 0));
            return Tuple.Create(
                (-d2 + rootDisc) / (2 * d1),
                (-d2 - rootDisc) / (2 * d1));
        }
    }
    internal class ContinuationPassingStyleDemo
    {
        static void Main(string[] args)
        {
            var solver = new QuadraticEquationSolver();
            Tuple<Complex, Complex> solution;
            var flag = solver.Start(1, 10, 16, out solution);
        }
    }
}
