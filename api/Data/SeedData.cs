using HOVLaneViolation.Entities;
using Bogus; // Install-Package Bogus
using Microsoft.EntityFrameworkCore;

namespace HOVLaneViolation.Data;

public class SeedData
{
    public static async Task InitializeAsync(DataContext context)
    {
        if (await context.HOVMasters.AnyAsync()) return;

        var faker = new Faker();

        // Base data
        var makesAndModels = new Dictionary<string, string[]>
        {
            { "Toyota", new[] { "Camry", "Corolla", "RAV4", "Highlander", "Tacoma" } },
            { "Honda", new[] { "Civic", "Accord", "CR-V", "Pilot", "Fit" } },
            { "Ford", new[] { "F-150", "Focus", "Escape", "Mustang", "Explorer" } },
            { "Chevrolet", new[] { "Malibu", "Silverado", "Equinox", "Tahoe", "Camaro" } },
            { "Tesla", new[] { "Model 3", "Model S", "Model Y", "Cybertruck" } },
            { "BMW", new[] { "X5", "X3", "3 Series", "5 Series" } },
            { "Audi", new[] { "A4", "Q5", "A6", "Q7" } },
            { "Hyundai", new[] { "Elantra", "Sonata", "Tucson", "Santa Fe" } },
            { "Kia", new[] { "Sorento", "Sportage", "Optima", "Seltos" } },
            { "Nissan", new[] { "Altima", "Rogue", "Sentra", "Pathfinder" } }
        };

        // Generate 10,000 random cars
        var cars = new List<HOVMasterData>();
        var rand = new Random();

        for (int i = 0; i < 10000; i++)
        {
            var make = makesAndModels.Keys.ElementAt(rand.Next(makesAndModels.Count));
            var modelList = makesAndModels[make];
            var model = modelList[rand.Next(modelList.Length)];
            var weight = rand.Next(1400, 2600); // random kg weight

            cars.Add(new HOVMasterData
            {
                Make = make,
                Model = model,
                Weight = weight
            });
        }

        // Generate 1000 fake customers
        var customers = new Faker<HOVCustomerData>()
            .RuleFor(c => c.LicensePlate, f => f.Random.AlphaNumeric(7).ToUpper())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("##########"))
            .RuleFor(c => c.Address, f => f.Address.StreetAddress())
            .Generate(1000);

        // One sample rule
        var rule = new HOVRule
        {
            Id = 1,
            Value = 300
        };

        // Save all to DB
        await context.HOVMasters.AddRangeAsync(cars);
        await context.HOVCustomers.AddRangeAsync(customers);
        await context.HOVRules.AddAsync(rule);
        await context.SaveChangesAsync();
    }
}
