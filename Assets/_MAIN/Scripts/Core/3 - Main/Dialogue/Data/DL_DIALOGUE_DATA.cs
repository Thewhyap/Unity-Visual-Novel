using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.Windows;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

namespace DIALOGUE
{
    public class DL_DIALOGUE_DATA
    {
        private const char TEXT_CONTAINER = '"';
        private const char ESCAPE_CHAR = '\\';
        private const char TAG_CONTAINER = '<';

        public string dialogue;
        public bool shouldAppend;

        public DL_DIALOGUE_DATA(string rawDialogue, bool shouldAppend = false)
        {
            this.dialogue = ParseDialogue(rawDialogue);
            this.shouldAppend = shouldAppend;
        }

        private string ParseDialogue(string dialogue)
        {
            dialogue = VariablesManager.instance.ParseVariablesInText(dialogue);
            dialogue = PreProcessDialogue(dialogue);
            return dialogue;
        }

        private string PreProcessDialogue(string dialogue)
        {
            dialogue = ReplaceSpecialChars(dialogue);

            string backslashPattern = @"(?<!\\)<(\\)>";
            return Regex.Replace(dialogue, backslashPattern, "\\");
        }

        private static string ReplaceSpecialChars(string input)
        {
            var replacements = new Dictionary<string, string>
            {
                { "\\<", "<noparse><</noparse>" },
                { "\\\"", "\"" },
                { "\\{", "{" }
            };

            foreach (var pair in replacements)
            {
                input = input.Replace(pair.Key, pair.Value);
            }

            return input;
        }
    }
}
