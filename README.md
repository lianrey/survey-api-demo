# Project: SurveyAPI

- This is a demo that exposes an API to create a survey.

### Tech stack:

- C#, NET5.0, mstest
- Database: Mongodb

### Step to test locally with docker:

- `cd MyCompany.Services.SurveyAPI`
- `docker build -t survey-api-austin .`
- `docker run -p 8080:80 survey-api-austin`

Go to: http://localhost:8080/swagger/index.html

### Test the API now:

https://survey-api-demo.herokuapp.com/swagger/index.html
