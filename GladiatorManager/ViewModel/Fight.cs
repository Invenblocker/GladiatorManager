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
        private List<Gladiator> _participants;
        public Gladiator[] Participants
        {
            get
            {
                Gladiator[] gladiators = new Gladiator[_participants.Count()];
                for(int i = 0; i < gladiators.Length; i++)
                {
                    gladiators[i] = _participants[i];
                }
                return gladiators;
            }
        }
        private Status target;
        public Fight(Status target, params Gladiator[] participants)
        {
            this.target = target;
            this._participants = new List<Gladiator>();
            this._participants.AddRange(participants);
        }
        
        public Gladiator Run(bool visible)
        {
            foreach (Gladiator participant in _participants)
            {
                participant.PoolRemaining = new Dictionary<Stat, byte>();
                foreach(Stat stat in participant.PoolMax.Keys)
                {
                    participant.PoolRemaining[stat] = participant.PoolMax[stat];
                }
                participant.Status = Status.Hale;
            }
            if(visible)
            {
                bool first = true;
                foreach(Gladiator participant in _participants)
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
            while (_participants.Count() > 0)
            {
                int index = Die.RollFromZero(_participants.Count());
                shuffled.Add(_participants[index]);
                _participants.RemoveAt(index);
            }
            _participants = shuffled;

            while (_participants.Count() > 1)
            {
                foreach (Gladiator gladiator in _participants)
                {
                    if (gladiator.Status < target)
                    {
                        string result = gladiator.PerformTurn(_participants.Where(x => x != gladiator && x.Status < target));
                        if (visible)
                        {
                            Console.WriteLine(result);
                            Thread.Sleep(500);
                        }
                    }
                }

                _participants.RemoveAll(x => x.Status >= target);
            }

            if (visible) Console.WriteLine(_participants[0].FullName + " wins!");

            return (_participants[0]);
        }
    }
}
