# Documentation

## Dialogue files

### Dialogue Parsing Structure

|**Element**|**Explanation**|
|-----------|---------------|
| `+` * n | Set the line as an element of a special command (The number of `+` represents the layer) **(optional)** |
| `[wait]` / `[w]` | Will wait for the command to end before you can parse the next line. **(optional)** |
| `characterName` | The character that will be displayed on screen. **(optional)** |
| `{as} customName` | Used to set a display name for the character. **(optional)** |
| `action(value -param paramValue)` | Used to set an action that will modify how the elements in the line will be displayed. **(optional)** |
| `"text"` | The dialogue text. Starts and ends with quotes. **(optional)** |
| `command(value -param paramValue)` | Command to execute after the dialogue. **(optional)** |
| `,` | End of the line. **(optional)** |
---

#### Order and conditions

`+` -> `[wait]` -> `characterName` -> `{as} customName` -> `action()` -> `"text"` -> `command()` -> `,`

- `{as} customName` and `action()` cannot be set if there is no `characterName`.

#### Special scenarios

- If you want the dipslay name of the character to be nothing, simply use `""` as the `customName`.
- Setting a dialogue without setting a character will use the previously selected character.
- Actions persist through the lines until a `characterName` is set.

#### Default values

- It won't wait for the command (if there is one) to end before you can parse the next line.
- Character is null.
- layer = 0
- setting a dialogue will replace the previous one

#### Notes

- Empty lines are skipped.
- An empty text `""` is considered a dialogue.
- If you put no text at all, the line will not be considered as a dialogue.
- If line is a dialogue, it will wait for user input. (except if there is a command saying otherwise).
- If line is not a dialogue, it won't wait for user input. (use the `wu()` command to wait for a user input).
- `characterName`, parameters names, tags, `{as}`, `[wait]`, variables, command names and action names are all case insensitive.
- `characterName` must have at least an empty space before the next element, and should not be a number or contains any `(` or `"`.
- If the element layer is too high (no `+` means **layer = 0**), element layer will reduce until it reaches 0.
- `displayName` can have special characters but not a line break.

&nbsp;

### Variables

```
{variableName}
```

Variables can be used in `text`, `characterNames`, `customNames`, and parameters.
- Variables can be updated and refer to variables in the code.
- Use `{variable.variable}` to call variables linked to other variables.

#### Predefined variables

|**Variable**|**Description**|
|-------------|---------------|
| `character` | Get the current character |
| `displayname` | Get the current custom name of the character |
---

&nbsp;

### Tags and special characters

Special characters must be used inside quotes.

