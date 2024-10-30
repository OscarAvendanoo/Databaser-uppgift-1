using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentregistreringsprogram
{
    
    internal class ApplicationUI
    {
        public bool UiRunning { get; set; } = true;
        public  DatabaseHandler DbHandler { get; set; }

        public ApplicationUI(DatabaseHandler dbHandler)
        {
            this.DbHandler = dbHandler;
        }

        public void RunUI()
        {
            while (UiRunning)
            {
                PrintWelcomeMessage();
                PrintMainMenu();
                int choice = ChoiceHandler();
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        string[] StudentInfo = NewStudentInput();
                        string message = DbHandler.AddNewStudent(StudentInfo[0], StudentInfo[1], StudentInfo[2]);
                        Console.WriteLine(message);
                        PressEnterToContinue();

                        break;
                    case 2:
                        var newData = NewDataInput();
                        if ((int)newData[0] == 1)
                        {
                            // Cast each element conditionally, allowing null values to be passed as parameters.

                            Student? student = newData[1] as Student;  
                            string? firstName = newData[2] as string;     
                            string? lastName = newData[3] as string;      
                            string? city = newData[4] as string;
                            
                            string returnedMessage = DbHandler.ChangeStudentData(student, firstName, lastName, city);
                            Console.WriteLine(returnedMessage);
                            PressEnterToContinue();
                        }  
                        break;
                    case 3:
                        var allStudents = DbHandler.GetAllStudents();
                        ListAllStudents(allStudents);
                        PressEnterToContinue();
                        break;
                    case 4:
                        UiRunning = false;
                        break;
                    default:
                        break;
                }

            }

            
           
        }
        public void ListAllStudents(List<Student> allStudents)
        {
            foreach (Student s in allStudents)
            {
                Console.WriteLine($"{s.StudentId}. {s.FirstName} {s.LastName} {s.City}");
            }
        }
        public void PrintWelcomeMessage()
        {
            Console.WriteLine("WELCOME TO THE STUDENT REGISTER\n");
            
        }

        public void PrintMainMenu()
        {
            Console.WriteLine("Choose one of the following alternatives;\n");
            Console.WriteLine("1. Registry a new student.");
            Console.WriteLine("2. Change a students data.");
            Console.WriteLine("3. List all students in the registry.");
            Console.WriteLine("4. Exit program.");
        }
        public void PressEnterToContinue()
        {
            Console.WriteLine("press enter to continue..");
            Console.ReadLine();
            Console.Clear();
        }
        public int ChoiceHandler()
        {
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }
        public string[] NewStudentInput()
        {
            string? firstName;
            string? lastName;
            string? city;
            string[] StudentInfo = new string[3];

            // ändrade inlämningen i efterhand här nedan för att programmet inte ska tillåta
            // att namn och städer skrivs i något annat än bokstäver
            
            do {
                Console.WriteLine("Please enter the firstname of the student;");
                firstName = Console.ReadLine();
                Console.Clear();
            } while (firstName is "" || firstName.All(char.IsLetter) == false);
            StudentInfo[0] = firstName;

            
            do
            {
                Console.WriteLine("Please enter the lastname of the student;");
                lastName = Console.ReadLine();
                Console.Clear();
            } while (lastName is "" || lastName.All(char.IsLetter) == false);

            StudentInfo[1] = lastName;

           
            do
            {
                Console.WriteLine("Please enter the city in which the student is located;");
                city = Console.ReadLine();
                Console.Clear();
            } while (city.All(char.IsLetter) == false);
            if (city is "")
            {
                city = "unknown location";
            }
            
            StudentInfo[2] = city;
            Console.Clear();

            return StudentInfo; 
        }

        public object[] NewDataInput()
        {

            object[] newData = new object[5];

            // validData is set to 0 if students cant be found. Else it is set to 1. This integer is checked in an if statement when its returned to the calling method. 

            int? validData = null;
            string? newFirstName = null;
            string? newLastName = null;
            string? newCity = null;
            int? newID = null;
            int choice;

            Console.WriteLine("The students will now be listed. Choose which student to change data on by typing its id number.");
            PressEnterToContinue();
            var listOfStudents = DbHandler.GetAllStudents();
            if(listOfStudents == null)
            {
                Console.WriteLine("There are no students in the registry yet. Returning to main menu.");
                PressEnterToContinue();
                validData = 0;
                newData[0] = validData;
                return newData;
            }
            Console.WriteLine("Choose the ID of the student you want to change data on;\n");
            ListAllStudents(listOfStudents);
            Console.WriteLine();
            int choosenStudent = ChoiceHandler();
            Console.Clear();
            
            var student = this.DbHandler.GetStudent(choosenStudent);
            if (student == null) {
                Console.WriteLine("The student was not found, please try again.");
                PressEnterToContinue();
                validData = 0;
                newData[0] = validData;
                return newData;
            }
            
            Console.WriteLine("What data do you want to change?\n");
            Console.WriteLine("1. Name of the student");
            Console.WriteLine("2. City of the student");
            choice = int.Parse(Console.ReadLine());
            Console.Clear();

            // indentionen här ser konstigt ut då jag ändrade i koden efter inlämning
            // Laddade upp hela projektet och orkade inte tracka projektet via git nu i efterhand,
            // så det blev copy paste på den nya switchen. 
           switch (choice)
{
    case 1:
        do {
            Console.WriteLine("Write the firstname of the student;");
            newFirstName = Console.ReadLine();
            Console.Clear();
        }while (newFirstName is "" || newFirstName.All(char.IsLetter) == false);

        do
        {
            Console.WriteLine("Write the lastname of the student;");
            newLastName = Console.ReadLine();
            Console.Clear();
        } while (newLastName is "" || newLastName.All(char.IsLetter) == false);

        validData = 1;
        break;

    case 2:
        do
        {
            Console.WriteLine("Write the new city of the student;");
            newCity = Console.ReadLine();
            Console.Clear();
        } while (newCity.All(char.IsLetter) == false);
        if (newCity is "")
        {
            newCity = "unknown location";
        }
        validData = 1;
        break;
        
    default:
        Console.WriteLine("You did not choose a valid option");
        PressEnterToContinue();
        validData = 0;

        break;
}
            

            newData[0] = validData;
            newData[1] = student;
            newData[2] = newFirstName;
            newData[3] = newLastName;
            newData[4] = newCity;
            


            return newData;

        }
    }
}
