using CHARACTERS;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    public class DL_SPEAKER_DATA
    {
        private const string EMPTY_SPEAKER_NAME = "null";

        public Character character = null;
        public string displayName = null;

        public DL_SPEAKER_DATA(string rawSpeaker, string displayName = null)
        {
            if (displayName != null) this.displayName = displayName;
            if (rawSpeaker.ToLower() != EMPTY_SPEAKER_NAME)
            {
                try
                {
                    character = CharacterManager.instance.GetCharacter(VariablesManager.instance.ProcessWord(rawSpeaker)?.ToString() ?? rawSpeaker);
                    this.displayName = character.displayName;
                }
                catch
                {
                    this.displayName ??= rawSpeaker;
                }
            }
            displayName ??= "";
        }
    }
}
