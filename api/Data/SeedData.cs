using HOVLaneViolation.Entities;

namespace HOVLaneViolation.Data;

public class SeedData
{
    public static async Task InitializeAsync(DataContext context)
    {
        if (context.HOVMasters.Any()) return;

        var cars = new[]
        {
            new HOVMasterData { Make = "Toyota", Model = "Camry", Weight = 1700 },
            new HOVMasterData { Make = "Honda", Model = "Civic", Weight = 1600 },
            new HOVMasterData { Make = "Ford", Model = "F-150", Weight = 2500 },
            new HOVMasterData { Make = "Chevrolet", Model = "Malibu", Weight = 1800 },
            new HOVMasterData { Make = "Tesla", Model = "Model 3", Weight = 1900 },
            new HOVMasterData { Make = "BMW", Model = "X5", Weight = 2200 },
            new HOVMasterData { Make = "Audi", Model = "A4", Weight = 1800 },
            new HOVMasterData { Make = "Hyundai", Model = "Elantra", Weight = 1600 },
            new HOVMasterData { Make = "Kia", Model = "Sorento", Weight = 2100 },
            new HOVMasterData { Make = "Nissan", Model = "Altima", Weight = 1700 },
            new HOVMasterData { Make = "Mazda", Model = "CX-5", Weight = 2000 },
            new HOVMasterData { Make = "Jeep", Model = "Wrangler", Weight = 2300 },
            new HOVMasterData { Make = "Subaru", Model = "Outback", Weight = 1900 },
            new HOVMasterData { Make = "Volkswagen", Model = "Passat", Weight = 1800 },
            new HOVMasterData { Make = "Dodge", Model = "Charger", Weight = 2200 }
        };

        var customers = new[]
        {
            new HOVCustomerData { LicensePlate = "ABC1234", FirstName = "John", LastName = "Doe", Email = "john@example.com", Phone = "5551234567", Address = "123 Main St" },
            new HOVCustomerData { LicensePlate = "XYZ5678", FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", Phone = "5559876543", Address = "456 Oak Ave" },
            new HOVCustomerData { LicensePlate = "LMN4321", FirstName = "Mike", LastName = "Brown", Email = "mike@example.com", Phone = "5556781234", Address = "789 Pine Rd" },
            new HOVCustomerData { LicensePlate = "GHI5678", FirstName = "Sara", LastName = "Lee", Email = "sara@example.com", Phone = "5553456789", Address = "321 Cedar Ln" },
            new HOVCustomerData { LicensePlate = "QRS9999", FirstName = "Ali", LastName = "Khan", Email = "ali@example.com", Phone = "5557891234", Address = "654 Birch St" },
            new HOVCustomerData { LicensePlate = "TUV8888", FirstName = "Linda", LastName = "Green", Email = "linda@example.com", Phone = "5554567890", Address = "890 Spruce Ct" },
            new HOVCustomerData { LicensePlate = "JKL2222", FirstName = "Tom", LastName = "Davis", Email = "tom@example.com", Phone = "5556543210", Address = "135 Elm Way" },
            new HOVCustomerData { LicensePlate = "MNO3333", FirstName = "Nina", LastName = "Patel", Email = "nina@example.com", Phone = "5553219876", Address = "246 Palm Dr" },
            new HOVCustomerData { LicensePlate = "PQR4444", FirstName = "Chris", LastName = "Kim", Email = "chris@example.com", Phone = "5551237890", Address = "357 Maple Blvd" },
            new HOVCustomerData { LicensePlate = "STU5555", FirstName = "Emily", LastName = "Clark", Email = "emily@example.com", Phone = "5552345678", Address = "468 Willow St" },
        };

        var rule = new HOVRule
        {
            Id = 1,
            Value = 300
        };

        context.HOVMasters.AddRange(cars);
        context.HOVCustomers.AddRange(customers);
        context.HOVRules.Add(rule);

        await context.SaveChangesAsync();
    }
}
