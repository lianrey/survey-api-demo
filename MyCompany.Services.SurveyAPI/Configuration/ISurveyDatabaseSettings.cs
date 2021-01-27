namespace MyCompany.Services.SurveyAPI.Configuration
{
    public interface ISurveyDatabaseSettings
    {
        string SurveysCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}