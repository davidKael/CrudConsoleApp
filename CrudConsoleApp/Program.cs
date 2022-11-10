using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudConsoleApp.Db;
using CrudConsoleApp.Models;
using CrudConsoleApp.Helpers;

namespace CrudConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool programRunning = true;
            while (programRunning)
            {
                GetUsers();
                programRunning = MenuSelection();
            }

        }


        static void AddUser(User newUser)
        {
            using (var db = new KunskapsProvDbContext())
            {

                db.Add(newUser);
                db.SaveChanges();
            }

        }

        static void EditUserPrompt()
        {



            using (var db = new KunskapsProvDbContext())
            {
                List<string> editableValues = new List<string>()
                {
                    "FirstName",
                    "LastName",
                    "Email",
                    "Address"
                };

                
                List<User> users = db.Users.ToList();



                int selectedUser = 0;

                while (selectedUser == 0)
                {

                    while (selectedUser == 0)
                    {
                        ConsoleHelper.DrawLine();
                        Console.WriteLine("Select which user you would like to edit: ");
                        List<string> UserInfos = new List<string>();

                        foreach (User item in users)
                        {

                            UserInfos.Add($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName} Email: {item.Email} Address: {item.Address} Registered At: {item.UserRegisteredAt}");
                        }


                        selectedUser = SelectOptions("Select which user you would like to edit: ", UserInfos.ToArray());

                        if (selectedUser == users.Count + 1)
                        {
                            return;
                        }

                    }

                    int selectedAttribute = 0;


                    while (selectedAttribute == 0)
                    {
                        string[] valueOptions ={
                            $"First Name: { users[selectedUser - 1].FirstName}",
                            $"Last Name: { users[selectedUser - 1].LastName}",
                            $"Email: { users[selectedUser - 1].Email}",
                            $"Address: { users[selectedUser - 1].Address}"
                         };

                        selectedAttribute = SelectOptions("Select what you would like to edit: ", valueOptions);

                    }

                    if (selectedAttribute == 5)
                    {
                        selectedUser = 0;
                    }
                    else
                    {
                        Console.Write($"Enter new {editableValues[selectedAttribute - 1]}: ");
                        string input = Console.ReadLine();

                        switch (selectedAttribute)
                        {
                            case 1:
                                db.Users.ToList()[selectedUser - 1].FirstName = input;
                                break;
                            case 2:
                                db.Users.ToList()[selectedUser - 1].LastName = input;
                                break;
                            case 3:
                                db.Users.ToList()[selectedUser - 1].Email = input;
                                break;
                            case 4:
                                db.Users.ToList()[selectedUser - 1].Address = input;
                                break;
                            default:
                                Console.WriteLine("I cant read.. sorry my fault...");
                                Console.ReadKey();
                                Console.Clear();
                                return;
                        }

                        db.SaveChanges();
                        Console.WriteLine($"{editableValues[selectedAttribute - 1]} edited on user with id {db.Users.ToList()[selectedUser - 1].Id}...");
                        Console.ReadKey();
                        Console.Clear();

                    }

                }

              


            }
        }

        static int SelectOptions(string selectionMessage, string[] options)
        {
            ConsoleHelper.DrawLine();
            Console.WriteLine(selectionMessage);
            for(int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i+1}. {options[i]}");
            }
            Console.WriteLine($"{options.Length +1}. Go Back");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int parsedInput))
            {
                if(parsedInput > 0 && parsedInput < options.Length +1)
                {
                    return parsedInput;
                }
                else if(parsedInput == options.Length +1)
                {
                    return parsedInput;
                }
                else
                {
                    return 0;
                }
               
            }
            else
            {
                Console.WriteLine("Invalid input... has to be a number....");
                Console.WriteLine("Press any key to submit");
                Console.ReadKey();
                Console.Clear();

                return 0;
            }
        }

        static void RemoveUserPrompt()
        {
            ConsoleHelper.DrawLine();
            Console.Write("Select (by id) which user you would like to remove: ");

            string input = Console.ReadLine();

            if(int.TryParse(input, out int parsedInput))
            {
                DeleteUserById(parsedInput);
            }
            else
            {
                Console.WriteLine("Invalid input... has to be a number....");
                Console.WriteLine("Press any key to submit");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static bool MenuSelection()
        {
            ConsoleHelper.DrawLine();
            Console.WriteLine("1.Add 2.Edit 3.Remove 4.Exit");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddUser(AddUserForm());
                    break;
                case "2":
                    EditUserPrompt();
                    break;
                case "3":
                    RemoveUserPrompt();
                    break;
                case "4":
                    return false; ;
                default:
                    Console.WriteLine("Invalid input...");
                    break;
            }

            return true;
        }

        static void GetUsers()
        {
            using (var db = new KunskapsProvDbContext())
            {
                List<User> users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}, {u.FirstName}, {u.LastName}, {u.Email}, {u.Address}, {u.UserRegisteredAt}");
                }
            }
            return;
        }

        static void DeleteUserById(int id)
        {
            using (var db = new KunskapsProvDbContext())
            {

                User userToRemove = db.Users.Find(id);

                if(userToRemove == null)
                {
                    Console.WriteLine("User not found...");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                db.Remove(userToRemove);
                db.SaveChanges();
            }
            return;
        }

        static void EditUserById(int id)
        {
            using (var db = new KunskapsProvDbContext())
            {

                User userToRemove = db.Users.Find(id);

                if (userToRemove == null)
                {
                    Console.WriteLine("User not found...");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                db.Remove(userToRemove);
                db.SaveChanges();
            }
            return;
        }

        static User AddUserForm()
        {
            ConsoleHelper.DrawLine();
            Console.WriteLine("Create new User.....");
            Console.Write("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your home address: ");
            string address = Console.ReadLine();
            ConsoleHelper.DrawLine();
            Console.WriteLine("Press any key to submit");
            Console.ReadKey();
            Console.Clear();
            return new User(firstName, lastName, email, address);
        }
    }
}
