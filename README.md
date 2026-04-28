# CM1113 2024/25 Coursework

# Talon Protocol

Talon Protocol is a text based adventure game designed for a solo adventure.

The player has to explore a secret talon style base, solve puzzles, complete challenges, and attempt to override the command centre.

## How to Run

The project should be opened in visual studio code.

Then navigate to the project folder: 

cd src/TalonProtocol

Then: dotnet run

## Commands

help - shows available commands
look - displays the current location
status - shows player health and inventory
inventory - shows collected items
go forward - moves forward
go back - moves back
go left - moves left
go right - moves right
solve - attempts a puzzle challenge
attack - attempts a combat challenge
heal - uses healing supplies
quit - exits the game

## Win Condition

The player wins by:

Collecting the Access Key
Collecting the System Override
Completing the final puzzle in the Command Centre

## Lose Condition

The player Loses if their health reaches 0.

## Classes

Player - stores health, inventory, and current location
Location - stores room descriptions, exits, and challenges
Game - controls game setup and win/loss conditions
GameController - handles user input and commands
Challenge - abstract base class for all challenges
PuzzleChallenge - handles puzzle-based challenges
CombatChallenge - handles enemy encounters
HealingChallenge - handles healing rooms