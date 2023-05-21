using System;
using System.Collections.Generic;


class Program {
    static void Main(string[] args) {
        TextStart();
        int currentRow = 1;
        int currentColumn = 1;
        List<int> mainRollbacks = new List<int>();
        List<int> secondaryRollbacks = new List<int>();

        while (true) {
            Console.Write("Enter the Queen's move COMMAND (1-11) : ");
            int moveCommand = int.Parse(Console.ReadLine());

            if (moveCommand >= 1 && moveCommand <= 8) {
                Console.Write("Enter Walking spaces (1-8) : ");
                int numSpaces = int.Parse(Console.ReadLine());

                int newRow = currentRow;
                int newColumn = currentColumn;

                switch (moveCommand) {
                    case 1: // Move Up
                        newRow += numSpaces;
                        break;
                    case 2: // Move diagonally left up
                        newRow += numSpaces;
                        newColumn -= numSpaces;
                        break;
                    case 3: // Move Left
                        newColumn -= numSpaces;
                        break;
                    case 4: // Move diagonally left down
                        newRow -= numSpaces;
                        newColumn -= numSpaces;
                        break;
                    case 5: // Move Down
                        newRow -= numSpaces;
                        break;
                    case 6: // Move diagonally right down
                        newRow -= numSpaces;
                        newColumn += numSpaces;
                        break;
                    case 7: // Move Right
                        newColumn += numSpaces;
                        break;
                    case 8: // Move diagonally right up
                        newRow += numSpaces;
                        newColumn += numSpaces;
                        break;
                }

                if (IsValidMove(newRow, newColumn)) {
                    mainRollbacks.Add(currentRow);
                    mainRollbacks.Add(currentColumn);

                    currentRow = newRow;
                    currentColumn = newColumn;

                    secondaryRollbacks.Clear();
                    Console.WriteLine("Move successful.");
                    Console.WriteLine("Current position: {0}",GetPosition(currentRow, currentColumn));
                } else {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            } else if (moveCommand == 9) { // Reverse last move (1st rollback)
                if (mainRollbacks.Count >= 2) {
                    int previousColumn = mainRollbacks[mainRollbacks.Count - 1];
                    int previousRow = mainRollbacks[mainRollbacks.Count - 2];
                    mainRollbacks.RemoveAt(mainRollbacks.Count - 1);
                    mainRollbacks.RemoveAt(mainRollbacks.Count - 1);

                    secondaryRollbacks.Add(currentRow);
                    secondaryRollbacks.Add(currentColumn);

                    currentRow = previousRow;
                    currentColumn = previousColumn;

                    Console.WriteLine("Last move reversed.");
                    Console.WriteLine("Current position: {0}",GetPosition(currentRow, currentColumn));
                } else {
                    Console.WriteLine("No moves to reverse. Please try again.");
                }
            } else if (moveCommand == 10) { // Reverse last move (2nd rollback)
                if (secondaryRollbacks.Count >= 2) {
                    int previousColumn = secondaryRollbacks[secondaryRollbacks.Count - 1];
                    int previousRow = secondaryRollbacks[secondaryRollbacks.Count - 2];
                    secondaryRollbacks.RemoveAt(secondaryRollbacks.Count - 1);
                    secondaryRollbacks.RemoveAt(secondaryRollbacks.Count - 1);

                    mainRollbacks.Add(currentRow);
                    mainRollbacks.Add(currentColumn);

                    currentRow = previousRow;
                    currentColumn = previousColumn;

                    Console.WriteLine("Last move reversed.");
                    Console.WriteLine("Current position: {0}",GetPosition(currentRow, currentColumn));
                } else {
                    Console.WriteLine("No moves to reverse. Please try again.");
                }
            } else if (moveCommand == 11) { // End of the game
                Console.WriteLine("Game ended.");
                Console.WriteLine("Current position: {0}",GetPosition(currentRow, currentColumn));
                Console.WriteLine("*****+*****+*****+*****+*****+*****+*****+*****+*****+*****+*****");
                break;
            } else {
                Console.WriteLine("Invalid command. Please try again.");
            }
        }
    }

    static bool IsValidMove(int row, int column) {
        return row >= 1 && row <= 8 && column >= 1 && column <= 8;
    }

    static string GetPosition(int row, int column) {
        char columnChar = (char)('A' + column - 1);
        return columnChar.ToString() + row.ToString();
    }
    static void TextStart() {
        Console.WriteLine("*****+*****+*****+*****+*****+*****+*****+*****+*****+*****+*****");
        Console.WriteLine(" 1 - Up");
        Console.WriteLine(" 2 - Left up");
        Console.WriteLine(" 3 - Left");
        Console.WriteLine(" 4 - Left down");
        Console.WriteLine(" 5 - Down");
        Console.WriteLine(" 6 - Right down");
        Console.WriteLine(" 7 - Right");
        Console.WriteLine(" 8 - Right up");
        Console.WriteLine(" 9 - Undo move");
        Console.WriteLine("10 - Redo move");
        Console.WriteLine("11 - End games");
        Console.WriteLine("*****+*****+*****+*****+*****+*****+*****+*****+*****+*****+*****");
    }
}