using AutoMapper;

namespace MyCompany.Services.SurveyAPI.Profiles
{
    public class SurveyProfile : Profile
    {
        public SurveyProfile()
        {
            CreateMap<Models.SurveyInput, Models.Survey>();
        }
    }
}
