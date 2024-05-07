using System;

namespace adonet_db_videogame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Benvenuto al gestore dei videogiochi per i tornei!");
            VideogameManager manager = new VideogameManager();
            bool exit = false;
            do
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Inserire un nuovo videogioco");
                Console.WriteLine("2. Stampa la lista dei videogiochi");
                Console.WriteLine("3. Ricercare un videogioco per ID");
                Console.WriteLine("4. Ricercare tutti i videogiochi aventi il nome contenente una determinata stringa");
                Console.WriteLine("5. Cancellare un videogioco");
                Console.WriteLine("0. Chiudere il programma");
                Console.Write("Selezione: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.InsertVideogame();
                        break;
                    case "2":
                        manager.ShowAllVideogames();
                        break;
                    case "3":
                        manager.SearchVideogameById();
                        break;
                    case "4":
                        manager.SearchVideogamesByName();
                        break;
                    case "5":
                        manager.DeleteVideogame();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Scelta non valida. Riprova.");
                        break;
                }
            } while (!exit);
        }
    }
}
