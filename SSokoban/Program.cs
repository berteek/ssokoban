﻿using SFML.Window;

using SSokoban.Core;

namespace SSokoban
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "SSokoban");
            game.Run();
        }
    }
}
