# endeavor-steps

Interface and classes used to create steps for the Endeavor Workflow system.

# Endeavor.Steps

![Endeavor.Steps](https://img.shields.io/nuget/v/Endeavor.Steps.svg)

The interface and classes needed to implement your own steps. When implementing your own step, you will need to create a table in the Endeavor database to store your steps data as well as a stored procedure to retieve the data ( see [endeavor-database](https://github.com/keryhe/endeavor-database) ). 

**IStep** - The interface for implementing steps.

```c#
void Initialize(Dictionary<string, object> properties);
TaskResponse Execute(TaskRequest task);
```

**Step** - The abstract base class that you should inherit from to implement your step. It implements the IStep interface. The Step base class provides you with 2 abstract methods you need to implement.

```c#
protected abstract void Load(Dictionary<string, object> properties);
protected abstract TaskResponse Run(TaskRequest task);
```

## Implementing the Load method

The Load method is where you initialize the propertes of your class. The dictionary will contain the data from the stored procedure that was added to the Endeavor database. The string will be the name of the database column, the object will be the value of that column. You will need to map the values of this dictionary to the properties of the step you created.

## Implementing the Run method

The Run method is where everything happens, it is the action that the step needs to perform. 

 **TaskRequest** - Parameter passed into the Run method.

```c#
public class TaskRequest
{
    public long TaskId;
    public StatusType Status;
    public string Input;
}
```
- `TaskId` - The id of the task being worked.
- `Status` - the status of the task `Processing`.
- `Input` is a json formated string of the output from the step before it. Use the Newtonsoft json library to deserialize the string into the datatype your step is expecting. Newtonsoft is included as a reference in the endeavor.steps package.

**TaskResponse** - The return value from the Run method.

```c#
public class TaskResponse
{
    public StatusType Status;
    public string ReleaseValue;
    public string Output;
}
```

- `Status`: set the status to one of the following
    - `Complete` if everything is successful.
    - `Waiting` if a task in this step needs to wait to be released at a later date. Either by a person or an automated process.
    - `Error` if something went wrong. You can also throw an exception instead.

- `ReleaseValue` is used for branching logic. A step can contain one or many output paths to one or more different steps. These paths, or `links` can have a value associated with it. Set the ReleaseValue to equal the value on one of the links to have the task follow that path. If the step only has one path, leave this blank.

- `Output`- this is the data that will be used as the input to the next step. Use the Newtonsoft json library to serialize the object to a json string and return that string.

# Endeavor.Steps.Core

![Endeavor.Steps.Core](https://img.shields.io/nuget/v/Endeavor.Steps.Core.svg)

The core steps 'built-in' to the Endeavor Workflow System.

- **StartStep** - The first step in the flow
- **ManualStep** - A step that waits for the task to be released manually (or by an automated process).
- **DecisionStep** - Creates a branch to two different steps depending on whether the condition statement evaluates to true or false.
- **EndStep** - the last step in the flow.
