using System;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;

namespace Projekt
{
    public static class Database
    {

        public static string connectionPath = @"Data Source=..\..\..\bazaV2.db";
        public static void InitializeDatabase()
        {
            if (!File.Exists(@"..\..\..\bazaV2.db"))
            {
                SQLiteConnection.CreateFile(@"..\..\..\bazaV2.db");
                
                using (var connection = new SQLiteConnection(connectionPath))
                {
               
                    connection.Open();
             
                    string createTasksTable = @"CREATE TABLE IF NOT EXISTS [Tasks] (
                      [TaskId] INTEGER primary key autoincrement 
                    , [userId] bigint NOT NULL
                    , [taskContent] text NOT NULL
                    , [Status] bigint DEFAULT (0) NOT NULL
     
                    );";
                    string createUsersTable = @"CREATE TABLE IF NOT EXISTS [Users] (
                      [userId] INTEGER  primary key autoincrement
                    , [email] text NOT NULL
                    , [password] text NOT NULL
              
                    );";


                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = createTasksTable;
                        command.ExecuteNonQuery();

                        command.CommandText = createUsersTable;
                        command.ExecuteNonQuery();
                    
                    }
                }
               

            }
        }
        public static bool UserLogin(string email, string password)
        {
            using (var connection = new SQLiteConnection(connectionPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Users WHERE email = @email AND password = @password";
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                          
                            return true;
                        }
                        return false;
                      
                    }
                }
            }
        }
        public static bool AddUser(string email, string password)
        {
            using (var connection = new SQLiteConnection(connectionPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Users WHERE email = @email";
                    command.Parameters.AddWithValue("@email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                          
                            return false;
                        }
                    }



                    command.CommandText = "INSERT INTO Users (email, password) VALUES (@email, @password)";
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        public static int GetUserId(string email)
        {
            using (var connection = new SQLiteConnection(connectionPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT userId FROM Users WHERE email = @email";
                    command.Parameters.AddWithValue("@email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        return -1;
                    }
                }
            }
        }
        public static bool AddTask(int userId, string taskContent)
        {
            using (var connection = new SQLiteConnection(connectionPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Tasks (userId, taskContent) VALUES (@userId, @taskContent)";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@taskContent", taskContent);
                    command.ExecuteNonQuery();

                }
            }
            return true;
        }
        public static void RemoveTask(int taskId,int userId)
        {
            using (var connection = new SQLiteConnection(connectionPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "DELETE FROM Tasks WHERE TaskId = @taskId and userId=@userId";
                    command.Parameters.AddWithValue("@taskId", taskId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();

                }
            }
        }
        public static void ShowTasks(int userId)
        {
            using (var connection = new SQLiteConnection(connectionPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Tasks WHERE userId = @userId";
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Zadania:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id: {reader["TaskId"]}, Zadanie: {reader["taskContent"]}, Status: {reader["Status"]}");
                        }
                    }
                }
            }
        }
        public static void MarkTaskAsDone(int taskId,int userId)
        {
            using (var connection = new SQLiteConnection(connectionPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "UPDATE Tasks SET Status = 1 WHERE TaskId = @taskId and userId =@userId";
                    command.Parameters.AddWithValue("@taskId", taskId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }   
        public static bool ValidateTask(int taskId, int userId)
        {
            using (var connection = new SQLiteConnection(connectionPath))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT * FROM Tasks WHERE TaskId = @taskId and userId = @userId";
                    command.Parameters.AddWithValue("@taskId", taskId);
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }


    }
}
