using System.Text;

namespace ConwaysGameofLife
{
    class Program
    {
        static int GridWidth = 80;
        static int GridHeight = 25;
        static bool[,] currentGeneration = new bool[GridWidth, GridHeight];
        static bool[,] nextGeneration = new bool[GridWidth, GridHeight];
        static Random random = new Random();

        // Threading
        public static ConsoleKey lastKey;
        public static object Keylock = new object();

        // Ascii variables
        static CharacterAnimationStates CurrentAnimation = CharacterAnimationStates.Static;
        static int XLocation = 10;
        static int YLocation = 2;
        
        
        enum CharacterAnimationStates
        {//the list of animations we have avalible to use
            Movingleft,
            Static,
            MovingRight,
        }
        
        static void Main(string[] args)
        {   //displays menu options
            
            Console.OutputEncoding = Encoding.UTF8;

            Thread inputthreadread = new Thread(inputthereadloop);
            inputthreadread.Start();

            Console.ForegroundColor = ConsoleColor.White;

            bool IsInMenu = true;
            Console.WriteLine("1 - Game of Life");
            Console.WriteLine("2 - Ascii Background");
            Console.WriteLine("3 - Character animation");
            Console.WriteLine("4 - Close application");
            ConsoleKey pressedKey = ConsoleKey.None;
            while (IsInMenu)
            {//menu functions
                if (Console.KeyAvailable)
                {
                    if(Console.ReadKey(true).Key == ConsoleKey.D1)
                    {
                        pressedKey = ConsoleKey.D1;
                        IsInMenu = false;
                    }
                    if (Console.ReadKey(true).Key == ConsoleKey.D2)
                    {
                        pressedKey = ConsoleKey.D2;
                        IsInMenu = false;
                    }
                    if (Console.ReadKey(true).Key == ConsoleKey.D3)
                    {
                        pressedKey = ConsoleKey.D3;
                        IsInMenu = false;
                    }
                    if (Console.ReadKey(true).Key == ConsoleKey.D4)
                    {
                        pressedKey = ConsoleKey.D4;
                        System.Environment.Exit(0);

                    }
                }
            }
            switch(pressedKey)
            {
                case ConsoleKey.D1:
                    gameoflife();
                    break;
                case ConsoleKey.D2:
                    Ascii();
                    break;
                case ConsoleKey.D3:
                    Character();
                    break;
                
            }
            
        }
        
        static void gameoflife()
        {
            InitializeGrid();
            Console.CursorVisible = false;
            ConsoleKey pressedKey = ConsoleKey.None;
            if (Console.ReadKey(true).Key == ConsoleKey.D4)
            {
                pressedKey = ConsoleKey.D4;
                System.Environment.Exit(0);

            }


            while (true)
            {
                Console.Clear();
                DisplayGrid();
                UpdateGrid();
                Thread.Sleep(500); // Adjust the delay 
                Thread.Sleep(200);//to control the speed.
                
            }


        }



