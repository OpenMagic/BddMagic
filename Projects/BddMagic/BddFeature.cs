using System.Diagnostics;
using System.IO;
using Humanizer;
using OpenMagic;
using OpenMagic.Extensions;

namespace BddMagic
{
    public class BddFeature
    {
        public BddFeature(string story)
            : this("", story)
        { }

        public BddFeature(string feature, string story)
        {
            Argument.MustNotBeNullOrWhiteSpace(story, "story");

            if (string.IsNullOrWhiteSpace(feature))
            {
                feature = this.GetFeatureNameFromClassName();
            }

            this.Feature = feature;
            this.Story = story;
        }

        private string GetFeatureNameFromClassName()
        {
            var className = this.GetType().Name;
            var featureName = className.Humanize(LetterCasing.Title);

            featureName = featureName.TextBeforeLast(" Feature", featureName);

            return featureName;
        }

        public string Feature { get; private set; }
        public string Story { get; private set; }

        public Scenario Scenario()
        {
            var callingMethod = new StackTrace().GetFrame(1).GetMethod().Name;
            var scenarioTitle = callingMethod.Humanize();

            return this.Scenario(scenarioTitle);
        }

        public Scenario Scenario(string scenarioTitle)
        {
            return new Scenario(this, scenarioTitle);
        }

        public void Write(TextWriter textWriter)
        {
            textWriter.WriteLine(this.Feature);
            textWriter.WriteLine();
            this.Story.WriteLines(textWriter, trimLines: true);
        }
    }
}
