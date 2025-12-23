using AutoMapper;
using CarRental.Application.Contracts.Dtos.Rentals;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Domain.Models;
using CarRental.Domain.Repositories;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for <see cref="Rental"/>.
/// </summary>
public class RentalService(
    IRepository<Rental> repository,
    IRepository<Client> clientRepository,
    IRepository<Car> carRepository,
    IMapper mapper)
    : CrudServiceBase<Rental, RentalDto, RentalCreateUpdateDto>(repository, mapper), IApplicationService<RentalDto, RentalCreateUpdateDto>
{
    /// <inheritdoc/>
    public override async Task<RentalDto> Create(RentalCreateUpdateDto input)
    {
        var client = await clientRepository.GetById(input.ClientId) ?? throw new InvalidOperationException($"Client with id={input.ClientId} was not found.");
        var car = await carRepository.GetById(input.CarId) ?? throw new InvalidOperationException($"Car with id={input.CarId} was not found.");
        var entity = Mapper.Map<Rental>(input);
        entity.Client = client;
        entity.Car = car;

        await Repository.Add(entity);
        return Mapper.Map<RentalDto>(entity);
    }

    /// <inheritdoc/>
    public override async Task<RentalDto?> Update(int id, RentalCreateUpdateDto input)
    {
        var existing = await Repository.GetById(id);
        if (existing is null)
        {
            return null;
        }

        var client = await clientRepository.GetById(input.ClientId) ?? throw new InvalidOperationException($"Client with id={input.ClientId} was not found.");
        var car = await carRepository.GetById(input.CarId) ?? throw new InvalidOperationException($"Car with id={input.CarId} was not found.");
        Mapper.Map(input, existing);
        existing.Client = client;
        existing.Car = car;

        await Repository.Update(existing);
        return Mapper.Map<RentalDto>(existing);
    }
}