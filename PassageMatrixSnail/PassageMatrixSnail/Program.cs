using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PassageMatrixSnail
{

    public class Matrix
    {

        private int[,] matrix;
        private int rows;
        private int columns;


        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public int[,] GetMatrix
        {
            get { return matrix; }
        }

        public Matrix(string fileName)
            {
                StreamReader sr = new StreamReader(fileName);
                string dimensions = sr.ReadLine();
                string[] dimensionsOfMatrix = dimensions.Split('x');
                rows = int.Parse(dimensionsOfMatrix[0]);
                columns = int.Parse(dimensionsOfMatrix[1]);

                int[,] outputMatrix = new int[rows, columns];

                string nextLine;
                string[] nextLineArr;

                for (int i = 0; i < rows; i++)
                {

                    if (sr.EndOfStream)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            outputMatrix[i, j] = 0;
                        }
                    }
                    else
                    {
                        nextLine = sr.ReadLine();
                        nextLineArr = nextLine.Split(' ');
                        for (int j = 0; j < columns; j++)
                        {
                            if (j > nextLineArr.Length - 1)
                            {
                                outputMatrix[i, j] = 0;
                            }
                            else
                            {
                                outputMatrix[i, j] = int.Parse(nextLineArr[j]);
                            }

                        }
                    }
                }
                matrix = outputMatrix;
            }

        public void PassageMatrix(ref int[] arr, int linearPosition, int positionR, int positionC, int numRows, int numColumns, Direction direction)
        {
            if(numColumns !=0 && numRows!=0)
            {
                switch (direction.CurrentDirection)
                {
                    case "right":
                        {
                            for (int i=positionC; i<positionC+numColumns; i++)
                            {
                                arr[linearPosition] = matrix[positionR, i];
                                linearPosition++;
                                
                            }

                            direction.SwitchDirection();
                            PassageMatrix(ref arr, linearPosition, positionR + 1, positionC + numColumns - 1, numRows - 1, numColumns, direction);
                            break;
                        }

                    case "down":
                        {
                            for (int i = positionR; i < positionR+numRows; i++)
                            {
                                arr[linearPosition] = matrix[i, positionC];
                                linearPosition++;

                            }

                            direction.SwitchDirection();
                            PassageMatrix(ref arr, linearPosition, positionR +numRows- 1, positionC - 1, numRows , numColumns-1, direction);
                            break;
                        }

                    case "left":
                        {
                            for (int i = positionC; i >=positionC+1-numColumns; i--)
                            {
                                arr[linearPosition] = matrix[positionR, i];
                                linearPosition++;

                            }

                            direction.SwitchDirection();
                            PassageMatrix(ref arr, linearPosition, positionR -1, positionC - numColumns + 1, numRows - 1, numColumns, direction);
                            break;
                        }

                    case "up":
                        {
                            for (int i = positionR; i >=positionR+1-numRows; i--)
                            {
                                arr[linearPosition] = matrix[i, positionC];
                                linearPosition++;

                            }

                            direction.SwitchDirection();
                            PassageMatrix(ref arr, linearPosition, positionR+1-numRows , positionC + 1, numRows, numColumns - 1, direction);
                            break;
                        }

                }

            }


            //recursion exit (do nothing)
            

        }

            
     }

    public class Direction
    {
        private string [] direction = { "right", "down", "left", "up" };
        private string currentDirection;


        public Direction()
        {
            currentDirection = direction[0];
        }

        public string CurrentDirection
        {
            get {return currentDirection;}
        } 


        public void SwitchDirection()
        {
            switch (currentDirection)
            {
                case "right":
                    currentDirection = direction[1];
                    break;
                case "down":
                    currentDirection = direction[2];
                    break;
                case "left":
                    currentDirection = direction[3];
                    break;
                case "up":
                    currentDirection = direction[0];
                    break;

            }
        }
    }


class Program
    {
        static void Main(string[] args)
        {
            Matrix matrTest = new Matrix(@"D:\matrix3.txt");
            Matrix matrCheck = new Matrix(@"D:\matrix2.txt");
            int[] arr = new int[matrTest.Columns * matrTest.Rows];
            Direction direct = new Direction();

            //act
            matrTest.PassageMatrix(ref arr, 0, 0, 0, matrTest.Rows, matrTest.Columns, direct);

            foreach(int val in arr)
            {
                Console.Write(" ");
                Console.Write(val);
            }

            Console.ReadKey();
        }
    }
}
