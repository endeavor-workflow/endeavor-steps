using System;
using System.Collections.Generic;
using System.Text.Json;
using Jint;

namespace Endeavor.Steps.Core
{
    public class DecisionStep : Step
    {
        public int DecisionStepId { get; private set; }
        public string Condition { get; private set; }

        protected override void Load(Dictionary<string, object> properties)
        {
            foreach (string key in properties.Keys)
            {
                switch (key)
                {
                    case "ID":
                        DecisionStepId = (int)properties[key];
                        break;
                    case "Condition":
                        Condition = properties[key].ToString();
                        break;
                }
            }
        }

        protected override TaskResponse Run(TaskRequest task)
        {
            if (string.IsNullOrEmpty(Condition))
            {
                throw new NullReferenceException("This step's Condition field is Empty.");
            }

            dynamic data = JsonSerializer.Deserialize<dynamic>(task.Input);

            bool result = new Engine()
                .SetValue("data", data)
                .Execute(Condition)
                .GetCompletionValue()
                .AsBoolean();

            TaskResponse response = new TaskResponse
            {
                Status = StatusType.Complete,
                ReleaseValue = result.ToString(),
                Output = task.Input
            };

            return response;
        }
    }
}
