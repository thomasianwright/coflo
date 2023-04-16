﻿using Coflo.Abstractions.Workflow.Enums;

namespace Coflo.Abstractions.Workflow.Models;

public class WorkflowDefinition
{
    public string Id { get; set; }
    public int Version { get; set; }
    public string Description { get; set; }
    public WorkflowStepCollection Steps { get; set; } = new();
    public Type DataType { get; set; }
    public WorkflowErrorHandling DefaultErrorBehavior { get; set; }
    public Type OnPostMiddlewareError { get; set; }
    public Type OnExecuteMiddlewareError { get; set; }
    public TimeSpan? DefaultErrorRetryInterval { get; set; }
}