using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace Endeavor.Steps.Core.Tests
{
    public class DecisionStepTest
    {
        [Fact]
        public void VerifyConditionIsTrue()
        {
            Dictionary<string, object> properties = GetProperties("Data.X > Data.Y");

            TaskRequest task = GetTask();

            IStep step = new DecisionStep();
            step.Initialize(properties);
            TaskResponse response = step.Execute(task);

            Assert.Equal(StatusType.Complete, response.Status);
            Assert.Equal(true.ToString(), response.ReleaseValue);
        }

        [Fact]
        public void VerifyConditionIsFalse()
        {
            Dictionary<string, object> properties = GetProperties("Data.X < Data.Y");

            TaskRequest task = GetTask();

            IStep step = new DecisionStep();
            step.Initialize(properties);
            TaskResponse response = step.Execute(task);

            Assert.Equal(StatusType.Complete, response.Status);
            Assert.Equal(false.ToString(), response.ReleaseValue);
        }

        private Dictionary<string, object> GetProperties(string condition)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("StepID", 2);
            properties.Add("Name", "DecisionStep");
            properties.Add("WorkflowID", 1);
            properties.Add("StepType", "DecisionStep");
            properties.Add("ID", 3);
            properties.Add("Condition", condition);

            return properties;
        }

        private TaskRequest GetTask()
        {
            MyData data = new MyData()
            {
                X = 4,
                Y = 2
            };

            string ip = JsonConvert.SerializeObject(data);

            TaskRequest task = new TaskRequest()
            {
                TaskId = 1,
                Status = StatusType.Processing,
                Input = ip
            };

            return task;
        }
    }

    public class MyData
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
