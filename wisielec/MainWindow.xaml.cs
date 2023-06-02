using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace wisielec
{
    public partial class MainWindow : Window
    {
        private string[] words = { "KOT", "PIES", "DOM", "KOMPUTER", "PROGRAMOWANIE", "KATAPULTA" };
        private string selectedWord;
        private List<char> guessedLetters;
        private int attemptsLeft;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            selectedWord = words[new Random().Next(words.Length)];
            guessedLetters = new List<char>();
            attemptsLeft = 7;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            string wordDisplay = "";
            foreach (char letter in selectedWord)
            {
                if (guessedLetters.Contains(letter))
                    wordDisplay += letter;
                else
                    wordDisplay += "_";
            }
            WordLabel.Text = wordDisplay;

            AttemptsLabel.Text = $"Attempts left: {attemptsLeft}";

            if (attemptsLeft == 0)
                ShowLoseMessage();
            else if (!wordDisplay.Contains("_"))
                ShowWinMessage();
        }

        private void ShowWinMessage()
        {
            MessageBox.Show("Congratulations! You won!", "Game Over");
            NewGame();
        }

        private void ShowLoseMessage()
        {
            MessageBox.Show("Game over! The word was: " + selectedWord, "Game Over");
            NewGame();
        }

        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuessTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a letter.", "Invalid Input");
                return;
            }

            char guessedLetter = GuessTextBox.Text.ToUpper()[0];

            if (!Char.IsLetter(guessedLetter))
            {
                MessageBox.Show("Invalid input. Please enter a single letter.", "Invalid Input");
                return;
            }

            if (guessedLetters.Contains(guessedLetter))
            {
                MessageBox.Show("You already guessed that letter.", "Already Guessed");
                return;
            }

            guessedLetters.Add(guessedLetter);

            if (!selectedWord.Contains(guessedLetter))
                attemptsLeft--;

            UpdateDisplay();

            GuessTextBox.Text = "";
            }
            
        
    }
}
