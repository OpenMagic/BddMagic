using System;
using System.Diagnostics;
using System.IO;
using BddMagic.Core;
using Humanizer;
using OpenMagic;
using OpenMagic.Extensions;

namespace BddMagic
{
    public class BddFeature
    {
        private Lazy<Scenario> ScenarioFactory;

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
            this.ScenarioFactory = new Lazy<Scenario>(NewScenario);
        }

        public Scenario Scenario { get { return this.ScenarioFactory.Value; } }

        public string Feature { get; private set; }
        public string Story { get; private set; }

        private string GetFeatureNameFromClassName()
        {
            var className = this.GetType().Name;
            var featureName = className.Humanize(LetterCasing.Title);

            featureName = featureName.TextBeforeLast(" Feature", featureName);

            return featureName;
        }

        private Scenario NewScenario()
        {
            var callingMethod = new StackTrace().GetFrame(5).GetMethod().Name;
            var scenarioTitle = callingMethod.Humanize();

            return new Scenario(this, scenarioTitle);
        }

        public void Given(string text)
        {
            this.Given(text, null);
        }

        public void Given(string text, Action<dynamic> action)
        {
            this.AddStep(text, action);
        }

        public void And(string text)
        {
            this.And(text, null);
        }

        public void And(string text, Action<dynamic> action)
        {
            this.AddStep(text, action);
        }

        public void When(string text)
        {
            this.When(text, null);
        }

        public void When(string text, Action<dynamic> action)
        {
            this.AddStep(text, action);
        }

        public void Then(string text)
        {
            this.Then(text, null);
        }

        public void Then(string text, Action<dynamic> action)
        {
            this.AddStep(text, action);
        }

        private void AddStep(string text, Action<dynamic> action)
        {
            var step = string.Format("{0} {1}", new StackTrace().GetFrame(1).GetMethod().Name, text);

            this.Scenario.Steps.Add(new Step(step, action));
        }

        public void Write(TextWriter textWriter)
        {
            textWriter.WriteLine(this.Feature);
            textWriter.WriteLine();
            this.Story.WriteLines(textWriter, trimLines: true);
        }
    }
}
