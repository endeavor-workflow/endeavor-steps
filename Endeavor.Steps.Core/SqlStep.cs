using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Endeavor.Steps.Core
{
    public class SqlStep : Step
    {
        public int SqlStepId { get; private set; }
        public string ConnectionString { get; private set; }
        public string CommandText { get; private set; }
        public CommandType CommandType { get; private set; }

        protected override void Load(Dictionary<string, object> properties)
        {
            foreach (string key in properties.Keys)
            {
                switch (key)
                {
                    case "ID":
                        SqlStepId = (int)properties[key];
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
