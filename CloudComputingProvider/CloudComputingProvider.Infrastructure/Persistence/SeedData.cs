using CloudComputingProvider.DataModel.Domain.Models;
using CloudComputingProvider.Infrastructure.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.Infrastructure.Persistence
{
    public class SeedData : ISeedData
    {
        private readonly CloudComputingProviderDBContext _cloudComputingProviderDBContext;
        private readonly ILogger<SeedData> _logger;

        public SeedData(CloudComputingProviderDBContext cloudComputingProviderDBContext, ILogger<SeedData> logger)
        {
            _cloudComputingProviderDBContext = cloudComputingProviderDBContext;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogInformation("Database seeding began");

            if (!await _cloudComputingProviderDBContext.States.AnyAsync())
            {
                var states = new List<States>()
                {
                    new()
                    {
                        State = "Active",
                        CreatedBy = "System",
                        IsDeleted = false
                    },
                    new()
                    {
                        State = "Inactive",
                        CreatedBy = "System",
                        IsDeleted = false
                    },
                    new()
                    {
                        State = "On Hold",
                        CreatedBy = "System",
                        IsDeleted = false
                    },
                    new()
                    {
                        State = "Cancelled",
                        CreatedBy = "System",
                        IsDeleted = false
                    }
                };
                await _cloudComputingProviderDBContext.States.AddRangeAsync(states);
                await _cloudComputingProviderDBContext.SaveChangesAsync();
            }

            if (!await _cloudComputingProviderDBContext.Customers.AnyAsync())
            {
                var customers = new List<Customers>()
                {
                    new()
                    {
                        Code = "123456789",
                        TaxIdentificationNumber = "123456789",
                        Name = "Test Customer 1",
                        Address = "Omladinskih Brigada 1111, Belgrade",
                        Phone = "/",
                        Email = "/",
                        Description = "Test Customer",
                        CreatedBy = "System",
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        CustomerAccounts = new List<CustomerAccounts>()
                        {
                            new()
                            {
                                AccountNo = "EUR-01",
                                Description = "Test Customer Account - Europe",
                                CreatedBy = "System",
                                CreatedDate = DateTime.Now,
                                IsDeleted = false
                            },
                            new()
                            {
                                AccountNo = "US-01",
                                Description = "Test Customer Account - US",
                                CreatedBy = "System",
                                CreatedDate = DateTime.Now,
                                IsDeleted = false
                            }
                        }
                    },
                    new()
                    {
                        Code = "987654321",
                        TaxIdentificationNumber = "987654321",
                        Name = "Test Customer 2",
                        Address = "Omladinskih Brigada 2222, Belgrade",
                        Phone = "/",
                        Email = "/",
                        Description = "Test Customer",
                        CreatedBy = "System",
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        CustomerAccounts = new List<CustomerAccounts>()
                        {
                            new()
                            {
                                AccountNo = "EUR-02",
                                Description = "Test Customer Account - Europe",
                                CreatedBy = "System",
                                CreatedDate = DateTime.Now,
                                IsDeleted = false
                            }
                        }
                    }
                };

                await _cloudComputingProviderDBContext.Customers.AddRangeAsync(customers);
                await _cloudComputingProviderDBContext.SaveChangesAsync();
            }

            _logger.LogInformation("Database seeding ended");
        }
    }
}
