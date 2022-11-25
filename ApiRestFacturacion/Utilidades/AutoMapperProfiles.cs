using ApiRestFacturacion.DTOS;
using ApiRestFacturacion.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ClienteCreacionDTO, Cliente>();
            CreateMap<Cliente, ClienteDTO>();

            CreateMap<TasaCambioCreacionDTO, TasaCambio>();
            CreateMap<TasaCambio, TasaCambioDTO>();
            CreateMap<TasaCambioPatchDTO, TasaCambio>().ReverseMap();

            CreateMap<ProductoCreacionDTO, Producto>();
            CreateMap<Producto, ProductoDTO>();
        }
    }
}
