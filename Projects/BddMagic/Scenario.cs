using System;
using System.Collections.Generic;
using BddMagic.Core;
using BddMagic.Core.Helpers;
using OpenMagic;

namespace BddMagic
{
    public class Scenario : IHideObjectMembers
    {
        public Scenario(BddFeature feature, string title)
        {
            Argument.MustNotBeNull(feature, "feature");
            Argument.MustNotBeNullOrWhiteSpace(title, "title");

            this.Feature = feature;
            this.Title = title;
            this.Steps = new List<Step>();
        }

        public BddFeature Feature { get; private set; }
        public string Title { get; private set; }
        public List<Step> Steps { get; private set; }

        public Action<dynamic> this[string step]
        {
            set
            {
                Argument.MustNotBeNullOrWhiteSpace(step, "step");

                this.Steps.Add(new Step(step, value));
            }
        }

        public void Execute()
        {
            var textWriter = Console.Out;

            this.Feature.Write(textWriter);

            textWriter.WriteLine();
            textWriter.WriteLine("Scenario: {0}", this.Title);
            textWriter.WriteLine();

            var previousStepWasSuccessful = true;

            foreach (var step in this.Steps)
            {
                previousStepWasSuccessful = step.Execute(textWriter, previousStepWasSuccessful);
            }

            if (!previousStepWasSuccessful)
            {
                throw new Exception("Failed specification.");
            }
        }
    }
}
