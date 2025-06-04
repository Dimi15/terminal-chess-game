# Terminal Chess Game

A terminal-based chess game developed in C#. This project was an 11th grade, high school group project, focused on implementing a Player vs. Computer chess experience using the **Minimax algorithm** for the AI. We focused on core game logic within the constraints of limited C# features, as we weren't permitted to use Object-Oriented Programming (OOP) concepts.

---

## Features

* **Player vs. Computer AI:** Challenge a computer opponent powered by the Minimax algorithm.
* **Standard chess rules:** All the familiar moves, captures, and special rules (castling, en passant, pawn promotion) are implemented.
* **Clear terminal interface:** Easy-to-read board and game state, even in a console window.

---

## How to Run

The easiest way to play is by using our pre-built executable. You won't need Visual Studio or even to install the .NET SDK!

### 1. Download the Game

* Go to the [**Releases**](https://github.com/YOUR_USERNAME/YOUR_REPOSITORY/releases) section of this repository.
* Download the `.zip` file suitable for your operating system (e.g., `chess-game-win-x64.zip` for Windows, `chess-game-linux-x64.tar.gz` for Linux, `chess-game-osx-x64.tar.gz` for macOS).

### 2. Extract and Play!

* **Windows:**
    * Unzip the downloaded file.
    * Navigate into the extracted folder.
    * **Double-click** `YourGameAppName.exe` to start the game.
* **Linux/macOS:**
    * Unpack the downloaded archive (e.g., `tar -xzf chess-game-linux-x64.tar.gz`).
    * Open your terminal, navigate to the extracted folder.
    * First, give execute permissions:
        ```bash
        chmod +x YourGameAppName
        ```
    * Then, run the game:
        ```bash
        ./YourGameAppName
        ```

---

## Troubleshooting: Missing Chess Pieces?

If you run the game and don't see the chess pieces (they might appear as squares, question marks, or garbled text), it's likely that your terminal's font doesn't support the **Unicode characters** used for the pieces.

To fix this, you'll need to change your terminal's font to one that supports a wide range of Unicode symbols. We highly recommend using **DejaVu Sans Mono**.

Here's how you typically change the font in common terminals:

* **Windows Terminal (Recommended for Windows):** Open Settings (`Ctrl + ,`), navigate to "Profiles" -> "Defaults," and under "Text" -> "Font face," select a suitable font.
* **Windows Command Prompt/PowerShell:** Right-click the title bar, select "Properties," go to the "Font" tab, and choose a Unicode-supporting font like "DejaVu Sans Mono" or "Consolas" (if it works for you).
* **Linux/macOS Terminals (e.g., GNOME Terminal, iTerm2, Kitty):** Look for "Preferences" or "Settings" in your terminal application's menu, find the "Profiles" or "Appearance" section, and change the font there.

---

## Documentation

For a comprehensive guide on how the program is structured, please download our project documentation (written in italian):

* [**Download Game Documentation (PDF)**](https://github.com/YOUR_USERNAME/YOUR_REPOSITORY/raw/main/docs/ChessGameDocumentation.pdf)


---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
