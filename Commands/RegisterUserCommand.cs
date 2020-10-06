using System;

namespace CommandsAndHandlers.Commands
{
    [Command("register-user")]
    public class RegisterUserCommand : Command
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }


        public override void FillCommandValues()
        {
            Console.Write("Enter user first name: ");
            UserFirstName = Console.ReadLine();

            Console.Write("Enter user last name: ");
            UserLastName = Console.ReadLine();
        }
    }
}