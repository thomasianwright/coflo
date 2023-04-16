﻿using Coflo.Abstractions.Steps.Contracts;
using Coflo.Abstractions.Workflow.Models;

namespace Coflo.Abstractions.Primitives;

public class SagaContainer<TStepBody> : WorkflowStep<TStepBody>
    where TStepBody : IStepBody
{
    public override bool ResumeChildrenAfterCompensation => false;
    public override bool RevertChildrenAfterCompensation => true;

    public override void PrimeForRetry(ExecutionPointer pointer)
    {
        base.PrimeForRetry(pointer);
        pointer.PersistenceData = null;
    }
}