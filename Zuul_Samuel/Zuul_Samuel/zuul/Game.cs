using System;
using zuul;

namespace Zuul
{
    public class Game
    {
        private Parser parser;
        private Player player;
        private Inventory inventory;

        public Game()
        {

            parser = new Parser();
            player = new Player();
            //item = 

            CreateRooms();
            
        }
       
        private void CreateRooms()
        {

             


            // create the rooms
            Room outside = new Room("outside the main entrance of the university");
            Room inside = new Room("inside the university");
            Room theatre = new Room("in a lecture theatre");
            Room pub = new Room("in the campus pub");
            Room lab = new Room("in a computing lab");
            Room office = new Room("in the computing admin office");
            Room labn2 = new Room("on the second flooor of the computing lab");
            Room pubn2 = new Room("on the second level of the pub");
            Room theatren2 = new Room("on the second level of the the theatre");
            Room officen2 = new Room("on the second level of the the admin office");
            Room basement = new Room("in the pub basement");
            Room hall = new Room(" a hallway to the left leading to mutiple classrooms ");


            // initialise room exits
            outside.AddExit("east", theatre);
            outside.AddExit("south", lab);
            outside.AddExit("west", pub);
           

            theatre.AddExit("west", outside);

            pub.AddExit("east", outside);

            lab.AddExit("north", outside);
            lab.AddExit("east", office);

            office.AddExit("west", lab);

            outside.AddExit("inside", inside);
            inside.AddExit("outside", outside);
            inside.AddExit("west", hall); 

            lab.AddExit("up", labn2);
            labn2.AddExit("down", lab);

            pub.AddExit("up", pubn2);
            pubn2.AddExit("down", pub);

            pub.AddExit("down", basement);
            basement.AddExit("down", pub);



            theatre.AddExit("up", theatren2);
            theatren2.AddExit("down", theatre);

            office.AddExit("up", officen2);
            officen2.AddExit("down", office);

            lab.chest.Put("axe", new Item(5, " an big and heavy axe "));
            lab.chest.Put("medkit", new Item(2, " heals a great amount of health "));
            office.chest.Put("bandage", new Item(1, " heals a little amount of health "));
            pub.Chest.Put("handgun", new Item(4, " a handgun usefull for selfdefense "));
            officen2.Chest.Put("bulletCase", new Item(2, " bullets for a gun "));


            player.CurrentRoom = outside;  // start game outside
        }

           
    




        /**
		 *  Main play routine.  Loops until end of play.
		 */
        public void Play()
        {
            PrintWelcome(); 

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the player wants to quit.
            bool finished = false;
            while (!finished)
            {

                if (!player.IsAlive())
                {
                    Console.WriteLine("\n you died...\n");
                    finished = true;
                    continue;
                }

                Command command = parser.GetCommand();
                finished = ProcessCommand(command);


            }
            Console.WriteLine("Thank you for playing.");



        }

        /**
		 * Print out the opening message for the player.
		 */
        private void PrintWelcome()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Zuul!");
            Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
            Console.WriteLine("Type 'help' if you need help.");
            Console.WriteLine();
            Console.WriteLine("you have been wounded.");
            Console.WriteLine("\nif you move around you'll lose health\n ");
            Console.WriteLine(player.CurrentRoom.GetLongDescription());

        }

        /**
		 * Given a command, process (that is: execute) the command.
		 * If this command ends the game, true is returned, otherwise false is
		 * returned.
		 */
        private bool ProcessCommand(Command command)
        {
            bool wantToQuit = false;

            if (command.IsUnknown())
            {
                Console.WriteLine("I don't know what you mean...");
                return false;
            }


            string commandWord = command.GetCommandWord();
            switch (commandWord)
            {
                case "help":
                    PrintHelp();
                    break;
                case "go":
                    GoRoom(command);
                    break;
                case "quit":
                    wantToQuit = true;
                    break;
                case "look":
                    Console.WriteLine(player.CurrentRoom.GetLongDescription());
                    Console.WriteLine(player.CurrentRoom.chest.show());
                    break;
                case "health":
                    Console.WriteLine(" current health " + player.Health);
                    break;
                case "take":
                    Take(command);
                    break;
                case "drop":
                    drop(command);
                    break;
            }

            return wantToQuit;
        }


        // implementations of user commands:

        /**
		 * Print out some help information.
		 * Here we print the mission and a list of the command words.
		 */
        private void PrintHelp()
        {
            Console.WriteLine("You are lost. You are alone.");
            Console.WriteLine("You wander around at the university.");
            Console.WriteLine();
            // let the parser print the commands
            parser.PrintValidCommands();
        }

        /**
		 * Try to go to one direction. If there is an exit, enter the new
		 * room, otherwise print an error message.
		 */
        private void GoRoom(Command command)
        {
            if (!command.HasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("Go where?");
                return;
            }

            string direction = command.GetSecondWord();

            // Try to go to the next room.
            Room nextRoom = player.CurrentRoom.GetExit(direction);

            if (nextRoom == null)
            {
                Console.WriteLine("There is no door to " + direction + "!");
            }
            else
            {
                player.Damage(5);
                player.CurrentRoom = nextRoom;
                Console.WriteLine(player.CurrentRoom.GetLongDescription());
                Console.WriteLine(" current health " + player.Health);
            }
        }
        private void Take(Command command)
        {
            if (!command.HasSecondWord())
            {
                Console.WriteLine(" take what? ");
                return;
            }

            string itemName = command.GetSecondWord();

            if (!player.TakeFromChest(itemName))
            {
                Console.WriteLine(" you were unanle to take the " + itemName);
                return;
            }
            else
            {
                Console.WriteLine("you have taken the " + itemName);
                return;
            }
        }
        private void drop (Command command)
        {
            if (!command.HasSecondWord())
            {
                Console.WriteLine(" drop what? ");
                return;
            }

            string itemName = command.GetSecondWord();

            if (!player.DropToChest(itemName))
            {
                Console.WriteLine(" you were unanle to drop the " + itemName);
                return;
            }
            
        }
    }
}

