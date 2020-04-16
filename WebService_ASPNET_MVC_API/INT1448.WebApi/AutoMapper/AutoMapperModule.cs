using Autofac;
using AutoMapper;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INT1448.WebApi.AutoMapper
{
    public class AppProfile : Profile
    {
        [ObsoleteAttribute]
        protected override void Configure()
        {
            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorDTO, Author>();
            CreateMap<BookCategory, BookCategoryDTO>();
            CreateMap<BookCategoryDTO, BookCategory>();
            CreateMap<Publisher, PublisherDTO>();
            CreateMap<PublisherDTO, Publisher>();
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
            CreateMap<BookAuthor, BookAuthorDTO>();
            CreateMap<BookAuthorDTO, BookAuthor>();
            CreateMap<BookImage, BookImageDTO>();
            CreateMap<BookImageDTO, BookImage>();
        }
    }

    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register all profile classes in the calling assembly
            builder.RegisterAssemblyTypes(typeof(AutoMapperModule).Assembly).As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}