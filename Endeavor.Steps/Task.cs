using System;
using System.Collections.Generic;
using System.Text;

namespace Endeavor.Steps
{
    public class TaskRequest
    {
        public long TaskId;
        public StatusType Status;
        public string Input;
    }

    public class TaskResponse
    {
        public StatusType Status;
        public string ReleaseValue;
        public string Output;
    }

    public enum StatusType
    {
        Ready,
        Queued,
        Processing,
        Waiting,
        Complete,
        Error
    }
}
