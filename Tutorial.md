## How it works

### 1) Making a character speak:

```
characterName "text"
```

**Example:**

```
joe "My name is Joe"
```

**Notes:**
- `characterName` must be a single word (not a number, and should not contain any quotes or parentheses).
- To set no character, use `null` as the `characterName`.
- `characterName` is case insensitive.
- If `text` is empty, this will display the character name with no dialogue.

### 2) Making a character speak (without mentioning the character):

This will make the previously selected character speak (if none, the character is `null` by default).

```
"text"
```

### 3) Making a character speak with another name:

```
characterName {as} customName "text"
```

**Example:**

```
joe {as} "little Joe" "You can call me Little Joe!"
```

**Notes:**
- `customName` must be surrounded with quotes if it has more than one word.
- `{as}` is case insensitive.
- If `{as}` is used without setting a `customName`, it will result in a parsing error.

### 4) Setting multiple dialogues on a single line:

You can separate lines by using `,`.

```
characterName {as} customName "text", "text2"
```

### 5) Calling a command:

```
command()
```

**Example:**

```
move()
```

**Notes:**
- `command()` is case insensitive.
- Don't forget the parentheses or it will be parsed as a `characterName`.
- Commands can be custom, you can find a list of existing commands in the documentation.

### 6) Calling a command with dialogue:

```
characterName {as} customName "text" command()
```

**Notes:**
- `command()` is always called at the end of a line.

### 7) Calling a command with parameters:

Commands can have parameters, some of which may be optional.

```
command(value -x optionalValue ...)
```

**Example:**

```
move(joe -speed 1 -x 12 -y 1)
```

**Notes:**
- Optional parameters are called with `-(x)` before setting the value. (case insensitive)
- Parameter values can be multiple words or numbers if surrounded by quotes.
- If a parameter value is a string containing parentheses, surround it with quotes.

### 8) Calling a character action:

An action can modify the way the text, character name, etc., will be displayed.

```
characterName {as} customName action() "text"
```

**Example:**

```
Joe shout() "I am not little!"
```

**Notes:**
- `action()` works exactly like `command()` and can also have parameters.
- `action()` can only be called if a `characterName` is specified.
- `action()` is always called between the `customName` and the `text` but can be called without either.
- Actions persist through the lines until a `characterName` is called.

### 9) Using variables:

```
{variableName}
```

**Example:** 

`({player} = Joe)`

```
{player} {as} "little {player}"
```
**Result =>** `joe {as} "little Joe"`

**Notes:**
- Variables can be used in text, as `characterNames`, `customNames`, and parameters.
- Variables are case insensitive.
- Use `{variable.variable}` to call variables linked to other variables.

### 10) Waiting for command completion:

Some commands take time to complete (e.g., moving a character). You can force the game to wait for this action to end before parsing the next line.

```
[wait] command()
```

```
[w] command()
```

**Notes:**
- `[wait]` / `[w]` is always called at the start of a line (after `+`).
- `[wait]` / `[w]` is case insensitive.
- It can be called without any `command()`, but will have no effect.

### 11) Special characters:

Inside quotes, you can use special characters.

**Notes:**  
You can find the list of all special characters in the documentation.

### 12) Adding elements to special commands:

Some commands (e.g., `choices()`) await a list of elements. These elements are marked by adding a `+` at the start of the line. Stop using `+` to stop adding elements.

```
specialCommand()
+ ...
+ ...
...
```

**Example:**

```
choices()
+ choice("1" -t "I think the answer is 1" -to a1)
+ choice("2" -t "I think the answer is 2" -to a2)
```

**Notes:**  
Elements must sometimes respect specific conditions depending on the special command.

### 13) Adding special commands inside other special commands:

Some special commands allow you to set other special commands inside them. Add an additional `+` at the start of the line to set the elements of the new special command.


```
specialCommand()
+ specialCommand()
++ ...
...
```

**Example:**

```
if({player.health} < {ennemy.damage})
+ if({inventory} contains "resurrection stone")
++ "I won't get another chance!" deleteIn({inventory} "resurrection stone")
+ else()
++ gameover()
```

### Examples:

```
[W] joe {as} {schoolname} shout() "{grade}!" a(), "How is that possible!"
```
```
joe whisper() "Goodbye" hidecharacter(joe)
```