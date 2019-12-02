using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelInfo
{
    //******************************
    // Title: Novel Info
    // Description: Stores and organizes persisting data for user's novel
    // Author: Anna Parsons
    // Date Created: 11/11/19
    // Date Modified:
    //******************************
    class Program
    {
        static void Main(string[] args)
        {
            List<Character> characters = ReadFromCharacterFile();
            List<Character> archiveCharacters = ReadFromCharacterArchive(); 
            List<Note> notes = ReadFromNotesFile();
            List<Note> archiveNotes = ReadFromNoteArchive();

            DisplayWelcomeScreen();
            DisplayMenuScreen(characters, notes, archiveCharacters, archiveNotes);
            DisplayClosingScreen();
        }

        static void DisplayMenuScreen(List<Character> characters, List<Note> notes, List<Character> archiveCharacters, List<Note> archiveNotes)
        {
            bool quitApp = false;
            char menuChoice;
            ConsoleKeyInfo menuChoiceKey;

            do
            {
                DisplayNewScreen("Main Menu");

                //
                // Get menu choice
                //
                Console.WriteLine("a) Characters");
                Console.WriteLine("b) Notes");
                Console.WriteLine("c) View Archived Characters");
                Console.WriteLine("d) View Archived Notes");
                Console.WriteLine("q) Exit");
                Console.WriteLine("Enter Choice");
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                //
                // Process menu choice
                //
                switch (menuChoice)
                {
                    case 'a':
                        DisplayCharacterMenu(characters, archiveCharacters);
                        break;
                    case 'b':
                        DisplayNotesMenu(notes, archiveNotes);
                        break;
                    case 'c':
                        DisplayCharacterArchiveMenu(archiveCharacters);
                        break;
                    case 'd':
                        DisplayNoteArchiveMenu(archiveNotes);
                        break;
                    case 'q':
                        quitApp = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid letter for menu choice");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApp);
        }

        #region Characters

        static void DisplayCharacterMenu(List<Character> characters, List<Character> archiveCharacters)
        {
            bool exit = false;
            char menuChoice;
            ConsoleKeyInfo menuChoiceKey;

            do
            {
                DisplayNewScreen("Character Menu");

                //
                // Get menu choice
                //
                Console.WriteLine("a) View All Characters");
                Console.WriteLine("b) View Character Details");
                Console.WriteLine("c) Add Character");
                Console.WriteLine("d) Edit Character");
                Console.WriteLine("e) Remove Character");
                Console.WriteLine("f) Archive Character");
                Console.WriteLine("g) Filter Characters");
                Console.WriteLine("h) Save Characters");
                Console.WriteLine("q) Return to main menu");
                Console.WriteLine("Enter Choice");
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                //
                // Process menu choice
                //
                switch (menuChoice)
                {
                    case 'a':
                        DisplayAllCharacters(characters);
                        break;
                    case 'b':
                        DisplayViewCharacterDetails(characters);
                        break;
                    case 'c':
                        DisplayAddCharacter(characters);
                        break;
                    case 'd':
                        DisplayEditCharacter(characters);
                        break;
                    case 'e':
                        DisplayDeleteCharacter(characters);
                        break;
                    case 'f':
                        DisplayArchiveCharacter(characters, archiveCharacters);
                        break;
                    case 'g':
                        DisplayFilterCharactersMenu(characters);
                        break;
                    case 'h':
                        DisplayWriteCharacters(characters);
                        break;
                    case 'q':
                        exit = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid letter for menu choice");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!exit);
        }

        static void DisplayAllCharacters(List<Character> characters)
        {
            DisplayNewScreen("Characters");

            Console.WriteLine();
            foreach (Character character in characters)
            {
                DisplayCharacter(character);
                Console.WriteLine();
            }

            DisplayContinuePrompt();
        }
        
        static void DisplayViewCharacterDetails(List<Character> characters)
        {
            bool validResponse = false;

            do
            {
                DisplayNewScreen("Find Character");

                //
                // Display character names
                //
                Console.WriteLine("\tCharacter Names:");
                Console.WriteLine();
                foreach (Character character in characters)
                {
                    Console.WriteLine("\t" + character.Name);
                }

                //
                // Get character name from user
                //
                Console.WriteLine();
                Console.Write("\tEnter name of character: ");
                string characterName = Console.ReadLine();

                //
                // Get character
                //
                Character selectedCharacter = null;
                foreach (Character character in characters)
                {
                    if (character.Name == characterName)
                    {
                        selectedCharacter = character;
                        validResponse = true;
                        break;
                    }
                }

                if (validResponse)
                {
                    //
                    // Display character information
                    //
                    DisplayNewScreen("Selected Character");
                    Console.WriteLine();
                    Console.WriteLine();
                    DisplayCharacter(selectedCharacter);
                    Console.WriteLine();
                }
                else
                {
                    //
                    // Invalid input response
                    //
                    Console.WriteLine();
                    Console.WriteLine("\tPlease select an existing character.");
                    DisplayContinuePrompt();
                }
            } while (!validResponse);

            DisplayContinuePrompt();
        }

        static void DisplayAddCharacter(List<Character> characters)
        {
            Character newCharacter = new Character();
            bool validInput = false;
            string userResponse;

            //
            // Inquire user for character information
            //
            while (!validInput)
            {
                DisplayNewScreen("Add Character");
                Console.WriteLine();
                Console.WriteLine("\tWhat is the character's name? ");
                Console.WriteLine();
                Console.Write("\t");
                newCharacter.Name = Console.ReadLine();
                Console.WriteLine();
                Console.Write($"\tIs {newCharacter.Name} correct? [yes, no]: ");
                userResponse = Console.ReadLine().ToLower();
                if (userResponse == "yes")
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease input the correct name for the character");
                }
                DisplayContinuePrompt();
            }

            validInput = false;
            while (!validInput)
            {
                DisplayNewScreen("Add Character");
                Console.WriteLine();
                Console.WriteLine("\tWhat is the character's courtesy name? ");
                Console.WriteLine();
                Console.Write("\t");
                newCharacter.CourtesyName = Console.ReadLine();
                Console.WriteLine();
                Console.Write($"\tIs {newCharacter.CourtesyName} correct? [yes, no]: ");
                userResponse = Console.ReadLine().ToLower();
                if (userResponse == "yes")
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"\tPlease input the correct courtesy name for {newCharacter.Name}");
                }
                DisplayContinuePrompt();
            }

            validInput = false;
            while (!validInput)
            {
                DisplayNewScreen("Add Character");
                Console.WriteLine();
                Console.WriteLine($"\tHow old is {newCharacter.Name}? ");
                Console.WriteLine();
                Console.Write("\t");
                userResponse = Console.ReadLine();

                if (int.TryParse(userResponse, out int age))
                {
                    if (age >= 0)
                    {
                        newCharacter.Age = age;
                        Console.WriteLine();
                        Console.Write($"\tIs {newCharacter.Age} correct? [yes, no]: ");
                        userResponse = Console.ReadLine().ToLower();

                        if (userResponse == "yes")
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\tPlease input the correct age for {newCharacter.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\t{newCharacter.Name}'s age must be greater than or equal to 0");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\tInvalid Response. Please enter a valid integer.");
                }
                DisplayContinuePrompt();
            }

            validInput = false;
            while (!validInput)
            {
                DisplayNewScreen("Add Character");
                Console.WriteLine();
                Console.WriteLine($"\tWhat clan is {newCharacter.Name} affiliated with?");
                Console.WriteLine();
                Console.WriteLine( 
                    "\tGusu_Lan, " +
                    "Yunmeng_Jiang, " +
                    "Lanling_Jin, " +
                    "Qishan_Wen, " +
                    "Qinghe_Nie");
                Console.WriteLine();
                Console.Write("\t");
                userResponse = Console.ReadLine();

                if (Enum.TryParse(userResponse, out Character.Affiliation affiliation))
                {
                    newCharacter.Clan = affiliation;
                    Console.WriteLine();
                    Console.Write($"\tIs {newCharacter.Clan} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse == "yes")
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct affiliation of {newCharacter.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Response. Please enter a valid affiliation.");
                }
                DisplayContinuePrompt();
            }

            validInput = false;
            while (!validInput)
            {
                DisplayNewScreen("Add Character");
                Console.WriteLine();
                Console.Write($"\tIs {newCharacter.Name} alive? [true, false]: ");
                userResponse = Console.ReadLine();

                if (bool.TryParse(userResponse, out bool status))
                {
                    newCharacter.Status = status;
                    Console.WriteLine();
                    Console.Write($"\tIs Alive: {newCharacter.Status} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse == "yes")
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct status of {newCharacter.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Response. Please enter a valid status.");
                }
                DisplayContinuePrompt();
            }

            characters.Add(newCharacter);

            //
            // Display new character's information
            //
            DisplayNewScreen("Add Character");
            Console.WriteLine();
            Console.WriteLine("\tNew Character:");
            Console.WriteLine();
            DisplayCharacter(newCharacter);
            Console.WriteLine();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void DisplayEditCharacter(List<Character> characters)
        {
            bool validResponse = false;
            Character selectedCharacter = null;

            do
            {
                DisplayNewScreen("Edit Character");

                //
                // Display character names
                //
                Console.WriteLine("\tCharacter Names:");
                Console.WriteLine();
                foreach (Character character in characters)
                {
                    Console.WriteLine("\t" + character.Name);
                }

                //
                // Get character from user
                //
                Console.WriteLine();
                Console.Write("\tEnter name of character to be edited: ");
                string characterName = Console.ReadLine();

                //
                // Get character
                //
                foreach (Character character in characters)
                {
                    if (character.Name == characterName)
                    {
                        selectedCharacter = character;
                        validResponse = true;
                        break;
                    }
                }

                //
                // Invalid input response
                //
                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease select an existing character.");
                    DisplayContinuePrompt();
                }
            } while (!validResponse);


            //
            // Edit character
            //
            bool validInput = false;
            string userResponse;
            string name;
            while (!validInput)
            {
                DisplayNewScreen("Edit Character Name");
                Console.WriteLine();
                Console.WriteLine("\tPrepared to edit. Press enter to keep current information.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Name: {selectedCharacter.Name}");
                Console.WriteLine();
                Console.Write("\tNew Name: ");
                name = Console.ReadLine();
                if (name != "")
                {
                    Console.WriteLine();
                    Console.Write($"\tIs {name} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        selectedCharacter.Name = name;
                        Console.WriteLine();
                        Console.WriteLine($"\tCharacter's name changed to {selectedCharacter.Name}");
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct name for {selectedCharacter.Name}");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.Write($"\tIs {selectedCharacter.Name} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct name for {selectedCharacter.Name}");
                    }
                }
                DisplayContinuePrompt();
            }

            validInput = false;
            string courtName;
            while (!validInput)
            {
                DisplayNewScreen("Edit Character Courtesy Name");
                Console.WriteLine();
                Console.WriteLine("\tPrepared to edit. Press enter to keep current information.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Name: {selectedCharacter.CourtesyName}");
                Console.WriteLine();
                Console.Write("\tNew Courtesy Name: ");
                courtName = Console.ReadLine();
                if (courtName != "")
                {
                    Console.WriteLine();
                    Console.Write($"\tIs {courtName} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        selectedCharacter.CourtesyName = courtName;
                        Console.WriteLine();
                        Console.WriteLine($"\tCharacter's courtesy name changed to {selectedCharacter.CourtesyName}");
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct courtesy name for {selectedCharacter.Name}");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"\tIs {selectedCharacter.CourtesyName} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct courtesy name for {selectedCharacter.Name}");
                    }
                }
                DisplayContinuePrompt();
            }

            validInput = false;
            int characterAge;
            while (!validInput)
            {
                DisplayNewScreen("Edit Character Age");
                Console.WriteLine();
                Console.WriteLine("\tPrepared to edit. Press enter to keep current information.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Age: {selectedCharacter.Age}");
                Console.WriteLine();
                Console.Write("\tNew Age: ");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    if (int.TryParse(userResponse, out int age))
                    {
                        if (age >= 0)
                        {
                            characterAge = age;
                            Console.WriteLine();
                            Console.Write($"\tIs {characterAge} correct? [yes, no]: ");
                            userResponse = Console.ReadLine().ToLower();

                            if (userResponse == "yes")
                            {
                                selectedCharacter.Age = characterAge;
                                validInput = true;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine($"\tPlease input the correct age for {selectedCharacter.Name}");
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tCharacter's age must be greater than or equal to 0");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tInvalid Response. Please enter a valid integer.");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.Write($"\tIs {selectedCharacter.Age} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct age for {selectedCharacter.Name}");
                    }
                }
                DisplayContinuePrompt();
            }

            validInput = false;
            string affiliation;
            while (!validInput)
            {
                DisplayNewScreen("Edit Character Affiliation");
                Console.WriteLine();
                Console.WriteLine("\tPrepared to edit. Press enter to keep current information.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Clan: {selectedCharacter.Clan}");
                Console.WriteLine();
                Console.WriteLine("\tNew Clan:");
                Console.WriteLine();
                Console.WriteLine(
                    "\tGusu_Lan, " +
                    "Yunmeng_Jiang, " +
                    "Lanling_Jin, " +
                    "Qishan_Wen, " +
                    "Qinghe_Nie");
                Console.WriteLine();
                Console.Write("\t");
                affiliation = Console.ReadLine();
                if (affiliation != "")
                {
                    if (Enum.TryParse(affiliation, out Character.Affiliation clan))
                    {
                        Console.WriteLine();
                        Console.Write($"\tIs {affiliation} correct? [yes, no]: ");
                        userResponse = Console.ReadLine().ToLower();

                        if (userResponse == "yes")
                        {
                            selectedCharacter.Clan = clan;
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\tPlease input the correct clan for {selectedCharacter.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tInvalid Response. Please enter a valid clan.");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.Write($"\tIs {selectedCharacter.Clan} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct clan for {selectedCharacter.Name}");
                    }
                }
                DisplayContinuePrompt();
            }

            validInput = false;
            while (!validInput)
            {
                DisplayNewScreen("Edit Character Status");
                Console.WriteLine();
                Console.WriteLine("\tPrepared to edit. Press enter to keep current information.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Status - Alive: {selectedCharacter.Status}");
                Console.WriteLine();
                Console.Write($"\tIs {selectedCharacter.Name} alive? [true, false]: ");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    if (bool.TryParse(userResponse, out bool status))
                    {
                        Console.WriteLine();
                        Console.Write($"\tIs {userResponse} correct? [yes, no]: ");
                        userResponse = Console.ReadLine().ToLower();

                        if (userResponse == "yes")
                        {
                            selectedCharacter.Status = status;
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\tPlease input the correct status of {selectedCharacter.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tInvalid Response. Please enter a valid status.");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.Write($"\tIs Alive: {selectedCharacter.Status} correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tPlease input the correct status for {selectedCharacter.Name}");
                    }
                    DisplayContinuePrompt();
                }
            }

            //
            // Display new character information
            //
            DisplayNewScreen("Edit Character");
            Console.WriteLine();
            Console.WriteLine("\tCharacter's New Information");
            Console.WriteLine();
            DisplayCharacter(selectedCharacter);
            Console.WriteLine();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void DisplayDeleteCharacter(List<Character> characters)
        {
            DisplayNewScreen("Remove Character");

            //
            // Display character names
            //
            Console.WriteLine();
            Console.WriteLine("\tCharacter Names");
            Console.WriteLine();
            foreach (Character character in characters)
            {
                Console.WriteLine("\t" + character.Name);
            }

            //
            // Get character name from user
            //
            Console.WriteLine();
            Console.Write("\tEnter name of character to be removed: ");
            string characterName = Console.ReadLine();

            //
            // Get character
            //
            Character selectedCharacter = null;
            foreach (Character character in characters)
            {
                if (character.Name == characterName)
                {
                    selectedCharacter = character;
                    break;
                }
            }

            //
            // Remove character
            //
            if (selectedCharacter != null)
            {
                characters.Remove(selectedCharacter);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedCharacter.Name} removed");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{characterName} not found");
            }

            DisplayContinuePrompt(); 
        }

        static void DisplayArchiveCharacter(List<Character> characters, List<Character> archiveCharacters)
        {
            DisplayNewScreen("Archive Character");
            Console.WriteLine("*Note; this will remove the character from the character list,");
            Console.WriteLine("though they will remain accessible through the archive feature"); 

            //
            // Display character names
            //
            Console.WriteLine();
            Console.WriteLine("\tCharacter Names");
            Console.WriteLine();
            foreach (Character character in characters)
            {
                Console.WriteLine("\t" + character.Name);
            }

            //
            // Get character name from user
            //
            Console.WriteLine();
            Console.Write("\tEnter name of character to be archived: ");
            string characterName = Console.ReadLine();

            //
            // Get character
            //
            Character selectedCharacter = null;
            Character archive = null;
            foreach (Character character in characters)
            {
                if (character.Name == characterName)
                {
                    selectedCharacter = character;
                    break;
                }
            }

            //
            // Add character to archive and remove from list
            //
            if (selectedCharacter != null)
            {
                archive = selectedCharacter;
                archiveCharacters.Add(archive);
                WriteToCharacterArchive(archiveCharacters);
                characters.Remove(selectedCharacter);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedCharacter.Name} archived");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{characterName} not found");
            }

            DisplayContinuePrompt();
        }

        static void DisplayFilterCharactersMenu(List<Character> characters)
        {
            string selectedProperty;

            DisplayNewScreen("Filter Characters");
            Console.WriteLine();
            Console.WriteLine("\tProperty to filter by");
            Console.WriteLine();
            Console.Write("\t[Clan, Status]: ");

            selectedProperty = Console.ReadLine().ToLower();

            switch (selectedProperty)
            {
                case ("clan"):
                    DisplayFilterCharactersByClan(characters);
                    break;
                case ("status"):
                    DisplayFilterCharactersByStatus(characters);
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("\tInvalid property. Returning to character menu.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        static void DisplayFilterCharactersByClan(List<Character> characters)
        {
            //
            // Get clan from user
            //
            Character.Affiliation selectedClan;
            List<Character> filteredCharacters = new List<Character>();

            DisplayNewScreen("Filter by Clan");

            Console.WriteLine("\tWhat clan do you wish to filter by?");
            Console.WriteLine();
            Console.WriteLine(
                "\t[Gusu_Lan, " +
                "Yunmeng_Jiang, " +
                "Lanling_Jin, " +
                "Qishan_Wen, " +
                "Qinghe_Nie]");
            Console.WriteLine();
            Console.Write("\t");

            if (Enum.TryParse(Console.ReadLine(), out selectedClan))
            {
                foreach (Character character in characters)
                {
                    if (character.Clan == selectedClan)
                    {
                        filteredCharacters.Add(character);
                    }
                }

                //
                // Display filtered characters
                //
                DisplayNewScreen("Filter by Clan");
                Console.WriteLine();
                Console.WriteLine($"\tCharacters in {selectedClan} Clan: ");
                Console.WriteLine();
                foreach (Character character in filteredCharacters)
                {
                    Console.WriteLine();
                    DisplayCharacter(character);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\tInvalid clan entered. Returning to character menu.");
            }
            DisplayContinuePrompt();
        }

        static void DisplayFilterCharactersByStatus(List<Character> characters)
        {
            //
            // Get status from user
            //
            string selectedStatus;
            List<Character> filteredCharacters = new List<Character>();

            DisplayNewScreen("Filter by Status");

            Console.WriteLine("\tWhat status do you wish to filter by?");
            Console.WriteLine();
            Console.Write("\t[Deceased, Alive]: ");
            selectedStatus = Console.ReadLine().ToLower();
            if (selectedStatus == "deceased")
            {
                foreach (Character character in characters)
                {
                    if (character.Status == false)
                    {
                        filteredCharacters.Add(character);
                    }
                }

                //
                // Display filtered deceased characters
                //
                Console.WriteLine();
                Console.WriteLine($"\tDeceased Characters: ");
                Console.WriteLine();
                foreach (Character character in filteredCharacters)
                {
                    DisplayCharacter(character);
                    Console.WriteLine();
                }
            }
            else if (selectedStatus == "alive")
            {
                foreach (Character character in characters)
                {
                    if (character.Status == true)
                    {
                        filteredCharacters.Add(character);
                    }
                }

                //
                // Display filtered living characters

                DisplayNewScreen("Filter By Status");
                Console.WriteLine();
                Console.WriteLine("\tLiving Characters: ");
                Console.WriteLine();
                foreach (Character character in filteredCharacters)
                {
                    DisplayCharacter(character);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\tInvalid status entered. Returning to character menu.");
            }
            DisplayContinuePrompt();
        }

        static void DisplayWriteCharacters(List<Character> characters)
        {
            DisplayNewScreen("Save Characters");

            //
            // Prompt user
            //
            Console.WriteLine();
            Console.WriteLine("Save characters?");
            Console.WriteLine("Type 'y' to continue. To cancel, type 'n'");
            if (Console.ReadLine().ToLower() == "y")
            {
                WriteToCharacterFile(characters);
                Console.WriteLine();
                Console.WriteLine("Characters saved");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Characters not saved");
            }
            DisplayContinuePrompt();
        }

        #endregion

        #region Notes

        static void DisplayNotesMenu(List<Note> notes, List<Note> archiveNotes)
        {
            bool exit = false;
            char menuChoice;
            ConsoleKeyInfo menuChoiceKey;

            do
            {
                DisplayNewScreen("Notes Menu");

                //
                // Get menu choice
                //
                Console.WriteLine("a) View Notes");
                Console.WriteLine("b) Find Note");
                Console.WriteLine("c) Add Note");
                Console.WriteLine("d) Edit Note");
                Console.WriteLine("e) Remove Note");
                Console.WriteLine("f) Archive Note");
                Console.WriteLine("g) Filter Notes");
                Console.WriteLine("h) Save Notes");
                Console.WriteLine("q) Return to main menu");
                Console.WriteLine("Enter Choice");
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                //
                // Process menu choice
                //
                switch (menuChoice)
                {
                    case 'a':
                        DisplayAllNotes(notes);
                        break;
                    case 'b':
                        DisplayViewNoteDetails(notes);
                        break;
                    case 'c':
                        DisplayAddNote(notes);
                        break;
                    case 'd':
                        DisplayEditNote(notes);
                        break;
                    case 'e':
                        DisplayDeleteNote(notes);
                        break;
                    case 'f':
                        DisplayArchiveNote(notes, archiveNotes);
                        break;
                    case 'g':
                        DisplayFilterNotesMenu(notes);
                        break;
                    case 'h':
                        DisplayWriteNotes(notes);
                        break;
                    case 'q':
                        exit = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid letter for menu choice");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!exit);
        }

        static void DisplayAllNotes(List<Note> notes)
        {
            DisplayNewScreen("Notes");

            Console.WriteLine();
            foreach (Note note in notes)
            {
                DisplayNote(note);
                Console.WriteLine();
            }

            DisplayContinuePrompt();
        }

        static void DisplayViewNoteDetails(List<Note> notes)
        {
            bool validInput = false;

            do
            {
                DisplayNewScreen("Find Note");

                //
                // Display note names
                //
                Console.WriteLine("\tNote Names:");
                Console.WriteLine();
                foreach (Note note in notes)
                {
                    Console.WriteLine("\t" + note.Name);
                }

                //
                // Get note name from user
                //
                Console.WriteLine();
                Console.Write("\tEnter name of note: ");
                string noteName = Console.ReadLine();

                //
                // Get note
                //
                Note selectedNote = null;
                foreach (Note note in notes)
                {
                    if (note.Name == noteName)
                    {
                        selectedNote = note;
                        validInput = true;
                        break;
                    }
                }

                if (validInput)
                {
                    //
                    // Display note information
                    //
                    DisplayNewScreen("Selected Note");
                    Console.WriteLine();
                    Console.WriteLine();
                    DisplayNote(selectedNote);
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else
                {
                    //
                    // Invalid input response
                    //
                    Console.WriteLine();
                    Console.WriteLine("\tPlease select an existing note.");
                    DisplayContinuePrompt();
                }
            } while (!validInput);
           
            DisplayContinuePrompt();
        }

        static void DisplayAddNote(List<Note> notes)
        {
            Note newNote = new Note();
            bool validInput = false;
            while (!validInput)
            {
                string userResponse;
                DisplayNewScreen("Add Note");

                //
                // Get note name and contents from user
                //
                Console.WriteLine();
                Console.Write("\tEnter name of new note: "); 

                newNote.Name = Console.ReadLine();

                        Console.WriteLine();
                        Console.Write($"\tIs {newNote.Name} correct? [yes, no]: ");
                        userResponse = Console.ReadLine().ToLower();
                        if (userResponse == "yes")
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease input the correct name for the note");
                        }
                DisplayContinuePrompt();
            }

            validInput = false;
            while (!validInput)
            {
                string userResponse;
                DisplayNewScreen("Add Note");

                Console.WriteLine();
                Console.Write("\tEnter new note: ");
                newNote.ANote = Console.ReadLine().ToLower();

                Console.WriteLine();
                Console.Write($"\tIs \"{newNote.ANote}\" correct? [yes, no]:  ");

                        userResponse = Console.ReadLine().ToLower();
                        if (userResponse == "yes")
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease input the correct note");
                        }
                DisplayContinuePrompt();
            }

            notes.Add(newNote);

            //
            // Display new note
            //
            DisplayNewScreen("Add Note");
            Console.WriteLine();
            Console.WriteLine("\tNew Note:");
            Console.WriteLine();
            DisplayNote(newNote);
            Console.WriteLine();
            Console.WriteLine();

            DisplayContinuePrompt();

        }

        static void DisplayEditNote(List<Note> notes)
        {
            bool validResponse = false;
            Note selectedNote = null;

            do
            {
                DisplayNewScreen("Edit Note");

                //
                // Display note names
                //
                Console.WriteLine("\tNotes");
                Console.WriteLine();
                foreach (Note note in notes)
                {
                    Console.WriteLine("\t" + note.Name);
                }

                //
                // Get note from user
                //
                Console.WriteLine();
                Console.Write("\tEnter name of note to be edited: ");
                string noteName = Console.ReadLine();

                //
                // Get note
                //
                foreach (Note note in notes)
                {
                    if (note.Name == noteName)
                    {
                        selectedNote = note;
                        validResponse = true;
                        break;
                    }
                }

                //
                // Invalid input response
                //
                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease select an existing note.");
                    DisplayContinuePrompt();
                }
            } while (!validResponse);


            //
            // Edit note
            //
            bool validInput = false;
            string userResponse;
            string name;
            while (!validInput)
            {
                DisplayNewScreen("Edit Note Name");
                Console.WriteLine();
                Console.WriteLine("\tPrepared to edit. Press enter to keep current information.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Name: {selectedNote.Name}");
                Console.WriteLine();
                Console.Write("\tNew Name: ");
                name = Console.ReadLine();

                    if (name != "")
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tIs {name} correct? [yes, no] ");
                        userResponse = Console.ReadLine().ToLower();
                        if (userResponse == "yes")
                        {
                            selectedNote.Name = name;
                            Console.WriteLine();
                            Console.WriteLine($"\tNote's name changed to {selectedNote.Name}");
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\tPlease input the correct name for {selectedNote.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write($"\tIs {selectedNote.Name} correct? [yes, no]: ");
                        userResponse = Console.ReadLine().ToLower();
                        if (userResponse == "yes")
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\tPlease input the correct name for {selectedNote.Name}");
                        }
                    }

                DisplayContinuePrompt();
            }

            validInput = false;
            string contents;
            while (!validInput)
            {
                DisplayNewScreen("Edit Note Contents");
                Console.WriteLine();
                Console.WriteLine("\tPrepared to edit. Press enter to keep current information.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent contents: {selectedNote.ANote}");
                Console.WriteLine();
                Console.Write("\tNew contents: ");
                contents = Console.ReadLine();

                    if (contents != "")
                    {
                        Console.WriteLine();
                        Console.Write($"\tIs \"{contents}\" correct? [yes, no]: ");
                        userResponse = Console.ReadLine().ToLower();
                        if (userResponse == "yes")
                        {
                            selectedNote.ANote = contents;
                        Console.WriteLine();
                        Console.WriteLine($"\tNote's contents changed to \"{selectedNote.ANote}\"");
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\tPlease input the correct contents for {selectedNote.Name}");
                        }
                    }
                    else
                    {
                    Console.WriteLine();
                    Console.Write($"\tIs \"{selectedNote.ANote}\" correct? [yes, no]: ");
                    userResponse = Console.ReadLine().ToLower();
                        if (userResponse == "yes")
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\tPlease input the correct contents for {selectedNote.ANote}");
                        }
                    }
                DisplayContinuePrompt();
            }

            //
            // Display updated note
            //
            DisplayNewScreen("Edit Note");
            Console.WriteLine();
            Console.WriteLine("\tNew Note:");
            Console.WriteLine();
            DisplayNote(selectedNote);
            Console.WriteLine();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void DisplayDeleteNote(List<Note> notes)
        {
            DisplayNewScreen("Remove Note");

            //
            // Display notes
            //
            Console.WriteLine("\tNote Names");
            Console.WriteLine();
            foreach (Note note in notes)
            {
                Console.WriteLine("\t" + note.Name);
            }

            //
            // Get note name from user
            //
            Console.WriteLine();
            Console.Write("\tEnter name of note to be removed: ");
            string noteName = Console.ReadLine();

            //
            // Get note
            //
            Note selectedNote = null;
            foreach (Note note in notes)
            {
                if (note.Name == noteName)
                {
                    selectedNote = note;
                    break;
                }
            }

            //
            // Remove note
            //
            if (selectedNote != null)
            {
                notes.Remove(selectedNote);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedNote.Name} removed");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{noteName} not found");
            }

            DisplayContinuePrompt();
        }

        static void DisplayArchiveNote(List<Note> notes, List<Note> archiveNotes)
        {
            DisplayNewScreen("Archive Note");
            Console.WriteLine("*Note; this will remove the note from the note list, " +
                "though it will remain accessible through the archive feature");

            //
            // Display notes
            //
            Console.WriteLine("\tNote Names");
            Console.WriteLine();
            Console.WriteLine();
            foreach (Note note in notes)
            {
                Console.WriteLine("\t" + note.Name);
            }

            //
            // Get note name from user
            //
            Console.WriteLine();
            Console.Write("\tEnter name of note to be archived: ");
            string noteName = Console.ReadLine();

            //
            // Get note
            //
            Note selectedNote = null;
            Note archive = null;
            foreach (Note note in notes)
            {
                if (note.Name == noteName)
                {
                    selectedNote = note;
                    break;
                }
            }

            //
            // Add note to archive and remove from list
            //
            if (selectedNote != null)
            {
                archive = selectedNote;
                archiveNotes.Add(archive);
                notes.Remove(selectedNote);
                WriteToNoteArchive(archiveNotes);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedNote.Name} archived");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{noteName} not found");
            }

            DisplayContinuePrompt();    
        }

        static void DisplayFilterNotesMenu(List<Note> notes)
        {
            string selectedProperty;

            DisplayNewScreen("Filter Notes");
            Console.WriteLine();
            Console.WriteLine("Property to filter by");
            Console.WriteLine();
            Console.Write("\t[Name, Contents]: ");

            selectedProperty = Console.ReadLine().ToLower();

            switch (selectedProperty)
            {
                case ("name"):
                    DisplayFilterNotesByName(notes);
                    break;
                case ("contents"):
                    DisplayFilterNotesByContents(notes);
                    break;
                default:
                    Console.WriteLine("Invalid property. Returning to note menu.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        static void DisplayFilterNotesByName(List<Note> notes)
        {
            //
            // Get name from user
            //
            string selectedName;
            List<Note> filteredNotes = new List<Note>();

            DisplayNewScreen("Filter by Name");

            Console.WriteLine("What name do you wish to filter for?");
            selectedName = Console.ReadLine();

                foreach (Note note in notes)
                {
                    if (note.Name.Contains (selectedName))
                    {
                        filteredNotes.Add(note);
                    }
                }

            if (filteredNotes.Count != 0)
            {
                //
                // Display filtered notes
                //
                DisplayNewScreen("Filter by Name");
                Console.WriteLine();
                Console.WriteLine($"Note names conatining {selectedName}: ");
                Console.WriteLine();
                foreach (Note note in filteredNotes)
                {
                    DisplayNote(note);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"No notes contaning {selectedName} found. Returning to note menu.");
            }
            DisplayContinuePrompt();
        }

        static void DisplayFilterNotesByContents(List<Note> notes)
        {
            //
            // Get contents from user
            //
            string selectedContent;
            List<Note> filteredNotes = new List<Note>();

            DisplayNewScreen("Filter by Content");

            Console.WriteLine("What content do you wish to filter for?");
            selectedContent = Console.ReadLine().ToLower();

            foreach (Note note in notes)
            {
                if (note.ANote.Contains(selectedContent))
                {
                    filteredNotes.Add(note);
                }
            }

            if (filteredNotes.Count != 0)
            {
                //
                // Display filtered notes
                //
                DisplayNewScreen("Filter by Content");
                Console.WriteLine();
                Console.WriteLine($"Notes conatining {selectedContent}: ");
                Console.WriteLine();
                foreach (Note note in filteredNotes)
                {
                    DisplayNote(note);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"No notes contaning {selectedContent} found. Returning to note menu.");
            }
            DisplayContinuePrompt();
        }

        static void DisplayWriteNotes(List<Note> notes)
        {
            DisplayNewScreen("Save Notes");

            //
            // Prompt user
            //
            Console.WriteLine();
            Console.WriteLine("Save notes?");
            Console.WriteLine("Type 'y' to continue. To cancel, type 'n'");
            if (Console.ReadLine() == "y")
            {
                WriteToNotesFile(notes);
                Console.WriteLine();
                Console.WriteLine("Notes saved");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Notes not saved");
            }
            DisplayContinuePrompt();
        }

        #endregion

        #region Archives

        static void DisplayCharacterArchiveMenu(List<Character> archiveCharacters)
        {
            bool exit = false;
            char menuChoice;
            ConsoleKeyInfo menuChoiceKey;

            do
            {
                DisplayNewScreen("Archived Character Menu");

                //
                // Get menu choice
                //
                Console.WriteLine("a) View All Characters");
                Console.WriteLine("b) View Character Details");
                Console.WriteLine("c) Remove Character");
                Console.WriteLine("q) Return to main menu");
                Console.WriteLine("Enter Choice");
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                //
                // Process menu choice
                //
                switch (menuChoice)
                {
                    case 'a':
                        DisplayAllArchivedCharacters(archiveCharacters);
                        break;
                    case 'b':
                        DisplayViewArchivedCharacterDetails(archiveCharacters);
                        break;
                    case 'c':
                        DisplayDeleteArchivedCharacter(archiveCharacters);
                        break;
                    case 'q':
                        exit = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid letter for menu choice");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!exit);
        }

        static void DisplayAllArchivedCharacters (List<Character> archiveCharacters)
        {
            DisplayNewScreen("Archived Characters");

            Console.WriteLine();
            foreach (Character archiveCharacter in archiveCharacters)
            {
                DisplayArchiveCharacter(archiveCharacter);
                Console.WriteLine();
            }

            DisplayContinuePrompt();
        }

        static void DisplayViewArchivedCharacterDetails (List<Character> archiveCharacters)
        {
            bool validResponse = false;

            DisplayNewScreen("Find Archived Character");

            //
            // Display character names
            //
            Console.WriteLine("\tArchived Character Names:");
            Console.WriteLine();
            foreach (Character archiveCharacter in archiveCharacters)
            {
                Console.WriteLine("\t" + archiveCharacter.Name);
            }

            //
            // Get character name from user
            //
            Console.WriteLine();
            Console.Write("\tEnter name of archived character: ");
            string characterName = Console.ReadLine();

            //
            // Get character
            //
            Character selectedCharacter = null;
            foreach (Character archiveCharacter in archiveCharacters)
            {
                if (archiveCharacter.Name == characterName)
                {
                    selectedCharacter = archiveCharacter;
                    validResponse = true;
                    break;
                }
            }

            if (validResponse == true)
            {
                //
                // Display character information
                //
                DisplayNewScreen("Selected Character");
                Console.WriteLine();
                DisplayArchiveCharacter(selectedCharacter);
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{characterName} not found. Returning to menu");
            }

            DisplayContinuePrompt();
        }

        static void DisplayDeleteArchivedCharacter(List<Character> archiveCharacters)
        {
            DisplayNewScreen("Remove Archived Character");
            Console.WriteLine("*WARNING: Removing a character from the archive will result in permanent deletion");

            //
            // Display character names
            //
            Console.WriteLine();
            Console.WriteLine("\tArchived Character Names:");
            Console.WriteLine();
            foreach (Character archiveCharacter in archiveCharacters)
            {
                Console.WriteLine("\t" + archiveCharacter.Name);
            }

            //
            // Get character name from user
            //
            Console.WriteLine();
            Console.Write("\tEnter name of archived character to be removed: ");
            string characterName = Console.ReadLine();

            //
            // Get character
            //
            Character selectedCharacter = null;
            foreach (Character archiveCharacter in archiveCharacters)
            {
                if (archiveCharacter.Name == characterName)
                {
                    selectedCharacter = archiveCharacter;
                    break;
                }
            }

            //
            // Remove character from archive
            //
            if (selectedCharacter != null)
            {
                archiveCharacters.Remove(selectedCharacter);
                WriteToCharacterArchive(archiveCharacters);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedCharacter.Name} removed");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{characterName} not found. Returning to menu");
            }

            DisplayContinuePrompt();
        }

        static void DisplayNoteArchiveMenu(List<Note> archiveNotes)
        {
            bool exit = false;
            char menuChoice;
            ConsoleKeyInfo menuChoiceKey;

            do
            {
                DisplayNewScreen("Archived Note Menu");

                //
                // Get menu choice
                //
                Console.WriteLine("a) View All Notes");
                Console.WriteLine("b) View Note Details");
                Console.WriteLine("c) Remove Note");
                Console.WriteLine("q) Return to main menu");
                Console.WriteLine("Enter Choice");
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                //
                // Process menu choice
                //
                switch (menuChoice)
                {
                    case 'a':
                        DisplayAllArchivedNotes(archiveNotes);
                        break;
                    case 'b':
                        DisplayViewArchivedNoteDetails(archiveNotes);
                        break;
                    case 'c':
                        DisplayDeleteArchivedNote(archiveNotes);
                        break;
                    case 'q':
                        exit = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid letter for menu choice");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!exit);
        }

        static void DisplayAllArchivedNotes(List<Note> archiveNotes)
        {
            DisplayNewScreen("Archived Notes");

            Console.WriteLine();
            foreach (Note archiveNote in archiveNotes)
            {
                DisplayArchiveNote(archiveNote);
                Console.WriteLine();
            }

            DisplayContinuePrompt();
        }

        static void DisplayViewArchivedNoteDetails (List<Note> archiveNotes)
        {
            DisplayNewScreen("Find Archived Note");

            //
            // Display note names
            //
            Console.WriteLine("\tArchived Note Names:");
            Console.WriteLine();
            foreach (Note archiveNote in archiveNotes)
            {
                Console.WriteLine("\t" + archiveNote.Name);
            }

            //
            // Get note name from user
            //
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("\tEnter name of archived note: ");
            string noteName = Console.ReadLine();

            //
            // Get note
            //
            Note selectedNote = null;
            foreach (Note archiveNote in archiveNotes)
            {
                if (archiveNote.Name == noteName)
                {
                    selectedNote = archiveNote;
                    break;
                }
            }
            if (selectedNote != null)
            {
                //
                // Display note information
                //
                DisplayNewScreen("Selected Note");
                Console.WriteLine();
                DisplayArchiveNote(selectedNote);
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{noteName} not found. Returning to menu");
            }

            DisplayContinuePrompt();
        }

        static void DisplayDeleteArchivedNote(List<Note> archiveNotes)
        {
            DisplayNewScreen("Remove Note");
            Console.WriteLine("*WARNING Removing a note from the archive will result in permanent deletion from the database");
            Console.WriteLine();
            Console.WriteLine();

            //
            // Display archived notes
            //
            Console.WriteLine("\tArchived Note Names:");
            Console.WriteLine();
            foreach (Note archiveNote in archiveNotes)
            {
                Console.WriteLine("\t" + archiveNote.Name);
                Console.WriteLine();
            }

            //
            // Get note name from user
            //
            Console.WriteLine();
            Console.Write("\tEnter name of archived note to be removed: ");
            string noteName = Console.ReadLine();

            //
            // Get note
            //
            Note selectedNote = null;
            foreach (Note archiveNote in archiveNotes)
            {
                if (archiveNote.Name == noteName)
                {
                    selectedNote = archiveNote;
                    break;
                }
            }

            //
            // Remove note
            //
            if (selectedNote != null)
            {
                archiveNotes.Remove(selectedNote);
                WriteToNoteArchive(archiveNotes);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedNote.Name} removed");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{noteName} not found. Returning to menu");
            }

            DisplayContinuePrompt();
        }

        #endregion

        #region File IO

        static List<Character> ReadFromCharacterFile()
        {
            List<Character> characters = new List<Character>();

            //
            // Read characters into a string array
            //
            string[] stringCharacters = File.ReadAllLines("Character\\Characters.txt");

            //
            // Create list of characters
            //
            string[] characterInfo;
            foreach (string character in stringCharacters)
            {
                characterInfo = character.Split(',');

                Character newCharacter = new Character();

                newCharacter.Name = characterInfo[0];

                newCharacter.CourtesyName = characterInfo[1];

                int.TryParse(characterInfo[2], out int age);
                newCharacter.Age = age;

                Enum.TryParse(characterInfo[3], out Character.Affiliation affiliation);
                newCharacter.Clan = affiliation;

                bool.TryParse(characterInfo[4], out bool status);
                newCharacter.Status = status;

                characters.Add(newCharacter);
            }
            return characters;
        }   

        static List<Character> ReadFromCharacterArchive()
        {
            List<Character> archiveCharacters = new List<Character>();

            //
            // Read characters into a string array
            //
            string[] stringCharacters = File.ReadAllLines("Archive\\CharacterArchive.txt");

            //
            // Create list of characters
            //
            string[] characterInfo;
            foreach (string character in stringCharacters)
            {
                characterInfo = character.Split(',');

                Character newCharacter = new Character();

                newCharacter.Name = characterInfo[0];

                newCharacter.CourtesyName = characterInfo[1];

                int.TryParse(characterInfo[2], out int age);
                newCharacter.Age = age;

                Enum.TryParse(characterInfo[3], out Character.Affiliation affiliation);
                newCharacter.Clan = affiliation;

                bool.TryParse(characterInfo[4], out bool status);
                newCharacter.Status = status;

                archiveCharacters.Add(newCharacter);
            }
            return archiveCharacters;
        }

        static List<Note> ReadFromNotesFile()
        {
            List<Note> notes = new List<Note>();

            //
            // Read notes into a string array
            //
            string[] stringNotes = File.ReadAllLines("Note\\Notes.txt");

            //
            // Create list of notes
            //
            string[] noteInfo;
            foreach (string note in stringNotes)
            {
                noteInfo = note.Split(';');

                Note newNote = new Note();

                newNote.Name = noteInfo[0];

                newNote.ANote = noteInfo[1];

                notes.Add(newNote);
            }
            return notes;
        }

        static List<Note> ReadFromNoteArchive()
        {
            List<Note> archiveNotes = new List<Note>();

            //
            // Read notes into a string array
            //
            string[] stringNotes = File.ReadAllLines("Archive\\NoteArchive.txt");

            //
            // Create list of notes
            //
            string[] noteInfo;
            foreach (string note in stringNotes)
            {
                noteInfo = note.Split(';');

                Note newNote = new Note();

                newNote.Name = noteInfo[0];

                newNote.ANote = noteInfo[1];

                archiveNotes.Add(newNote);
            }
            return archiveNotes;
        }

        static void WriteToCharacterFile(List<Character> characters)
        {
            //
            // Instantiate and fill in string array
            //
            string stringCharacter;
            string[] stringCharacters = new string[characters.Count];
            for (int index = 0; index < characters.Count; index++)
            {
                stringCharacter =
                    characters[index].Name + "," +
                    characters[index].CourtesyName + "," +
                    characters[index].Age + "," +
                    characters[index].Clan + "," +
                    characters[index].Status;

                    stringCharacters[index] = stringCharacter;
            }

            //
            // Write array to the character file
            //
            File.WriteAllLines("Character\\Characters.txt", stringCharacters);
        }

        static void WriteToCharacterArchive(List<Character> archiveCharacters)
        {
            //
            // Instantiate and fill in string array
            //
            string stringCharacter;
            string[] stringCharacters = new string[archiveCharacters.Count];
            for (int index = 0; index < archiveCharacters.Count; index++)
            {
                stringCharacter =
                    archiveCharacters[index].Name + "," +
                    archiveCharacters[index].CourtesyName + "," +
                    archiveCharacters[index].Age + "," +
                    archiveCharacters[index].Clan + "," +
                    archiveCharacters[index].Status;

                stringCharacters[index] = stringCharacter;
            }

            //
            // Write array to the character archive
            //
            File.WriteAllLines("Archive\\CharacterArchive.txt", stringCharacters);
        }

        static void WriteToNotesFile(List<Note> notes)
        {
            //
            // Instantiate and fill in string array
            //
            string stringNote;
            string[] stringNotes = new string[notes.Count];
            for (int index = 0; index < notes.Count; index++)
            {
                stringNote =
                    notes[index].Name + ";" +
                    notes[index].ANote;

                stringNotes[index] = stringNote;
            }

            //
            // Write array to the character file
            //
            File.WriteAllLines("Note\\Notes.txt", stringNotes);
        }

        static void WriteToNoteArchive(List<Note> archiveNotes)
        {
            //
            // Instantiate and fill in string array
            //
            string stringNote;
            string[] stringNotes = new string[archiveNotes.Count];
            for (int index = 0; index < archiveNotes.Count; index++)
            {
                stringNote =
                    archiveNotes[index].Name + ";" +
                    archiveNotes[index].ANote;

                stringNotes[index] = stringNote;
            }

            //
            // Write array to the character file
            //
            File.WriteAllLines("Archive\\NoteArchive.txt", stringNotes);
        }
        
        #endregion

        #region Helper Methods

        /// <summary>
        /// Displays Character
        /// </summary>
        /// <param name="character"></param>
        static void DisplayCharacter(Character character)
        {
            Console.WriteLine("\t***************************");
            Console.WriteLine($"\tName: {character.Name}");
            Console.WriteLine($"\tCourtesy Name: {character.CourtesyName}");
            Console.WriteLine($"\tAge: {character.Age}");
            Console.WriteLine($"\tClan: {character.Clan}");
            Console.WriteLine($"\tAlive: {character.Status}");
            Console.WriteLine("\t***************************");
        }

        static void DisplayArchiveCharacter(Character archiveCharacters)
        {
            Console.WriteLine("\t***************************");
            Console.WriteLine($"\tName: {archiveCharacters.Name}");
            Console.WriteLine($"\tCourtesy Name: {archiveCharacters.CourtesyName}");
            Console.WriteLine($"\tAge: {archiveCharacters.Age}");
            Console.WriteLine($"\tClan: {archiveCharacters.Clan}");
            Console.WriteLine($"\tAlive: {archiveCharacters.Status}");
            Console.WriteLine("\t***************************");
        }

        static void DisplayNote(Note note)
        {
            Console.WriteLine("\t***************************");
            Console.WriteLine($"\t{note.Name}:");
            Console.WriteLine($"\t{note.ANote}");
            Console.WriteLine("\t***************************");
            Console.WriteLine();
        }

        static void DisplayArchiveNote(Note archiveNotes)
        {
            Console.WriteLine("\t***************************");
            Console.WriteLine($"\t{archiveNotes.Name}:");
            Console.WriteLine($"\t{archiveNotes.ANote}");
            Console.WriteLine("\t***************************");
            Console.WriteLine();
        }

        /// <summary>
        /// Display Welcome Screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tWelocome to Novel Database");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\t\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Display new screen with header
        /// </summary>
        static void DisplayNewScreen(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        /// <summary>
        /// Display Closing Screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Novel Database");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display continue prompt
        /// </summary>
        static void DisplayExitPrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\t\tPress any key to exit.");
            Console.ReadKey();
        }


        #endregion
    }
}
