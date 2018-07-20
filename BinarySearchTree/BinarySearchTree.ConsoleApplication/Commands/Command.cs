using System;
using System.Collections.Generic;
using System.Linq;
using BinarySearchTree.Core;

namespace BinarySearchTree.ConsoleApplication.Commands
{
    public class Parameter
    {
        public string Index;
        public string Name;

        public Parameter(string index, string name)
        {
            Index = index;
            Name = name;
        }
    }
    public abstract class Command<T>  where T : AbstractNode
    {
        public WiredTree<T> Tree;
        public string CommandName { get; set; }
        public string Description { get; set; }
        public List<Parameter> Parameters;

        protected Command(WiredTree<T> tree, string commandName, string description, List<Parameter> parameters)
        {
            Tree = tree;
            CommandName = commandName;
            Description = description;
            Parameters = parameters;
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

        #region Console writes

        protected void WriteSuccess(string format, params object[] parameters)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(format, parameters);
            Console.ResetColor();
        }

        protected void PrintChainList(List<object> list)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            list.ForEach(node =>
            {
                if (node != list.Last())
                    Console.Write(node + " => ");
                else
                {
                    Console.Write(node + ".");
                    Console.WriteLine();
                }
            });
            Console.ResetColor();
        }

        protected void WriteError(string format, params object[] parameters)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(format, parameters);
            Console.ResetColor();
        }
        
        protected void WriteWarning(string format, params object[] parameters)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(format, parameters);
            Console.ResetColor();
        }

        #endregion

        #region Validations

        protected bool ReceivedIdOnlyParameter(params string[] parameters)
        {
            int id;
            return parameters.Length == 1 && int.TryParse(parameters[0], out id);
        }

        protected bool IsEmptyParameters(params string[] parameters)
        {
            return parameters.Length == 0;
        }

        #endregion

        #region Parsing parameters

        protected int GetIdFromFirstParameter(params string[] parameters)
        {
            return int.Parse(parameters[0]);
        }

        protected string GetPath(params string[] parameters)
        {
            return string.Join(" ", parameters);
        }

        #endregion
    }
}
