using System;
using System.Collections.Generic;
using System.Linq;

namespace Rosalind
{

    class Rosalind1Solver : SimpleInputFileStringSolver
    {
        public Rosalind1Solver(string filename) : base(filename)
        {
            Id = 1;
        }

        public override string Process(string data)
        {
            var indexes = new Dictionary<char, int>() { { 'A', 1 }, { 'C', 2 }, { 'G', 3 }, { 'T', 4 } };
            var result = data
                .GroupBy(source => indexes[source])
                .OrderBy(i => i.Key)
                .Aggregate("", (acc, element) => acc + element.Count() + " ");
            return result;
        }
    }

    class Rosalind2Solver : SimpleInputFileStringSolver
    {
        public Rosalind2Solver(string filename) : base(filename)
        {
            Id = 2;
        }

        public override string Process(string data)
        {
            return string.Concat(data.Select(c => c == 'T' ? 'U' : c));
        }
    }

    class Rosalind3Solver : SimpleInputFileStringSolver
    {
        public Rosalind3Solver(string filename) : base(filename)
        {
            Id = 3;
        }

        public override string Process(string data)
        {
            var pairs = new Dictionary<char, char> { { 'A', 'T' }, { 'G', 'C' }, { 'T', 'A' }, { 'C', 'G' } };
            var result = string.Concat(data.Reverse().Select(c => pairs[c]));
            return result;
        }
    }

    // fib
    class Rosalind4Solver : SimpleInputFileStringSolver
    {
        public Rosalind4Solver(string filename) : base(filename)
        {
            Id = 4;
        }

        public override string Process(string data)
        {
            var numbers = data.Split(' ');
            if (numbers.Length < 2) return "";
            var max_steps = int.Parse(numbers[0]);
            var litter = int.Parse(numbers[1]);
            var nstep = 2;
            var step1 = 1L;
            var step2 = 1L;
            while (nstep < max_steps)
            {
                var step0 = step2 * litter + step1;
                step2 = step1;
                step1 = step0;
                nstep += 1;
            }
            return step1.ToString();
        }
    }

    // gc
    class Rosalind_GC_Solver : FastaFileStringSolver
    {
        public Rosalind_GC_Solver(string fileName) : base(fileName)
        {
        }

        public override string Process(FastaFile data)
        {
            var max_gc_key = "";
            var max_gc = 0.0;
            foreach (var element in data)
            {
                var sequence = element.Value;
                var length = sequence.Where(c => c == 'G' || c == 'C').Count();
                if (max_gc == 0 || length / sequence.Length < max_gc)
                {
                    max_gc_key = element.Key;
                    max_gc = (double) length / sequence.Length;
                }
            }
            return $"{max_gc_key}\n{Math.Round(max_gc*100, 6)}";
        }

    }
}
