using System;
using System.Linq;

namespace AddingThisAddingThat
{
    class Program
    {
        private class AddResult
        {
            private readonly byte[] _f;
            private readonly byte[] _s;
            private readonly byte[] _result;

            public AddResult(byte[] f, byte[] s, byte[] result)
            {
                _f = f;
                _s = s;
                _result = result;
            }

            public override string ToString()
            {
                return "Input: {" + string.Join(", ", _f) + "}, {" + string.Join(", ", _s) +
                    "}\r\nResult: {" +
                    string.Join(", ", _result) + "}";
            }
        }

        private static byte[] AddRecursive(byte[] f, byte[] s)
        {
            byte[] result;

            if (f.Length == 1)
            {
                var sum = Convert.ToByte((f[0] + s[0]) % 256);
                var carReturn = Convert.ToByte((f[0] + s[0]) / 256);
                result = carReturn == 1 ? new[] { carReturn, sum } : new[] { sum };
            }
            else
            {
                var tail = AddRecursive(f.Skip(1).ToArray(), s.Skip(1).ToArray());
                var sum = Convert.ToByte((f[0] + s[0] + (tail.Length == f.Length ? tail[0] : 0)) % 256);
                var carReturn = Convert.ToByte((f[0] + s[0] + (tail.Length == f.Length ? tail[0] : 0)) / 256);
                if (carReturn == 1)
                {
                    result = new[] { carReturn, sum }.Concat(tail.Length == f.Length ? tail.Skip(1) : tail).ToArray();
                }
                else
                {
                    result = new[] { sum }.Concat(tail.Length == f.Length ? tail.Skip(1) : tail).ToArray();
                }
            }

            return result;
        }

        static void Main()
        {
            byte[] f = { 1, 1, 1 };
            byte[] s = { 1, 1, 1 };

            var result = AddRecursive(f,s);
            var addResult = new AddResult(f, s, result);

            Console.WriteLine(addResult);

            f = new byte[] { 1, 1, 255 };
            s = new byte[] { 0, 0, 1 };

            result = AddRecursive(f, s);
            addResult = new AddResult(f, s, result);
            Console.WriteLine(addResult);

            f = new byte[] { 1, 255, 255 };
            s = new byte[] { 0, 0, 1 };

            result = AddRecursive(f, s);
            addResult = new AddResult(f, s, result);
            Console.WriteLine(addResult);

            f = new byte[] { 255, 255, 255 };
            s = new byte[] { 0, 0, 1 };

            result = AddRecursive(f, s);
            addResult = new AddResult(f, s, result);
            Console.WriteLine(addResult);

            Console.ReadLine();
        }
    }
}
