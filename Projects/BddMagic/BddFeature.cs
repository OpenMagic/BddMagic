namespace BddMagic
{
    public class BddFeature
    {
        public BddFeature(string feature, string story)
        {
            throw new System.NotImplementedException();
        }

        public string Feature { get; private set; }
        public string Story { get; private set; }

        public Scenario Scenario(string scenarioTitle)
        {
            return new Scenario(this, scenarioTitle);
        }
    }
}
