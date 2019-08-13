using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SafestPlaceInTheGalaxy
{
    class Program
    {
        private const int DimensionsNumber = 3;
        private const int DimensionsSize = 1000;
        
        /// <summary>
        /// Calculates point of max distance inside an axis
        /// </summary>
        /// <param name="axis">Array of points in an axis</param>
        /// <returns>Maximum distance point with points in an axis</returns>
        static int MaxDistancePointPerAxis(int[] axis)
        {
            var orderedAxis = axis.OrderBy(x => x).ToArray();
            var firstDistance = orderedAxis.First();
            var lastDistance = DimensionsSize - orderedAxis.Last();
            var maxDistance = Math.Max(firstDistance, lastDistance);
            var maxDistancePoint = firstDistance > lastDistance ? 0 : DimensionsSize;

            for(var i = 0; i < orderedAxis.Count() - 1; i++)
            {
                var middlePointDistance = (orderedAxis[i + 1] - orderedAxis[i]) / 2;

                if (middlePointDistance > maxDistance)
                {
                    maxDistance = middlePointDistance;
                    maxDistancePoint = orderedAxis[i] + middlePointDistance;
                }
            }

            return maxDistancePoint;
        }

        /// <summary>
        /// Get all values for a coordinate in a set of points
        /// </summary>
        /// <param name="points">Set of points</param>
        /// <param name="coordinatePosition">Coordinate position in points</param>
        /// <returns></returns>
        static int[] GetAxis(int[][] points, int coordinatePosition)
        {
            var axisBuilder = new List<int>();

            foreach (var point in points)
            {
                axisBuilder.Add(point[coordinatePosition]);
            }

            return axisBuilder.ToArray();
        }

        /// <summary>
        /// Calculates the safest point in the galaxy
        /// </summary>
        /// <param name="bombs">Array of bombs positions</param>
        /// <returns>Safest point in the galaxy</returns>
        static int[] SafestPoint(int[][] bombs)
        {
            var dimensions = bombs[0].Length;
            var point = new int[dimensions];

            for(var i = 0; i < dimensions; i++)
            {
                point[i] = MaxDistancePointPerAxis(GetAxis(bombs, i));
            }

            return point;
        }


        /// <summary>
        /// Calculates square of distance to nearest bomb.
        /// </summary>
        /// <param name="bombs">Array of bombs positions</param>
        /// <param name="point">Point to calculate distance to bombs</param>
        /// <returns>Square of distance to nearest bomb</returns>
        static int NearestBombDistanceSquare(int[][] bombs, int[] point)
        {
            var minDistanceSquare = int.MaxValue;
            foreach (var bomb in bombs)
            {
                var bombDistanceSquare = bomb.Select((x, i) => (x - point[i]) * (x - point[i])).Sum();
                minDistanceSquare = Math.Min(minDistanceSquare, bombDistanceSquare);
            }

            return minDistanceSquare;
        }


        /// <summary>
        /// Read a file with the next format:
        /// T
        /// N B1x B1y B1z B2x B2y B2z ... BNx BNy BNz
        /// 
        /// Where:
        ///     T is the number of test cases
        ///     N is the number of bombs for a given test case
        ///     B[bombNumber][coordinate] is the coordinate [coordinate] 
        ///         of the bomb [bombNumber] of a total N bombs
        /// 
        /// The example is valid for a 3D universe, 
        /// however application is able to deal with whatever number of dimensions universe 
        /// 
        /// </summary>
        /// <param name="filePath">File to read</param>
        /// <returns>Multidimensional array containing the testcases</returns>
        static int[][][] GetTestCases(string filePath)
        {
            var inputLines = File.ReadAllLines(filePath);
            var testCasesNumber = int.Parse(inputLines[0]);
            var formattedTestCases = new int[testCasesNumber][][];

            for (var i = 1; i < inputLines.Length; i++)
            {
                var testCase = inputLines[i].Split(' ').Select(x => int.Parse(x)).ToArray();
                var bombsNumber = testCase[0];
                formattedTestCases[i - 1] = new int[bombsNumber][];
                for (var j = 0; j < bombsNumber; j ++)
                {
                    formattedTestCases[i - 1][j] = new int[DimensionsNumber];
                    for (var k = 0; k < DimensionsNumber; k++)
                    {
                        formattedTestCases[i - 1][j][k] = testCase[(j * DimensionsNumber) + k + 1];
                    }
                }
            }

            return formattedTestCases;
        }


        //**** IMPORTANT: *****************************************************|
        // Please note that a file for testing is included as argument         |
        // in debug options in project configuration                           |
        //_____________________________________________________________________|
        static void Main(string[] args)
        {
            var testCases = GetTestCases(args[0]);
            var testNumber = 1;
            foreach (var testCase in testCases)
            {
                var safestPoint = SafestPoint(testCase);
                var nearestBombDistanceSquare = NearestBombDistanceSquare(testCase, safestPoint);

                Console.WriteLine("Test case {0}: Square of distance to nearest bomb = {1}", testNumber++, nearestBombDistanceSquare);
            }
            
            Console.ReadLine();
        }
    }
}
