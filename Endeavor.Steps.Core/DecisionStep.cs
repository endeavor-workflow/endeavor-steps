using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis;
using System.Reflection;
using System.Dynamic;
using Newtonsoft.Json;

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

            dynamic data = JsonConvert.DeserializeObject<dynamic>(task.Input);

            var globals = new Globals { Data = data };

            var refs = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.DynamicAttribute).GetTypeInfo().Assembly.Location)
            };
            bool result = CSharpScript.EvaluateAsync<bool>(Condition, options: ScriptOptions.Default.AddReferences(refs), globals: globals).GetAwaiter().GetResult();

            TaskResponse response = new TaskResponse
            {
                Status = StatusType.Complete,
                ReleaseValue = result.ToString(),
                Output = task.Input
            };

            return response;
        }

        public class Globals
        {
            public dynamic Data;
        }
    }
}
