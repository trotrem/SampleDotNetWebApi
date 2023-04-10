using BuberBreakfast.Exceptions;
using BuberBreakfast.Models;
using BuberBreakfast.Services;
using FluentAssertions.Execution;
using System.Configuration;

namespace BuberBreakfast.Tests.Services
{
    public class BreakfastServiceTests
    {
        private readonly IBreakfastService service;

        public BreakfastServiceTests() 
        { 
            this.service = new BreakfastService();
        }

        [Fact]
        public void GetBreakfast_WhenExists_ReturnsBreakfast()
        {
            var existingEntity = this.SampleBreakfastEntity;
            this.service.CreateBreakfast(existingEntity);
            var result = this.service.GetBreakfast(existingEntity.Id);

            result.Should().Be(existingEntity);
        }

        [Fact]
        public void GetBreakfast_WhenNotExists_ReturnsNull()
        {
            var id = Guid.NewGuid();
            var result = this.service.GetBreakfast(id);

            result.Should().BeNull();
        }

        [Fact]
        public void CreateBreakfast_WhenNotExists_CreatesAndReturnsBreakfast()
        {
            var entityToCreate = this.SampleBreakfastEntity;
            var result = this.service.CreateBreakfast(entityToCreate);

            result.Should().Be(entityToCreate);

            var storageContent = this.service.GetBreakfast(result.Id);
            storageContent.Should().Be(entityToCreate);
        }

        [Fact]
        public void UpsertBreakfast_WhenNotExists_CreatesAndReturnsBreakfast()
        {
            var entityToCreate = this.SampleBreakfastEntity;
            var result = this.service.UpsertBreakfast(entityToCreate.Id, entityToCreate, out var isCreation);

            isCreation.Should().BeTrue();
            result.Should().Be(entityToCreate);

            var storageContent = this.service.GetBreakfast(result.Id);
            storageContent.Should().Be(entityToCreate);
        }

        [Fact]
        public void UpsertBreakfast_WhenExists_UpdatesAndReturnsBreakfast()
        {
            var entityToCreate = this.SampleBreakfastEntity;
            this.service.CreateBreakfast(entityToCreate);

            var modifiedEntity = this.SampleBreakfastEntity;
            modifiedEntity.Name = "Modified";
            var result = this.service.UpsertBreakfast(entityToCreate.Id, modifiedEntity, out var isCreation);

            isCreation.Should().BeFalse();
            result.Should().Be(modifiedEntity);

            var storageContent = this.service.GetBreakfast(entityToCreate.Id);
            storageContent.Should().Be(modifiedEntity);
        }

        [Fact]
        public void CreateBreakfast_WhenExists_ThrowsConflictException()
        {
            var entityToCreate = this.SampleBreakfastEntity;
            this.service.CreateBreakfast(entityToCreate);
            var act = () => this.service.CreateBreakfast(entityToCreate);

            act.Should().Throw<HttpException>().Where(e => e.StatusCode == 409);
        }

        private void AssertAreEqual(BreakfastEntity first, BreakfastEntity second)
        {
            using (new AssertionScope())
            {
                first.Id.Should().Be(second.Id);
                first.Name.Should().Be(second.Name);
                first.Description.Should().Be(second.Description);
                first.StartDateTime.Should().Be(second.StartDateTime);
                first.EndDateTime.Should().Be(second.EndDateTime);
                first.LastModifiedDateTime.Should().Be(second.LastModifiedDateTime);
                first.Savory.Should().Equal(second.Savory);
                first.Sweet.Should().Equal(second.Sweet);
            }
        }

        private BreakfastEntity SampleBreakfastEntity => new BreakfastEntity
        {
            Id = Guid.NewGuid(),
            Name = "SomeName",
            Description = "SomeDescription",
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now,
            LastModifiedDateTime = DateTime.Now,
            Savory = new List<string>() { "bread" },
            Sweet = new List<string>() { "cookie" }
        };
    }
}