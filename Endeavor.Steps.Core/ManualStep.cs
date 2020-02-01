using System;
using System.Collections.Generic;
using System.Text;

namespace Endeavor.Steps.Core
{
    public class ManualStep : Step
    {
        public int ManualStepId { get; private set; }

        protected override void Load(Dictionary<string, object> properties)
        {
            foreach (string key in properties.Keys)
            {
                switch (key)
                {
                    case "ID":
                        ManualStepId = (int)properties[key];
                        break;
                }
            }
        }

        protected override TaskResponse Run(TaskRequest task)
        {
            TaskResponse result = new TaskResponse
            {
                Status = StatusType.Waiting,
                Output = task.Input
            };

            return result;
        }
    }
}
