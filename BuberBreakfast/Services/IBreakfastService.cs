using BuberBreakfast.Models;

namespace BuberBreakfast.Services
{
    public interface IBreakfastService
    {
        BreakfastEntity CreateBreakfast(BreakfastEntity breakfast);

        BreakfastEntity? GetBreakfast(Guid id);

        BreakfastEntity UpsertBreakfast(Guid id, BreakfastEntity breakfast, out bool isCreation);

        void DeleteBreakfast(Guid id);
    }
}
