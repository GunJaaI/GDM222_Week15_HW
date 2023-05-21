using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        int num = int.Parse(Console.ReadLine());
        int tagNum = int.Parse(Console.ReadLine());

        Dictionary<string, string> productNum = new Dictionary<string, string>();
        Queue<string> keys = new Queue<string>();
        int letterCount = 1, numberCount = 0;
        string input;

        for (int i = 0; i < Math.Pow(26, num) * Math.Pow(10, tagNum); i++) { // Char A-Z(26) & Number 0-9(10)
            string code = GenerateCode(letterCount, numberCount, num, tagNum);
            keys.Enqueue(code);

            input = Console.ReadLine();
            if (input == "Stop") {
                break;
            }
            if (productNum.ContainsKey(code)) {
                productNum[code] = input;
            } else {
                productNum.Add(code, input);
                numberCount++; // Count number of key
            }
            if (numberCount >= Math.Pow(10, tagNum)) {
                numberCount = 0;
                letterCount++; //reset A -> B ->...
            }
        }

        string searchCode = Console.ReadLine();

        if (productNum.ContainsKey(searchCode) == true) {
            Console.WriteLine(productNum[searchCode]);
        } else {
            Console.WriteLine("Not found!");
        } //Console.WriteLine(productNum.ContainsKey(searchCode) ? productNum[searchCode] : "Not found!");
    }

    static string GenerateCode(int letterCount, int numberCount, int num, int m) {
        string letters = ConvertToBase26(letterCount - 1).PadLeft(num, 'A');
        string numbers = numberCount.ToString().PadLeft(m, '0');
        return letters + numbers;
    }

    static string ConvertToBase26(int number) {
        string result = "";
        while (number > 0)
        {
            number--;
            result = (char)('A' + number % 26) + result;
            number /= 26;
        }
        return result;
    }
}