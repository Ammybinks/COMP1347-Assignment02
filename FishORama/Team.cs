using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishORama
{
    class Team
    {
        private PiranhaToken[] fish;

        private int score;

        public int Score
        {
            get { return score; }
        }

        public Team(PiranhaToken[] fish)
        {
            this.fish = fish;
        }

        public bool CheckSwimming(int fishIndex)
        {
            return fish[fishIndex].Swimming;
        }

        public void StartSwimming(int fishIndex)
        {
            fish[fishIndex].StartMoving();
        }

        public void StopSwimming(int fishIndex)
        {
            if(fish[fishIndex].StopMoving())
            {
                score++;
            }
        }
    }
}
