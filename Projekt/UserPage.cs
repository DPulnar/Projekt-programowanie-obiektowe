using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class UserPage
    {
        private int userId;
        public UserPage(int userId)
        {
            this.userId = userId;
        }
        public void UserPageMenu()
        {


            do
            {
                Console.WriteLine("1. Dodaj zadanie");
                Console.WriteLine("2. Wyświetl zadania");
                Console.WriteLine("3. Oznacz zadanie jako zrobione");
                Console.WriteLine("4. Usuń zadanie");
                Console.WriteLine("5. Wyloguj");
                int selection = Int32.Parse(Console.ReadLine());
                UserPageOptionSelection(selection);
            } while(Program.exit == false);
            

          

       
        }
        private  void UserPageOptionSelection(int selection)
        {
            switch (selection)
            {
                case 1://dodaj zadanie
                 
                    AddTask();
                    break;
                case 2://wyswietl zadania

                    Database.ShowTasks(userId);
                    break;
                case 3: //oznacz zadanie jako zrobione
                    MarkTaskAsDone();
                    break;
                case 4:
                    RemoveTask();
                    break;
                case 5: //wyloguj
             
                    Program.exit = true;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór");
                    break;
            }
        }
        private  void AddTask()
        {
            Console.WriteLine("Podaj treść zadania"); 
            string taskContent = Console.ReadLine();
            bool response = Database.AddTask(userId,taskContent);


        }
        private void RemoveTask()
        {
            Console.WriteLine("Podaj id zadania do usunięcia");
            int taskId = Int32.Parse(Console.ReadLine());
            bool response = Database.ValidateTask(taskId, userId);
            if (response == false)
            {
                Console.WriteLine("Nie ma takiego zadania");
                return;
            }
            Database.RemoveTask(taskId, userId);

        }
        private void MarkTaskAsDone()
        {
            Console.WriteLine("Podaj id zadania do oznaczenia jako zrobione");
            int taskId = Int32.Parse(Console.ReadLine());
            bool response = Database.ValidateTask(taskId, userId);
            if (response == false)
            {
                Console.WriteLine("Nie ma takiego zadania");
                return;
            }


            Database.MarkTaskAsDone(taskId,userId);
          
        }

    }
}
