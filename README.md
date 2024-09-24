# Visual Novel Engine

## Project Overview

This Visual Novel Engine is built with Unity and is still in development.

### Key Features

- TODO

## Project Progress

[█░░░░░░░░░░░░░░░░░░░]

### Implemented Features

- Dialogue parsing system.

## Getting Started


To check out how to use this engine, refer to these files:
- Tutorial.md
- Documentation.md

## Contact

If you have any questions, would like to inquire about licensing, (or just want to chat, who knows?) feel free to reach out.

- **Email**: [your.email@example.com](mailto:your.email@example.com)
- **More about me**: There's nothing more to say about me.

## Donations and Support

If you'd like to support the development of this project, you can do so via:

- **Buy Me a Coffee**: [buymeacoffee.com/YourUsername](https://www.buymeacoffee.com/YourUsername)
- **Patreon**: [patreon.com/YourUsername](https://www.patreon.com/YourUsername)

Thank you for your support!


## License and Usage

This engine is free to use for **both non-commercial and commercial purposes** under the following conditions:

### Usage Rights:

- **Attribution is required**: Please give credit to the engine in a way that best fits your project (e.g., in the credits or documentation).
- **The use of the engine for commercial purposes is restricted to solo developers**. If you are part of a team or company, or if multiple developers are contributing, please contact me for permission before use.

**Not required but appreciated:** Please contact me, so I can know about your project ! (Just because I'm curious)

<br><br>

## Notes:

All paths are located in the IO/FilePaths

Live2D:
- not implemented yet, watch episode 10 part 1 to episode 11 from Stellar Studio
- Change Highlighting function: (check episode 13 part 4 33:45) + check how global highlighting function works (no more highlighted param)

Error:
- Material material = Resources.Load<Material>(MATERIAL_PATH);
if (material != null) return new Material(material);
- the material is not working(black)

if audio problems: see episode 15 part 2

## // TODOs
1. materials can be added in Resources/Materials by adding file of C:\Users\adrie\Downloads\Assets\Assets\Resources\Materials and directly in Shaders by adding files of C:\Users\adrie\Downloads\Assets\Assets\Shaders
2. implement commands for adding/removing panels in Assets/_MAIN/Scripts/Core/Commands/Database/Extensions/CMD_DatabaseExtension_GraphicPanels.cs
3. make GraphicLayer methods Coroutines and not void (maybe? see episode 14 part 4 if problems > 20mins)
4. InitGraphic in GraphicObject get immediate as param and if immediate then startingOpacity = 1f and not always 0 (same for audio.volume)
5.For commands add shortcuts (like a HOME_DIRECTORY symbol to go quicker and not having to write it each time)
