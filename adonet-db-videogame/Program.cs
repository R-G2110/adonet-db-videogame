using System;

namespace adonet_db_videogame
{
    class Program
    {
        static void Main(string[] args)
        {
            VideogameManager manager = new VideogameManager();
            bool exit = false;
            do
            {
                /*Console.Clear();*/ // Pulisce la console prima di visualizzare il menu
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Inserire un nuovo videogioco");
                Console.WriteLine("2. Stampa la lista dei videogiochi");
                Console.WriteLine("3. Ricercare un videogioco per ID");
                Console.WriteLine("4. Ricercare tutti i videogiochi per nome");
                Console.WriteLine("5. Cancellare un videogioco");
                Console.WriteLine("0. Chiudere il programma");
                Console.Write("Selezione: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("\nInserisci il nome del videogioco: ");
                        string name = Console.ReadLine();
                        Console.Write("Inserisci l'overview del videogioco: ");
                        string overview = Console.ReadLine();
                        Console.Write("Inserisci la data di rilascio del videogioco (formato gg-mm-yyyy): ");
                        string releaseDateStr = Console.ReadLine();
                        DateTime releaseDate;

                        // Converte la stringa in un oggetto DateTime
                        if (!DateTime.TryParse(releaseDateStr, out releaseDate))
                        {
                            Console.WriteLine("Formato data di rilascio non valido. Utilizzare il formato (gg-mm-yyyy).");
                            break;
                        }

                        Console.Write("Inserisci l'ID della software house: ");
                        string softwareHouseId = Console.ReadLine();

                        manager.InsertVideogame(new Videogame(name, overview, releaseDate, softwareHouseId));
                        Console.WriteLine("\nGioco inserito con successo!!!");
                        break;

                    case "2":
                        Console.Clear();
                        manager.ShowAllVideogames();
                        break;

                    case "3":
                        Console.Clear();
                        Console.Write("\nInserisci l'ID del videogioco da cercare: ");
                        int searchId = int.Parse(Console.ReadLine());

                        manager.SearchVideogameById(searchId);
                        break;

                    case "4":
                        Console.Clear();
                        Console.Write("\nInserisci il nome del videogioco da cercare: ");
                        string searchName = Console.ReadLine();

                        manager.SearchVideogamesByName(searchName);
                        break;

                    case "5":
                        Console.Clear();
                        Console.Write("\nInserisci l'ID del videogioco da cancellare: ");
                        int deleteId = int.Parse(Console.ReadLine());

                        manager.DeleteVideogame(deleteId);
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
