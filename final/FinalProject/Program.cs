using System;
using System.Diagnostics; // gives me acess to the stopwatch. 
using System.Runtime.InteropServices;

class Program
{

    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);

    static void Main(string[] args)
    {
        // ! Too much time waisted
        // Console.Out.Flush();
        // Console.SetOut(TextWriter.Synchronized(Console.Out));
        // Console.OutputEncoding = System.Text.Encoding.Unicode;
        // System.Reflection.PropertyInfo fontProp = typeof(Console).GetProperty("Font", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        // if (fontProp != null)
        // {
        //     Console.Font = new System.Drawing.Font("Arial Unicode MS", Console.Font.Size);
        // }

        // * revised character 
        // * Player: } 
        // * Player Projectiles: - / \ 
        // * Enemy: { ~ ]
        // * Enemy Projectiles: o 0 x +
        // * Sheild: ] 
        // * Towers: (C O) >
        // * Health bar: [][][][][][][][][][] 
        // Initialize classes 
        Screen windowSize = new Screen();
        Stopwatch stopwatch = new Stopwatch();


        Animation startAnimation = new Animation();
        startAnimation.SetFrames(30);

        // Initialize variables
        List<string> lineProjectile = new List<string>
        {
            "-"
        };

        HashSet<ConsoleKey> keysPressed = new HashSet<ConsoleKey>();
        List<Background> backgrounds = new List<Background>();
        List<Projectile> projectiles = new List<Projectile>();
        // ! Ill have to setup the player class individually. 
        List<Enemy> enemies = new List<Enemy>();
        List<Structures> structures = new List<Structures>();

        Animation baseBulletSpeed = new Animation(); 

        Projectile baseProjectile = new Projectile(0, 0, lineProjectile, 3, 3, true, baseBulletSpeed);

        string scene = "start";
        int fps = 60;

        double frameDuration = 1000.0 / fps;

        int lastWidth = 0;
        int lastHeight = 0;

        int frameCounter = 0;

        bool screenSizeChanged = false;

        bool sceneChange = true; 

        // Initilize other

        stopwatch.Start();

        LoadScreen game = null;
        LoadScreen start = null;
        LoadScreen gameOver = null;

        Player player = null;

        // ! pregame test
        // ! End of test area 


        Console.CursorVisible = false;
        // Game loop
        while (true)
        {
            double elapsedTime = stopwatch.ElapsedMilliseconds;
            // * I hope this will auto refresh every frame. 
            if (elapsedTime >= 60000)
            {
                frameCounter = 0;
                stopwatch.Reset();
                stopwatch.Restart();
            }
            else
            {
                frameCounter = (int)(elapsedTime / 16.67);
            }

            // * Check window size 
            windowSize.CheckSize();

            int screenWidth = windowSize.GetWidth();
            int screenHeight = windowSize.GetHeight();

            List<int> screenRect = windowSize.GetScreenRect();

            // Todo: Handle keyboard events
            keysPressed.Clear();

            for (int i = 0; i <= 255; i++)
            {
                short keyState = GetAsyncKeyState(i);
                if (keyState == -32767)
                {
                    ConsoleKey consoleKey = (ConsoleKey)i;
                    keysPressed.Add(consoleKey);
                }
            }

            // Todo: Allow entitys to update

            // Todo: Draw the scene 

            // ! checking if the screen size has changed. 
            if (lastWidth != screenWidth || lastHeight != screenHeight)
            {
                lastWidth = screenWidth;
                lastHeight = screenHeight;
                screenSizeChanged = true;
            }
            else
            {
                screenSizeChanged = false;
            }

            if (screenWidth <= 80 || screenHeight <= 30)
            {   
                Console.Clear();
                scene = "small screen";
                sceneChange = true; 
            }

            if (scene == "start")
            {
                if (sceneChange)
                {
                    Console.Clear();

                    start = new LoadScreen();

                    // ! Draws the title 
                    List<string> title = new List<string>
                    {
                    "          Terminal WAR",
                    "'`'`'`'`'`            '`'`'`'`'`"}; // TODO: It would be cool to add an animation where the word dropped in, one letter at a time. 

                    List<string> startText = new List<string>
                    {"Press Enter to Start: "};

                    List<string> highScore = new List<string>
                    {"High Score: {highScoreNumber}"};


                    Background titleObject = new Background(0, 0, title);

                    start.AddBackground(titleObject);

                    Background startObject = new Background(0, 0, startText);

                    start.AddBackground(startObject);

                    Background highScoreObject = new Background(0, 0, highScore);

                    start.AddBackground(highScoreObject);

                    backgrounds = start.GetBackground();

                    start.Update(keysPressed, screenRect, frameCounter);

                    // Set the starting positin of the titleObject. 
                    int width = backgrounds[0].GetWidth();
                    backgrounds[0].SetLocation((screenWidth - width) / 2, 3);

                    // Set the starting position of the startObject. 
                    width = backgrounds[1].GetWidth();
                    backgrounds[1].SetLocation((screenWidth - width) / 2, 12);

                    // Set the starting positoin of the highScoreObject. 
                    width = backgrounds[2].GetWidth();
                    backgrounds[2].SetLocation((screenWidth - width) / 2, 18);

                    // Draws the starting screen. 
                    start.Redraw(); 

                    sceneChange = false;
                }

                // Animate the press Enter to start. 
                backgrounds = start.GetBackground(); 

                startAnimation.SetFrames(30); 
                startAnimation.Animate(frameCounter);
                    int change = startAnimation.GetTimes();

                    if (change % 2 == 0)
                    {
                        backgrounds[1].Clear();
                    }
                    else
                    {
                        backgrounds[1].Draw();
                    }

                if (keysPressed.Contains(ConsoleKey.Enter))
                {
                    Console.Clear();
                    scene = "game";
                    sceneChange = true;
                }
            }
            else if (scene == "small screen") // I actually really like the weird blinking effect I got here. 
            {
                Console.Clear();
                Console.WriteLine(" --- Your screen is too small ---");
                Console.WriteLine(" --- increase the size to play the game. ---");

                if (screenWidth > 80 && screenHeight > 30)
                {
                    scene = "start";
                    sceneChange = true;
                    Console.Clear();
                }
            }
            else if (scene == "game")
            {

                if (game == null)
                {
                    List<string> gameBackground = new List<string> // ! Later we will have to update this background to change height depending on the current wave
                    {
                    " ____________________________________________________________________________",
                    "|                                                                            |",
                    "|                                                                            |",
                    "|                                                                            |",
                    "|                                                                            |",
                    "|                                                                            |",
                    "||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||"
                    };

                    List<string> playerIcon = new List<string>
                    {
                        "}"
                    };

                    // Add background images. 
                    Background playfeild = new Background(0, 0, gameBackground); // * first you need to draw out the object. 

                    game = new LoadScreen();
                    game.AddBackground(playfeild);

                    backgrounds = game.GetBackground();
                    List<int> playSpace = backgrounds[0].GetRect();
                    game.Update(keysPressed, playSpace, frameCounter);
                    int width = backgrounds[0].GetWidth(); //* Then you need to place it in the middle of the screen by using (width - screenWidth) / 2

                    int gameLeft = (screenWidth - width) / 2; // ! I made this so that I can calculate where the Health, Energy, Button options, and wave display will show up... Though maybe Ill put the wave in the true middle. 
                    backgrounds[0].SetLocation(gameLeft, 1);

                    game.SetBackground(backgrounds);

                    // Add the player 
                    Player localPlayer1 = new Player(gameLeft + 3, 3, playerIcon, 300, baseProjectile);

                    game.AddPlayer(localPlayer1);

                }



                if (screenSizeChanged == true)
                {
                    Player localPlayer2 = game.GetPlayers();
                    localPlayer2.SetDimensions();
                    game.SetPlayer(ref localPlayer2);
                    // Redrawing the background if the screen size has changed. 
                    backgrounds = game.GetBackground();
                    List<int> playSpace = backgrounds[0].GetRect();
                    game.Update(keysPressed, playSpace, frameCounter);
                    int width = backgrounds[0].GetWidth();
                    int gameLeft = (screenWidth - width) / 2;
                    backgrounds[0].SetLocation(gameLeft, 1);
                    game.SetBackground(backgrounds);
                }

                player = game.GetPlayers();
                player.SetDimensions();

                backgrounds = game.GetBackground();
                List<int> playerSpace = backgrounds[0].GetRect();
                game.Update(keysPressed, playerSpace, frameCounter);

                // Setup projectile stats
                // Base projectile

                // Check for projectiles. 
                if (keysPressed.Contains(ConsoleKey.Spacebar))
                {   
                    baseProjectile = new Projectile(0, 0, lineProjectile, 3, 3, true, baseBulletSpeed);
                    int x = player.GetX();
                    int y = player.GetY();
                    baseProjectile.SetLocation(x + 1, y);
                    game.AddPlayerProjectile(baseProjectile);
                    game.Update(keysPressed, playerSpace, frameCounter); 

                }
                

                game.Redraw();


                // Todo: Display the health, and have it change if the players total health ever drops. 

                // Todo: Display the action options, and have a blinking line appear under a different option if it is selected. 
                // Todo: Make it so that when the player presses the spacebar, they use an object. 
                // Todo: Make it so that the player can only use an object option if they have enough energy.   
                // Todo: Create objects in reference to the player when he uses them. 
                // Todo: Display the energy. Have it drop when the player uses an option. 
                // Todo: Make the energy and health increase slowly over time. 
                // Todo: Keep track of the wave number. 
                // Todo: Every three waves, increase the size of the game background until at the max size. 
                // Todo: Have enemies randomly spawn in emply spaces in the far row. 
                // Todo: Calculate enemy movements. 
                // Todo: Have enemies randomly spawn projectiles. 
                // Todo: Calculate projectile damage. 
                // Todo: Remove enemies when they die. 
                // Todo: Make objects lose health when hit. 
                // Todo: Make the player lose health when projectiles leave the far left side of the game background. 

                if (keysPressed.Contains(ConsoleKey.K)) // K stands for kill the character. 
                {
                    Console.Clear();
                    scene = "gameover";
                    sceneChange = true;
                }
            }
            else if (scene == "quit")
            {
                // TODO: Write code for the quit screen. 
            }
            else if (scene == "gameover")
            {
                List<string> gameOverWords = new List<string>
                    {
                        "  ________          _",
                        " /        \\        / \\         |\\        /|     |||||||||",
                        "/                 /   \\        | \\      / |     |",
                        "|       ____     /_____\\       |  \\    /  |     |||||||||",
                        "\\          /    /       \\      |   \\  /   |     |",
                        " \\________/    /         \\     |    \\/    |     |||||||||",
                        "",
                        "   ooooo",
                        " o       o                 ______        ____",
                        "o         o   \\      /    /      \\     |/    ",
                        "o         o    \\    /    /  ______\\    |",
                        " o       o      \\  /     |             |",
                        "   ooooo         \\/       \\______/     |"
                    };
                Background gameoverText = new Background(0, 0, gameOverWords);
                LoadScreen gameover = new LoadScreen();
                gameover.AddBackground(gameoverText);

                gameover.Update(keysPressed, screenRect, frameCounter);

                backgrounds = gameover.GetBackground();

                int width = backgrounds[0].GetWidth();
                backgrounds[0].SetLocation((screenWidth - width) / 2, 6);

                gameover.SetBackground(backgrounds);
                gameover.Update(keysPressed, screenRect, frameCounter);
                gameover.Redraw();



                // Todo: Show the players total score. 
                // Todo: Display a flashing: "New High score" If they got a new high score. 
            }


            // ! Test 

            // Todo: Draw the entitys 

            int sleepTime = (int)(frameDuration - elapsedTime % frameDuration);
            System.Threading.Thread.Sleep(sleepTime);
        }
    }
}