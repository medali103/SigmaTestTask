Job Candidate Hub API - Development Steps

Project Setup:

Created a new ASP.NET Core Web API project using Visual Studio 2022.
Configured the project to use .NET 6.0 as the target framework.
Added necessary NuGet packages (e.g., Entity Framework Core, AutoMapper).


Database Setup:

Created a SQLite.
Configured the connection string in the appsettings.json file.


Data Model:

Created a Candidate model class to represent the candidate information.
Defined properties based on the required fields and optional fields specified in the requirements.


Database Context:

Created a CandidateDbContext class inheriting from DbContext.
Configured the Candidate model as a DbSet.
Implemented the code-first approach for database migration and creation.


Repository Pattern:

Created an ICandidateRepository interface to define the contract for the repository.
Implemented the CandidateRepository class that inherits from the ICandidateRepository interface.
Implemented methods to create, update, and retrieve candidates from the database.


Service Layer:

Created an ICandidateService interface to define the contract for the service layer.
Implemented the CandidateService class that inherits from the ICandidateService interface.
Implemented the logic to handle candidate creation and updates, utilizing the repository layer.


API Controller:

Created a CandidatesController class inheriting from ControllerBase.
Implemented the POST action method to handle the creation and update of candidate information.
Utilized the service layer to perform the necessary operations.


Unit Testing:

Created a separate test project for unit testing.
Implemented unit tests for the service layer and repository layer using a mocking framework (e.g., Moq).
Ensured reasonable code coverage for the core functionality and edge cases.


Source Control:

Initialized a Git repository for the project.
Committed changes regularly with meaningful commit messages.
Created a GitHub repository and pushed the code to the remote repository.
