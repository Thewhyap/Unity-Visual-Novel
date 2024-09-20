Live2D: - not implemented yet, watch episode 10 part 1 to episode 11 from Stellar Studio
- Change Highlighting function: (check episode 13 part 4 33:45) + check how global highlighting function works (no more highlighted param)

//Material material = Resources.Load<Material>(MATERIAL_PATH);
//if (material != null) return new Material(material); :: the material is not working(black)
error "Objects are trying to be loaded during a domain backup. This is not allowed as it will lead to undefined behaviour!
UnityEditor.Graphing.GraphObject:OnBeforeSerialize ()" created in episode 14 part 2 (issue from renderer.material)

if audio problems: see episode 15 part 2

Parsing lines: states +, normal
search for +
search for [wait]
name or command() or ""
after name: {as} -> ""
after command( -> params -> )
endLineCommand
,



## How it works



### Dialogue files

- Notes:
Everything is case insensitive.
Empty lines are skipped.
Empty dialogues will just display the name.
Commands will activate directly and dialogues lines will wait for user input by default.
Every parameter can be wrapped in "" and can be a \{variable}.
Optionnal parameters are called like this: -x param
Non-optionnal parameters are called without using -x in front of the parameter


#### Lines parsing:

- Dialogue parsing:
[[wait]]character [{as}] pseudo [action] ["] dialogue ["] [end_line_commands] [,]
^       ^          ^             ^        ^            ^   ^                   ^
|       |          |             |        |            |   |                   End. (optional)
|       |          |             |        |            |   What to do before next command. (optionnal) [*****]
|       |          |             |        |            End of the dialogue. (optionnal)
|       |          |             |        Start of the dialogue. (optionnal)
|       |          |             Used to set an action that will modify how the text is displayed. (optionnal) [****]
|       |          Used to set a display name. (optionnal) [***]
|       The character that will be displayed on screen. (optionnal) [**]
Wait for the previous command to end before parsing this line. Can also be [w] (optionnal) [*]

