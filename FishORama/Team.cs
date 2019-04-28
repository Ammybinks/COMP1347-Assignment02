using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishORama
{
    class Team
    {
        private PiranhaToken[] fish; // Stores reference to each fish in the team

        private int score; // The team's current score

        public int Score
        {
            get { return score; }
        }

        public Team(PiranhaToken[] fish)
        {
            this.fish = fish;
        }

        // Checks whether the fish at fishIndex is currently moving, either to the chicken leg or its original position
        public bool CheckSwimming(int fishIndex)
        {
            return fish[fishIndex].Swimming;
        }

        // Starts the fish at fishIndex moving towards the chicken leg
        public void StartSwimming(int fishIndex)
        {
            fish[fishIndex].StartMoving();
        }

        // Starts the fish at fishIndex moving towards its original position, incrementing score if necessary
        public void StopSwimming(int fishIndex)
        {
            // If this fish reached the chicken leg before the other team
            if(fish[fishIndex].StopMoving())
            {
                score++;
            }
        }

        // Starts every fish in the team moving off the side of the screen
        public void Win()
        {
            for(int i = 0; i < fish.Length; i++)
            {
                fish[i].Win();
            }
        }
    }
}
