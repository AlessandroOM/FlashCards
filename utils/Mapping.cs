using AutoMapper;
using flashcards.DTOs;
using flashcards.Models;

namespace FlashCard;

public static class Mapping
{
    public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {             
                cfg.CreateMap<Stacks, StackDTO>()
                .ReverseMap()
                .ForMember(dest => dest.FlashCards, act => act.MapFrom(src => src.FlashCards))
                .ForMember(dest => dest.Studies, act => act.MapFrom(src => src.Studies))
                ;

                cfg.CreateMap<FlashCardDb, FlashCardDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Studies, act => act.MapFrom(src => src.Studies));

                cfg.CreateMap<Study, StudyDTO>()
                .ReverseMap();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    
}
