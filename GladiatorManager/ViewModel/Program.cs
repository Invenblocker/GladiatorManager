using Model;
using Contracts;
using System.Runtime.InteropServices;

namespace ViewModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            Gladiator champion = GladiatorGenerator.Generate();
            int winStreak = 0;
            int money = 100;
            List<Gladiator> losers = new List<Gladiator>();

            Fight SetupFight()
            {
                Gladiator[] participants = new Gladiator[Die.Roll(3) + 1];
                participants[0] = champion;
                for (int i = 1; i < participants.Length; i++)
                {
                    if (Die.RollFromZero(4) == 0 && losers.Count() > 0)
                    {
                        int index = Die.RollFromZero(losers.Count());
                        participants[i] = losers[index];
                        losers.RemoveAt(index);
                    }
                    else
                    {
                        participants[i] = GladiatorGenerator.Generate();
                    }
                }
                return new Fight(Status.Debilitated, participants);
            }

            Fight fight = SetupFight();

            do
            {
                Console.Clear();
                
                
                Console.WriteLine("The participants in the upcoming fight are:");
                for(int i = 0; i < fight.Participants.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + fight.Participants[i].FullName + " who is a " + fight.Participants[i].Description);
                }
                if(winStreak > 0)
                {
                    Console.WriteLine(fight.Participants[0].FullName + " is the reigning champion with a win streak of: " + winStreak + ".");
                }
                Console.WriteLine("\nYou have " + money + " gold.");
                //Console.WriteLine("To 10 gold for insider info on a gladiator, type \"INFO X\", where x is the number of the gladiator.");
                Console.WriteLine("To bet money on a gladiator, type \"BET X Y\", where X is the number of the gladiator, and Y is the ante in gold.");
                Console.WriteLine("To skip this fight, type \"SKIP\". To quit the game, type \"QUIT\"");

                string[] input = Console.ReadLine().Trim().ToLower().Split(' ');
                Gladiator[] participants = fight.Participants;
                if(input.Length > 0)
                {
                    switch(input[0])
                    {
                        case "skip":
                            champion = fight.Run(false);
                            foreach(Gladiator gladiator in participants)
                            {
                                if(gladiator.Status != Status.Dead && gladiator != champion)
                                {
                                    losers.Add(gladiator);
                                }
                            }
                            fight = SetupFight();
                            break;
                        case "bet":
                            int playerBet, ante, gladiatorCount = fight.Participants.Length;
                            if (int.TryParse(input[1], out playerBet) && int.TryParse(input[2],out ante))
                            {
                                if(playerBet > 0 && playerBet <= fight.Participants.Length)
                                {
                                    Gladiator playerChoice = fight.Participants[playerBet - 1];
                                    if (ante > 0 && ante <= money)
                                    {
                                        Console.WriteLine("You bet " + ante + " on " + playerChoice.FullName);
                                        Thread.Sleep(500);
                                        champion = fight.Run(true);

                                        if (champion == playerChoice)
                                        {
                                            Console.WriteLine("Your selected champion won, earning you " + (ante * (gladiatorCount - 1)) + " gold!");
                                            money += ante * (gladiatorCount - 1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Your selected champion lost. You lose " + ante + " gold.");
                                            money -= ante;
                                        }

                                        foreach (Gladiator gladiator in participants)
                                        {
                                            if (gladiator.Status != Status.Dead && gladiator != champion)
                                            {
                                                losers.Add(gladiator);
                                            }
                                        }
                                        fight =SetupFight();

                                        Console.WriteLine("Press any key to continute");
                                        Console.ReadKey(true);
                                    }
                                }
                            }
                            break;
                        case "quit":
                            running = false;
                            break;
                    }
                }
            }
            while (running);
        }
    }
}