using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DIALOGUE_LINE
    {
        public int elementLayer;
        public DL_SPEAKER_DATA speakerData;
        public DL_DIALOGUE_DATA dialogueData;
        public DL_COMMAND_DATA commandData;

        public bool hasSpeaker => speakerData != null;
        public bool hasDialogue => dialogueData != null;
        public bool hasCommand => commandData != null;

        public DIALOGUE_LINE(int elementLayer, bool shouldWait, string speaker, string displayName, string action, string dialogue, string command)
        {
            this.elementLayer = elementLayer;
            this.speakerData = string.IsNullOrWhiteSpace(speaker) ? null : new DL_SPEAKER_DATA(speaker, displayName);
            this.dialogueData = dialogue.Equals(null) ? null : new DL_DIALOGUE_DATA(dialogue);
            this.commandData = string.IsNullOrWhiteSpace(command) ? null : new DL_COMMAND_DATA(command, shouldWait);
        }
    }
}
