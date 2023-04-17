namespace Coflo.Abstractions.Activities.Contracts;

public interface IActivity : IActivityBody
{
    public string Name { get; }
}