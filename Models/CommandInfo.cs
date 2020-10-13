namespace CommandsAndHandlers.Models
{
    public class CommandInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public CommandInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}