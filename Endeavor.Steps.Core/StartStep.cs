using System;
using System.Collections.Generic;

namespace Endeavor.Steps.Core
{
    public class StartStep : Step
    {
        public int StartStepId { get; private set; }

        protected override void Load(Dictionary<string, object> properties)
        {
            foreach (string key in properties.Keys)
            {
                switch (key)
                {
                    case "ID":
                        StartStepId = (int)properties[key];
                        break;
                }
            }
        }

        protected override TaskResponse Run(TaskRequest task)
        {
            TaskResponse result = new TaskResponse
            {
                Status = StatusType.Complete,
                Output = task.Input
            };

            return result;
        }
    }
}
