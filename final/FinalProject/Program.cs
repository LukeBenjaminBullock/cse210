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

        Animation spawnClock = new Animation(); 
        spawnClock.SetFrames(600);

        Animation shootClock = new Animation(); 
        shootClock.SetFrames(120);

        // Initialize variables
        List<string> lineProjectile = new List<string>
        {
            "-"
        };

        List<string> circleProjectile = new List<string>
        {
            "o"
        };

        List<string> basicEnemy = new List<string>
        {
            "{"
        };

        HashSet<ConsoleKey> keysPressed = new HashSet<ConsoleKey>();
        List<Background> backgrounds = new List<Background>();
        List<Projectile> enemyProjectiles = new List<Projectile>();
        List<Projectile> playerProjectiles = new List<Projectile>(); 
        // ! Ill have to setup the player class individually. 
        List<Enemy> enemies = new List<Enemy>();
        List<Structures> structures = new List<Structures>();

        Animation baseBulletSpeed = new Animation(); 

        Projectile baseProjectile = new Projectile(0, 0, lineProjectile, true);

        Projectile baseEnemyProjectile = new Projectile(0, 0, circleProjectile, false);

        string scene = "start";
        int fps = 60;

        double frameDuration = 1000.0 / fps;

        int lastWidth = 0;
        int lastHeight = 0;

        int frameCounter = 0;

        bool screenSizeChanged = false;

        bool sceneChange = true; 

        int level = 0;
        int spawnRate = 600;
        int bulletRate = 120;
        int enemyNumber = 1; 
        int enemiesKilled = 0;
        int currentScore = 0;
        bool skipSpawn = false; 
        int healthDisplayNumber = 55;
        string healthDisplayString = string.Concat(Enumerable.Repeat("[]", healthDisplayNumber));

        // Initilize other

        stopwatch.Start();

        Random random = new Random(); 

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
            if (frameCounter < 6000) // The framerate resets every 100 seconds. 
            {
                frameCounter += 1;
            }
            else
            {
                frameCounter = 0;
                stopwatch.Reset();
                stopwatch.Restart();
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

                if (sceneChange)
                {

                    level = 1;
                    spawnRate = 600; 

                    List<string> gameBackground = new List<string> // ! Later we will have to update this background to change height depending on the current wave
                    {
                    " _____________________________________________________________________________________________________________________",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|                                                                                                                     |",
                    "|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||"
                    };
                    

                    List<string> startingLevelDisplay = new List<string>
                    {
                        $"Level: {level}"
                    };

                    List<string> startingHealthDisplay = new List<string>
                    {
                        $"Health: {healthDisplayString}"
                    };

                    List<string> startingScoreDisplay = new List<string>
                    {
                        $"Score: {currentScore}"
                    };

                    List<string> playerIcon = new List<string>
                    {
                        "}"
                    };

                    // Add background images. 
                    Background playfeild = new Background(0, 0, gameBackground); // * first you need to draw out the object. 
                    Background levelBackground = new Background(0, 0, startingLevelDisplay);
                    Background healthBackground = new Background(0, 0, startingHealthDisplay);
                    Background scoreBackground = new Background(0, 0, startingScoreDisplay);

                    game = new LoadScreen();
                    game.AddBackground(playfeild);
                    game.AddBackground(levelBackground);
                    game.AddBackground(healthBackground);
                    game.AddBackground(scoreBackground);

                    backgrounds = game.GetBackground();
                    List<int> playingSpace = backgrounds[0].GetRect();
                    List<int> levelSpace = backgrounds[1].GetRect();
                    game.Update(keysPressed, playingSpace, frameCounter);
                    int width = backgrounds[0].GetWidth(); //* Then you need to place it in the middle of the screen by using (width - screenWidth) / 2
                    int levelWidth = backgrounds[1].GetWidth();

                    int gameLeft = (screenWidth - width) / 2; // ! I made this so that I can calculate where the Health, Energy, Button options, and wave display will show up... Though maybe Ill put the wave in the true middle. 
                    int levelLeft = (screenWidth - levelWidth) / 2;
                    backgrounds[0].SetLocation(gameLeft, 6);
                    backgrounds[1].SetLocation(levelLeft, 2);
                    backgrounds[2].SetLocation(gameLeft, 4);
                    backgrounds[3].SetLocation(levelLeft, screenHeight - 3);

                    game.SetBackground(backgrounds);

                    // Add the player 
                    Player localPlayer1 = new Player(gameLeft + 3, 10, playerIcon, 55, baseProjectile);

                    game.AddPlayer(localPlayer1);

                    sceneChange = false;

                }

                player = game.GetPlayers();
                player.SetDimensions();

                // setting various variables. 
                healthDisplayNumber = player.GetHealth();
                healthDisplayString = string.Concat(Enumerable.Repeat("[]", healthDisplayNumber));

                List<string> levelDisplay = new List<string>
                {
                    $"Level: {level}"
                };

                List<string> healthDisplay = new List<string>
                {
                    $"Health: {healthDisplayString}"
                };

                List<string> scoreDisplay = new List<string>
                {
                    $"Score: {currentScore}"
                };

                backgrounds = game.GetBackground();
                backgrounds[1].SetImage(levelDisplay);
                backgrounds[2].SetImage(healthDisplay);
                backgrounds[3].SetImage(scoreDisplay);

                List<int> playSpace = backgrounds[0].GetRect();

                game.SetBackground(backgrounds);



                game.Update(keysPressed, playSpace, frameCounter);

                if (screenSizeChanged == true)
                {
                    Console.Clear(); 
                    Player localPlayer2 = game.GetPlayers();
                    localPlayer2.SetDimensions();
                    game.SetPlayer(ref localPlayer2);
                    // Redrawing the background if the screen size has changed. 
                    backgrounds = game.GetBackground();
                    playSpace = backgrounds[0].GetRect();
                    List<int> levelSpace = backgrounds[1].GetRect();
                    game.Update(keysPressed, playSpace, frameCounter);
                    int width = backgrounds[0].GetWidth();
                    int levelWidth = backgrounds[1].GetWidth();
                    int gameLeft = (screenWidth - width) / 2;
                    int levelLeft = (screenWidth - levelWidth) / 2;
                    backgrounds[0].SetLocation(gameLeft, 6);
                    backgrounds[1].SetLocation(levelLeft, 2);
                    backgrounds[2].SetLocation(gameLeft, 4);
                    backgrounds[3].SetLocation(levelLeft, screenHeight - 3);
                    game.SetBackground(backgrounds);
                }

                player = game.GetPlayers();
                player.SetDimensions();

                backgrounds = game.GetBackground();
                List<int> playerSpace = backgrounds[0].GetRect();
                

                // Setup projectile stats
                // Base projectile

                // Check for player projectiles. 
                if (keysPressed.Contains(ConsoleKey.Spacebar))
                {   
                    baseProjectile = new Projectile(0, 0, lineProjectile, true);
                    int x = player.GetX();
                    int y = player.GetY();
                    baseProjectile.SetLocation(x + 1, y);
                    baseProjectile.SetDimensions();
                    game.AddPlayerProjectile(baseProjectile); 
                    game.Update(keysPressed, playerSpace, frameCounter);
                }
                

                //Spawn enemies

                spawnClock.SetFrames(spawnRate);
                bool spawn = spawnClock.Animate(frameCounter);

                if (spawn)
                {   
                    enemies = game.GetEnemies();
                    backgrounds = game.GetBackground(); 
                    List<int> rect = backgrounds[0].GetRect(); 
                    int randomPosition = random.Next(8,28);
                    int spawnRow = (rect[1] - 2);

                    List<int> spawnCordinates = new List<int>{spawnRow, randomPosition, spawnRow, randomPosition};

                    while (spawnRow <= 20)
                    {
                        foreach (Enemy enemy in enemies)
                        {
                            bool collision = enemy.DetectCollision(spawnCordinates);
                            if (collision)
                            {
                                spawnRow ++;
                                spawnCordinates = new List<int>{spawnRow, randomPosition, spawnRow, randomPosition};
                            }
                            if (spawnRow == 28)
                            {
                                skipSpawn = true;
                            }
                        }
                    }

                    if (!skipSpawn)
                    {   
                        Enemy enemy = new Enemy(spawnRow, randomPosition, basicEnemy, 5);
                        enemy.SetDimensions();
                        game.AddEnemy(enemy);
                    }
                }

                shootClock.SetFrames(bulletRate);
                bool shoot = shootClock.Animate(frameCounter);

                foreach (Enemy enemy in enemies)
                {
                    if (shoot)
                    {
                        baseProjectile = new Projectile(0, 0, circleProjectile, false);
                        baseProjectile.SetDimensions();
                        int x = enemy.GetX();
                        int y = enemy.GetY();
                        baseProjectile.SetLocation(x - 1, y);
                        game.AddEnemyProjectile(baseProjectile);
                    }
                }
                    

                if (level == 0 || enemiesKilled >= enemyNumber)
                {
                    level += 1; 
                    enemiesKilled = 0;
                    double fractionSpawn = spawnRate / 1.2;
                    spawnRate = (int)Math.Ceiling(fractionSpawn);
                    double fractionEnemies = 10 * (level * 1.2);
                    enemyNumber = (int)Math.Ceiling(fractionEnemies);
                }

                // Detect collisions and calculate damage. 
                enemyProjectiles = game.GetEnemyProjectiles();
                playerProjectiles = game.GetPlayerProjectiles();
                foreach (Projectile projectile in enemyProjectiles)
                {   
                    List<int> playerRect = player.GetRect();
                    bool playerCollision = projectile.DetectCollision(playerRect);

                    backgrounds = game.GetBackground(); 
                    List<int> rect = backgrounds[0].GetRect(); 
                    int healthRow = (rect[0] + 2);
                    List<int> healthCordinates = new List<int>{healthRow, 1, healthRow, screenHeight};

                    bool backgroundCollision = projectile.DetectCollision(healthCordinates);

                    if (playerCollision || backgroundCollision)
                    {
                        player.TakeDamage();
                    }

                    foreach (Projectile playerProjectile in playerProjectiles)
                    {
                        List<int> playerProjectileRect = playerProjectile.GetRect();
                        bool projectileCollision = projectile.DetectCollision(playerProjectileRect);
                        if (projectileCollision)
                        {
                            projectile.Destroy();
                            playerProjectile.Destroy(); 
                        }
                    }
                }
                foreach (Projectile projectile in playerProjectiles)
                {   
                    if (enemies is List<Enemy>)
                    {
                        foreach(Enemy enemy in enemies)
                        {   
                            List<int> enemyRect = enemy.GetRect();
                            bool enemyCollision = projectile.DetectCollision(enemyRect);
                            if (enemyCollision)
                            {
                                enemy.TakeDamage();
                                bool isDestroyed = enemy.GetDestroyed();
                                if (isDestroyed)
                                {
                                    currentScore += 50;
                                    enemiesKilled += 1;
                                }
                            }
                        }
                    }
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