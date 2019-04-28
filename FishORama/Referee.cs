using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishORama
{
    class Referee
    {
        AquariumToken aquarium;

        Random rand;

        Team[] teams; // List of every team

        int numPlayers; // Total number of players in the game

        int activePlayer = -1; // The player currently swimming towards the chicken leg

        int winningScore = 5; // Score each team needs to win the game

        public Referee(AquariumToken aquarium, Random rand, Team[] teams, int numPlayers)
        {
            this.aquarium = aquarium;
            this.rand = rand;
            this.teams = teams;
            this.numPlayers = numPlayers;
        }

        public void Update()
        {
            // If chicken leg has spawned
            if (aquarium.ChickenLeg != null)
            {
                // If there is no active player
                if(activePlayer == -1)
                {
                    // Initialise swimming behaviour

                    while (true)
                    {
                        // Randomise the currently active player
                        activePlayer = rand.Next(0, numPlayers);

                        // If the randomised activePlayer is not currently returning to its original position
                        if (!teams[0].CheckSwimming(activePlayer))
                        {
                            // Break out of loop and continue program
                            break;
                        }
                    }

                    // For each team
                    for (int i = 0; i < teams.Length; i++)
                    {
                        teams[i].StartSwimming(activePlayer);
                    }
                }
            }
            // If there was an active player and the chicken leg no longer exists
            else if (activePlayer > -1)
            {
                // End swimming behaviour

                // For each team
                for(int i = 0; i < teams.Length; i++)
                {
                    // Start the currently active fish on the team returning to its original position
                    teams[i].StopSwimming(activePlayer);

                    Console.WriteLine($"Team {i + 1}: {teams[i].Score}");

                    // If this team has won
                    if(teams[i].Score >= winningScore)
                    {
                        // End the game

                        Console.WriteLine($"Team {i + 1} wins!");

                        teams[i].Win();
                        aquarium.End();
                    }
                }

                // Reset activePlayer
                activePlayer = -1;
            }
        }
    }
}
