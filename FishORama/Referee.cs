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

        Team[] teams;

        int numPlayers;

        int activePlayer = -1;

        public Referee(AquariumToken aquarium, Random rand, Team[] teams, int numPlayers)
        {
            this.aquarium = aquarium;
            this.rand = rand;
            this.teams = teams;
            this.numPlayers = numPlayers;
        }

        public void Update()
        {
            if (aquarium.ChickenLeg != null)
            {
                if(activePlayer == -1)
                {
                    while (true)
                    {
                        activePlayer = rand.Next(0, numPlayers);

                        if (!teams[0].CheckSwimming(activePlayer))
                        {
                            break;
                        }
                    }

                    for (int i = 0; i < teams.Length; i++)
                    {
                        teams[i].StartSwimming(activePlayer);
                    }
                }
            }
            else if (activePlayer > -1)
            {
                for(int i = 0; i < teams.Length; i++)
                {
                    teams[i].StopSwimming(activePlayer);

                    Console.WriteLine($"Team {i}: {teams[i].Score}");
                }

                activePlayer = -1;
            }
        }
    }
}
