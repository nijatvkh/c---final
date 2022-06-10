using System;
using CarTeLL.Entities;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Threading;

namespace CarTeLL
{
    internal class Program
    {
        static string fileName = "cartell.dat";
        static GenericStore<Brands> brands = new GenericStore<Brands>();
        static GenericStore<Models> models = new GenericStore<Models>();
        static GenericStore<Cars> cars = new GenericStore<Cars>();

        [Obsolete]
        static void Main(string[] args)
        {

            Console.Title = "CarTeLL";
            Console.CursorVisible = false;

            brands.Add(new Brands { Name = "MBZ" });
            brands.Add(new Brands { Name = "BMW" });
            brands.Add(new Brands { Name = "TOYOTA" });
            brands.Add(new Brands { Name = "NISSAN" });



            try
            {
                using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    var db = (CarContext)bf.Deserialize(file);

                    brands = db.Brands;
                    models = db.Models;
                    cars = db.Cars;

                    Brands.SetCounter(brands[brands.Count - 1].Id);
                    Models.SetCounter(models[models.Count - 1].Id);
                    Cars.SetCounter(cars[cars.Count - 1].Id);
                }
            }
            catch (Exception)
            {
            }


            Logo();


        l1:
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
            switch (selectedIndex)
            {
                case 0:
                    int brandID;

                    string promptBrand = @"
                                                  
                                      ____  _____            _   _ _____   _____ 
                                     |  _ \|  __ \     /\   | \ | |  __ \ / ____|
                                     | |_) | |__) |   /  \  |  \| | |  | | (___  
                                     |  _ <|  _  /   / /\ \ | . ` | |  | |\___ \ 
                                     | |_) | | \ \  / ____ \| |\  | |__| |____) |
                                     |____/|_|  \_\/_/    \_\_| \_|_____/|_____/ 
                                            

";
                    string[] optionsBrand = { @"
                                                   A D D   B R A N D", @"
                                                  E D I T   B R A N D", @"
                                                R E M O V E   B R A N D", @"
                                              L I S T   O F   B R A N D S", @"
                                           B A C K   T O   M A I N   M E N U" };



                    Menu brandMenu = new Menu(promptBrand, optionsBrand);
                    int selectedIndexBrand = brandMenu.Run();

                    switch (selectedIndexBrand)
                    {
                        case 0:
                            Console.Clear();
                            Brands brand = new Brands();
                            brand.Name = Scanner.ReadString("Brand Name: ");
                            brands.Add(brand);
                            Console.Clear();
                            goto case 3;


                        case 1:
                            Console.Clear();
                            GetAllBrands();
                        l2:
                            brandID = Scanner.ReadInteger("Brand ID: ");

                            var foundBrand = brands.Find(b => b.Id == brandID);

                            if (foundBrand == null)
                            {
                                Console.WriteLine("You must select from the list: ");
                                goto l2;
                            }
                            foundBrand.Name = Scanner.ReadString("Brand name: ");

                            goto case 3;


                        case 2:
                            Console.Clear();
                            GetAllBrands();
                        l3:
                            brandID = Scanner.ReadInteger("Brand ID: ");

                            if (!brands.Exists(b => b.Id == brandID))
                            {
                                Console.WriteLine("You must select from the list ");
                                goto l3;
                            }

                            Brands found = brands.Find(b => b.Id == brandID);
                            brands.Remove(found);
                            goto case 3;


                        case 3:
                            Console.Clear();
                            GetAllBrands();
                            Console.WriteLine();
                            Console.WriteLine("Press ENTER to return menu...");
                            Console.ReadKey();
                            goto l1;
                        case 4:
                            Console.Clear();
                            goto l1;
                    }
                    return;

                case 1:
                    string promptModel = @"

                                       __  __  ____  _____  ______ _       _____ 
                                      |  \/  |/ __ \|  __ \|  ____| |     / ____|
                                      | \  / | |  | | |  | | |__  | |    | (___  
                                      | |\/| | |  | | |  | |  __| | |     \___ \ 
                                      | |  | | |__| | |__| | |____| |____ ____) |
                                      |_|  |_|\____/|_____/|______|______|_____/ 
                                            

";
                    string[] optionsModel = { @"
                                                   A D D   M O D E L", @"
                                                  E D I T   M O D E L", @"
                                                R E M O V E   M O D E L", @"
                                              L I S T   O F   M O D E L S", @"
                                           B A C K   T O   M A I N   M E N U" };

                    Menu modelMenu = new Menu(promptModel, optionsModel);
                    int selectedIndexModel = modelMenu.Run();


                    int modelID;
                    switch (selectedIndexModel)
                    {
                        case 0:
                            Console.Clear();
                            Console.WriteLine();
                            GetAllBrands();
                            Console.WriteLine("Please select from this list: ");
                            Console.WriteLine();
                            brandID = Scanner.ReadInteger("Brand ID: ");

                            var chooseBrand = brands.Find(b => b.Id == brandID);

                            if (chooseBrand == null)
                            {
                                Console.WriteLine("This brand is not exists. Please create a new brand.");
                                goto case 0;
                            }
                            Models model = new Models();
                            model.Brand = chooseBrand.Id;
                            Console.Clear();
                            model.Name = Scanner.ReadString("Model Name: ");
                            models.Add(model);
                            Console.Clear();
                            goto case 3;


                        case 1:
                            Console.Clear();
                            GetAllBrands();
                            GetAllModels();

                        l4:
                            modelID = Scanner.ReadInteger("Model ID: ");

                            var foundModel = models.Find(m => m.Id == modelID);

                            if (foundModel == null)
                            {
                                Console.WriteLine("You must select from the list: ");
                                goto l4;
                            }
                            foundModel.Name = Scanner.ReadString("Model name: ");

                            goto case 3;


                        case 2:
                            Console.Clear();
                            GetAllBrands();
                            GetAllModels();
                        l5:
                            modelID = Scanner.ReadInteger("Model ID: ");

                            if (!models.Exists(m => m.Id == modelID))
                            {
                                Console.WriteLine("You must select from the list ");
                                goto l5;
                            }

                            Models found2 = models.Find(m => m.Id == modelID);

                            models.Remove(found2);

                            goto case 3;


                        case 3:
                            Console.Clear();
                            Console.WriteLine();
                            GetAllBrands();
                            GetAllModels();
                            Console.WriteLine();
                            Console.WriteLine("Press ENTER to return menu...");
                            Console.ReadKey();
                            goto l1;
                        case 4:
                            Console.Clear();
                            goto l1;
                    }
                    return;
                case 2:

                    string promptCar = @"

                                             _____          _____   _____ 
                                            / ____|   /\   |  __ \ / ____|
                                           | |       /  \  | |__) | (___  
                                           | |      / /\ \ |  _  / \___ \ 
                                           | |____ / ____ \| | \ \ ____) |
                                            \_____/_/    \_\_|  \_\_____/ 

                                
                                ";
                    string[] optionsCar = { @"
                                                    A D D   C A R", @"
                                                   E D I T   C A R", @"
                                                 R E M O V E   C A R", @"
                                               L I S T   O F   C A R S", @"
                                          B A C K   T O   M A I N   M E N U" };

                    Menu carMenu = new Menu(promptCar, optionsCar);
                    int selectedIndexCar = carMenu.Run();

                    switch (selectedIndexCar)
                    {
                        case 0:
                            //select brand111
                            Console.Clear();
                            Console.WriteLine("Please select brand from this list: ");
                            GetAllBrands();
                            brandID = Scanner.ReadInteger("Brand ID: ");

                            var chooseBrands = brands.Find(b => b.Id == brandID);

                            if (chooseBrands == null)
                            {
                                Console.WriteLine("This brand is not exists. Please create a new brand.");
                                goto l1;
                            }


                            //select model
                            Console.Clear();
                            GetAllBrands();

                            GetAllModels();
                            Console.WriteLine();
                            modelID = Scanner.ReadInteger("Model ID: ");

                            var chooseModels = models.Find(m => m.Id == modelID);

                            if (chooseModels == null)
                            {
                                Console.WriteLine("This model is not exists. Please create a new model.");
                                goto l1;
                            }

                            Cars car = new Cars();
                            car.Brand = chooseBrands.Id;
                            car.Model = chooseModels.Id;
                            Console.Clear();
                            car.Year = Scanner.ReadInteger("Year: ");
                            Console.Clear();
                            car.VIN = Scanner.ReadString("VIN: ");
                            Console.Clear();
                            car.Color = Scanner.ReadString("Color: ");
                            Console.Clear();
                            car.Engine = Scanner.ReadDouble("Engine: ");
                            Console.Clear();
                            car.HP = Scanner.ReadInteger("Horse Power: ");
                            Console.Clear();
                            car.Fuel = Scanner.ReadString("Fuel: ");
                            Console.Clear();
                            car.Mileage = Scanner.ReadDouble("Mileage: ");
                            Console.Clear();
                            car.Gearbox = Scanner.ReadString("Gearbox: ");
                            Console.Clear();
                            car.AllWheelDrive = Scanner.ReadString("AllWheelDrive: ");
                            Console.Clear();
                            car.New = Scanner.ReadBoolean("New: ");
                            Console.Clear();
                            car.Price = Scanner.ReadDouble("Price: ");
                            cars.Add(car);
                            Console.Clear();
                            goto case 3;


                        case 1://edit
                            Console.Clear();
                            GetAllCars();

                        l5:
                            int carID = Scanner.ReadInteger("Car ID: ");

                            var foundCar = cars.Find(c => c.Id == carID);

                            if (foundCar == null)
                            {
                                Console.WriteLine("You must select from the list: ");
                                goto l5;
                            }
                            Console.Clear();
                            foundCar.Year = Scanner.ReadInteger("Year: ");
                            Console.Clear();
                            foundCar.VIN = Scanner.ReadString("VIN: ");
                            Console.Clear();
                            foundCar.Color = Scanner.ReadString("Color: ");
                            Console.Clear();
                            foundCar.Engine = Scanner.ReadDouble("Engine: ");
                            Console.Clear();
                            foundCar.HP = Scanner.ReadInteger("Horse Power: ");
                            Console.Clear();
                            foundCar.Fuel = Scanner.ReadString("Fuel: ");
                            Console.Clear();
                            foundCar.Mileage = Scanner.ReadDouble("Mileage: ");
                            Console.Clear();
                            foundCar.Gearbox = Scanner.ReadString("Gearbox: ");
                            Console.Clear();
                            foundCar.AllWheelDrive = Scanner.ReadString("All Wheel Drive: ");
                            Console.Clear();
                            foundCar.New = Scanner.ReadBoolean("New: ");
                            Console.Clear();
                            foundCar.Price = Scanner.ReadDouble("Price: ");
                            Console.Clear();
                            goto case 3;
                        case 2:
                            Console.Clear();
                            GetAllCars();
                        l6:
                            carID = Scanner.ReadInteger("Car ID: ");

                            if (!cars.Exists(c => c.Id == carID))
                            {
                                Console.WriteLine("You must select from the list ");
                                goto l6;
                            }

                            Cars found = cars.Find(c => c.Id == carID);

                            cars.Remove(found);

                            goto case 3;



                        case 3:
                            Console.Clear();
                            Console.WriteLine();
                            GetAllCars();
                            Console.WriteLine();
                            Console.WriteLine("Press ENTER to return menu...");
                            Console.ReadKey();
                            goto l1;
                        case 4:
                            goto l1;
                    }
                    return;

                case 3: SaveData(); goto l1;
                case 4: SaveData(); goto case 5;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Press ENTER to exit.");
                    break;
            }
        }

        public static void GetAllBrands()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" L I S T   O F   B R A N D S ");
            Console.WriteLine();
            Console.ResetColor();
            foreach (var item in brands)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        private static void GetAllModels()
        {
            Console.WriteLine();
            Console.WriteLine("Select Brand ID to view the Models");
            int brandID = Scanner.ReadInteger("BRAND ID: ");
            Console.Clear();

            if (brandID > 0)
            {
                var chooseModel = brands.Find(s => s.Id == brandID);

                var chooseItems = models.FindAll(s => s.Brand == brandID);

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($" M O D E L S   O F   [{chooseModel.Name}] ");
                Console.WriteLine();
                Console.ResetColor();
                foreach (var item in chooseItems)
                {
                    var brand = brands.Find(b => b.Id == item.Brand);

                    Console.WriteLine($"{item}");
                }
            }
            else
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" L I S T   O F   M O D E L S ");
                Console.WriteLine();
                Console.ResetColor();
                foreach (var item in models)
                {
                    var brand = brands.Find(b => b.Id == item.Brand);

                    Console.WriteLine($"{brand.Name}: {item}");
                }
            }


        }

        private static void GetAllCars()
        {
            GetAllBrands();
            Console.WriteLine();

            GetAllModels();
            Console.WriteLine();
            int modelID = Scanner.ReadInteger("MODEL ID: ");
            Console.Clear();


            if (modelID > 0)
            {
                var chooseCar = models.Find(s => s.Id == modelID);

                var chooseItems = cars.FindAll(s => s.Model == modelID);

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($" C A R S   O F   [{chooseCar.Name}] ");
                Console.ResetColor();
                Console.WriteLine();



                foreach (var item in chooseItems)
                {
                    var model = models.Find(b => b.Id == item.Model);

                    Console.WriteLine($"{item}");
                }
            }
            else
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" L I S T   O F   C A R S ");
                Console.ResetColor();
                Console.WriteLine();

                foreach (var item in cars)
                {
                    var model = models.Find(b => b.Id == item.Model);

                    Console.WriteLine($"{model.Name}: {item}");
                }
            }


        }

        private static void SaveData()
        {

            CarContext cr = new CarContext();
            cr.Brands = brands;
            cr.Models = models;
            cr.Cars = cars;

            using (var file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(file, cr);
            }
        }

        private static void Logo()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
             ▄▄▄▄▄▄▄▄▄▄▄ 
            ▐░░░░░░░░░░░▌
            ▐░█▀▀▀▀▀▀▀▀▀ 
            ▐░▌          
            ▐░▌          
            ▐░▌          
            ▐░▌          
            ▐░▌          
            ▐░█▄▄▄▄▄▄▄▄▄ 
            ▐░░░░░░░░░░░▌
             ▀▀▀▀▀▀▀▀▀▀▀ ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"  
                          ▄▄▄▄▄▄▄▄▄▄▄ 
                         ▐░░░░░░░░░░░▌
                         ▐░█▀▀▀▀▀▀▀█░▌
                         ▐░▌       ▐░▌
                         ▐░█▄▄▄▄▄▄▄█░▌
                         ▐░░░░░░░░░░░▌
                         ▐░█▀▀▀▀▀▀▀█░▌
                         ▐░▌       ▐░▌
                         ▐░▌       ▐░▌
                         ▐░▌       ▐░▌
                          ▀         ▀ ");

            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
                                        ▄▄▄▄▄▄▄▄▄▄▄ 
                                       ▐░░░░░░░░░░░▌
                                       ▐░█▀▀▀▀▀▀▀█░▌
                                       ▐░▌       ▐░▌
                                       ▐░█▄▄▄▄▄▄▄█░▌
                                       ▐░░░░░░░░░░░▌
                                       ▐░█▀▀▀▀█░█▀▀ 
                                       ▐░▌     ▐░▌  
                                       ▐░▌      ▐░▌ 
                                       ▐░▌       ▐░▌
                                        ▀         ▀ ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
                                                     ▄▄▄▄▄▄▄▄▄▄▄ 
                                                    ▐░░░░░░░░░░░▌
                                                     ▀▀▀▀█░█▀▀▀▀ 
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                          ▀  ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
                                                                   ▄▄▄▄▄▄▄▄▄▄▄ 
                                                                  ▐░░░░░░░░░░░▌
                                                                  ▐░█▀▀▀▀▀▀▀▀▀ 
                                                                  ▐░▌          
                                                                  ▐░█▄▄▄▄▄▄▄▄▄ 
                                                                  ▐░░░░░░░░░░░▌
                                                                  ▐░█▀▀▀▀▀▀▀▀▀ 
                                                                  ▐░▌          
                                                                  ▐░█▄▄▄▄▄▄▄▄▄ 
                                                                  ▐░░░░░░░░░░░▌
                                                                   ▀▀▀▀▀▀▀▀▀▀▀ ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
                                                                                 ▄           
                                                                                ▐░▌          
                                                                                ▐░▌          
                                                                                ▐░▌          
                                                                                ▐░▌          
                                                                                ▐░▌          
                                                                                ▐░▌          
                                                                                ▐░▌          
                                                                                ▐░█▄▄▄▄▄▄▄▄▄ 
                                                                                ▐░░░░░░░░░░░▌
                                                                                 ▀▀▀▀▀▀▀▀▀▀▀ ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
                                                                                               ▄           
                                                                                              ▐░▌          
                                                                                              ▐░▌          
                                                                                              ▐░▌          
                                                                                              ▐░▌          
                                                                                              ▐░▌          
                                                                                              ▐░▌          
                                                                                              ▐░▌          
                                                                                              ▐░█▄▄▄▄▄▄▄▄▄ 
                                                                                              ▐░░░░░░░░░░░▌
                                                                                               ▀▀▀▀▀▀▀▀▀▀▀ ");
            
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
                                                                   ▄▄▄▄▄▄▄▄▄▄▄ 
                                                                  ▐░░░░░░░░░░░▌
                                                                  ▐░█▀▀▀▀▀▀▀▀▀ 
                                                                  ▐░▌          
                                                                  ▐░█▄▄▄▄▄▄▄▄▄ 
                                                                  ▐░░░░░░░░░░░▌
                                                                  ▐░█▀▀▀▀▀▀▀▀▀ 
                                                                  ▐░▌          
                                                                  ▐░█▄▄▄▄▄▄▄▄▄ 
                                                                  ▐░░░░░░░░░░░▌
                                                                   ▀▀▀▀▀▀▀▀▀▀▀ ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
                                                     ▄▄▄▄▄▄▄▄▄▄▄ 
                                                    ▐░░░░░░░░░░░▌
                                                     ▀▀▀▀█░█▀▀▀▀ 
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                         ▐░▌     
                                                          ▀  ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
                                        ▄▄▄▄▄▄▄▄▄▄▄ 
                                       ▐░░░░░░░░░░░▌
                                       ▐░█▀▀▀▀▀▀▀█░▌
                                       ▐░▌       ▐░▌
                                       ▐░█▄▄▄▄▄▄▄█░▌
                                       ▐░░░░░░░░░░░▌
                                       ▐░█▀▀▀▀█░█▀▀ 
                                       ▐░▌     ▐░▌  
                                       ▐░▌      ▐░▌ 
                                       ▐░▌       ▐░▌
                                        ▀         ▀ ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"  
                          ▄▄▄▄▄▄▄▄▄▄▄ 
                         ▐░░░░░░░░░░░▌
                         ▐░█▀▀▀▀▀▀▀█░▌
                         ▐░▌       ▐░▌
                         ▐░█▄▄▄▄▄▄▄█░▌
                         ▐░░░░░░░░░░░▌
                         ▐░█▀▀▀▀▀▀▀█░▌
                         ▐░▌       ▐░▌
                         ▐░▌       ▐░▌
                         ▐░▌       ▐░▌
                          ▀         ▀ ");
            Thread.Sleep(10);
            Console.Clear();
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.Write(@"
             ▄▄▄▄▄▄▄▄▄▄▄ 
            ▐░░░░░░░░░░░▌
            ▐░█▀▀▀▀▀▀▀▀▀ 
            ▐░▌          
            ▐░▌          
            ▐░▌          
            ▐░▌          
            ▐░▌          
            ▐░█▄▄▄▄▄▄▄▄▄ 
            ▐░░░░░░░░░░░▌
             ▀▀▀▀▀▀▀▀▀▀▀ ");

            Thread.Sleep(10);
            Console.Clear();
            Console.CursorTop = 10;
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(100);

                Console.Clear();
                Thread.Sleep(100);
                Console.CursorTop = 10;
                Console.WriteLine(@"
                 ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄            ▄           
                ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌          ▐░▌          
                ▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌ ▀▀▀▀█░█▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░▌          ▐░▌          
                ▐░▌          ▐░▌       ▐░▌▐░▌       ▐░▌     ▐░▌     ▐░▌          ▐░▌          ▐░▌          
                ▐░▌          ▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄█░▌     ▐░▌     ▐░█▄▄▄▄▄▄▄▄▄ ▐░▌          ▐░▌          
                ▐░▌          ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌     ▐░▌     ▐░░░░░░░░░░░▌▐░▌          ▐░▌          
                ▐░▌          ▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀█░█▀▀      ▐░▌     ▐░█▀▀▀▀▀▀▀▀▀ ▐░▌          ▐░▌          
                ▐░▌          ▐░▌       ▐░▌▐░▌     ▐░▌       ▐░▌     ▐░▌          ▐░▌          ▐░▌          
                ▐░█▄▄▄▄▄▄▄▄▄ ▐░▌       ▐░▌▐░▌      ▐░▌      ▐░▌     ▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄ 
                ▐░░░░░░░░░░░▌▐░▌       ▐░▌▐░▌       ▐░▌     ▐░▌     ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌
                 ▀▀▀▀▀▀▀▀▀▀▀  ▀         ▀  ▀         ▀       ▀       ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀ 
                                                                                           
");
            }
            Thread.Sleep(1000);

        }
    }
}