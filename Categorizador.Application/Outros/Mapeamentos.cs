using AutoMapper;
using Categorizador.Application.Dtos;
using Categorizador.Domain;

namespace Categorizador.Application.Mappgings
{
    public class Mapeamentos : Profile
    {
        public Mapeamentos ()
        {
            //mapeando da classe de domínio para objeto e vice-versa           
            CreateMap<Grupo, GrupoDto>().ReverseMap(); 
            CreateMap<Grupo, GrupoInsertDto>().ReverseMap(); 
            CreateMap<Grupo, GrupoUpdateDto>().ReverseMap(); 


            CreateMap<Fila, FilaDto>().ReverseMap(); 
            CreateMap<Fila, FilaInsertDto>().ReverseMap(); 
            CreateMap<Fila, FilaUpdateDto>().ReverseMap(); 


            CreateMap<Finalizacao, FinalizacaoDto>().ReverseMap(); 
            CreateMap<Finalizacao, FinalizacaoInsertDto>().ReverseMap(); 
            CreateMap<Finalizacao, FinalizacaoUpdateDto>().ReverseMap(); 


            CreateMap<SubFinalizacao, SubFinalizacaoDto>().ReverseMap(); 
            CreateMap<SubFinalizacao, SubFinalizacaoInsertDto>().ReverseMap(); 
            CreateMap<SubFinalizacao, SubFinalizacaoUpdateDto>().ReverseMap(); 
        }

    }
}