[*]: After [wait], spaces will be stripped.
[**]: Work like a variable and can be a {variable} (sometimes characters have custom display names, by default it's the character name. By default it will use the previous name, if there is no previous name it will be null).
Variables in file should not have special characters and should not be a number
If a character is called but isn't an asset (it will be displayed at a text character only)
if you use null variable as the character : sets no character.
[***]: You have to put it between "" if there are many words. Can be a {variable}, or have special chars like [\"], [\\], etc...
[****]: You can't set actions without setting a character. Actions will continue to be effective until a character is setted again. Actions can affect if it's bold/italic/crossed, the color, the font, the size, the speed, the sound, and the pseudo.
[*****]: By default we wait for the user input

- Command parsing:
[wait] command(), command(value), command(-param1 value1 -paramN valueN) [end_line_commands]

#### Actions
- shouting() or shout()
- loving() or love()
- horny()
- whispering() or whisper()
- singing() or sing()
- scared()
- happy()
- excited()
- thinking() or think()
- reading() or read()
- writting() or write()
- typing() or type()
- drunk()
- talking(params) or talk(params) with params: -c string: color, -b: bold, -i: italic, -cr: crossed, -f string: font, -s int: size, -speed float: speed, -im: immediate, -a string: audio (if no params: character default values)

#### End line commands
It is possible to wrap these commands with {} for readibility.
- w(n) or w : wait for n seconds before parsing next line (n = 0 by default, directly parse the next line)
- a : append next line to this dialogue (only for dialogue lines, if used with commands: do nothing)
- wa(n) or wa : wait for n seconds before parsing next line and then append it to this dialogue.
- wuser() or wu() or wuser or wu : wait for user input after a command (default behaviour for dialogues) (can be used as a command but you must use ())

#### Special chars
- \" : ["]
- \n : [break line]
- \\ : [\]
- \{ : [{]
- \< : [<]

- {variable} : [search in the database to display the variable value]
- {object.variable} : [search in the database to display the value of the variable of an object] (ex: player.life)

- <b text \b> : [displays the text inside in bold]
- <i text \i> : [displays the text inside in italic]
- <cr> text \cr> : [displays the text inside crossed]

(if <x{NEW_VALUE} is called before the ending \x> it will just replace the previous value)
- <f{FONT_NAME} text \f> : [displays the text inside in an other FONT]
- <s{INT} text \s> : [displays the text inside in an other size]
- <c{COLOR_NAME} text \c> : [displays the text inside in an other color]
- <speed{FLOAT} text \speed> or \sp> : [displays the text inside in a different speed, if FLOAT == 0 then displays the text immediatly]
- <im text \im> [same as using \speed{0}>] (if speed and immediate are both used, immediate overrides speed)
- <a{SOUND_NAME} text \a> [displays the text inside with a different sound] (Empty string: SOUND = none)
- \> : can be used to ends last <x


#### Errors
All errors give a log warning.

- ({) without a closing (}) : [{]
- {invalidVariable} : [{invalidVariable}]
- <invalid : skip the tag

- (no ending (\x>) : displays everything after the (<) as if there is a closing (\>) at the end
- (< invalid font \>) : if font can't be displayed in italic, bold, crossed, ... it will display normally
- (<invalid values text \>) : default character values

- (single (\) with no special char after it) : [\]

#### Commands

##### Logic commands
- if(condition) (condition is a string that will be parsed, you can use: ==,!=,<,<=,>,>=,&,| and {variable}, true, false, null, Number, "string")
+ "" or command(),
+ if(condition) (if inside an if)
++ (use + like indentations)
+ if(condition)
+ elseif(condition) or else() or elseif() (else() or elsif() are the same)
(then stop using "+" char to end the if)

- repeat(n)
+ ...
+ break() (will stop the repeat immediatly)

- rollback(n)
- skip(n)

##### Dialogue related commands
- delete("stringToDelete" -i index -r reverse) (reverse is true by default, index by default is all occurences, it can be a list, ex: 0 1 5 6)
- replace("stringToReplace" "stringReplacing" -i index -r reverse)
- 

##### Character related commands

- setPosition(character)
- move(character)

##### Scene related commands

- setup(presetName)

##### Audio related commands

##### Story paths related commands
- to(path) (path is the name of the file to go to, ex: 23A)

- choices(-t "text" -type string -)
+ "previewText" -text "fullText" -to "path" -type "type" -qr "quickResponse"
+ ...
(then stop using "+" char to end listing the choices)

choices types:
- list (default, displays a vertical list of choices)
- circle (displays a circle of choices)
- bubbles (displays bubbles of choices displayed horizontaly)

- input("instruction text" -d "default text" -ud bool -type "type" -min n -max n) (instruction text is to tell you what to type, default text will be the text displayed by default, ud (useDefault) is true by default and make the display default text the default input too, min and max are the min and max characters possible(min/max number for int and number))
(types are: text, name (text without special characters and not a Number), number, numberpos, int, intpos)

##### Stats and variables related commands

- set(variable value) or set(variable) (if variable is not existant, create it, default value is null)
- update(variable -q quantity -o operation) or u() (quantity by default is 1, operation can be -,+,*,/,+%,-% and by default is +)

##### UI related commands
- hideDialogue()

### Characters


### Backgrounds




//TODOs
1. materials can be added in Resources/Materials by adding file of C:\Users\adrie\Downloads\Assets\Assets\Resources\Materials and directly in Shaders by adding files of C:\Users\adrie\Downloads\Assets\Assets\Shaders
2. implement commands for adding/removing panels in Assets/_MAIN/Scripts/Core/Commands/Database/Extensions/CMD_DatabaseExtension_GraphicPanels.cs
3. make GraphicLayer methods Coroutines and not void (maybe? see episode 14 part 4 if problems > 20mins)
4. InitGraphic in GraphicObject get immediate as param and if immediate then startingOpacity = 1f and not always 0 (same for audio.volume)
5.For commands add shortcuts (like a HOME_DIRECTORY symbol to go quicker and not having to write it each time)

//NOTES
1. All paths are located in the IO/FilePaths