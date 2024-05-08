using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AssortedTopics
{
    public class QuadraticEquationSolver
    {
        // ax^2 + bx + c = 0 
        public Tuple<Complex, Complex> Start(double a, double b, double c)
        {
            var disc = b * b - 4 * a * c;
            if (disc < 0)
                return SolveComplex(a, b, c, disc);
            else
                return SolveSimple(a, b, c, disc);
        }

        private Tuple<Complex, Complex> SolveSimple(double d, double d1, double d2, double disc)
        {
            var rootDisc = Math.Sqrt(disc);
            return Tuple.Create(
                new Complex((-d2 + rootDisc) / (2 * d1), 0),
                new Complex((-d2 - rootDisc) / (2 * d1), 0));

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
            var solutions = solver.Start(1, 10, 16);
        }
    }
}
