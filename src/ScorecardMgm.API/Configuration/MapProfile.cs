using AutoMapper;


namespace ScorecardMgm.API.MapProfile;
public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Contracts.Over, Models.Over>().ReverseMap();
        CreateMap<ScorecardMgm.API.Models.Over, ScorecardMgm.Common.Entities.Over>().ReverseMap();
        CreateMap<Contracts.Match, Models.Match>().ReverseMap();
        CreateMap<ScorecardMgm.API.Models.Match, ScorecardMgm.Common.Entities.Match>().ReverseMap();
        CreateMap<Contracts.Tournament, Models.Tournament>().ReverseMap();
        CreateMap<ScorecardMgm.API.Models.Tournament, ScorecardMgm.Common.Entities.Tournament>().ReverseMap();
        CreateMap<Contracts.Team, Models.Team>().ReverseMap();
        CreateMap<ScorecardMgm.API.Models.Team, ScorecardMgm.Common.Entities.Team>().ReverseMap();
        CreateMap<Contracts.Player, Models.Player>().ReverseMap();
        CreateMap<ScorecardMgm.API.Models.Player, ScorecardMgm.Common.Entities.Player>().ReverseMap();

    }
}