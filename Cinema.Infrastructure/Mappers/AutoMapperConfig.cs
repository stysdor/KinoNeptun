using AutoMapper;
using Cinema.Core.Domain;
using Cinema.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Mappers
{
    /// <summary>
    /// Mapper maps domain objects from Cinema.Core to DTO objects from Cinema.Infrastructure and revers.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Initializes mapping.
        /// </summary>
        /// <returns></returns>
        public static IMapper Initialize() => new MapperConfiguration(cfg =>
        {
            
            cfg.CreateMap<Movie, MovieDto>().ForMember(x=> x.CategoryName, m=> m.MapFrom(p=> p.CategoryId.CategoryName))
                                            .ReverseMap();
            cfg.CreateMap<MovieDto, Movie>().ForPath(x => x.CategoryId.CategoryName, m => m.MapFrom(p => p.CategoryName));

            cfg.CreateMap<ReservationDto, Reservation>().ForPath(x => x.ShowingId.Id, m => m.MapFrom(p => p.ShowingId))
                                                        .ForPath(x => x.RowSeatId.Id, m => m.MapFrom(p => p.RowSeatId))
                                                        .ForPath(x => x.CustomerId.Id, m => m.MapFrom(p => p.CustomerId));

            cfg.CreateMap<Reservation, ReservationDto>().ForPath(x => x.ShowingId, m => m.MapFrom(p => p.ShowingId.Id))
                                                        .ForPath(x => x.RowSeatId, m => m.MapFrom(p => p.RowSeatId.Id))
                                                        .ForPath(x => x.CustomerId, m => m.MapFrom(p => p.CustomerId.Id));

            cfg.CreateMap<ShowingDto, Showing>().ForPath(x => x.MovieId.Id, m => m.MapFrom(p => p.MovieId))
                                                 .ForPath(x => x.MovieId.MovieTitle, m => m.MapFrom(p => p.MovieTitle))
                                                 .ForPath(x => x.TheatreId.Id, m => m.MapFrom(p => p.TheatreId));

            cfg.CreateMap<Showing, ShowingDto>().ForPath(x => x.MovieId, m => m.MapFrom(p => p.MovieId.Id))
                                                .ForPath(x => x.MovieTitle, m => m.MapFrom(p => p.MovieId.MovieTitle))
                                                .ForPath(x => x.TheatreId, m => m.MapFrom(p => p.TheatreId.Id));

            cfg.CreateMap<RowSeat, RowSeatDto>().ForPath(m => m.SeatStatus, x => x.Ignore());

            cfg.CreateMap<Customer, CustomerDto>();
            cfg.CreateMap<CustomerDto, Customer>();
            cfg.CreateMap<User, UserDto>().ReverseMap();

        }).CreateMapper();
    }
}


