using BuberBreakfast.Models;
using BuberBreakfast.Exceptions;
using System.Reflection.Metadata.Ecma335;

namespace BuberBreakfast.Services
{
    public class BreakfastService : IBreakfastService
    {
        private readonly Dictionary<Guid, BreakfastEntity> breakfasts = new();

        public BreakfastEntity CreateBreakfast(BreakfastEntity breakfast)
        {
            try
            {
                this.breakfasts.Add(breakfast.Id, breakfast);
                return breakfast;
            }
            catch (ArgumentException)
            {
                // TODO, change this to do nothing?
                throw ServiceErrors.ConflictException($"{nameof(Contracts.Breakfast)} with id {breakfast.Id} already exists.");
            }
        }

        public void DeleteBreakfast(Guid id)
        {
            this.breakfasts.Remove(id);
        }

        public BreakfastEntity? GetBreakfast(Guid id)
        {
            if (this.breakfasts.TryGetValue(id, out var breakfast))
                return breakfast;           

            return null;
        }

        public BreakfastEntity UpsertBreakfast(Guid id, BreakfastEntity breakfast, out bool isCreation)
        {
            if (id != breakfast.Id)
            { }
                // TODO fix
                // throw ServiceErrors.BadRequestException();
            isCreation = !this.breakfasts.ContainsKey(id);
            this.breakfasts[id] = breakfast;
            return breakfast;
        }
    }
}
