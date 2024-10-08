using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueParser
    {
        private const char LINE_SEPARATOR = ',';
        private const char ESCAPE_CHAR = '\\';
        private const char TEXT_CONTAINER = '"';

        private const string WAIT_COMMAND = @"\[\s*(?i:wait)\s*\]";
        private const string WAIT_COMMAND_SHORT = @"\[\s*(?i:w)\s*\]";
        private const string AS_COMMAND = @"\{\s*(?i:as)\s*\}";

        public static List<DIALOGUE_LINE> Parse(string rawLine)
        {
            List<DIALOGUE_LINE> result = new();

            foreach(string parsedLine in SplitRawLine(rawLine))
            {
                result.Add(GetDataFromLine(parsedLine.Trim()));
            }
            return result;
        }

        private static string[] SplitRawLine(string rawLine)
        {
            List<string> parsedLines = new();

            bool inText = false;
            bool isEscaped = false;
            int lastIndex = 0;

            for(int i = 0; i < rawLine.Length; i++)
            {
                char c = rawLine[i];
                if (c == ESCAPE_CHAR) isEscaped = !isEscaped;
                else if (c == TEXT_CONTAINER && !isEscaped) inText = !inText;
                else if (c == LINE_SEPARATOR && !inText)
                {
                    parsedLines.Add(rawLine[lastIndex..i]);
                    lastIndex = i + 1;
                }
            }

            if(lastIndex < rawLine.Length) parsedLines.Add(rawLine[lastIndex..]);
            return parsedLines.ToArray();
        }

        private static DIALOGUE_LINE GetDataFromLine(string parsedLine)
        {
            string elementPattern = @"^(?<elementCount>\s*(\+\s*)+)?\s*";
            string waitPattern = $@"(?<wait>{WAIT_COMMAND}|{WAIT_COMMAND_SHORT})?\s*";
            string namePattern = @"(?<name>[^\(\s""]+(?=\s|$))?";
            string displayNamePattern = $@"(?(name)\s*{AS_COMMAND}\s*(?:""(?<displayName>(?:[^""\\]|\\.)*)""|(?<displayName>\w+)))?";
            string actionPattern = @"(?(name)\s*(?<action>\w+\((?:[^()""\\]|""(?:[^""\\]|\\.)*""|\\.)*\)))?";
            string textPattern = @"\s*(?:""(?<text>(?:[^""\\]|\\.)*)""\s*)?";
            string commandPattern = @"(?<command>\w+\((?:[^()""\\]|""(?:[^""\\]|\\.)*""|\\.)*\))?";

            string pattern = $@"^{elementPattern}{waitPattern}{namePattern}{displayNamePattern}{actionPattern}{textPattern}{commandPattern}$";
            Match match = Regex.Match(parsedLine, pattern);

            int elementLayer = 0;
            bool shouldWait = false;
            string speaker = null, displayName = null, action = null, dialogue = null, command = null;

            if (match.Success)
            {
                elementLayer = match.Groups["elementCount"].Value.Count(c => c == '+');
                shouldWait = !string.IsNullOrEmpty(match.Groups["wait"].Value);
                speaker = match.Groups["name"].Value;
                displayName = match.Groups["displayName"].Value;
                action = match.Groups["action"].Value;
                dialogue = match.Groups["text"].Value;
                command = match.Groups["command"].Value;

                Debug.Log(elementLayer + ", shouldWait: " + shouldWait + ", speaker: " + speaker + ", dipslayName: " + displayName + ", action: " + action + ", dialogue: " + dialogue + ", command: " + command);
            }
            else
            {
                Debug.Log("Parsed line: \"" + parsedLine + "\" does not match the pattern.");
            }

            return new DIALOGUE_LINE(elementLayer, shouldWait, speaker, displayName, action, dialogue, command);
        }
    }
}
