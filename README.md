# Snake Game

A classic 2D Snake game developed with Unity, featuring smooth gameplay, score tracking, and various food effects.

## Description

This project implements the timeless Snake game in a 2D environment using the Unity game engine. Players control a snake that grows by consuming food items, while avoiding collisions with walls and the snake's own body. The game includes different types of food with special effects, score management, and responsive controls.

## Features

- **Classic Snake Gameplay**: Navigate the snake to eat food and grow longer
- **Score System**: Track and persist high scores
- **Food Variety**: Different food types with unique effects on the snake
- **Smooth Controls**: Responsive movement with arrow keys or WASD
- **Cross-Platform**: Built for web (WebGL) and mobile (Android)

## How to Play

1. Use arrow keys or WASD to control the snake's direction
2. Eat the food items to grow and increase your score
3. Avoid hitting the walls or the snake's own body
4. Try to achieve the highest score possible!

## Development

### Prerequisites

- Unity 2021.3 or later
- Git for version control

### Setup

1. Clone this repository:
   ```bash
   git clone https://github.com/erickclarivet/snakeGame.git
   ```

2. Open the project in Unity Hub or Unity Editor

3. Open the main scene from `Assets/Scenes/`

4. Press Play to test the game in the editor

## Build Instructions

### WebGL Build (for GitHub Pages)

1. In Unity Editor: File > Build Settings
2. Select "WebGL" as the platform
3. Click "Build" and choose the `snake-webgl` folder
4. The build will be in the `snake-webgl` directory

### Android Build

1. In Unity Editor: File > Build Settings
2. Select "Android" as the platform
3. Configure Android settings (keystore, etc.)
4. Click "Build" to create an APK file

## CI/CD Pipeline

This project uses GitHub Actions for automated building and deployment:

- **Continuous Integration**: Automatically builds the project on every push to main branch
- **WebGL Build**: Creates a WebGL version of the game
- **Deployment**: Deploys the WebGL build to GitHub Pages at [https://erickclarivet.github.io/snakeGame/](https://erickclarivet.github.io/snakeGame/)
- **Android Build**: Also builds an Android APK (available as artifact in Actions)

The CI/CD workflow ensures that the latest version is always available online, and provides APK downloads for mobile testing.

## Project Structure

- `Assets/Scripts/`: C# scripts for game logic
- `Assets/Scenes/`: Unity scenes
- `Assets/Prefabs/`: Game object prefabs
- `Assets/Resources/`: Game assets
- `snake-webgl/`: WebGL build output

## Contributing

Feel free to fork this project and submit pull requests with improvements or new features!

## License

This project is open source. Please check the license file for details.
