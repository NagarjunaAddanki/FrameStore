using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FrameStore.Data;
using FrameStore.Models;

namespace FrameStore.Profiles
{
    public class FrameProfile : Profile
    {
        public FrameProfile()
        {
            CreateMap<FrameMaterial, Material>()
                .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Material.Name));

            CreateMap<FrameTracingRadii, double>().ConvertUsing(source => source.Radius);

            CreateMap<Frame, FrameViewModel>()
                .ForMember(dest => dest.FrameId, opt => opt.MapFrom(src => src.FrameId))
                .ForMember(dest => dest.FrameColor, opt => opt.MapFrom(src => src.FrameColor))
                .ForMember(dest => dest.Bridge, opt => opt.MapFrom(src => src.Bridge))
                .ForMember(dest => dest.Horizontal, opt => opt.MapFrom(src => src.Horizontal))
                .ForMember(dest => dest.Vertical, opt => opt.MapFrom(src => src.Vertical))
                .ForMember(dest => dest.SKU, opt => opt.MapFrom(src => src.SKU))
                .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.Style.Name))
                .ForMember(dest => dest.Collection, opt => opt.MapFrom(src => src.Style.Collection.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Style.Collection.Brand.Name))
                .ForMember(dest => dest.Materials, opt => opt.MapFrom(src => src.Materials))
                .ForMember(dest => dest.TracingRadii, opt => opt.MapFrom(src => src.TracingRadii));
        }
    }
}