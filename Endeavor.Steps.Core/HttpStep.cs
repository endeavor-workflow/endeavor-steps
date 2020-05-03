using System;
using System.Collections.Generic;
using System.Text;

namespace Endeavor.Steps.Core
{
    public class HttpStep : Step
    {
        public int HttpStepId { get; private set; }
        public string Uri { get; private set; }
        public string Method { get; private set; }
        public string ContentType { get; private set; }

        protected override void Load(Dictionary<string, object> properties)
        {
            foreach (string key in properties.Keys)
            {
                switch (key)
                {
                    case "ID":
                        HttpStepId = (int)properties[key];
                        break;
                    case "Uri":
                        Uri = properties[key].ToString();
                        break;
                    case "Method":
                        Method = properties[key].ToString();
                        break;
                    case "ContentType":
                        ContentType = properties[key].ToString();
                        break;
                }
            }
        }

        protected override TaskResponse Run(TaskRequest task)
        {
            switch (Method)
            {
                case "GET":
                    break;
                case "POST":
                    break;
                case "PUT":
                    break;
                case "PATCH":
                    break;
                case "DELETE":
                    break;
                default:
                    break;
            }

            TaskResponse result = new TaskResponse
            {
                Status = StatusType.Complete,
                Output = task.Input
            };

            return result;
        }
    }
}
