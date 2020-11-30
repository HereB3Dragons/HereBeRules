using System;

namespace HereBeRules
{
    [Serializable]
    public abstract class Rule<Z, V, T> : IRule<Z, V> where V : Enum 
    {
        private readonly Func<T> _stateFunc;
        private string _name;

        protected Rule(string name, Func<T> stateGetter) 
        {
            _stateFunc = stateGetter;
            _name = name;
        }
        public bool ValidateAction(V action, Z state)
        {
            if (!TestAction(action))
                throw new ArgumentException("Wrong action for this rule");
            return Validate(ref state);
        }

        protected T GetState()
        {
            return _stateFunc.Invoke();
        }

        public string GetName()
        {
            return _name;
        }
        public abstract bool TestAction(V action);
        public abstract bool Validate(ref Z state);

        public override string ToString()
        {
            return "[" + _name + " RULE]\n";
        }
    }
}