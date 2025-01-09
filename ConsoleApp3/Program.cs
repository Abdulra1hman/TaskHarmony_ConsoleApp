using System;
using System.Collections.Generic;
using System.Threading;

namespace TaskHarmonyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskHarmonyApp app = new TaskHarmonyApp();
            app.Start();
        }
    }

    public class TaskHarmonyApp
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private int userIdCounter = 1000;

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Welcome to ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("TaskHarmony App!");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Choose an action:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Reset Password");
                Console.WriteLine("4. Change Password");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("5. Exit");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Enter your choice (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        ResetPassword();
                        break;
                    case "4":
                        ChangePassword();
                        break;
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Exiting Task Harmony App. Goodbye!");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
        }

        private void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Enter your email address: ");
            string email = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (users.ContainsKey(email) && users[email].Password == password)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login successful!");
                GoToMainPage(users[email]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("Invalid email or password.");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(" Please try again.");
            }
        }

        private void GoToMainPage(User user)
        {
            Console.Clear();

            while (true)
            {
                Thread.Sleep(1000);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Navigating to Main Page...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Welcome back, {user.Name}!");
                Console.WriteLine($"ID Number: {user.IDNumber}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Operations:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Chat");
                Console.WriteLine("2. Safety");
                Console.WriteLine("3. Absence");
                Console.WriteLine("4. Information about the app");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("5. Logout");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Enter your choice (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Chat(user);
                        break;
                    case "2":
                        Safety(user);
                        break;
                    case "3":
                        Absence(user);
                        break;
                    case "4":
                        DisplayAppInformation();
                        break;
                    case "5":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void Register()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.Write("Enter your email address: ");
            string email = Console.ReadLine();

            if (!email.EndsWith("@gmail.com"))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine("Please enter a valid Gmail address.");
                return;
            }

            if (users.ContainsKey(email))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine("Email already exists. Please choose another email.");
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.Write("Enter your password between (8 - 15): ");
            var Password = Console.ReadLine();

            bool IsPasswordValid(string password)
            {
                Password = password;
                return password.Length > 8 &&
                       password.Length <= 15 &&
                       password.Any(char.IsDigit) &&
                       password.Any(char.IsLetter) &&
                       (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));
            }
            if (Password.Length! < 8)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.Write("Password length is less than 8.");
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Please Enter a password between 8 or 15");
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.Write("the name is empty .");
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Please Enter the correct name.");
                return;
            }


            int idNumber = userIdCounter++;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Register in process...");

            int professionChoice = ChooseProfession();

            bool hasChatAccess = true;

            User newUser = null;
            Thread.Sleep(2000);
            switch (professionChoice)
            {
                case 1:
                    newUser = new Worker(email, Password, name, idNumber, hasChatAccess);
                    break;
                case 2:
                    newUser = new Supervisor(email, Password, name, idNumber, hasChatAccess);
                    break;
                case 3:
                    newUser = new Engineer(email, Password, name, idNumber);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Invalid profession.");
                    break;
            }

            users.Add(email, newUser);
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Registration successful!");
            Thread.Sleep(500);
            GoToMainPage(newUser);
        }

        private int ChooseProfession()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Choose your profession:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("1. Worker");
            Console.WriteLine("2. Supervisor");
            Console.WriteLine("3. Engineer");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Enter your choice (1-3): ");
            int professionChoice = int.Parse(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("Your request is being processed....");

            switch (professionChoice)
            {
                case 1:
                case 2:
                case 3:
                    return professionChoice;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    throw new InvalidOperationException("Invalid profession choice.");
            }

        }

        private void Chat(User user)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;

            if (user is Worker worker)
            {
                Console.WriteLine("Choose who you want to chat with:");
                Console.WriteLine("1. Engineer");
                Console.WriteLine("2. Supervisor");
                Console.Write("Enter your choice (1-2): ");
                int chatChoice = int.Parse(Console.ReadLine());

                switch (chatChoice)
                {
                    case 1:
                        foreach (var message in Worker.chattoEngineer)
                        {
                            Console.WriteLine($"  Worker: {message}");
                        }
                        foreach (var message in Engineer.chattoWorker)
                        {
                            Console.WriteLine($"  Engineer: {message}");
                        }
                        Console.WriteLine(" Choose what you do want to do:");
                        Console.WriteLine("1. Reply");
                        Console.WriteLine("2. Exit");
                        int todochoose = int.Parse(Console.ReadLine());
                        switch (todochoose)
                        {
                            case 1:
                                Console.Write("You can send now: ");
                                worker.chattoEngineerr(Console.ReadLine());
                                break;
                        }
                        break;
                    case 2:
                        foreach (var message in Worker.chattosupervisor)
                        {
                            Console.WriteLine($"  Worker: {message}");
                        }
                        foreach (var message in Supervisor.chattoWorker)
                        {
                            Console.WriteLine($"  Supervisor: {message}");
                        }
                        Console.WriteLine(" Choose what you do want to do:");
                        Console.WriteLine("1. Reply");
                        Console.WriteLine("2. Exit");
                        int todochoose1 = int.Parse(Console.ReadLine());
                        switch (todochoose1)
                        {
                            case 1:
                                Console.Write("You can send now: ");
                                worker.chattosupervisorr(Console.ReadLine());
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            else if (user is Supervisor supervisor)
            {
                Console.WriteLine("Choose who you want to chat with:");
                Console.WriteLine("1. Engineer");
                Console.WriteLine("2. Worker");
                Console.Write("Enter your choice (1-2): ");
                int chatChoice = int.Parse(Console.ReadLine());
                switch (chatChoice)
                {
                    case 1:
                        foreach (var message in Engineer.chattoWorker)
                        {
                            Console.WriteLine($" Engineer: {message}");
                        }
                        foreach (var message in Supervisor.chattoEngineer)
                        {
                            Console.WriteLine($" Supervisor: {message}");
                        }
                        Console.WriteLine(" Choose what you do want to do:");
                        Console.WriteLine("1. Reply");
                        Console.WriteLine("2. Exit");
                        int todochoose = int.Parse(Console.ReadLine());
                        switch (todochoose)
                        {
                            case 1:
                                Console.Write("You can send now: ");
                                supervisor.chattoEngineerr(Console.ReadLine());
                                break;
                        }
                        break;
                    case 2:
                        foreach (var message in Worker.chattosupervisor)
                        {
                            Console.WriteLine($" Worker: {message}");
                        }
                        foreach (var message in Supervisor.chattoWorker)
                        {
                            Console.WriteLine($" Supervisor: {message}");
                        }
                        Console.WriteLine(" Choose what you do want to do:");
                        Console.WriteLine("1. reply");
                        Console.WriteLine("2. Exit");
                        int todochoose1 = int.Parse(Console.ReadLine());
                        switch (todochoose1)
                        {
                            case 1:
                                Console.Write("You can send now: ");
                                supervisor.chattoWorkerr(Console.ReadLine());
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }

            else if (user is Engineer Engineer)
            {

                Console.WriteLine("Choose who you want to chat with:");
                Console.WriteLine("1. Worker");
                Console.WriteLine("2. Supervisor");
                Console.Write("Enter your choice (1-2): ");
                int chatChoice = int.Parse(Console.ReadLine());

                switch (chatChoice)
                {
                    case 1:
                        foreach (var message in Worker.chattoEngineer)
                        {
                            Console.WriteLine($"  Worker: {message}");
                        }
                        foreach (var message in Engineer.chattoWorker)
                        {
                            Console.WriteLine($"  Engineer: {message}");
                        }
                        Console.WriteLine(" Choose what you do want to do:");
                        Console.WriteLine("1. Reply");
                        Console.WriteLine("2. Exit");
                        int todochoose = int.Parse(Console.ReadLine());
                        switch (todochoose)
                        {
                            case 1:
                                Console.Write("You can send now: ");
                                Engineer.chattoWorkerr(Console.ReadLine());
                                break;
                        }
                        break;
                    case 2:
                        foreach (var message in Supervisor.chattoEngineer)
                        {
                            Console.WriteLine($"  Supervisor: {message}");
                        }
                        foreach (var message in Engineer.chattosupervisor)
                        {
                            Console.WriteLine($"  Engineer: {message}");
                        }
                        Console.WriteLine(" Choose what you do want to do:");
                        Console.WriteLine("1. Reply");
                        Console.WriteLine("2. Exit");
                        int todochoose1 = int.Parse(Console.ReadLine());
                        switch (todochoose1)
                        {
                            case 1:
                                Console.Write("You can send now: ");
                                Engineer.chattosupervisorr(Console.ReadLine());
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void Safety(User user)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;

            if (user is Worker worker)
            {
                Console.WriteLine("Safety image:");
                string imagePath = Console.ReadLine();
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                worker.AddSharedImage(imageBytes);
            }
            else if (user is Supervisor supervisor)
            {
                Console.WriteLine("Safety image:");
                string imagePath = Console.ReadLine();
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                supervisor.AddSharedImage(imageBytes);
            }
            else
            {
                Console.WriteLine("Choose image from worker or supervisor:");
                Console.WriteLine("1. Worker");
                Console.WriteLine("2. Supervisor");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        if (Worker.SharedImages == null)
                        {
                            Console.WriteLine("Not found the Image.");
                        }
                        else
                        {
                            Console.WriteLine("Shared images uploaded by Worker:");
                            foreach (var image in Worker.SharedImages)
                            {
                                Console.WriteLine($"Image: {image}");
                            }
                        }
                        break;
                    case 2:
                        if (Supervisor.SharedImages == null)
                        {
                            Console.WriteLine("Not found the Image.");
                        }
                        else
                        {
                            Console.WriteLine("Shared images uploaded by Supervisor:");
                            foreach (var image in Supervisor.SharedImages)
                            {
                                Console.WriteLine($"Image: {image}");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Your input is not correct.");
                        break;
                }
            }
        }

        private void Absence(User user)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;

            if (!user.IsAbsenceRecorded)
            {
                user.RecordAbsenceEntry();
                Console.WriteLine("Absence entry recorded.");
                Thread.Sleep(200);
            }
            else
            {
                user.RecordAbsenceExit();
                Console.WriteLine("Absence exit recorded.");
                Console.WriteLine($"Absence duration: {user.GetAbsenceDuration()}");
                Thread.Sleep(200);
            }
        }

        private void ResetPassword()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write("Enter your email address: ");
            string email = Console.ReadLine();

            if (!users.ContainsKey(email))
            {
                Console.WriteLine("Email not found. Please register before resetting your password.");
                return;
            }

            User user = users[email];

            user.ResetPassword();

            Console.WriteLine("Password reset successfully! Please remember your new password.");
        }

        private void ChangePassword()
        {
            Console.Clear();

            Console.Write("Enter your email address: ");
            string email = Console.ReadLine();

            if (!users.ContainsKey(email))
            {
                Console.WriteLine("Email not found. Please register before changing your password.");
                return;
            }

            User user = users[email];

            Console.Write("Enter your current password: ");
            string currentPassword = Console.ReadLine();

            if (user.Password != currentPassword)
            {
                Console.WriteLine("Incorrect current password. Please try again.");
                return;
            }

            Console.Write("Enter your new password: ");
            string newPassword = Console.ReadLine();
            user.ChangePassword(newPassword);

            Console.WriteLine("Password changed successfully! Please remember your new password.");
        }

        private void DisplayAppInformation()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Information about this app:");
            Console.WriteLine("This app was made to help you and reduce a little dealing with paper,");
            Console.WriteLine("and also save your energy. It's also done to make it easier to communicate");
            Console.WriteLine("between workers and supervisors.");
            Console.WriteLine("We hope that you like the app.");
            Thread.Sleep(1000);
        }
    }

    public abstract class User
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public int IDNumber { get; private set; }
        public bool HasChatAccess { get; private set; }
        public bool IsAbsenceRecorded { get; private set; }
        public DateTime AbsenceEntryTime { get; private set; }
        public DateTime AbsenceExitTime { get; private set; }

        protected User(string email, string password, string name, int idNumber, bool hasChatAccess)
        {
            Email = email;
            Password = password;
            Name = name;
            IDNumber = idNumber;
            HasChatAccess = hasChatAccess;
        }

        public void RecordAbsenceEntry()
        {
            Console.Clear();
            AbsenceEntryTime = DateTime.Now;
            IsAbsenceRecorded = true;
        }

        public void RecordAbsenceExit()
        {
            Console.Clear();
            AbsenceExitTime = DateTime.Now;
            IsAbsenceRecorded = false;
        }

        public TimeSpan GetAbsenceDuration()
        {
            Console.Clear();
            return AbsenceExitTime - AbsenceEntryTime;
        }

        public void ResetPassword()
        {
            Password = GenerateRandomPassword();
        }

        public void ChangePassword(string newPassword)
        {
            Console.Clear();
            Password = newPassword;
        }

        private string GenerateRandomPassword()
        {
            Random rand = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] passwordChars = new char[8];
            for (int i = 0; i < passwordChars.Length; i++)
            {
                passwordChars[i] = chars[rand.Next(chars.Length)];
            }
            Console.WriteLine(passwordChars);
            return new string(passwordChars);
        }
    }

    public class Worker : User
    {
        public static List<byte[]> SharedImages { get; } = new List<byte[]>();
        public static Queue<string> chattosupervisor = new Queue<string>();
        public static Queue<string> chattoEngineer = new Queue<string>();

        public Worker(string email, string password, string name, int idNumber, bool hasChatAccess)
            : base(email, password, name, idNumber, hasChatAccess)
        {
        }

        public void AddSharedImage(byte[] image)
        {
            Console.Clear();
            SharedImages.Add(image);
        }

        public void chattosupervisorr(string Words)
        {
            chattosupervisor.Enqueue(Words);
        }

        public void chattoEngineerr(string Words)
        {
            chattoEngineer.Enqueue(Words);
        }
    }

    public class Supervisor : User
    {
        public static List<byte[]> SharedImages { get; } = new List<byte[]>();
        public static Queue<string> chattoEngineer = new Queue<string>();
        public static Queue<string> chattoWorker = new Queue<string>();

        public Supervisor(string email, string password, string name, int idNumber, bool hasChatAccess)
            : base(email, password, name, idNumber, hasChatAccess)
        {
        }

        public void AddSharedImage(byte[] image)
        {
            Console.Clear();
            SharedImages.Add(image);
        }

        public void chattoEngineerr(string Words)
        {
            chattoEngineer.Enqueue(Words);
        }

        public void chattoWorkerr(string Words)
        {
            chattoWorker.Enqueue(Words);
        }
    }

    public class Engineer : User
    {
        public static List<byte[]> SharedImages { get; } = new List<byte[]>();
        public static Queue<string> chattosupervisor = new Queue<string>();
        public static Queue<string> chattoWorker = new Queue<string>();

        public Engineer(string email, string password, string name, int idNumber)
            : base(email, password, name, idNumber, false)
        {
        }

        public void AddSharedImage(byte[] image)
        {
            Console.Clear();
            SharedImages.Add(image);
        }

        public void chattosupervisorr(string Words)
        {
            chattosupervisor.Enqueue(Words);
        }

        public void chattoWorkerr(string Words)
        {
            chattoWorker.Enqueue(Words);
        }
    }
}