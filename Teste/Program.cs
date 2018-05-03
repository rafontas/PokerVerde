using System;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            int impar = 0, par = 0;
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    if(i % 2 != 0)
                    {   
                        impar++;
                    }

                    if (j % 2 == 0)
                    {
                        par++;
                    }
                }
            }

            Console.WriteLine("Impar: {0}, Par: {1}", impar, par);
        }
    }
}