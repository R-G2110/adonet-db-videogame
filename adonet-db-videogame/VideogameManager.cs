using ConsoleTables;
using System;
using System.Data.SqlClient;

namespace adonet_db_videogame
{
    public class VideogameManager
    {
        string connectionString = "Data Source=localhost;Initial Catalog=db_videogames;Integrated Security=True;";

        public void InsertVideogame(Videogame videogame)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO Videogames (Name, Overview, release_date, software_house_id, created_at, updated_at) 
                             VALUES (@Name, @Overview, CONVERT(DATE, @ReleaseDate, 103), @SoftwareHouseId, @CreatedAt, @UpdatedAt)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        
                        cmd.Parameters.AddWithValue("@Name", videogame.Name);
                        cmd.Parameters.AddWithValue("@Overview", videogame.Overview);
                        cmd.Parameters.AddWithValue("@SoftwareHouseId", videogame.SoftwareHouseId);
                        cmd.Parameters.AddWithValue("@CreatedAt", videogame.CreatedAt);
                        cmd.Parameters.AddWithValue("@UpdatedAt", videogame.UpdatedAt);
                        cmd.Parameters.AddWithValue("@ReleaseDate", videogame.ReleaseDate.ToString("dd/MM/yyyy")); 

                        
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Videogioco inserito con successo!");
                        }
                        else
                        {
                            Console.WriteLine("Errore durante l'inserimento del videogioco.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Errore durante l'inserimento del videogioco: {ex.Message}");
                }
            }
        }


        //public void ShowAllVideogames()
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT id, name, overview, release_date AS ReleaseDate, software_house_id AS SoftwareHouseId, created_at AS CreatedAt, updated_at AS UpdatedAt " +
        //               "FROM Videogames " +
        //               "ORDER BY id DESC";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            connection.Open();

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                Console.WriteLine("\nLista di tutti i videogiochi:");
        //                Console.WriteLine("ID\tNome\tOverview\tData di rilascio\tID della software house\tCreato il\tAggiornato il");

        //                while (reader.Read())
        //                {
        //                    Console.WriteLine($"\n{reader["id"]}\t{reader["name"]}\t{reader["overview"]}\t{reader["ReleaseDate"]}\t{reader["SoftwareHouseId"]}\t{reader["CreatedAt"]}\t{reader["UpdatedAt"]}");
        //                }
        //            }
        //        }
        //    }
        //}
        public void ShowAllVideogames()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT id, name, release_date AS ReleaseDate, software_house_id AS SoftwareHouseId " +
                               "FROM Videogames " +
                               "ORDER BY id ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var table = new ConsoleTable("ID", "Nome", "Data di rilascio", "ID della software house");

                        while (reader.Read())
                        {
                            DateTime releaseDate = (DateTime)reader["ReleaseDate"];

                            string formattedDate = releaseDate.ToString("dd/MM/yyyy");

                            table.AddRow(reader["id"], reader["name"], formattedDate, reader["SoftwareHouseId"]);
                        }

                        Console.WriteLine("\nLista di tutti i videogiochi:");
                        table.Write(Format.Default);
                    }
                }
            }
        }



        public void SearchVideogameById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Overview, release_date AS ReleaseDate, software_house_id AS SoftwareHouseId, created_at AS CreatedAt, updated_at AS UpdatedAt FROM Videogames WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("\nVideogioco trovato:");
                            Console.WriteLine($"* ID: ........................ {reader["Id"]}");
                            Console.WriteLine($"  Nome: ...................... {reader["Name"]}");
                            Console.WriteLine($"  Overview: .................. {reader["Overview"]}");
                            Console.WriteLine($"  Data di rilascio: .......... {reader["ReleaseDate"]}");
                            Console.WriteLine($"  ID della software house: ... {reader["SoftwareHouseId"]}");
                            Console.WriteLine($"  Creato il: ................. {reader["CreatedAt"]}");
                            Console.WriteLine($"  Aggiornato il: ............. {reader["UpdatedAt"]}");
                        }
                        else
                        {
                            Console.WriteLine("Nessun videogioco trovato con l'ID specificato.");
                        }
                    }
                }
            }
        }


        public void SearchVideogamesByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [id], [name], [overview], [release_date] AS ReleaseDate, [software_house_id] AS SoftwareHouseId, [created_at] AS CreatedAt, [updated_at] AS UpdatedAt " +
                               "FROM [Videogames] " +
                               "WHERE LOWER([name]) LIKE '%' + LOWER(@Name) + '%'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        bool found = false;
                        Console.WriteLine("\nRisultati della ricerca per nome:");
                        while (reader.Read())
                        {
                            found = true;
                            Console.WriteLine($"\n* ID: ........................ {reader["id"]}");
                            Console.WriteLine($"  Nome: ...................... {reader["name"]}");
                            Console.WriteLine($"  Overview: .................. {reader["overview"]}");
                            Console.WriteLine($"  Data di rilascio: .......... {reader["ReleaseDate"]}");
                            Console.WriteLine($"  ID della software house: ... {reader["SoftwareHouseId"]}");
                            Console.WriteLine($"  Creato il: ................. {reader["CreatedAt"]}");
                            Console.WriteLine($"  Aggiornato il: ............. {reader["UpdatedAt"]}");
                        }

                        if (!found)
                        {
                            Console.WriteLine("Nessun videogioco trovato con il nome specificato.");
                        }
                    }
                }
            }
        }


        public void DeleteVideogame(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string deleteTournamentQuery = "DELETE FROM tournament_videogame WHERE videogame_id = @Id";
                    using (SqlCommand deleteTournamentCommand = new SqlCommand(deleteTournamentQuery, connection, transaction))
                    {
                        deleteTournamentCommand.Parameters.AddWithValue("@Id", id);
                        deleteTournamentCommand.ExecuteNonQuery();
                    }

                    string deleteReviewsQuery = "DELETE FROM reviews WHERE videogame_id = @Id";
                    using (SqlCommand deleteReviewsCommand = new SqlCommand(deleteReviewsQuery, connection, transaction))
                    {
                        deleteReviewsCommand.Parameters.AddWithValue("@Id", id);
                        deleteReviewsCommand.ExecuteNonQuery();
                    }
                    string deletePegiLabelQuery = "DELETE FROM pegi_label_videogame WHERE videogame_id = @Id";
                    using (SqlCommand deletePegiLabelCommand = new SqlCommand(deletePegiLabelQuery, connection, transaction))
                    {
                        deletePegiLabelCommand.Parameters.AddWithValue("@Id", id);
                        deletePegiLabelCommand.ExecuteNonQuery();
                    }

                    string deleteDeviceQuery = "DELETE FROM device_videogame WHERE videogame_id = @Id";
                    using (SqlCommand deleteDeviceCommand = new SqlCommand(deleteDeviceQuery, connection, transaction))
                    {
                        deleteDeviceCommand.Parameters.AddWithValue("@Id", id);
                        deleteDeviceCommand.ExecuteNonQuery();
                    }

                    string deleteCategoryQuery = "DELETE FROM category_videogame WHERE videogame_id = @Id";
                    using (SqlCommand deleteCategoryCommand = new SqlCommand(deleteCategoryQuery, connection, transaction))
                    {
                        deleteCategoryCommand.Parameters.AddWithValue("@Id", id);
                        deleteCategoryCommand.ExecuteNonQuery();
                    }

                    string deleteVideogameQuery = "DELETE FROM Videogames WHERE Id = @Id";
                    using (SqlCommand deleteVideogameCommand = new SqlCommand(deleteVideogameQuery, connection, transaction))
                    {
                        deleteVideogameCommand.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = deleteVideogameCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Videogioco con ID {id} cancellato con successo.");
                        }
                        else
                        {
                            Console.WriteLine($"Nessun videogioco trovato con l'ID {id}.");
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {    
                    Console.WriteLine($"Errore durante la cancellazione del videogioco: {ex.Message}");
                    transaction.Rollback();
                }
            }
        }



    }

}
