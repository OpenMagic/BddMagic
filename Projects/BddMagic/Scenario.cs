using System;
using BddMagic.Core.Helpers;

namespace BddMagic
{
    public class Scenario : IHideObjectMembers
    {
        public Scenario(BddFeature feature, string scenarioTitle)
        {
            throw new System.NotImplementedException();
        }

        public Func<dynamic, dynamic> this[string step]
        {
            set { throw new System.NotImplementedException(); }
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
