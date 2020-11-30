using System;

namespace HereBeRules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Z">GameState object to be validated (what is sent tipically)</typeparam>
    /// <typeparam name="V">Enum representing game Action type</typeparam>
    public interface IRule<Z,V> where V : Enum
    {
        bool ValidateAction(V action, Z state);

        string GetName();
        
        bool TestAction(V action);
    }
    

    public class RuleViolationException : Exception
    {
        private string _reason;
        public RuleViolationException(string reason = "")
        {
            _reason = reason;
        }

        public override string ToString()
        {
            return "[Rule violation] " + _reason;
        }
    }
}