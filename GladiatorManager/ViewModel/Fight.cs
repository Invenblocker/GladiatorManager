using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Contracts;

namespace ViewModel
{
    public class Fight
    {
        private List<Gladiator> participants;
        private Status target;
        public Fight(Status target, params Gladiator[] participants)
        {
            this.target = target;
            this.participants = new List<Gladiator>();
            this.participants.AddRange(participants);
        }
        
        public Gladiator Run(bool visible)
        {
            foreach (Gladiator participant in participants)
            {
                participant.PoolRemaining = new Dictionary<Stat, byte>();
                foreach(Stat stat in participant.PoolMax.Keys)
                {
                    participant.PoolRemaining[stat] = participant.PoolMax[stat];
                }
            }
            if(visible)
            {
                bool first = true;
                foreach(Gladiator participant in participants)
                {
                    if (!first)
                    {
                        Thread.Sleep(1000);
                        Console.Write(" vs. ");
                    }
                    Console.Write(participant.FullName);
                    first = false;
                }
                Thread.Sleep(1000);
                Console.WriteLine();
                switch(target)
                {
                    case Status.Impaired:
                        Console.WriteLine("Fight till impairment");
                        break;
                    case Status.Debilitated:
                        Console.WriteLine("Fight till debilitation");
                        break;
                    case Status.Dead:
                        Console.WriteLine("Fight till death.");
                        break;
                }
                Thread.Sleep(2500);
            }

            List<Gladiator> shuffled = new List<Gladiator>();
            while (participants.Count() > 0)
            {
                int index = Die.RollFromZero(participants.Count());
                shuffled.Add(participants[index]);
                participants.RemoveAt(index);
            }
            participants = shuffled;

            while (participants.Count() > 1)
            {
                foreach (Gladiator gladiator in participants)
                {
                    if (gladiator.Status < target)
                    {
                        string result = gladiator.PerformTurn(participants.Where(x => x != gladiator && x.Status < target));
                        if (visible)
                        {
                            Console.WriteLine(result);
                            Thread.Sleep(500);
                        }
                    }
                }

                participants.RemoveAll(x => x.Status >= target);
            }

            if (visible) Console.WriteLine(participants[0].FullName + " wins!");

            return (participants[0]);
        }
    }
}
