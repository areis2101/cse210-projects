using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int number;


        do
        {
            Console.Write("Enter number: ");
            number = Convert.ToInt32(Console.ReadLine());

            if (number != 0)
            {
                numbers.Add(number);
            }

        } while (number != 0);

        // Soma
        int sum = 0;

        foreach (int n in numbers)
        {
            sum += n;
        }

        // Média
        double average = (double)sum / numbers.Count;

        // Maior número
        int largest = numbers[0];

        foreach (int n in numbers)
        {
            if (n > largest)
            {
                largest = n;
            }
        }


        int smallestPositive = int.MaxValue;

        foreach (int n in numbers)
        {
            if (n > 0 && n < smallestPositive)
            {
                smallestPositive = n;
            }
        }


        numbers.Sort();

        // Resultados
        Console.WriteLine();
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largest}");

        if (smallestPositive != int.MaxValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }

        Console.WriteLine("The sorted list is:");

        foreach (int n in numbers)
        {
            Console.WriteLine(n);
        }
    }
}