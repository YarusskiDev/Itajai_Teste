using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste_Sc.Models;
using Teste_Sc.ViewModels;

namespace Teste_Sc.AutoMapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Distritos, DistritosViewModel>().ReverseMap();
            CreateMap<Distritos, EstadosViewModel>().ReverseMap();
            CreateMap<Distritos, CidadesViewModel>().ReverseMap();
            

        }
    }
}
