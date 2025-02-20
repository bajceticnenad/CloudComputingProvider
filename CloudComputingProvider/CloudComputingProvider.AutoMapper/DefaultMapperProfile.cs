using AutoMapper;
using CloudComputingProvider.BusinessModel.Enums;
using CloudComputingProvider.DataModel.Order;

namespace CloudComputingProvider.AutoMapper
{
    public class DefaultMapperProfile : Profile
    {
        public DefaultMapperProfile()
        {
            //Response
            CreateMap<DataModel.Response, BusinessModel.Response>()
                .ReverseMap();

            //Customer
            CreateMap<DataModel.Domain.Models.CustomerAccounts, BusinessModel.ResponseModels.CustomerAccountsResponse>()
                .ReverseMap();

            //Subscriptions
            CreateMap<DataModel.Domain.Models.SubscriptionDetails, BusinessModel.ResponseModels.SubscriptionDetailsResponse>()
                .ReverseMap();
            CreateMap<DataModel.Domain.Models.States, BusinessModel.ResponseModels.States>()
                .ReverseMap();

            CreateMap<DataModel.Domain.Models.Subscriptions, BusinessModel.ResponseModels.SubscriptionsResponse>()
                .ForMember(m => m.CustomerAccount, opt => opt.MapFrom(src => src.CustomerAccount))
                .ForMember(m => m.SubscriptionDetails, opt => opt.MapFrom(src => src.SubscriptionDetails))
                .ReverseMap();


            //Order
            CreateMap<DataModel.Order.Order, BusinessModel.ResponseModels.Order>()
                .ReverseMap();
            CreateMap<DataModel.Order.OrderItem, BusinessModel.ResponseModels.OrderItem>()
                .ReverseMap();
            CreateMap<DataModel.Order.OrderLicence, BusinessModel.ResponseModels.OrderLicence>()
                .ReverseMap();
            CreateMap<DataModel.Software.SoftwareService, BusinessModel.ResponseModels.SoftwareService>()
                .ReverseMap();
            CreateMap<DataModel.Software.SoftwareLicence, BusinessModel.ResponseModels.SoftwareLicence>()
                .ReverseMap();
            CreateMap<CreateOrderRequest, BusinessModel.Commands.CreateOrderCommand>()
                .ReverseMap();
            CreateMap<OrderItem, DataModel.Domain.Models.Subscriptions>()
                .ForMember(m => m.SoftwareId, opt => opt.MapFrom(src => src.SoftwareId))
                .ForMember(m => m.SoftwareName, opt => opt.MapFrom(src => src.SoftwareName))
                .ForMember(m => m.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(m => m.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(m => m.StateId, opt => opt.MapFrom(src => (int)State.Active))
                .ForMember(m => m.SubscriptionDetails, opt => opt.MapFrom(src => src.OrderLicences))
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(m => m.CreatedBy, opt => opt.MapFrom(src => DataModel.Enums.MockUserLog.ApiUser.ToString()))
                ;
            CreateMap<OrderLicence, DataModel.Domain.Models.SubscriptionDetails>()
                .ForMember(m => m.LicenceId, opt => opt.MapFrom(src => src.LicenceId))
                .ForMember(m => m.Licence, opt => opt.MapFrom(src => src.Licence))
                .ForMember(m => m.ValidToDate, opt => opt.MapFrom(src => src.ValidToDate))
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(m => m.CreatedBy, opt => opt.MapFrom(src => DataModel.Enums.MockUserLog.ApiUser.ToString()))
                ;

            CreateMap<BusinessModel.Commands.ChangeLicenceValidDateCommand, DataModel.Software.ExtendLicenceValidDateRequest>();

            CreateMap<DataModel.Software.CancelSubscriptionLicenceRequest, DataModel.Domain.Models.Subscriptions>()
                .ForMember(m => m.SoftwareId, opt => opt.MapFrom(src => src.SoftwareId))
                .ForMember(m => m.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(m => m.SubscriptionDetails, opt => opt.MapFrom(src => src.SoftwareLicences))
                ;

            CreateMap<DataModel.Domain.Models.SubscriptionDetails, BusinessModel.ResponseModels.SoftwareLicence>()
                .ForMember(m => m.LicenceId, opt => opt.MapFrom(src => src.LicenceId))
                .ForMember(m => m.Licence, opt => opt.MapFrom(src => src.Licence))
                .ForMember(m => m.ValidToDate, opt => opt.MapFrom(src => src.ValidToDate))
                ;

            CreateMap<DataModel.Software.SoftwareLicence, DataModel.Domain.Models.SubscriptionDetails>()
                .ForMember(m => m.LicenceId, opt => opt.MapFrom(src => src.LicenceId))
                .ForMember(m => m.Licence, opt => opt.MapFrom(src => src.Licence))
                .ForMember(m => m.ValidToDate, opt => opt.MapFrom(src => src.ValidToDate))
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(m => m.CreatedBy, opt => opt.MapFrom(src => DataModel.Enums.MockUserLog.ApiUser.ToString()))
                .ReverseMap();
        }
    }
}
