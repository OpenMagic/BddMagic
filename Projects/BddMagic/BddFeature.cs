using System.IO;
using BddMagic.Core.Helpers;

namespace BddMagic
{
    public class BddFeature
    {
        public BddFeature(string feature, string story)
        {
            Argument.MustNotBeNullOrWhiteSpace(feature, "feature");
            Argument.MustNotBeNullOrWhiteSpace(story, "story");

            this.Feature = feature;
            this.Story = story;
        }

        public string Feature { get; private set; }
        public string Story { get; private set; }

        public Scenario Scenario(string scenarioTitle)
        {
            return new Scenario(this, scenarioTitle);
        }

        public void Write(TextWriter textWriter)
        {
            Argument.MustNotBeNull(textWriter, "textWriter");

            textWriter.WriteLine(this.Feature);
            textWriter.WriteLine();
            this.Story.WriteLines(textWriter, trimLines: true);
        }
    }
}
