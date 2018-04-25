using AutoMapper;
using Stomatology.Web.Models;

namespace Stomatology.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<CustomerViewModel, Customer>();
                cfg.CreateMap<Staff, StaffViewModel>();
                cfg.CreateMap<StaffViewModel, Staff>();
                cfg.CreateMap<Service, ServiceViewModel>();
                cfg.CreateMap<ServiceViewModel, Service>();
                cfg.CreateMap<Appointment, AppointmentViewModel>()
                    .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.LastName + " "
                        + (string.IsNullOrWhiteSpace(src.Staff.FirstName) ? src.Staff.FirstName[0] + "." : "")
                        + (string.IsNullOrWhiteSpace(src.Staff.MiddleName) ? src.Staff.MiddleName[0] + "." : "")))
                    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.LastName + " "
                        + (string.IsNullOrWhiteSpace(src.Customer.FirstName) ? src.Customer.FirstName[0] + "." : "")
                        + (string.IsNullOrWhiteSpace(src.Customer.MiddleName) ? src.Customer.MiddleName[0] + "." : "")));
                cfg.CreateMap<AppointmentViewModel, Appointment>();
            });
        }
    }
}