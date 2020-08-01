using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace B19_Ex02
{
    public class UserInterface
    {
        Othelo OtheloGame = new Othelo();

        public void InitialAndStartGame()
        {
            Console.WriteLine("Welcome To Otelo game!!!" + Environment.NewLine + "Press Q anytime for exit");

            string stringSize = GetBoardSize();
            while (!OtheloGame.IsValidInput(stringSize, true))
            {
                Console.WriteLine("Invalid size! Try again");
                stringSize = GetBoardSize();
            }
            int size = int.Parse(stringSize);

            string howManyPlayers = ChooseNumOfPlayers();
            while (howManyPlayers != "1" && howManyPlayers != "2")
            {
                Console.WriteLine("Invalid numbers of player! Try again");
                howManyPlayers = ChooseNumOfPlayers();
            }
            int numOfPlayers = int.Parse(howManyPlayers);

            string playerName1 = GetPlayerName();
            string playerName2;

            if (numOfPlayers == 2)
            {
                playerName2 = GetSecondPlayerName();
            }
            else
            {
                playerName2 = null;
            }

            OtheloGame.InitializePlayers(size, numOfPlayers, playerName1, playerName2);
            OtheloGame.CreateBoard(size);

            PrintBoard(OtheloGame.FullBoardGame);
            Console.WriteLine(OtheloGame.XPlayer.Name + " is X, " + OtheloGame.OPlayer.Name + " is O");
            Console.WriteLine("Good Luck!" + Environment.NewLine + OtheloGame.XPlayer.Name + " is starting");
            PlayGame(OtheloGame.XPlayer, OtheloGame.OPlayer);
        }

        public void PlayGame(Player i_XPlayer, Player i_OPlayer)
        {
            Coin coin = new Coin();
            bool validCoin;
            Random computerChoosing = new Random();
            string inputPlayer;

            OtheloGame.CurrPlayer.CreatePossibleCoinToMove(OtheloGame.FullBoardGame);

            if (OtheloGame.CurrPlayer.PlayerType == ePlayerType.HUMAN)
            {
                inputPlayer = GetCoinFromUser();
            }
            else
            {
                int numOfPossibleCoins = OtheloGame.CurrPlayer.PossibleCoin.Count;
                coin = OtheloGame.CurrPlayer.PossibleCoin[computerChoosing.Next(0, numOfPossibleCoins)];
                inputPlayer = null;
            }

            while (!(inputPlayer == "Q" || inputPlayer == "q"))
            {

                if (!OtheloGame.CurrPlayer.CanMove(OtheloGame.FullBoardGame))
                {
                    OtheloGame.SwitchTurn(OtheloGame.CurrPlayer);
                    PlayGame(i_XPlayer, i_OPlayer);
                }

                if (OtheloGame.CurrPlayer.PlayerType == ePlayerType.HUMAN)
                {
                    if (OtheloGame.IsValidInput(inputPlayer, false))
                    {

                        coin.Row = int.Parse(inputPlayer[0].ToString()) - 1;
                        coin.Colum = int.Parse(inputPlayer[2].ToString()) - 1;

                        validCoin = OtheloGame.FullBoardGame.CheckValidCoin(coin, OtheloGame.CurrPlayer);
                        if (!validCoin)
                        {
                            Console.WriteLine("Invalid place! Try again");
                            PlayGame(i_XPlayer, i_OPlayer);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Try again");
                        PlayGame(i_XPlayer, i_OPlayer);
                    }
                }

                OtheloGame.Move(coin);

                PrintBoard(OtheloGame.FullBoardGame);
                if (OtheloGame.FullBoardGame.IsFull() || !(i_OPlayer.CanMove(OtheloGame.FullBoardGame) || i_XPlayer.CanMove(OtheloGame.FullBoardGame)))
                {
                    GameOver();
                }

                OtheloGame.SwitchTurn(OtheloGame.CurrPlayer);
                OtheloGame.CurrPlayer.CreatePossibleCoinToMove(OtheloGame.FullBoardGame);

                if (OtheloGame.CurrPlayer.CanMove(OtheloGame.FullBoardGame))
                {
                    if (OtheloGame.CurrPlayer.PlayerType == ePlayerType.HUMAN)
                    {
                        inputPlayer = GetCoinFromUser();
                    }
                    else
                    {
                        int numOfPossibleCoins = OtheloGame.CurrPlayer.PossibleCoin.Count;
                        coin = OtheloGame.CurrPlayer.PossibleCoin[computerChoosing.Next(0, numOfPossibleCoins)];
                        inputPlayer = null;
                    }
                }
                else
                {
                    Console.WriteLine(OtheloGame.CurrPlayer.Name + " can't move. Switch turn");
                }
            }

            if (inputPlayer.ToUpper() == "Q")
            {
                OtheloGame.EndGame();
            }
            else
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            Player winner;
            if (OtheloGame.OPlayer.NumOfCoins < OtheloGame.XPlayer.NumOfCoins)
            {
                winner = OtheloGame.XPlayer;
            }
            else if (OtheloGame.OPlayer.NumOfCoins > OtheloGame.XPlayer.NumOfCoins)
            {
                winner = OtheloGame.OPlayer;
            }
            else
            {
                winner = null;
                Console.WriteLine("It's a tie!");
            }

            if (winner != null)
            {
                Console.WriteLine("The winner is: " + winner.Name);
            }

            string continueGame = PrintScoreAndEndGame(OtheloGame.XPlayer, OtheloGame.OPlayer);
            if (continueGame.ToUpper() == "Q")
            {
                OtheloGame.EndGame();
            }
            else if (continueGame.ToUpper() == "S")
            {
                InitialAndStartGame();
            }
        }

        public static string GetBoardSize()
        {
            Console.WriteLine("Please choose board size: press 6 for 6x6 or 8 for 8x8");
            return Console.ReadLine();
        }

        public static string PrintScoreAndEndGame(Player i_XPlayer, Player i_OPlayer)
        {
            string o_QOrS;
            Console.WriteLine(i_XPlayer.Name + " score: " + i_XPlayer.NumOfCoins.ToString());
            Console.WriteLine(i_OPlayer.Name + " score: " + i_OPlayer.NumOfCoins.ToString());
            Console.WriteLine("To exit press Q, to play again press S");
            o_QOrS = Console.ReadLine();
            while(o_QOrS.ToUpper() != "S" && o_QOrS.ToUpper() != "Q")
            {
                Console.WriteLine("Invalid input! Try again");
                o_QOrS = Console.ReadLine();
            }

            return o_QOrS; 
        }

        public static string GetCoinFromUser()
        {
            Console.WriteLine("Please enter place you want to put your coin, as the follow format: row,colum");
            return Console.ReadLine();
        }

        public static string ChooseNumOfPlayers()
        {
            Console.WriteLine("Are you 2 players or 1?");
            return Console.ReadLine();
        }

        public static string GetPlayerName()
        {
            Console.WriteLine("What is your name?");
            return Console.ReadLine();
        }

        public static string GetSecondPlayerName()
        {
            Console.WriteLine("What is second player name?");
            return Console.ReadLine();
        }

        public static void PrintBoard(BoardGame i_FullBoardGame)
        {
            Console.Clear();
            Console.Write(" ");
            StringBuilder rowToPrint = new StringBuilder();

            for (int i = 1; i <= i_FullBoardGame.Size; i++)
            {
                rowToPrint.Append("   ");
                rowToPrint.Append(i);
            }

            Console.WriteLine(rowToPrint);
            rowToPrint.Remove(0, rowToPrint.Length);
            rowToPrint.Append("   ");

            for (int i = 0; i < i_FullBoardGame.Size * 4; i++)
            {
                rowToPrint.Append("=");
            }

            Console.WriteLine(rowToPrint);
            rowToPrint.Remove(0, rowToPrint.Length);

            for (int i = 0; i < i_FullBoardGame.Size; i++)
            {
                rowToPrint.Append(i + 1);
                rowToPrint.Append(" | ");

                for (int j = 0; j < i_FullBoardGame.Size; j++)
                {
                    if (i_FullBoardGame.OtheloBoardGame[i, j] == 0)
                    {
                        rowToPrint.Append("  | ");
                    }
                    else
                    {
                        rowToPrint.Append(i_FullBoardGame.OtheloBoardGame[i, j]);
                        rowToPrint.Append(" | ");
                    }
                }

                Console.WriteLine(rowToPrint);
                rowToPrint.Remove(0, rowToPrint.Length);
                rowToPrint.Append("   ");

                for (int k = 0; k < i_FullBoardGame.Size * 4; k++)
                {
                    rowToPrint.Append("=");
                }

                Console.WriteLine(rowToPrint);
                rowToPrint.Remove(0, rowToPrint.Length);
            }
        }
    }
}