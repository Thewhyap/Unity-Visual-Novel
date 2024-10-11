using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.Windows;
using Unity.VisualScripting;

namespace DIALOGUE
{
    public class DL_COMMAND_DATA
    {
        public Command command;
        private const char ARGUMENTS_CONTAINER = '(';
        private const char ARGS_SEPARATOR = ' ';
        private const char ESCAPE_CHAR = '\\';
        private const char TEXT_CONTAINER = '"';
        private const char OPTIONAL_VARIABLE_CHAR = '-';

        public struct Command
        {
            public string name;
            public Dictionary<string, string> optionalParameters;
            public string[] mandatoryParameters;
            public bool waitForCompletion;
        }

        public DL_COMMAND_DATA(string rawCommand, bool shouldWait = false)
        {
            command = GetCommandFromString(rawCommand);
            command.waitForCompletion = shouldWait;
        }

        private Command GetCommandFromString(string rawCommand)
        {
            Command command = new();
            int index = rawCommand.IndexOf(ARGUMENTS_CONTAINER);
            command.name = rawCommand[..index];

            (command.mandatoryParameters, command.optionalParameters) = GetParameters(rawCommand.Substring(index + 1, rawCommand.Length - index - 2));

            return command;
        }

        private (string[], Dictionary<string, string>) GetParameters(string args)
        {
            Dictionary<string, string> optionalParameters = new();
            List<string> mandatoryParameters = new();
            string optionalParameter = "";
            bool inText = false;
            bool isEscaped = false;
            int lastIndex = 0;

            for (int i = 0; i < args.Length; i++)
            {
                char c = args[i];
                if (c == ESCAPE_CHAR) isEscaped = !isEscaped;
                else if (c == TEXT_CONTAINER && !isEscaped) inText = !inText;
                else if (c == ARGS_SEPARATOR && !inText)
                {
                    string arg = args[lastIndex..i];
                    if (arg.StartsWith(OPTIONAL_VARIABLE_CHAR) && !double.TryParse(arg, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
                    {
                        optionalParameter = arg[1..];
                    }
                    else
                    {
                        if(optionalParameter != "")
                        {
                            optionalParameters.Add(optionalParameter, arg);
                            optionalParameter = "";
                        }
                        else
                        {
                            mandatoryParameters.Add(arg);
                        }
                    }
                    lastIndex = i + 1;
                }
            }

            if (lastIndex < args.Length)
            {
                string arg = args[lastIndex..];
                if (optionalParameter != "")
                {
                    optionalParameters.Add(optionalParameter, arg);
                    optionalParameter = "";
                }
                else
                {
                    mandatoryParameters.Add(arg);
                }
            }

            return (mandatoryParameters.ToArray(), optionalParameters);
        }
    }
}