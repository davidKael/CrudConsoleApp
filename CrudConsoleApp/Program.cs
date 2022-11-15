using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudConsoleApp.Db;
using CrudConsoleApp.Models;
using CrudConsoleApp.Helpers;
using CrudConsoleApp.Interfaces;
using CrudConsoleApp.Controllers;
using CrudConsoleApp.Other;

namespace CrudConsoleApp
{
    class Program 
    {
        static UsersController uc = new UsersController();

        static void Main(string[] args)
        {

            bool programRunning = true;

            while (programRunning)
            {
                programRunning = MenuSelection();
            }

        }

        static void EditUserPrompt()
        {

            List<string> editableValues = new List<string>()
            {
                "First Name",
                "Last Name",
                "Email",
                "Address"
            };


            string editingStep = "choose user";

            User selectedUser = null;
            int selectedAttribute = 0;

            while (editingStep != "back to main menu")
            {
                if(uc.Read(out List<User> users))
                {

                    List<string> UserInfos = new List<string>();

                    foreach (User item in users)
                    {
                        UserInfos.Add($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName} Email: {item.Email} Address: {item.Address} Registered At: {item.UserRegisteredAt}");
                    }


                    switch (editingStep)
                    {
                        case "choose user":

                            selectedUser = null;

                            while (selectedUser == null)
                            {

                                int choosenIndex = SelectOptions("Select which user you would like to edit: ", UserInfos.ToArray());

                                if (choosenIndex == -1)
                                {

                                    editingStep = "back to main menu";
                                    break;

                                }
                                else if (choosenIndex > 0)
                                {
                                    selectedUser = users[choosenIndex - 1];
                                    editingStep = "choose attribute";
                                }

                            }

                            break;


                        case "choose attribute":
                            selectedAttribute = 0;
                            while (selectedUser != null && selectedAttribute == 0)
                            {

                                string[] valueOptions ={
                                $"First Name: { selectedUser.FirstName}",
                                $"Last Name: { selectedUser.LastName}",
                                $"Email: { selectedUser.Email}",
                                $"Address: { selectedUser.Address}"
                            };

                                selectedAttribute = SelectOptions("Select what you would like to edit: ", valueOptions);

                                if (selectedAttribute == -1)
                                {
                                    editingStep = "choose user";
                                    selectedAttribute = 0;
                                    selectedUser = null;
                                }
                                else
                                {
                                    editingStep = "edit attribute";
                                }
                            }

                            break;


                        case "edit attribute":

                            Console.Write($"Enter new {editableValues[selectedAttribute - 1]}: ");
                            string input = Console.ReadLine();
                            if (uc.Update(ref selectedUser, editableValues[selectedAttribute - 1], ref input))
                            {

                                Console.WriteLine($"{editableValues[selectedAttribute - 1]} edited on user with id {selectedUser.Id}...");

                                editingStep = "again";

                            }

                            else
                            {
                                Console.WriteLine($"Oh.. something went wrong here.. sorry 'bout that ;)");
                            }

                            break;

                        case "again":

                            int again = 0;
                            while (again != 1 && again != 2)
                            {
                                Console.WriteLine("Any thing else you would like to edit?");
                                again = SelectOptions("Any thing else you would like to edit?", new string[] { "Yes", "No" }, false);
                            }


                            editingStep = again == 1 ? "choose attribute" : "back to main menu";

                            break;
                        default:
                            //huh?
                            break;

                    }
                    

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No users found..");
                    return;
                }
                
              

            }
            Console.Clear();

        }

        static int SelectOptions(string selectionMessage, string[] options, bool exitable = true)
        {
            if(selectionMessage != "")
            {
                ConsoleHelper.DrawLine();
                Console.WriteLine(selectionMessage);
            }

            for(int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i+1}. {options[i]}");
            }

            if (exitable)
            {
                Console.WriteLine($"{options.Length + 1}. Go Back");
            }

            ConsoleHelper.DrawLine();
            Console.Write("Select: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int parsedInput))
            {
                if(parsedInput > 0 && parsedInput < options.Length +1)
                {
                    return parsedInput;
                }
                else if(exitable && parsedInput == options.Length +1)
                {
                    return -1;
                }

               
            }

            Console.WriteLine("Invalid input... ");

            return 0;
            
        }

        static void RemoveUserPrompt()
        {
            if(uc.Read(out List<User> users))
            {
                List<string> UserInfos = new List<string>();

                foreach (User item in users)
                {
                    UserInfos.Add($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName} Email: {item.Email} Address: {item.Address} Registered At: {item.UserRegisteredAt}");
                }


                int input = 0;

                while (input == 0)
                {
                    input = SelectOptions("Select which user you would like to remove: ", UserInfos.ToArray());



                    if (input > 0 && input - 1 < UserInfos.Count)
                    {
                        User userToDelete = users[input - 1];

                        if (userToDelete == null)
                        {
                            Console.WriteLine("User not found...");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                            return;
                        }

                        uc.Delete(ref userToDelete);
                    }

                }
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No user´s found..");
            }
              
            
        }

        static bool MenuSelection()
        {
            DisplayUsers();
            ConsoleHelper.DrawLine();
            string[] options = { "Add", "Edit", "Remove", "Binary Search", "Exit"};
            int input = SelectOptions("", options, false);

            switch (input)
            {
                case 1:
                    
                    var newUser = AddUserForm();
                    uc.Create(ref newUser);
                    break;
                case 2:
                    EditUserPrompt();
                    break;
                case 3:
                    RemoveUserPrompt();
                    break;
                case 4:
                    new BinarySearcher().Run(); ;
                    break;
                case 5:
                    return false; 
                default:
                    Console.WriteLine("Invalid input...");
                    break;
            }

            return true;
        }

        static void DisplayUsers()
        {
            
            ConsoleHelper.DrawLine();

            if(uc.Read(out List<User> users))
            {

                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}, {u.FirstName}, {u.LastName}, {u.Email}, {u.Address}, {u.UserRegisteredAt}");
                }

            }
            else
            {
                Console.WriteLine("No user´s found..");
            }
            
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
