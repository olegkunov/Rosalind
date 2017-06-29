using System;

namespace Rosalind
{
    class Program
    {
        private static void Main(string[] args)
        {
            var solver = new Rosalind_GC_Solver("rosalind_gc.ex.txt");
            solver.WriteFile();
        }
    }
}
