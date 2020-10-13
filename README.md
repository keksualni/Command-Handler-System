# Command-Handler-System
Console application that uses Command-Handler system for proccessing commands. Automatic Dispatcher initialization (you don't need rewrite existing code to add new functionality)

## How To Use
1. Create Command
   * Define class with your command name.
      ```
      public class YourCommandName
      {
      }
      ```
   * Use attribute to define command execute name.
      ```
      [Command("execute-name")]
      public class YourCommandName
      {
      }
      ```
      You can also define command description using second argument of attribute (later, you can see it by 'help' command).
      ```
      [Command("execute-name", "Some cool command!")]
      public class YourCommandName
      {
      }
      ```
   * Inherit from 'Command' abstract class and implement it like below. You also may not to implement 'Execute' function, and it will just log in console, that command executed.
      ```
      [Command("execute-name", "Some cool command!")]
      public class YourCommandName : Command
      {
          public YourCommandName(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }


          public override void Execute()
          {
              CommandDispatcher.Dispatch(this);
          }
      }
      ```
    * You can add parameters with which command will be executed (as many as you want). To fill them implement 'FillCommandValues' function.
      ```
      [Command("execute-name", "Some cool command!")]
      public class YourCommandName : Command
      {
          public YourCommandName(ICommandDispatcher commandDispatcher) : base(commandDispatcher) { }


          public string SomeParameter { get; set; }


          public override void FillCommandValues()
          {
              Console.Write("Enter parameter value: ");
              SomeParameter = Console.ReadLine();
          }

          public override void Execute()
          {
              CommandDispatcher.Dispatch(this);
          }
      }
      ```
1. Create Handler
   * Define class, that implements 'ICommandHandler<>' interface. Pass your command name, you want to handle, as generic parameter.
     ```
     public class YourCommandHandler : ICommandHandler<YourCommandName>
     {
     }
     ```
   * Implement interface. You can get access to the command parameters in 'Handle' method.
     ```
     public class YourCommandHandler : ICommandHandler<YourCommandName>
      {
          public void Handle(YourCommandName command)
          {
              Console.WriteLine($"Command with parameter {command.SomeParameter} has been handled!");
          }
      }
     ```
1. Use It!
Just run the programm, and type command execute name you passed in attribute.
