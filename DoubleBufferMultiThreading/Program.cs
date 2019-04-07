using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Implementation of Double Buffer using multithreading
/// Implemented By: Komal Shahzadi (2015-CS-163)
/// </summary>

namespace DoubleBufferMultiThreading
{
    class Program
    {
        //path of file which will read using double buffers
        const string filePath = @"C:\Users\Komal Shahzadi\Desktop\DoubleBufferMultiThreading\Sample.txt";
        private static StreamReader fileReader = new StreamReader(filePath);

        //Declation of buffers and end of buffers
        private static string firstBuffer = "";
        private static string secondBuffer = "";
        private static Boolean endOfFirstBuffer = false;
        private static Boolean endOfSecondBuffer = true;
        private static Boolean flag = true;

        static void Main(string[] args)
        {
            //Thread creation and start
            Thread Load = new Thread(new ThreadStart(LoadBuffer));
            Thread Read = new Thread(new ThreadStart(ReadBuffer));
            firstBuffer = fileReader.ReadLine();
            Load.Start();
            Read.Start();
            Console.ReadKey();
        }

        ///<summary>
        /// function to read data and load the buffer
        /// </summary> 
        ///<parm>void</parm>
        ///<return>void</return>
        private static void LoadBuffer()
        {
            while (!fileReader.EndOfStream)
            {
                if (endOfFirstBuffer == true)
                {
                    firstBuffer = fileReader.ReadLine();
                }
                Thread.Sleep(800);
                if (endOfSecondBuffer == true)
                {
                    secondBuffer = fileReader.ReadLine();
                }
                Thread.Sleep(800);
            }
            flag = false;
        }

        ///<summary>
        /// function to print data
        /// </summary> 
        ///<parm>void</parm>
        ///<return>void</return>
        private static void ReadBuffer()
        {
            while (flag)
            {
                if (endOfSecondBuffer == true)
                {
                    Console.WriteLine(firstBuffer + " First Buffer Read ");
                    endOfFirstBuffer = true;
                }
                Thread.Sleep(1000);
                if (endOfFirstBuffer == true)
                {
                    Console.WriteLine(secondBuffer + " Second Buffer Read ");
                    endOfSecondBuffer = true;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
