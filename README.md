# Visual Novel Engine

## Project Overview

This Visual Novel Engine is built with Unity and is still in development.

It will help you create rich stories with high level of customization while still being very easy to use. The code being public, and the engine being created with unity make it easily and fully customizable.

### Key Features

- 
- 

## Project Progress

[█░░░░░░░░░░░░░░░░░░░]

### Implemented Features

- Dialogue parsing system.

## Getting Started

### Builds

You can download the **engine builds** to create games very easily without having to open unity at all :

- The **Full version** will give you access to everything but will be more space consumming.
- The **Lite version** have all the basic functionalities you need to create your game (check out the [documentation](./Documentation.md) for more informations).


If you want to customize further your game, you can fork this project and open it in unity. You can then use the **Game builds** inside the unity editor to direclty build your game :

- The **Full version** will build everything in the project.
- The **Custom version** has to be customized to build a game with only the things that you need inside.


### More

For more informations on how to use this engine, refer to these files:
- [Tutorial.md](./Tutorial.md)
- [Documentation.md](./Documentation.md)

## Contact

If you have any questions, would like to inquire about licensing, (or just want to chat, who knows?) feel free to reach out.

- **Email**: [apexbaguette.studio@gmail.com](mailto:apexbaguette.studio@gmail.com)
- **More about me**: There's nothing more to say about me.

## Donations and Support

If you'd like to support the development of this project, you can do so via:

- **Buy Me a Coffee**: [buymeacoffee.com/apexbaguette](https://buymeacoffee.com/apexbaguette)

Thank you for your support!


## License and Usage

This engine is free to use for **both non-commercial and commercial purposes** under the following conditions:

### Usage Rights:

- **The use of the engine for commercial purposes is restricted to solo developers**. If you are part of a team or company, or if multiple developers are contributing, please contact me for permission before use.
- **Attribution is required**. Please do not delete the prebuilt intro logo.

**Not required but appreciated:** Please contact me, so I can know about your project ! (Just because I'm curious)

<br>

## Dev notes:

All paths are located in the IO/FilePaths

Live2D:
- not implemented yet, watch episode 10 part 1 to episode 11 from Stellar Studio
- Change Highlighting function: (check episode 13 part 4 33:45) + check how global highlighting function works (no more highlighted param)

Error:
- Material material = Resources.Load<Material>(MATERIAL_PATH);
if (material != null) return new Material(material);
- the material is not working(black)

if audio problems: see episode 15 part 2

### // TODOs
1. materials can be added in Resources/Materials by adding file of C:\Users\adrie\Downloads\Assets\Assets\Resources\Materials and directly in Shaders by adding files of C:\Users\adrie\Downloads\Assets\Assets\Shaders
2. implement commands for adding/removing panels in Assets/_MAIN/Scripts/Core/Commands/Database/Extensions/CMD_DatabaseExtension_GraphicPanels.cs
3. make GraphicLayer methods Coroutines and not void (maybe? see episode 14 part 4 if problems > 20mins)
4. InitGraphic in GraphicObject get immediate as param and if immediate then startingOpacity = 1f and not always 0 (same for audio.volume)
5.For commands add shortcuts (like a HOME_DIRECTORY symbol to go quicker and not having to write it each time)

### Ideas

Langage system, dislexic font mode, let the user setting custom modes and options

when launching the game put "created using (team) VisualNovelEngine" intro and then letting the user put if he wants his own intro -> create a conf file for the user where he can change params as he want.

**configuration.txt :**

- intro: false (or just search for conf image or video and if not is false, same for introAudio)
- intro.skippable: false
- langage.default:
- settings.volume: advanced (basic)
- settings.audio: advanced (basic)
