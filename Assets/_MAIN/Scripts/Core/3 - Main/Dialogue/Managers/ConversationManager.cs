using COMMANDS;
using CHARACTERS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
        private Coroutine process = null;
        private TextArchitect architect = null;
        private bool userPrompt = false;

        public bool isRunning => process != null;

        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }
        
        public Coroutine StartConversation(List<string> conversation)
        {
            StopConversation();

            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));

            return process;
        }

        public void StopConversation()
        {
            if(!isRunning) return;

            dialogueSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            for(int i = 0; i < conversation.Count; i++)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(conversation[i])) continue;

                List<DIALOGUE_LINE> parsedLines = DialogueParser.Parse(conversation[i]);

                foreach (DIALOGUE_LINE parsedLine in parsedLines)
                {
                    //if (parsedLine.hasAction) yield return Line_RunAction(parsedLine);
                    if (parsedLine.hasDialogue) yield return Line_RunDialogue(parsedLine);
                    if (parsedLine.hasCommand) yield return Line_RunCommand(parsedLine);

                    // Default behaviour for dialogue lines
                    if (parsedLine.hasDialogue)
                    {
                        yield return WaitForUserInput();

                        CommandManager.instance.StopAllProcesses();
                    }
                }
            }
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            if (line.hasSpeaker) HandleSpeakerLogic(line.speakerData);

            yield return BuildDialogue(line.dialogueData);
        }

        private void HandleSpeakerLogic(DL_SPEAKER_DATA speakerData)
        {

            /*if (speakerData.showCharacter && (!character.isVisible && !character.isRevealing)) character.Show();

            dialogueSystem.ShowSpeakerName(speakerData.displayName);

            DialogueSystem.instance.ApplySpeakerDataToDialogueContainer(speakerData.name);

            if (speakerData.isCastingPosition) character.MoveToPosition(speakerData.castPosition);

            if (speakerData.isCastingExpressions)
            {
                foreach (var castingExpression in speakerData.CastExpressions)
                    character.OnReceiveCastingExpression(castingExpression.layer, castingExpression.expression);
            }*/
        }

        IEnumerator Line_RunCommand(DIALOGUE_LINE line)
        {
            DL_COMMAND_DATA.Command command = line.commandData.command;

            if (command.waitForCompletion || command.name == "wait")
            {
                /*CoroutineWrapper cw = CommandManager.instance.Execute(command.name, command.mandatoryParameters);
                while (!cw.isDone)
                {
                    if (userPrompt)
                    {
                        CommandManager.instance.StopCurrentProcess(); //TODO do we really want that?
                        userPrompt = false;
                    }
                            
                    yield return null;
                };*/
            }
            else CommandManager.instance.Execute(command.name, command.mandatoryParameters);

            yield return null;
        }

        IEnumerator BuildDialogue(DL_DIALOGUE_DATA dialogueData)
        {
            if(!dialogueData.shouldAppend) architect.Build(dialogueData.dialogue);
            else architect.Append(dialogueData.dialogue);

            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    architect.ForceComplete();
                    userPrompt = false;
                }
                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while (!userPrompt) { 
                yield return null;
            }

            userPrompt = false;
        }
    }
}
