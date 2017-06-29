using System;
using System.Diagnostics;
using System.IO;

namespace Rosalind
{

    interface ISolver<out TInput, out TResult>
    {

        TInput ReadInputFile();

        TResult Solve();

        void WriteFile();

    }

    abstract class GeneralizedSolver<TInput, TResult> : ISolver<TInput, TResult>
    {
        protected string FileName { get; set; }

        protected GeneralizedSolver(string fileName)
        {
            FileName = fileName;
        }

        public int Id { get; protected set; }

        public abstract TInput ReadInputFile();

        public abstract void WriteFile();

        public TResult Solve()
        {
            return Process(ReadInputFile());
        }

        public abstract TResult Process(TInput data);
    }

    abstract class FastaFileStringSolver : GeneralizedSolver<FastaFile, string>
    {
        protected FastaFileStringSolver(string fileName) : base(fileName) { }

        public override FastaFile ReadInputFile()
        {
            return FastaFile.Parse(FileName);
        }

        public override void WriteFile()
        {
            using (var fs = new FileStream(FileName + ".out", FileMode.Create))
            {
                var writer = new StreamWriter(fs);
                var contents = Solve();
                writer.WriteLine(contents);
                writer.Flush();
            }
        }
    }

    abstract class SimpleInputFileStringSolver : GeneralizedSolver<string, string>
    {
        protected SimpleInputFileStringSolver(string filename) : base(filename) { }

        public abstract override string Process(string data);

        public override string ReadInputFile()
        {
            using(var fs = new FileStream(FileName, FileMode.Open))
            {
                var reader = new StreamReader(fs);
                var contents = reader.ReadLine();
                return contents;
            }
        }

        public override void WriteFile()
        {
            using(var fs = new FileStream(FileName + ".out", FileMode.Create))
            {
                var writer = new StreamWriter(fs);
                var contents = Solve();
                writer.WriteLine(contents);
                writer.Flush();
            }
        }
    }

}
