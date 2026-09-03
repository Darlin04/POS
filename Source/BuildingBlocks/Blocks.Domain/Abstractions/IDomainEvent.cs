namespace Blocks.Domain.Abstractions;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}