using System;
using System.Text;

namespace ClientCLI
{
	public class Menu
	{
		public List<Menu> options;
		public string name;
        public string message = null;

        const string CancelString = "<CANCEL>";

        public Menu(string name, List<Menu> options=null)
		{
			this.options = options == null ? new List<Menu>() : options;
			this.name = name;
		}

		internal void PrintMenu()
		{
            Console.Clear();

            Console.WriteLine(name);

            int i = 1;
            foreach (Menu option in options)
            {
                Console.WriteLine(i + ". " + option.name);
                i++;
            }
        }

		public virtual Menu Show()
		{
			PrintMenu();

            if (message != null)
            {
                Console.WriteLine(message);
                message = null;
            }

			int? selectedOption = null;

			while (selectedOption is null)
			{
				Console.WriteLine("\nSelect option:");
				try
				{
					selectedOption = int.Parse(Console.ReadLine());
					if (selectedOption > this.options.Count || selectedOption < 1)
					{
						selectedOption = null;
					}
					else
					{
						break;
					}
				}
				catch (System.FormatException)
				{
					
                }
                PrintMenu();
                Console.WriteLine("\nIncorrect option!");
            }
            return options[(int) selectedOption - 1];
        }

		public bool RequestString(string prompt, out string result, Func<string, (bool, string)> inputValidator = null, bool clearConsole=true, bool showName=true)
		{
            string input;

            (bool, string) inputValidation = (false, "");

            do
            {
                input = "";
                // Clear console
                if (clearConsole)
                    Console.Clear();

                if (showName)
                    Console.WriteLine(name);


                if (inputValidation.Item2 != "")
                {
                    Console.WriteLine(inputValidation.Item2);
                }
                Console.WriteLine(prompt);
                //string input = Console.ReadLine();

                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("\nInput cancelled.");
                        result = input;
                        return false;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("\nYour input: " + input);
                        break;
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length > 0)
                        {
                            input = input.Substring(0, input.Length - 1);
                            Console.Write("\b \b"); // Erase the previous character on the console
                        }
                    }
                    else
                    {
                        input += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }

                inputValidation = inputValidator is null ? (true, "") : inputValidator(input);
            } while (inputValidator != null && inputValidation.Item1 == false);

            result = input;
            return true;
		}
	}
}

