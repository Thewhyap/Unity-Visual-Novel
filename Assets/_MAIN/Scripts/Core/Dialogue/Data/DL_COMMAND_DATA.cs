using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DL_COMMAND_DATA
    {
        public Command command;
        private const char ARGUMENTSCONTAINER_ID = '(';

        public struct Command
        {
            public string name;
            public string[] arguments;
            public bool waitForCompletion;
        }

        public DL_COMMAND_DATA(string rawCommand, bool shouldWait)
        {
            command = GetCommandFromString(rawCommand);
            command.waitForCompletion = shouldWait;
        }

        private Command GetCommandFromString(string rawCommand)
        {
            Command command = new();
            int index = rawCommand.IndexOf(ARGUMENTSCONTAINER_ID);
            command.name = rawCommand.Substring(0, index).Trim();

            /*if (command.name.ToLower().StartsWith(WAITCOMMAND_ID))
            {
                command.name = command.name.Substring(WAITCOMMAND_ID.Length);
                command.waitForCompletion = true;
            }
            else command.waitForCompletion = false;

            command.arguments = GetArgs(cmd.Substring(index + 1, cmd.Length - index - 2));
            result.Add(command);*/

            return command;
        }

        private string[] GetArgs(string args)
        {
            List<string> argList = new List<string>();
            StringBuilder currentArg = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (!inQuotes && args[i] == ' ')
                {
                    argList.Add(currentArg.ToString());
                    currentArg.Clear();
                    continue;
                }

                currentArg.Append(args[i]);
            }
            if (currentArg.Length > 0)
            {
                argList.Add(currentArg.ToString());
            }

            return argList.ToArray();
        }
    }
}