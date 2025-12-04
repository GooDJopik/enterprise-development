using CarRental.Domain.Models;

namespace CarRental.Domain;

/// <summary>
/// Provides seeded data for car rental tests.
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Collection of car Models available in the system.
    /// </summary>
    public List<Model> Models { get; }
    /// <summary>
    /// Collection of specific model Generations with technical details and rental price.
    /// </summary>
    public List<ModelGeneration> Generations { get; }
    /// <summary>
    /// Collection of Cars in the fleet.
    /// </summary>
    public List<Car> Cars { get; }
    /// <summary>
    /// Collection of Clients who rent Cars.
    /// </summary>
    public List<Client> Clients { get; }
    /// <summary>
    /// Collection of rental records linking Clients to Cars and rental periods.
    /// </summary>
    public List<Rental> Rentals { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataSeeder"/> class and seeds all test data.
    /// </summary>
    public DataSeeder()
    {
        Models =
        [
            new() { Id = 1, Name = "Toyota Corolla", DriveType = "Передний", Seats = 5, BodyType = "Седан", CarClass = "Эконом" },
            new() { Id = 2, Name = "BMW X5", DriveType = "Полный", Seats = 5, BodyType = "Кроссовер", CarClass = "Премиум" },
            new() { Id = 3, Name = "Kia Rio", DriveType = "Передний", Seats = 5, BodyType = "Седан", CarClass = "Эконом" },
            new() { Id = 4, Name = "Audi A4", DriveType = "Передний", Seats = 5, BodyType = "Седан", CarClass = "Бизнес" },
            new() { Id = 5, Name = "Lada Vesta", DriveType = "Передний", Seats = 5, BodyType = "Седан", CarClass = "Эконом" },
            new() { Id = 6, Name = "Tesla Model 3", DriveType = "Полный", Seats = 5, BodyType = "Седан", CarClass = "Премиум" },
            new() { Id = 7, Name = "Volkswagen Polo", DriveType = "Передний", Seats = 5, BodyType = "Седан", CarClass = "Эконом" },
            new() { Id = 8, Name = "Hyundai Tucson", DriveType = "Полный", Seats = 5, BodyType = "Кроссовер", CarClass = "Средний" },
            new() { Id = 9, Name = "Mercedes C-Class", DriveType = "Задний", Seats = 5, BodyType = "Седан", CarClass = "Бизнес" },
            new() { Id = 10, Name = "Renault Duster", DriveType = "Полный", Seats = 5, BodyType = "Кроссовер", CarClass = "Средний" },
            new() { Id = 11, Name = "Mazda 6", DriveType = "Передний", Seats = 5, BodyType = "Седан", CarClass = "Бизнес" },
            new() { Id = 12, Name = "Nissan Qashqai", DriveType = "Полный", Seats = 5, BodyType = "Кроссовер", CarClass = "Средний" },
            new() { Id = 13, Name = "Skoda Octavia", DriveType = "Передний", Seats = 5, BodyType = "Седан", CarClass = "Средний" },
            new() { Id = 14, Name = "Chevrolet Tahoe", DriveType = "Полный", Seats = 8, BodyType = "Внедорожник", CarClass = "Премиум" },
            new() { Id = 15, Name = "Ford Focus", DriveType = "Передний", Seats = 5, BodyType = "Седан", CarClass = "Эконом" }
        ];

        Generations =
        [
            new() { Id = 1, Model = Models[0], Year = 2020, EngineVolume = 1.6, TransmissionType = "Автомат", PricePerHour = 1000 },
            new() { Id = 2, Model = Models[1], Year = 2021, EngineVolume = 2.0, TransmissionType = "Автомат", PricePerHour = 2000 },
            new() { Id = 3, Model = Models[2], Year = 2019, EngineVolume = 1.4, TransmissionType = "Механика", PricePerHour = 2500 },
            new() { Id = 4, Model = Models[3], Year = 2020, EngineVolume = 2.0, TransmissionType = "Автомат", PricePerHour = 1900 },
            new() { Id = 5, Model = Models[4], Year = 2018, EngineVolume = 1.6, TransmissionType = "Механика", PricePerHour = 1000 },
            new() { Id = 6, Model = Models[5], Year = 2022, EngineVolume = 1.2, TransmissionType = "Автомат", PricePerHour = 8000 },
            new() { Id = 7, Model = Models[6], Year = 2020, EngineVolume = 1.4, TransmissionType = "Механика", PricePerHour = 1250 },
            new() { Id = 8, Model = Models[7], Year = 2021, EngineVolume = 2.0, TransmissionType = "Автомат", PricePerHour = 2000 },
            new() { Id = 9, Model = Models[8], Year = 2019, EngineVolume = 2.0, TransmissionType = "Автомат", PricePerHour = 2500 },
            new() { Id = 10, Model = Models[9], Year = 2020, EngineVolume = 1.6, TransmissionType = "Механика", PricePerHour = 2300 },
            new() { Id = 11, Model = Models[10], Year = 2020, EngineVolume = 2.0, TransmissionType = "Автомат", PricePerHour = 3000 },
            new() { Id = 12, Model = Models[11], Year = 2021, EngineVolume = 1.6, TransmissionType = "Автомат", PricePerHour = 2500 },
            new() { Id = 13, Model = Models[12], Year = 2019, EngineVolume = 1.8, TransmissionType = "Механика", PricePerHour = 2500 },
            new() { Id = 14, Model = Models[13], Year = 2025, EngineVolume = 5.3, TransmissionType = "Автомат", PricePerHour = 8000 },
            new() { Id = 15, Model = Models[14], Year = 2018, EngineVolume = 1.6, TransmissionType = "Автомат", PricePerHour = 1250 }
        ];

        Cars =
        [
            new() { Id = 1, Generation = Generations[0], LicensePlate = "I101DH", Color = "Белый" },
            new() { Id = 2, Generation = Generations[1], LicensePlate = "O122OP", Color = "Черный" },
            new() { Id = 3, Generation = Generations[2], LicensePlate = "C323OB", Color = "Серый" },
            new() { Id = 4, Generation = Generations[3], LicensePlate = "M234PP", Color = "Белый" },
            new() { Id = 5, Generation = Generations[4], LicensePlate = "A754BA", Color = "Черный" },
            new() { Id = 6, Generation = Generations[5], LicensePlate = "Y349KY", Color = "Серый" },
            new() { Id = 7, Generation = Generations[6], LicensePlate = "Т009ТР", Color = "Белый" },
            new() { Id = 8, Generation = Generations[7], LicensePlate = "Р108ВР", Color = "Черный" },
            new() { Id = 9, Generation = Generations[8], LicensePlate = "А166СС", Color = "Серый" },
            new() { Id = 10, Generation = Generations[9], LicensePlate = "M110LC", Color = "Белый" },
            new() { Id = 11, Generation = Generations[10], LicensePlate = "E111EP", Color = "Черный" },
            new() { Id = 12, Generation = Generations[11], LicensePlate = "Е751ОВ", Color = "Серый" },
            new() { Id = 13, Generation = Generations[12], LicensePlate = "К361ЛО", Color = "Белый" },
            new() { Id = 14, Generation = Generations[13], LicensePlate = "А100МР", Color = "Черный" },
            new() { Id = 15, Generation = Generations[14], LicensePlate = "А185ЛР", Color = "Серый" }
        ];

        Clients =
        [
            new() { Id = 1, LicenseNumber = "133416", FullName = "Ivanov Ivan Ivanovich", BirthDate = new DateTime(1990, 1, 15) },
            new() { Id = 2, LicenseNumber = "214567", FullName = "Petrov Alexey Sergeevich", BirthDate = new DateTime(1985, 5, 20) },
            new() { Id = 3, LicenseNumber = "341178", FullName = "Sidorova Maria Pavlovna", BirthDate = new DateTime(1992, 3, 12) },
            new() { Id = 4, LicenseNumber = "436489", FullName = "Kuznetsov Dmitry Alekseevich", BirthDate = new DateTime(1988, 7, 8) },
            new() { Id = 5, LicenseNumber = "577890", FullName = "Smirnova Ekaterina Nikolaevna", BirthDate = new DateTime(1995, 9, 30) },
            new() { Id = 6, LicenseNumber = "688901", FullName = "Popov Sergey Viktorovich", BirthDate = new DateTime(1983, 12, 5) },
            new() { Id = 7, LicenseNumber = "789092", FullName = "Vasilyeva Olga Mikhailovna", BirthDate = new DateTime(1991, 4, 22) },
            new() { Id = 8, LicenseNumber = "850173", FullName = "Morozova Natalia Andreevna", BirthDate = new DateTime(1987, 6, 10) },
            new() { Id = 9, LicenseNumber = "901534", FullName = "Fyodorov Andrey Petrovich", BirthDate = new DateTime(1993, 11, 2) },
            new() { Id = 10, LicenseNumber = "012045", FullName = "Kovalev Pavel Yurievich", BirthDate = new DateTime(1989, 2, 17) },
            new() { Id = 11, LicenseNumber = "115033", FullName = "Novikova Elena Anatolyevna", BirthDate = new DateTime(1994, 8, 25) },
            new() { Id = 12, LicenseNumber = "223544", FullName = "Zaitsev Mikhail Fedorovich", BirthDate = new DateTime(1986, 10, 13) },
            new() { Id = 13, LicenseNumber = "332455", FullName = "Sokolova Svetlana Nikolaevna", BirthDate = new DateTime(1990, 12, 30) },
            new() { Id = 14, LicenseNumber = "447766", FullName = "Lebedev Konstantin Vyacheslavovich", BirthDate = new DateTime(1984, 1, 9) },
            new() { Id = 15, LicenseNumber = "580677", FullName = "Gusev Alexey Konstantinovich", BirthDate = new DateTime(1988, 5, 18) }
        ];

        Rentals =
        [
            new() { Id = 1, Client = Clients[0], Car = Cars[0], StartTime = new DateTime(2025, 11, 10, 8, 0, 0), DurationHours = 5 },
            new() { Id = 2, Client = Clients[0], Car = Cars[2], StartTime = new DateTime(2025, 11, 12, 19, 2, 2), DurationHours = 2 },
            new() { Id = 3, Client = Clients[0], Car = Cars[0], StartTime = new DateTime(2025, 11, 15, 9, 0, 0), DurationHours = 5 },
            new() { Id = 4, Client = Clients[0], Car = Cars[5], StartTime = new DateTime(2025, 11, 18, 10, 5, 5), DurationHours = 6 },
            new() { Id = 5, Client = Clients[1], Car = Cars[1], StartTime = new DateTime(2025, 11, 9, 10, 1, 1), DurationHours = 30 },
            new() { Id = 6, Client = Clients[1], Car = Cars[10], StartTime = new DateTime(2025, 11, 17, 13, 10, 10), DurationHours = 6 },
            new() { Id = 7, Client = Clients[2], Car = Cars[2], StartTime = new DateTime(2025, 11, 3, 11, 2, 2), DurationHours = 4 },
            new() { Id = 8, Client = Clients[2], Car = Cars[9], StartTime = new DateTime(2025, 11, 20, 10, 9, 9), DurationHours = 3 },
            new() { Id = 9, Client = Clients[3], Car = Cars[4], StartTime = new DateTime(2025, 11, 4, 12, 4, 4), DurationHours = 6 },
            new() { Id = 10, Client = Clients[4], Car = Cars[13], StartTime = new DateTime(2025, 11, 5, 9, 13, 13), DurationHours = 2 },
            new() { Id = 11, Client = Clients[4], Car = Cars[3], StartTime = new DateTime(2025, 11, 22, 11, 3, 3), DurationHours = 5 },
            new() { Id = 12, Client = Clients[5], Car = Cars[9], StartTime = new DateTime(2025, 11, 6, 12, 9, 9), DurationHours = 5 },
            new() { Id = 13, Client = Clients[5], Car = Cars[2], StartTime = new DateTime(2025, 11, 7, 11, 2, 2), DurationHours = 6 },
            new() { Id = 14, Client = Clients[5], Car = Cars[14], StartTime = new DateTime(2025, 11, 9, 14, 14, 14), DurationHours = 4 },
            new() { Id = 15, Client = Clients[5], Car = Cars[6], StartTime = new DateTime(2025, 11, 11, 10, 6, 6), DurationHours = 3 },
            new() { Id = 16, Client = Clients[5], Car = Cars[14], StartTime = new DateTime(2025, 11, 25, 9, 14, 14), DurationHours = 7 },
            new() { Id = 17, Client = Clients[6], Car = Cars[2], StartTime = new DateTime(2025, 11, 7, 9, 2, 2), DurationHours = 3 },
            new() { Id = 18, Client = Clients[7], Car = Cars[0], StartTime = new DateTime(2025, 11, 8, 10, 0, 0), DurationHours = 4 },
            new() { Id = 19, Client = Clients[7], Car = Cars[11], StartTime = new DateTime(2025, 11, 28, 15, 11, 11), DurationHours = 2 },
            new() { Id = 20, Client = Clients[8], Car = Cars[8], StartTime = new DateTime(2025, 11, 9, 11, 8, 8), DurationHours = 6 },
            new() { Id = 21, Client = Clients[9], Car = Cars[9], StartTime = new DateTime(2025, 11, 10, 12, 9, 9), DurationHours = 2 },
            new() { Id = 22, Client = Clients[9], Car = Cars[6], StartTime = new DateTime(2025, 11, 1, 8, 6, 6), DurationHours = 4 },
            new() { Id = 23, Client = Clients[10], Car = Cars[10], StartTime = new DateTime(2025, 11, 11, 9, 10, 10), DurationHours = 5 },
            new() { Id = 24, Client = Clients[11], Car = Cars[11], StartTime = new DateTime(2025, 11, 12, 14, 11, 11), DurationHours = 3 },
            new() { Id = 25, Client = Clients[12], Car = Cars[2], StartTime = new DateTime(2025, 11, 13, 9, 2, 2), DurationHours = 4 },
            new() { Id = 26, Client = Clients[13], Car = Cars[3], StartTime = new DateTime(2025, 11, 14, 10, 3, 3), DurationHours = 6 },
            new() { Id = 27, Client = Clients[14], Car = Cars[5], StartTime = new DateTime(2025, 11, 18, 11, 5, 5), DurationHours = 4 },
            new() { Id = 28, Client = Clients[14], Car = Cars[4], StartTime = new DateTime(2025, 11, 15, 11, 4, 4), DurationHours = 2 }
        ];
    }
}