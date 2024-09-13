using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CHARACTERS
{
    [System.Serializable]
    public class CharacterConfigData
    {
        public string name;
        public string alias;
        public Character.CharacterType characterType;

        public Color nameColor;
        public Color dialogueColor;

        public TMP_FontAsset nameFont;
        public TMP_FontAsset dialogueFont;

        public CharacterConfigData Copy()
        {
            CharacterConfigData copy = new CharacterConfigData();
            copy.name = name;
            copy.alias = alias;
            copy.characterType = characterType;
            copy.nameColor = new Color(nameColor.r, nameColor.g, nameColor.b, nameColor.a);
            copy.dialogueColor = new Color(dialogueColor.r, dialogueColor.g, dialogueColor.b, dialogueColor.a);
            copy.nameFont = nameFont;
            copy.dialogueFont = dialogueFont;
            return copy;
        }

        private static Color defaultColor => DialogueSystem.instance.config.defaultTextColor;
        private static TMP_FontAsset defaultFont => DialogueSystem.instance.config.defaultFont;

        public static CharacterConfigData Default
        {
            get
            {
                CharacterConfigData result = new CharacterConfigData();
                result.name = "";
                result.alias = "";
                result.characterType = Character.CharacterType.Text;
                result.nameColor = defaultColor;
                result.dialogueColor = defaultColor;
                result.nameFont = defaultFont;
                result.dialogueFont = defaultFont;
                return result;
            }
        }
    }
}