|**Character**|**Description**|
|-------------|---------------|
| `\"` | Displays `"` |
| `<\>` | Displays `\` |
| `\{` | Displays `{` |
| `\<` | Displays `<` |
---

The parsing system implemented in this engine uses TextMeshPro RichText.

The full list of tags can be find in the Unity TextMeshPro RichText documentation: [https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/RichText.html]

Here are some basic examples:
|**Tag**|**Description**|
|-------------|---------------|
| `<br>` | Break line |
| `<b></b>` | Displays the text inside in **bold** |
| `<i></i>` | Displays the text inside in *italic* |
|`<underline></underline>`| Displays the text inside is underlined |
|`<strikethrough></strikethrough>`| Displays the text inside crossed-out |
| `<font="fontName"></font>` | Displays the text inside in another FONT |
| `<size=x></s>` | Displays the text inside in another SIZE |
| `<color="colorName"></color>` | Displays the text inside in another COLOR |
---

- If `<x=newValue>` is called before the ending `</x>`, it will override the previous value.
- Some characters (Ex: chinese or mathematic symbols, emojis etc.) may not be parsed correctly. You will need to change the font. If the current font can't handle the character it will display `▯`.

&nbsp;

### Special character names

|**CharacterName**|**Description**|
|-----------------|---------------|
| `null` | Set no character |

&nbsp;

### Parameters

Parameters can be added inside `actions` and `commands`. There is 2 types of parameters: **optional** and **non-optional**.

- Optionnal parameters are called like this: `-paramName paramValue`.
- Non-optionnal parameters are called without using `-paramName`.

### Actions

| **Parameters**|**Description**|
|---------------|---------------|
| `-b bool` | Displays the text in **bold** or not |
| `-i bool` | Displays the text in *italic* or not |
| `-cr crossed` | Displays the text crossed or not |
| `-c string` | Set the COLOR of the text |
| `-f string` | Set the FONT of the text |
| `-s int` | Set the SIZE of the text |
| `-sp float`/`-speed float` | Displays the text with a different SPEED (if FLOAT == 0, displays the text immediately) |
| `-im bool` | Same as using `-sp 0`, displays the text immediately |
| `-a string` | Displays the text with a different SOUND (Empty string: SOUND = none) |
| `-n string` | Displays a custom name |
---

- All actions have predefined values for the parameters but you can still change them if you want.

| **Action**|**Description**|
|-----------|---------------|
| `talking()` / `talk()` | No predefined parameter |
| `shouting()` / `shout()` | `-b true` , `-s 18` | //TODO
| `whispering()` / `whisper()` | `-s 5` |
| `thinking()` / `think()` | `-n "{displayname} (thinking)"` |
| `singing()` / `sing()` | `-i true` , `-f "singingFont"` |
| `reading()` / `read()` | `-n "{displayName} (reading)"` |
| `typing()` / `type()` | `-n "{displayName} (typing)"` , `-f "typewritterFont"` |
| `writting()` / `write()` | `-n "{displayName} (writting)"` , `-f "handwrittingFont"` |
| `drunk()` | Custom effects: size and speed changing with time |
| `love()` | `-c red` |
| `horny()` | `-c pink` |
| `happy()` | `-b true` |
| `scared()` | `-n "{displayName} (reading)"` |
| `sad()` | `-n "{displayName} (reading)"` |
| `excited()` | `-n "{displayName} (reading)"` |
| `tired()` | `-n "{displayName} (reading)"` |
---

&nbsp;

### Commands

#### Parsing commands

|**Command**|**parameters**|**Description**|
|-----------|--------------|---------------|
| `w()` | `n` | Wait `n` seconds before parsing next line. <br><br> - `n`: positive float, set the time to wait in seconds. (0 is instant) |
| `a()` | / | Append next dialogue to the text instead of replacing it. |
| `wa()` | `n` | Wait `n` seconds before parsing next line and append next dialogue to the text instead of replacing it. <br><br> - `n`: positive float, set the time to wait in seconds. (0 is instant) |
| `wuser()`/`wu()` | `-n int` | Wait for `n` user input before parsing next line. (0 by default) <br><br> - `-n`: set the number of user inputs you have to wait. (0 is the same as doing `w(0)`) |
| `repeat()` | `n` | Used to repeat `n` times some lines. ([refer to repeat() in Special commands](#special-commands)) |
| `rollback()` | `n` | Used to go back `n` lines. <br><br> - `n`: positive int, set the number of lines. |
| `skip()` | `n` | Used to skip `n` lines. <br><br> - `n`: positive int, set the number of lines. |

#### Dialogue commands

|**Command**|**parameters**|**Description**|
|-----------|--------------|---------------|
| `delete()` | `stringToDelete` , `-i string` , `-r bool` | Delete the `stringToDelete` inside the text. <br><br> - `stringToDelete`: string, set what part of the text you want to delete. If `stringToDelete` == `""` : delete everything. <br> - `-i`: delete the string at the wanted occurence (starting with 1) (string must be an int or a list of int ([to create lists refer to 1.2.1](#1-paths))). If `-i` == `0` : delete every occurence (0 by default) <br> - `-r`: set if the text has to be deleted in reverse (*right to left*) (true by default) |
| `replace()` | `stringToReplace` , `stringReplacing` , `-i string` , `-r bool` | Replace the `stringToReplace` inside the text by the `stringReplacing`. <br><br> - `stringToReplace`: string, set what part of the text you want to replace. If `stringToReplace` == `""` : replace everything. <br> - `stringReplacing`: string, set the replacing string. <br> - `-i`: replace the string at the wanted occurence (starting with 1) (string must be an int or a list of int ([to create lists refer to 1.2.1](#1-paths))). If `-i` == `0` : delete every occurence (0 by default) <br> - `-r`: set if the text has to be deleted in reverse before being replaced (*right to left*) (true by default) |
| `setTextSpeed()` | `speed` , `-t posint` | Set the speed of the next dialogues. <br><br> - `speed`: positive float, set the target speed. (`0` is immediate) <br> - `-t`: Set the transition speed in seconds (`0` (instant) by default). |
| `setTextReverse()` | / | Next dialogues are going to be displayed in reverse. (starting from the last character) |
| `setTextAudio()` | `audio` | Set the audio of the next dialogues. <br><br> - `audio`: string, set the audio that will be played at each character. |
| `setTextImmediate()` | / | Displays next dialogues immediatly (same as doing `setTextSpeed(0)`) |

#### Logic commands

|**Command**|**parameters**|**Description**|
|-----------|--------------|---------------|
| `if()` | `condition` | This command is used to set a condition. ([refer to if() in Special commands](#special-commands)) |
| `elseif()` | `condition` | This command is used to set a condition and depends on the previous conditions. ([refer to elseif() in Special commands](#special-commands)) |
| `else()` | / | This command depends on the previous conditions to set a condition. ([refer to else() in Special commands](#special-commands)) |

#### Story commands

|**Command**|**parameters**|**Description**|
|-----------|--------------|---------------|
| `choices()` | `-t string` , `-type string` | This command is used to give choices to the user. ([refer to choices() in Special commands](#special-commands)) |
| `input()` | `-t string` , `-d string` , `-ud bool` , `-type string` , `-min float` , `-max float` , `-to string` , `-s string` | This command is used to ask the user for an input. <br><br> - `-t`: set the instruction text. (empty by default) <br> - `-d`: set the default dipslay text. (empty by default) <br> - `-ud`: use the default display text as a default value or not. (false by default) <br> - `-type`: set the type of the input. (`text` by default) ([refer to 1.2.1](#1-paths)) <br> - `-min`: set the minimal value. (depends on the type) ([refer to 1.2.1](#1-paths)) <br> - `-max`: set the maximal value. (depends on the type) ([refer to 1.2.1](#1-paths)) <br> - `-to`: set a variable to give the value to. (none by default) ([to see how unfind variables are managed refer to 1.2.1](#1-paths)) <br> - `-s`: set the style of the input. ([refer to 1.2.1](#1-paths)) |

#### Character commands

|**Command**|**parameters**|**Description**|
|-----------|--------------|---------------|
| `setPosition()` | `characterName` , `-x` , `-y` | Set the character to a position. ([to see how position is managed refer to 1.2.1](#1-paths)) <br><br> - `characterName`: string, set which character will be set at this position. ([to see how unfind characters are managed refer to 1.2.1](#1-paths)) <br> - `-x`: set the **x** position of the character. <br> - `-y`: set the **y** position of the character. |
| `move()` | `characterName` , `-x` , `-y` | Move a character. ([to see how position is managed refer to 1.2.1](#1-paths)) <br><br> - `characterName`: string, set which character will be moved. ([to see how unfind characters are managed refer to 1.2.1](#1-paths)) <br> - `-x`: set the **x** movement. <br> - `-y`: set the **y** movement. |

#### Scene commands

|**Command**|**parameters**|**Description**|
|-----------|--------------|---------------|
| `setPreset()` | `presetName` | Set the scene to a predefined preset. ([to create presets refer to 1.2.1](#1-paths)) <br><br> - `presetName`: string, set what preset you want to use. If `presetName` == `""`, set the scene to an empty scene. |

#### Variables commands

|**Command**|**parameters**|**Description**|
|-----------|--------------|---------------|
| `set()` | `variableName` , `value` | Set a variable to a new value. <br><br> - `variableName`: string, the variable you want to set. ([to see how unfind variable is managed refer to 1.2.1](#1-paths)) <br> - `value`: the new value you want to set. |
| `update()` | `variableName` , `-v float` , `-o string` | Update the value of a variable. <br><br> - `variableName`: string, the variable you want to update. ([to see how unfind variable is managed refer to 1.2.1](#1-paths)) <br> - `-v`: the value of update. (1 by default) <br> - `-o`: the operation for the update. ([to see all possible operations refer to 1.2.1](#1-paths)) (`+` by default) |

#### UI commands

|**Command**|**parameters**|**Description**|
|-----------|--------------|---------------|
| `hideUI()` | / | Hide the UI until the next user input. |

#### Special commands

Special commands are commands that use lines as additional parameters. To set a line as an element of the special command just put a `+` at the start. To stop adding elements to the special commands, you can simply stop adding `+` at the start of the line.

Special commands can be used inside other special commands, in that case add another `+` at the start of the lines that are elements of this special command.

|**Command**|**parameters**|**Subcommands**|**Description**|
|-----------|--------------|---------------|---------------|
| `choices()`/`cs()` | `-t string` , `-type string` | `choice()` | Elements of this command can only be `choice()`. This command is used to give choices (listed as `choice()`) to the user. <br><br> - `-t`: used to set the text that will be displayed to make the choice. <br> - `-type`: used to set the way choices are going to be displayed. ([refer to 1.3.4](#223-types-of-choices)) |
| `if()` | `condition` | / | Elements of this command are lines that will be parsed only if the condition is true. <br><br> - `condition`: string that will be parsed to make the condition. ([refer to 1.3.4](#211-conditions)) |
| `elseif()` | `condition` | / | Used after a `if()` or a `else if()`, if there is none, this will act as a `if()` command. Elements of this command are lines that will be parsed only if the condition is true and the previous `if()`/`elseif()` are false. <br><br> - `condition`: string that will be parsed to make the condition. ([refer to 1.3.4](#211-conditions)) |
| `else()` | / | / | Used after a `if()` or a `else if()`, if there is none, this will act as a `if()` command. Elements of this command are lines that will be parsed only if the previous `if()`/`elseif()` are false. |
| `repeat()` | `n` | / | Go `n` times through all the elements of this command. <br><br> - `n`: positive int, set the number of times that elements are repeated. |
---
|**Subcommand**|**parameters**|**Subcommand of**|**Description**|
|--------------|--------------|-----------------|---------------|
| `choice()`/`c()` | `-t string` , `-to string` | `choices()` | `choice()` is a special command (that have no specific subcommands). Elements of this command are lines that will be parsed only if this option is chosen by the user. <br><br> - `-t`: used to set the text of the choice. <br> - `-to`: used to set a dialogue file path to go to if this option is chosen by the user. ([refer to 1.3.4](#1-paths)) |
---

Subcommands can only be used as elements of their respective commands.

&nbsp;

## Glossary

### 1. Paths

#### [1.2.3] *Paths*

### 2. Commands

#### [2.1.1] *Conditions*

you can use: ==,!=,<,<=,>,>=,&,| and {variable}, true, false, null, Number, "string"

#### [2.1.2] *Operations*

+,-,/,*,^,abs,round,+%,-%,%

#### [2.2.3] *Types of choices*

- list (default, displays a vertical list of choices)
- circle (displays a circle of choices)
- bubbles (displays bubbles of choices displayed horizontaly)


#### [2.2.4] *Input types*

text, name (text without special characters and not a Number), number, numberpos, int, intpos, digits (int, max and min are the number of characters (usefull for codes like 0000))

### 3. String parsing

#### [3.1.1] *Lists*

Some parameters may need some lists.
- If you need a list of numbers you just have to separate the numbers inside the string with empty spaces.
- If you need a more complex list (like a string list), you need to separate every element of the list by `\,`.

## Technical notes

### Global

- When deleting or replacing text, code will analyse if the text breaks any `<\>`. If it does it will repair it by adding additional `<\>`.

### Errors handling

All errors give a log warning.

#### Text errors

- `{invalidVariable}` : displays `{invalidVariable}`
- `<invalid` : skip the tag
- no ending `\x>` : displays everything after the `<x` as if there is a closing `\>` at the end
- `< invalid font \>` : if font can't be displayed in italic, bold, crossed, etc. it will display normally
- `<x{invalidValue} text \>` : default character values
- single `\` with no special char after it : displays `\`
- `{` without a closing `}` : displays `{`