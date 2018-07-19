namespace BinarySearchTree.ConsoleApplication.Commands
{
    public abstract class Command
    {
        public string CommandName { get; set; }
        public string Description { get; set; }

        protected Command(string commandName, string description)
        {
            CommandName = commandName;
            Description = description;
        }

        /// <summary>
        /// Validate the parameters of the command
        /// </summary>
        /// <param name="parameters">parameters list</param>
        /// <returns></returns>
        public abstract bool ValidateParams(params string[] parameters);

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameters"></param>
        public abstract void Execute(params string[] parameters);
    }
}
