using CloudComputingProvider.DataModel;
using CloudComputingProvider.DataModel.Order;
using CloudComputingProvider.DataModel.Software;
using System.ComponentModel;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudComputingProvider.Helpers
{
    public class MockHttpMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Mock response for GetAvailableSoftwareServices
            if (request.RequestUri.AbsolutePath == "/available-software-services")
            {
                var responseObject = new List<SoftwareService>()
                {
                    new SoftwareService
                    {
                        SoftwareId = 1,
                        SoftwareName = "Microsoft Office",
                        Quantity = 5,
                        SoftwareLicences = new List<SoftwareLicence>
                        {
                            new SoftwareLicence { LicenceId = 1, Licence = "Office Licence 1", ValidToDate = new DateTime(2023, 8, 31) },
                            new SoftwareLicence { LicenceId = 2, Licence = "Office Licence 2", ValidToDate = new DateTime(2023, 8, 31) },
                        }
                    },
                    new SoftwareService
                    {
                        SoftwareId = 2,
                        SoftwareName = "Adobe Photoshop",
                        Quantity = 3,
                        SoftwareLicences = new List<SoftwareLicence>
                        {
                            new SoftwareLicence { LicenceId = 3, Licence = "Photoshop Licence 1", ValidToDate = new DateTime(2023, 7, 31) },
                            new SoftwareLicence { LicenceId = 4, Licence = "Photoshop Licence 2", ValidToDate = new DateTime(2023, 7, 31) },
                        }
                    },
                    new SoftwareService
                    {
                        SoftwareId = 3,
                        SoftwareName = "Visual Studio",
                        Quantity = 10,
                        SoftwareLicences = new List<SoftwareLicence>
                        {
                            new SoftwareLicence { LicenceId = 5, Licence = "Visual Studio Licence 1", ValidToDate = new DateTime(2023, 9, 30) },
                            new SoftwareLicence { LicenceId = 6, Licence = "Visual Studio Licence 2", ValidToDate = new DateTime(2023, 9, 30) },
                        }
                    }
                };

                // Convert the response object to a JSON string
                var responseJson = JsonSerializer.Serialize(responseObject);

                // Create the HttpContent using the JSON string
                var content = new StringContent(responseJson, Encoding.UTF8, "application/json");

                // Create the HttpResponseMessage and set the content
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = content
                };

                return await Task.FromResult(response);
            }

            // Mock response for CreateOrder
            if (request.RequestUri.AbsolutePath == "/create-order" && request.Method == HttpMethod.Post)
            {
                var requestBody = await request.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var createOrderRequest = JsonSerializer.Deserialize<CreateOrderRequest>(requestBody, options);

                var random = new Random();
                var orderLicences = new List<OrderLicence>();

                foreach (var softwareService in createOrderRequest.SoftwareServices ?? Enumerable.Empty<SoftwareService>())
                {
                    for (int i = 0; i < softwareService.Quantity; i++)
                    {
                        orderLicences.Add(new OrderLicence
                        {
                            OrderItemId = softwareService.SoftwareId,
                            LicenceId = random.Next(1, 10000), // Generate a random LicenceId between 1 and 10000
                            Licence = Guid.NewGuid().ToString(),
                            ValidToDate = DateTime.Today.AddYears(1)
                        });
                    }
                }

                var orderItems = new List<OrderItem>();

                foreach (var softwareService in createOrderRequest.SoftwareServices ?? Enumerable.Empty<SoftwareService>())
                {
                    var itemLicences = orderLicences.Where(ol => ol.OrderItemId == softwareService.SoftwareId).ToList();

                    var orderItem = new OrderItem
                    {
                        Id = softwareService.SoftwareId,
                        OrderId = random.Next(1, 1000),
                        SoftwareId = softwareService.SoftwareId,
                        SoftwareName = softwareService.SoftwareName,
                        CustomerAccountId = createOrderRequest.CustomerAccountId,
                        Price = 100,
                        Currency = "USD",
                        DiscountPercentage = 10, 
                        Quantity = softwareService.Quantity,
                        Discount = 10,
                        TotalPrice = 90,
                        OrderLicences = itemLicences
                    };

                    orderItems.Add(orderItem);
                }

                var orderResponse = new Order
                {
                    Id = 1,
                    OrderNo = "ORD12345",
                    OrderDate = DateTime.Now,
                    CustomerId = createOrderRequest.CustomerAccountId,
                    StatusId = 1,
                    Description = "Mock order description",
                    OrderItems = orderItems
                };

                var responseJson = JsonSerializer.Serialize(orderResponse);
                var content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json");

                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.Created) { Content = content });
            }

            // Mock response for ExtendLicenceValidDate
            if (request.RequestUri.AbsolutePath == "/extend-licence-valid-date" && request.Method == HttpMethod.Post)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var extendLicenceValidDateRequest = JsonSerializer.Deserialize<ExtendLicenceValidDateRequest>(requestBody, options);

                var response = new Response
                {
                    Success = true,
                    ResponseMessage = "Licence validity date extended successfully."
                };

                var responseJson = JsonSerializer.Serialize(response);
                var content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json");

                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
            }

            // Mock response for CancelSubscriptionLicence
            if (request.RequestUri.AbsolutePath == "/cancel-subscription-licence" && request.Method == HttpMethod.Post)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var cancelSubscriptionLicenceRequest = JsonSerializer.Deserialize<CancelSubscriptionLicenceRequest>(requestBody, options);

                var response = new Response
                {
                    Success = true,
                    ResponseMessage = "Subscription licence cancelled successfully."
                };

                var responseJson = JsonSerializer.Serialize(response);
                var content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json");

                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
            }
            // Mock response for CancelSubscription
            if (request.RequestUri.AbsolutePath == "/cancel-subscription" && request.Method == HttpMethod.Post)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var cancelSubscriptionRequest = JsonSerializer.Deserialize<CancelSubscriptionRequest>(requestBody, options);

                var response = new Response
                {
                    Success = true,
                    ResponseMessage = "Subscription cancelled successfully."
                };

                var responseJson = JsonSerializer.Serialize(response);
                var content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json");

                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
            }

            // Mock response for AddNewSubscriptionLicence
            if (request.RequestUri.AbsolutePath == "/add-new-subscription-licence" && request.Method == HttpMethod.Post)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var addNewSubscriptionLicenceRequest = JsonSerializer.Deserialize<AddNewSubscriptionLicenceRequest>(requestBody, options);

                var random = new Random();
                var response = new List<SoftwareLicence>();

                // Mocking the addition of new licences
                for (int i = 0; i < addNewSubscriptionLicenceRequest.Quantity; i++)
                {
                    response.Add(new SoftwareLicence
                    {
                        LicenceId = random.Next(1, 10000), // Generate a random LicenceId between 1 and 10000
                        Licence = Guid.NewGuid().ToString(),
                        ValidToDate = DateTime.Today.AddYears(1)
                    });
                }

                var responseJson = JsonSerializer.Serialize(response);
                var content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json");

                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
            }

            return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}