using AutoMapper;
using CarRental.Application.Contracts.Dtos.Cars;
using CarRental.Application.Contracts.Dtos.Clients;
using CarRental.Application.Contracts.Dtos.ModelGenerations;
using CarRental.Application.Contracts.Dtos.Models;
using CarRental.Application.Contracts.Dtos.Rentals;
using CarRental.Domain.Models;

namespace CarRental.Application.Profiles;

/// <summary>
/// AutoMapper profile for the car rental application layer.
/// </summary>
public sealed class CarRentalApplicationProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of class.
    /// </summary>
    public CarRentalApplicationProfile()
    {
        CreateMap<Car, CarDto>();
        CreateMap<CarCreateUpdateDto, Car>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Generation, opt => opt.Ignore());

        CreateMap<Client, ClientDto>();
        CreateMap<ClientCreateUpdateDto, Client>()
            .ForMember(d => d.Id, opt => opt.Ignore());

        CreateMap<Model, ModelDto>();
        CreateMap<ModelCreateUpdateDto, Model>()
            .ForMember(d => d.Id, opt => opt.Ignore());

        CreateMap<ModelGeneration, ModelGenerationDto>();
        CreateMap<ModelGenerationCreateUpdateDto, ModelGeneration>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Model, opt => opt.Ignore());

        CreateMap<Rental, RentalDto>();
        CreateMap<RentalCreateUpdateDto, Rental>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Client, opt => opt.Ignore())
            .ForMember(d => d.Car, opt => opt.Ignore());
    }
}