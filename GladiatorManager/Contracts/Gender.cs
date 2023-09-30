using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public enum Gender
    {
        Female,
        Male,
        Other
    }
    
    public static class Extensions
    {
        public static string SubjectPronoun(this Gender gender)
        {
            switch(gender)
            {
                case Gender.Female:
                    return "She";
                case Gender.Male:
                    return "He";
                case Gender.Other:
                default:
                    return "They";
            }
        }
        public static string ObjectPronoun(this Gender gender)
        {
            switch(gender)
            {
                case Gender.Female:
                    return "Her";
                case Gender.Male:
                    return "Him";
                case Gender.Other:
                default:
                    return "Them";
            }
        }
        public static string OwnerPronoun(this Gender gender)
        {
            switch(gender)
            {
                case Gender.Female:
                    return "Her";
                case Gender.Male:
                    return "His";
                case Gender.Other:
                default:
                    return "Their";
            }
        }
    }
}

