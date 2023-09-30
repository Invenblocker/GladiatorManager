using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Ability : IAbility
    {
        private Gladiator _user;
        private bool _powerEffort, _precisionEffort, _otherEffort;

        public abstract string Name { get; }
        public abstract string Description { get; }
        public Gladiator User { get { return _user; } }
        IGladiator IAbility.User { get { return User; } }
        public abstract Stat Stat { get; }
        public abstract byte Cost { get; }
        public bool PowerEffort { get { return _powerEffort; } }
        public bool PrecisionEffort { get { return _precisionEffort; } }
        public bool OtherEffort { get { return _otherEffort; } }

        public Ability(Gladiator user, bool powerEffort, bool precisionEffort, bool otherEffort)
        {
            _user = user;
            _powerEffort = powerEffort;
            _precisionEffort = precisionEffort;
            _otherEffort = otherEffort;
        }

        public abstract string Use(Gladiator target, byte powerEffort, byte precisionEffort, byte otherEffort);
    }
}
