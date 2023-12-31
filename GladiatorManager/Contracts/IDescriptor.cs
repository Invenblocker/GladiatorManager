﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDescriptor
    {
        string Name { get; }
        public string[] Nicknames { get; }
        public ISkill[] SkillModifiers { get; }
        public Dictionary<Stat, byte> GenerateStartingPools();
        public Dictionary<Stat, byte> GenerateStartingEdge();
    }
}
