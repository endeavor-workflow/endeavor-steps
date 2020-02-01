using System;
using System.Collections.Generic;
using System.Text;

namespace Endeavor.Steps
{
    public abstract class Step : IStep
    {
        public int Id;
        public string Name;
        public int WorkflowId;
        public string StepType;

        public void Initialize(Dictionary<string, object> properties)
        {
            Dictionary<string, object> extraProperties = new Dictionary<string, object>();

            foreach (string key in properties.Keys)
            {
                switch (key)
                {
                    case "StepID":
                        Id = (int)properties[key];
                        break;
                    case "Name":
                        Name = properties[key].ToString();
                        break;
                    case "WorkflowID":
                        WorkflowId = (int)properties[key];
                        break;
                    case "StepType":
                        StepType = properties[key].ToString();
                        break;
                    default:
                        extraProperties.Add(key, properties[key]);
                        break;
                }
            }

            Load(extraProperties);
        }

        public TaskResponse Execute(TaskRequest task)
        {
            return Run(task);
        }

        protected abstract void Load(Dictionary<string, object> properties);
        protected abstract TaskResponse Run(TaskRequest task);
    }
}
