﻿using OOPUNOExamples.Classes;
using OOPUNOExamples.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOPUNOExamples
{
    internal static class GameController //TODO: Make MVC
    {
        private static List<Player> players = new List<Player>();
        private static bool GameDone = true;
        private static int numberOfPlayers = 2;
        public static void GameLoop()
        {
            GameDone = false;
            while (!GameDone)
            {
                if (players.Count < 2)
                {
                    Console.WriteLine("You have to setup the game before you can start."); //TODO: could throw an exception here.
                    return;
                }
                foreach (Player player in players)
                {
                    Card card = player.PlayCard();
                    if (player.GetHandSize() == 0)
                    {
                        Console.WriteLine("player {0} won the game!", player.Name); 
                        LogPlayerWin(player);
                        GameDone = false;
                        Console.Read();
                        break;
                    }
                    if (card == null)
                    {
                        player.DrawCard();
                    }
                    if (player.Uno)
                    {
                        Console.WriteLine("{0}: Uno!", player.Name);
                    }
                }
                Console.WriteLine("Press enter for next round");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    GameDone = true;
                    break;
                }
            }
            MenuController();
        }
        public static void MenuController()
        {
            List<MenuOption> menuOptions = new List<MenuOption>
            {
                new MenuOption(nameof(DebugSetupGame), DebugSetupGame),
                new MenuOption(nameof(SetupGame), SetupGame),
                new MenuOption(nameof(GameLoop), GameLoop),
                new MenuOption(nameof(GetPlayerList), GetPlayerList)
            };
            Menu menu = new Menu(
                "Main Menu", 
                "here you can choose game mode and player", 
                menuOptions);

            menu.MenuControl();
        }
        public static void SetupGame() //TODO: this should be a screen
        {
            DeckCreator deckCreator = new DeckCreator();
            List<MenuOption> menuOptions = new List<MenuOption>
            {
                new MenuOption("Create A Normal Deck", delegate ()
                {
                    Player.Deck = deckCreator.FactoryMethodNormalDeck();
                    Menu.menuLoopControl = false;
                }),
                new MenuOption("Create A Advanced Deck", delegate ()
                {
                    Player.Deck = deckCreator.FactoryMethodAdvancedDeck();
                    Menu.menuLoopControl = false;
                })
            };
            Menu menu = new Menu("Choose Deck Type", "Here you can choose what kinda of deck the game should use. Press ECS to Exit.", menuOptions);
            menu.MenuControl();

            PromtScreen screen = new PromtScreen("Name Players", "Here you can give each player a name for the game. The game needs 2 - 4 players.");
            screen.Draw();
            string name;
            do
            {
                name = screen.PromtUser(String.Format("Enter the name of a player {0}.", players.Count + 1));
                players.Add(new Player(name));
                Console.WriteLine("Would you like to add another player. Press y/n.");
                if (Console.ReadKey(true).Key != ConsoleKey.Y)
                {
                    break;
                }
            } while (players.Count < 4);
        }
        public static void DebugSetupGame() //TODO: this should be a screen
        {

            DeckCreator deckCreator = new DeckCreator();
            Player.Deck = deckCreator.FactoryMethodNormalDeck();

            players.Add(new Player("lkri"));
            players.Add(new Player("sinb"));
        }
        public static void GetPlayerList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Player player in players)
            {
                if (player == players.Last())
                {
                    sb.Append(player.Name.ToString());
                }
                else
                {
                    sb.Append(player.Name.ToString() + ", ");
                }
            }
            Screen screen = new Screen("Player List", sb.ToString());
            screen.Draw();
            Console.ReadLine();
        }
        private static void LogPlayerWin(Player player)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(player.Name); //TODO: flesh out log describtion, number of rounds after win?
            File.AppendAllText("log.txt", sb.ToString());
            sb.Clear();
        }
    }
}
