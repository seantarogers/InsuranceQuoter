namespace InsuranceQuoter.Utility.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Dtos;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Configuration;

    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            Console.WriteLine("Building Cosmos Db and the containers required for the InsuranceQuoter application...");

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string endpoint = config["CosmosEndpoint"];
            string masterKey = config["CosmosMasterKey"];

            using (var client = new CosmosClient(endpoint, masterKey))
            {
                Database database = (await client.CreateDatabaseIfNotExistsAsync("InsuranceQuoter", ThroughputProperties.CreateManualThroughput(400)).ConfigureAwait(false)).Database;

                Container addressContainer = (await database.CreateContainerIfNotExistsAsync("Address_Manual", "/postcode", 400).ConfigureAwait(false)).Container;

                List<AddressDto> addresses = BuildTestAddresses();

                await StoreAddresses(addresses, addressContainer).ConfigureAwait(false);

                Container carContainer = (await database.CreateContainerIfNotExistsAsync("Car_Manual", "/registrationNumber", 400).ConfigureAwait(false)).Container;

                IEnumerable<CarDto> cars = BuildTestCars();

                await StoreCars(cars, carContainer).ConfigureAwait(false);

                await database.CreateContainerIfNotExistsAsync("Customer_Manual", "/email", 400).ConfigureAwait(false);
            }

            Console.WriteLine("Built the Cosmos Db and the containers required for the InsuranceQuoter application");
            Console.ReadLine();
        }

        private static async Task StoreCars(IEnumerable<CarDto> cars, Container carContainer)
        {
            foreach (CarDto car in cars)
            {
                ItemResponse<CarDto> response = await carContainer.CreateItemAsync(car, new PartitionKey(car.RegistrationNumber));

                Console.WriteLine($"RU cost of creating Car document: {response.RequestCharge}");
            }
        }

        private static async Task StoreAddresses(IEnumerable<AddressDto> addresses, Container addressContainer)
        {
            foreach (AddressDto address in addresses)
            {
                ItemResponse<AddressDto> response = await addressContainer.CreateItemAsync(address, new PartitionKey(address.Postcode));

                Console.WriteLine($"RU cost of creating Address document: {response.RequestCharge}");
            }
        }

        private static IEnumerable<CarDto> BuildTestCars() =>
            new List<CarDto>
            {
                new CarDto
                {
                    Id = "KP15TYJ",
                    RegistrationNumber = "KP15 TYJ",
                    Make = "Ford",
                    Model = "Fiesta",
                    FuelType = "Petrol",
                    Mileage = 15000,
                    TransmissionType = "Manual",
                    YearOfManufacture = 2015
                },
                new CarDto
                {
                    Id = "KP17TXG",
                    RegistrationNumber = "KP17 TXG",
                    Make = "Ford",
                    Model = "Mondeo",
                    FuelType = "Petrol",
                    Mileage = 5000,
                    TransmissionType = "Automatic",
                    YearOfManufacture = 2017
                }
            };

        private static List<AddressDto> BuildTestAddresses() =>
            new List<AddressDto>
            {
                new AddressDto
                {
                    Id = Guid.Parse("2b4126f4-4a2d-4d62-b772-61678e3f3bf9"),
                    AddressLine1 = "41 Brunswick Terrace",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1HJ"
                },
                new AddressDto
                {
                    Id = Guid.Parse("79945e4d-27b1-4f0d-8d0e-0fbaa0b903ea"),
                    AddressLine1 = "42 Brunswick Terrace",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1HJ "
                },
                new AddressDto
                {
                    Id = Guid.Parse("611334c4-8e23-4898-b544-72bf49de1c50"),
                    AddressLine1 = "43 Brunswick Terrace",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1HJ"
                },
                new AddressDto
                {
                    Id = Guid.Parse("fbd3892a-4ada-4bbf-bb05-e0086cf9e400"),
                    AddressLine1 = "44 Brunswick Terrace",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1HJ"
                },
                new AddressDto
                {
                    Id = Guid.Parse("e0b68ddf-c675-406f-bb79-ed050007adff"),
                    AddressLine1 = "45 Brunswick Terrace",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1HJ"
                },
                new AddressDto
                {
                    Id = Guid.Parse("a832b437-57da-4c25-9b3a-134f438a4e41"),
                    AddressLine1 = "5 Brunswick SquareTerrace",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1EE"
                },
                new AddressDto
                {
                    Id = Guid.Parse("44f60a57-e051-443b-8d5d-4fff3dc07c22"),
                    AddressLine1 = "6 Brunswick Square",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1EE"
                },
                new AddressDto
                {
                    Id = Guid.Parse("8b3a79ea-6213-4510-a402-3d485621609c"),
                    AddressLine1 = "7 Brunswick Terrace",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1EE"
                },
                new AddressDto
                {
                    Id = Guid.Parse("e4d3db90-08cd-4f20-b9dc-c491f0a08ce0"),
                    AddressLine1 = "8 Brunswick Terrace",
                    AddressLine2 = "Brunswick",
                    City = "Hove",
                    County = "Sussex",
                    Postcode = "BN3 1EE"
                }
            };
    }
}