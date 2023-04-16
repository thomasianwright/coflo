﻿namespace Coflo.Abstractions.Workflow.Enums;

public enum WorkflowErrorHandling
{
    Retry = 0,
    Suspend = 1,
    Terminate = 2,
    Compensate = 3
}