        static void Ascii()
        {
            Console.Clear();

            background();

            bool GameRunning = true;

            while (GameRunning)
            {
                Character();
                Thread.Sleep(100);

                //if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                //{
                //pressedkey = ConsoleKey.Escape;
                // GameRunning = false;
                // Console.Clear();
                //Main();
                //}
            }


            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 23);
        }
        static void background()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Clear();
            Console.WriteLine("████████████████████████████████████████████");
            Console.WriteLine("██                                        ██");
            Console.WriteLine("██  ███                             ███   ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██  ███                             ███   ██");
            Console.WriteLine("██                                        ██");
            Console.WriteLine("██  ███                             ███   ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██  ███                             ███   ██");
            Console.WriteLine("██                                        ██");
            Console.WriteLine("██  ███                             ███   ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██   █                               █    ██");
            Console.WriteLine("██  ███                             ███   ██");
            Console.WriteLine("██                                        ██");
            Console.WriteLine("██                                        ██");
            Console.WriteLine("████████████████████████████████████████████");
        }
        static void Character()
        {
            //XLocation
            //YLocation

            Console.ForegroundColor = ConsoleColor.Red;
            //the properties of each animation are set here
            if (CurrentAnimation == CharacterAnimationStates.Movingleft)
            {
                Console.SetCursorPosition(XLocation, YLocation);
                Console.Write(" ████████");
                Console.SetCursorPosition(XLocation, YLocation + 1);
                Console.Write("███ ███ ██");
                Console.SetCursorPosition(XLocation, YLocation + 2);
                Console.Write(" ████████");
                Console.SetCursorPosition(XLocation, YLocation + 3);
                Console.Write(" █ ██████ █");
                Console.SetCursorPosition(XLocation, YLocation + 4);
                Console.Write("  ████  ████");
                Console.SetCursorPosition(XLocation, YLocation + 5);
                Console.Write(" ████   ████");
                Console.SetCursorPosition(XLocation, YLocation + 6);
                Console.Write("████    ████");
            }
            else if (CurrentAnimation == CharacterAnimationStates.Static)
            {
                Console.SetCursorPosition(XLocation, YLocation);
                Console.Write("████████");
                Console.SetCursorPosition(XLocation, YLocation + 1);
                Console.Write("███ ███ ██");
                Console.SetCursorPosition(XLocation, YLocation + 2);
                Console.Write(" █████ ███");
                Console.SetCursorPosition(XLocation, YLocation + 3);
                Console.Write("█ ██████ █");
                Console.SetCursorPosition(XLocation, YLocation + 4);
                Console.Write("█ ██████ █");
                Console.SetCursorPosition(XLocation, YLocation + 5);
                Console.Write("█ ██████ █");
                Console.SetCursorPosition(XLocation, YLocation + 6);
                Console.Write("████  ████"); 
               
            }
            else if (CurrentAnimation == CharacterAnimationStates.MovingRight)
            {
                Console.SetCursorPosition(XLocation, YLocation);
                Console.Write(" ████████");
                Console.SetCursorPosition(XLocation, YLocation + 1);
                Console.Write("███ ███ ██");
                Console.SetCursorPosition(XLocation, YLocation + 2);
                Console.Write("█████ ███");
                Console.SetCursorPosition(XLocation, YLocation + 3);
                Console.Write("█ ██████ █");
                Console.SetCursorPosition(XLocation, YLocation + 4);
                Console.Write("█ ██████ █");
                Console.SetCursorPosition(XLocation, YLocation + 5);
                Console.Write(" ████   ████");
                Console.SetCursorPosition(XLocation, YLocation + 6);
                Console.Write(" ████     ████");
            }

        }
        static void InitializeGrid()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    currentGeneration[x, y] = random.Next(2) == 0;
                }
            }
        }

        static void DisplayGrid()
        {
            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    Console.Write(currentGeneration[x, y] ? "█" : " ");
                }
                Console.WriteLine();
            }
        }

        static void UpdateGrid()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int liveNeighbors = CountLiveNeighbors(x, y);

                    if (currentGeneration[x, y])
                    {
                        // Apply the rules for live cells.
                        nextGeneration[x, y] = liveNeighbors == 2 || liveNeighbors == 3;
                    }
                    else
                    {
                        // Apply the rules for dead cells.
                        nextGeneration[x, y] = liveNeighbors == 3;
                    }
                }
            }

            // Swap current and next generation grids.
            bool[,] temp = currentGeneration;
            currentGeneration = nextGeneration;
            nextGeneration = temp;
        }

        static int CountLiveNeighbors(int x, int y)
        {
            int count = 0;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && nx < GridWidth && ny >= 0 && ny < GridHeight &&
                      currentGeneration[nx, ny])
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        static void inputthereadloop()
        {//makes animations work on button press and transitions between them when used
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    lock (Keylock)
                    {
                        lastKey = Console.ReadKey().Key;

                        if (lastKey == ConsoleKey.LeftArrow || lastKey == ConsoleKey.A)
                        {
                            CurrentAnimation = CharacterAnimationStates.Movingleft;
                            XLocation--;
                            if (XLocation >= 10)
                            {
                                XLocation--;
                            }
                        }
                        else if (lastKey == ConsoleKey.RightArrow || lastKey == ConsoleKey.D)
                        {
                            CurrentAnimation = CharacterAnimationStates.MovingRight;
                            if (XLocation >= 10)
                            {
                                XLocation++;
                            }
                        }
                        else if (lastKey == ConsoleKey.DownArrow || lastKey == ConsoleKey.S)
                        {
                            CurrentAnimation = CharacterAnimationStates.Static;
                        }
                        if (YLocation >= 2)
                        {
                           YLocation--;
                        }
                        else
                        {
                            CurrentAnimation = CharacterAnimationStates.Static;
                        }

                    }
                }
            }
        }
    }
}
