namespace HereBeRules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The Full gameState Object</typeparam>
    /// <typeparam name="V">The Action Object</typeparam>
    public class RoundSpecification<T,V>
    {
        protected int _roundNumber;
        
        public RoundSpecification(int roundNumber)
        {
            _roundNumber = roundNumber;
        }

        public int GetRoundNumber()
        {
            return _roundNumber;
        }
    }
}