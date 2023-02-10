using System;
using System.Diagnostics; // gives me acess to the stopwatch. 

class Program
{
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
        List<ConsoleKey> keysPressed = new List<ConsoleKey>();

        string scene = "start";
        int fps = 60; 

        double frameDuration = 1000.0 / fps;  

        int lastWidth = 0;
        int lastHeight =  0;

        int frameCounter = 0; 

        // Initilize other

        stopwatch.Start(); 

        // ! Later I will want these initialized with the entities, but only if they do not yet exist. 

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


        // ! pregame test
        // ! End of test area 


        Console.CursorVisible = false;
        // Game loop
        while (true)
        {   
            double elapsedTime = stopwatch.ElapsedMilliseconds; 
            // * I hope this will auto refresh every frame. 
            if (elapsedTime >= 60000) {
                frameCounter = 0;
                stopwatch.Reset(); 
                stopwatch.Restart();
            } else {
                frameCounter = (int)(elapsedTime / 16.67);
            }

            // * Check window size 
            windowSize.CheckSize();

            int screenWidth = windowSize.GetWidth(); 
            int screenHeight = windowSize.GetHeight(); 

            // Todo: Handle keyboard events
            if (Console.KeyAvailable) // ! Looks like there is a problem with keys not stopping being printed as soon as your finger comes off them... this may be a problem. 
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keysPressed.Add(keyInfo.Key);

                if (keysPressed.Contains(ConsoleKey.Escape))
                {
                    scene = "quit";
                }
            }   

            // Todo: Allow entitys to update

            // Todo: Draw the scene 

            // ! checking if the screen size has changed. 
            if (lastWidth != screenWidth || lastHeight != screenHeight)
            {
                lastWidth = screenWidth;
                lastHeight = screenHeight;
                Console.Clear(); 
            }  

            if (screenWidth <= 80 || screenHeight <= 30)
            {
                scene = "small screen";
            }           

            if (scene == "start")
            {   
                // ! Draws the title 
                List<string> title = new List<string> 
                {
                "          Terminal WAR",
                "'`'`'`'`'`            '`'`'`'`'`"}; // TODO: It would be cool to add an animation where the word dropped in, one letter at a time. 

                Background titleObject = new Background(0, 0, title); 
                titleObject.SetDimensions(); 
                int width = titleObject.GetWidth();
                titleObject.SetLocation((screenWidth - width) / 2, 3);
                titleObject.Draw();

                // ! Draws a blinking "Press Enter to Start" 
                List<string> start = new List<string> 
                {"Press Enter to Start: "};
                Background startObject = new Background(0, 0, start); 
                startObject.SetDimensions();
                width = titleObject.GetWidth();
                startObject.SetLocation(((screenWidth - width) / 2) + 5, 12);

                startAnimation.Animate(frameCounter);
                int change = startAnimation.GetTimes(); 

                if (change % 2 == 0)
                { 
                    startObject.Clear();
                }
                else
                {
                    startObject.Draw();
                }
                
                
                // ! Draws a highscore equal to the highest number scored  
                List<string> highScore = new List<string>
                {"High Score: {highScoreNumber}"};
                Background highScoreObject = new Background(0, 0, highScore);   
                highScoreObject.SetDimensions();
                width = highScoreObject.GetWidth();
                highScoreObject.SetLocation((screenWidth - width)/2, 18);
                highScoreObject.Draw(); 

                if (keysPressed.Contains(ConsoleKey.Enter))
                {
                    Console.Clear(); 
                    scene = "game";
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
                }
            }
            else if (scene == "game")
            {
                Background game = new Background(0, 0, gameBackground); // * first you need to draw out the object. 
                game.SetDimensions(); // * Then you need to calculate width and height. 
                int width = game.GetWidth(); //* Then you need to place it in the middle of the screen by using (width - screenWidth) / 2
                int gameLeft = (screenWidth - width) / 2; // ! I made this so that I can calculate where the Health, Energy, Button options, and wave display will show up... Though maybe Ill put the wave in the true middle. 
                game.SetLocation(gameLeft, 1);
                game.Draw(); // * Finally you need to use a method to display it on the screen.  

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
                }
            }
            else if (scene == "quit")
            {
                // TODO: Write code for the quit screen. 
            }
            else if (scene == "gameover")
            {
                Background gameover = new Background(0, 0, gameOverWords);
                gameover.SetDimensions();
                int width = gameover.GetWidth();
                gameover.SetLocation((screenWidth - width) / 2, 6);
                gameover.Draw(); 



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