namespace MyCompany.Services.SurveyAPI.Configuration
{
     public class SurveyDatabaseSettings: ISurveyDatabaseSettings
    {
        public string SurveysCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}