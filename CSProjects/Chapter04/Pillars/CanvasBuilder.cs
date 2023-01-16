using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PillarsLib;

namespace Pillars
{
    public class CanvasBuilder
    {
        // private string[,] Canvas;
        private int Size;

        private int SwitchSize;
        /*
        public CanvasBuilder (int size)
        {
            if (size <= 0)
            {
                throw new System.Exception(message: $"Argument {nameof(size)} not positive.");
            }
            Canvas = new string[size, size];

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Canvas[i, j] = " ";
                }
            }
        }
        */
        public CanvasBuilder (int pillarSize, int switchSize)
        {
            this.Size = pillarSize;
            this.SwitchSize = switchSize;
        }
        public void DrawPillars(PillarsSet pillarsSet)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i + j < Size - 1)
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        if (pillarsSet.GetPillar(j).Status & (i + j == Size - 1))
                        {
                            Console.Write("___");
                        }
                        else if (pillarsSet.GetPillar(j).Status)
                        {
                            Console.Write("| |");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public void DrawSwitches(List<PillarSwitch> pillarSwitches)
        {
            string header = "x";
            string margin = "|";
            string numberRow = "|";
            string bottom = "x";
            for (int i = 0; i < this.SwitchSize; i++)
            {
                header += "xxxx";
                margin += "   |";
                numberRow += $" {i} |";
                bottom += "xxxx";
            }
            Console.WriteLine(header);
            Console.WriteLine(margin);
            Console.WriteLine(numberRow);
            Console.WriteLine(margin);
            Console.WriteLine(bottom);
        }

        public void Draw(PillarsSet pillarsSet, List<PillarSwitch> pillarSwitches)
        {
            DrawPillars(pillarsSet);
            Console.WriteLine();
            DrawSwitches(pillarSwitches);
        }

        public int Round(PillarsSet pillarsSet, List<PillarSwitch> pillarSwitches)
        {
            Draw(pillarsSet, pillarSwitches);
            

            bool flag = false;
            int result;
            do
            {
                Console.WriteLine();
                Console.Write("Which lever do you want to pull (R for reset)? ");
                string input = Console.ReadLine()!;

                
                flag = int.TryParse(input, out result);
                if (input.Equals("R"))
                {
                    flag = true;
                    result = -1;
                }

            } while (!flag | result >= SwitchSize);

            return result;
        }

        public void Game(PillarsSet pillarsSet, List<PillarSwitch> pillarSwitches)
        {
            while(true)
            {
                int inputSwitch = Round(pillarsSet, pillarSwitches);
                if (inputSwitch == -1)
                {
                    InitialConfig myConfig = new();
                    pillarsSet = myConfig.PillarsSetConfig;
                }
                else
                {
                    pillarSwitches[inputSwitch].Activate(pillarsSet);
                }
            }
        }
    }
}