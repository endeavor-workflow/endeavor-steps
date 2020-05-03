using System;
using System.Collections.Generic;
using System.Text;

namespace Endeavor.Steps.Core
{
    public class WorkflowStep : Step
    {
        public int WorkflowStepId { get; private set; }

        protected override void Load(Dictionary<string, object> properties)
        {
            foreach (string key in properties.Keys)
            {
                switch (key)
                {
                    case "ID":
                        WorkflowStepId = (int)properties[key];
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
