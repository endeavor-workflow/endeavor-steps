using System;
using System.Collections.Generic;

namespace Endeavor.Steps
{
    public interface IStep
    {
        void Initialize(Dictionary<string, object> properties);

        TaskResponse Execute(TaskRequest task);
    }
}