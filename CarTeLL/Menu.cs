using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarTeLL

{
    public class Menu
    {
        public int selectedIndex;
        public string[] options;
        public string prompt;


        public Menu(string prompt, string[] options)
        {
            this.prompt = prompt;
            this.options = options;
            selectedIndex = 0;
        }

       

        public void DisplayOptions()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(prompt);
            Console.WriteLine();
            Console.ResetColor();
            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];
                string prefix;

                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine($"{ currentOption}");
                Console.ResetColor();
            }
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = options.Length-1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == options.Length)
                    {
                        selectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);
            return selectedIndex;
        }

        public void MainMenu()
        {

            string prompt = @"
                                                  
                                             ___  ___ _____ _   _ _   _ 
                                             |  \/  ||  ___| \ | | | | |
                                             | .  . || |__ |  \| | | | |
                                             | |\/| ||  __|| . ` | | | |
                                             | |  | || |___| |\  | |_| |
                                             \_|  |_/\____/\_| \_/\___/ 
                           
                           
";
            string[] options = { @"
                                                     B R A N D S", @"
                                                     M O D E L S", @"
                                                       C A R S", @"
                                                       S A V E", @"
                                              S A V E   A N D   E X I T", @"
                                                       E X I T" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();
        }

        public void BrandMenu()
        {

            string prompt = @"
                                                  
                                         ____  _____            _   _ _____   _____ 
                                        |  _ \|  __ \     /\   | \ | |  __ \ / ____|
                                        | |_) | |__) |   /  \  |  \| | |  | | (___  
                                        |  _ <|  _  /   / /\ \ | . ` | |  | |\___ \ 
                                        | |_) | | \ \  / ____ \| |\  | |__| |____) |
                                        |____/|_|  \_\/_/    \_\_| \_|_____/|_____/ 
                                             ";
            string[] options = { @"
                                                     A D D   B R A N D", @"
                                                    E D I T   B R A N D", @"
                                                  R E M O V E   B R A N D", @"
                                                L I S T   O F   B R A N D S", @"
                                                     " };
                                                     
                                             

            Menu brandMenu = new Menu(prompt, options);
            int selectedIndex = brandMenu.Run();
        }

        public void ModelMenu()
        {
            string prompt = @"

                                         __  __  ____  _____  ______ _       _____ 
                                        |  \/  |/ __ \|  __ \|  ____| |     / ____|
                                        | \  / | |  | | |  | | |__  | |    | (___  
                                        | |\/| | |  | | |  | |  __| | |     \___ \ 
                                        | |  | | |__| | |__| | |____| |____ ____) |
                                        |_|  |_|\____/|_____/|______|______|_____/ 
                                            ";
            string[] options = { @"
                                                     A D D   M O D E L", @"
                                                    E D I T   M O D E L", @"
                                                  R E M O V E   M O D E L", @"
                                                L I S T   O F   M O D E L S", @"
                                                     " };

            Menu modelMenu = new Menu(prompt, options);
            int selectedIndex = modelMenu.Run();

        }
        
        public void CarMenu()
        {
            string prompt = @"

                                                _____          _____   _____ 
                                               / ____|   /\   |  __ \ / ____|
                                              | |       /  \  | |__) | (___  
                                              | |      / /\ \ |  _  / \___ \ 
                                              | |____ / ____ \| | \ \ ____) |
                                               \_____/_/    \_\_|  \_\_____/ 
                                
                                ";
            string[] options = { @"
                                                     A D D   C A R", @"
                                                    E D I T   C A R", @"
                                                  R E M O V E   C A R", @"
                                                L I S T   O F   C A R S", @"
                                                     " };

            Menu carMenu = new Menu(prompt, options);
            int selectedIndex = carMenu.Run();
        }
    }
}
