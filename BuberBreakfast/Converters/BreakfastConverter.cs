using BuberBreakfast.Contracts;
using BuberBreakfast.Models;

namespace BuberBreakfast.Converters
{
    public class BreakfastConverter
    {
        public static Breakfast ToContract(BreakfastEntity model)
        {
            return new Breakfast(model.Id, model.Name, model.Description, model.StartDateTime, model.EndDateTime, model.LastModifiedDateTime, model.Savory, model.Sweet);
        }

        public static BreakfastEntity ToModel(Breakfast contract)
        {
            // TODO shouldn't create the ID or the last modified date here...
            var id = contract.Id ?? Guid.NewGuid();
            var lastModifiedDateTime = contract.LastModifiedDateTime ?? DateTime.UtcNow;
            return new BreakfastEntity 
            { 
                Id=id, 
                Name=contract.Name, 
                Description=contract.Description, 
                StartDateTime=contract.StartDateTime, 
                EndDateTime=contract.EndDateTime, 
                LastModifiedDateTime=lastModifiedDateTime, 
                Savory=contract.Savory, 
                Sweet=contract.Sweet
            };
        }
    }
}
