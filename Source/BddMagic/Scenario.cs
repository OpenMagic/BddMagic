using System;
using System.Collections.Generic;
using BddMagic.Core;
using BddMagic.Core.Helpers;
using OpenMagic;

namespace BddMagic
{
    public class Scenario : IHideObjectMembers, IDisposable
    {
        private bool IsDisposed;

        public Scenario(BddFeature feature, string title)
        {
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
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                if (disposing)
                {
                    this.Execute();
                }
            }

            this.IsDisposed = true;
        }

        private void Execute()
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
