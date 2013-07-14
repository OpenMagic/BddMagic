using System;
using System.Collections.Generic;
using System.IO;
using OpenMagic;

namespace BddMagic.Core
{
    public class Step
    {
        public Step(string step, Action<dynamic> action)
        {
            Argument.MustNotBeNullOrWhiteSpace(step, "step");

            this.Text = step;
            this.Action = action;
        }

        public string Text { get; private set; }
        public Action<dynamic> Action { get; private set; }

        public bool Execute(TextWriter textWriter, bool previousStepWasSuccessful)
        {
            Argument.MustNotBeNull(textWriter, "textWriter");

            if (!previousStepWasSuccessful)
            {
                Write(textWriter, "Pending");
                return false;
            }

            if (Action == null)
            {
                Write(textWriter, "Undefined action");
                return false;
            }

            try
            {
                Action.Invoke(GetParameters());
                Write(textWriter, "Passed");
                return true;
            }
            catch (Exception ex)
            {
                Write(textWriter, ex.ToString());
                return false;
            }
        }

        private void Write(TextWriter textWriter, string result)
        {
            Console.WriteLine("{0} [{1}]", Text.PadRight(120), result);
        }

        private object[] GetParameters()
        {
            var parameters = new List<object>();
            var textParts = this.Text.Split('\'');

            for (int i = 1; i < textParts.Length; i = i + 2)
            {
                parameters.Add(textParts[i]);
            }

            return parameters.ToArray();
        }
    }
}
