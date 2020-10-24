# Monopoly Kata in C#

This is my implementation of the Monopoly Kata, described here:
https://schuchert.github.io/wikispaces/pages/Katas.MonopolyTheGame

This is currently a simulation.  There is no user input.  The rules are not perfectly implemented.  It's a practice project.

# Building Monopoly

With the code from this repo, building and running should be pretty simple.

```
$~/repos/MonopolyKata> cd MonopolyKata
$~/repos/MonopolyKata/MonopolyKata> dotnet build
```

# Testing Monopoly

There is a suite of unit tests for the project, written in Xunit.

```
$~/repos/MonopolyKata> cd MonopolyKataTests
$~/repos/MonopolyKata/MonopolyKataTests> dotnet build
$~/repos/MonopolyKata/MonopolyKataTests> dotnet test
```

# Running Monopoly

Once built, running Monopoly should be as simple as:

```
$~/repos/MonopolyKata/MonopolyKata> dotnet run
```

There is a default logger that will display the state of the game for each player during each turn.  The logger can be adjusted in the constructor in Program.cs.  
