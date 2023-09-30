using Model;
using Contracts;

namespace ViewModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fight fight = new Fight(Status.Debilitated, GladiatorGenerator.GenerateMultiple(4));
            fight.Run(true);
        }
    }
}