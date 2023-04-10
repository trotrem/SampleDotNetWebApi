namespace BuberBreakfast.Contracts
{
    public record Breakfast(
        Guid? Id,
        string Name, 
        string Description, 
        DateTime StartDateTime, 
        DateTime EndDateTime, 
        DateTime? LastModifiedDateTime, 
        List<string>? Savory, 
        List<string>? Sweet);
}
