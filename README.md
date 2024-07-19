# Sample REST API Endpoints Tests

This project is a set of automated tests for REST API endpoints, utilizing NUnit for test execution and RestSharp for making HTTP requests. The tests are designed to validate API responses against expected schemas and to ensure the functionality of various API endpoints.

## Project Structure

- **`/Utils`**: Contains utility classes and methods.
  - `ApiClient.cs`: Provides methods for sending requests and parsing responses.
  - `JsonSchemas.cs`: Provides methods to load and access JSON schemas for response validation.

- **`/Tests`**: Contains NUnit test classes.
  - `ApiEndpointTests.cs`: Contains various test cases for different API endpoints.

- **`/Configuration`**: Contains configuration settings for the project.
  - `RestAPISampleAutoTestsConfiguration.cs`: Holds base URL and other configuration settings.

- **`/Schemas`**: Contains JSON schema files for response validation.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 3.1 or later)
- [NUnit](https://nunit.org/)
- [RestSharp](https://restsharp.dev/)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/your-repo-name.git
    ```
2. Navigate to the project directory:
    ```bash
    cd your-repo-name
    ```
3. Restore the project dependencies:
    ```bash
    dotnet restore
    	```
This command will execute all the tests in the /Tests folder and report the results in the terminal.

### Configuration

- Update the `RestAPISampleAutoTestsConfiguration.cs` file with your API base URL and any other required configuration settings.

### Running Tests

To run the tests, use the following command:
```bash
dotnet test
    ```
This command will execute all the tests in the /Tests folder and report the results in the terminal.

## Test Cases

The test cases are organized into the `ApiEndpointTests` class. Each test method is annotated with `[Test]` and can be executed individually or as part of the entire test suite. Here are some examples of test methods:

- **Get List of Users (Default Page)**: Tests the API endpoint for fetching a list of users with default pagination.
- **Get Single User**: Tests the API endpoint for fetching a specific user by ID.
- **Create User**: Tests the API endpoint for creating a new user.
- **Update User**: Tests the API endpoint for updating an existing user.
- **Delete User**: Tests the API endpoint for deleting a user.

## Contributing

Contributions are welcome! Please follow the standard Git workflow:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Create a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- [NUnit](https://nunit.org/)
- [RestSharp](https://restsharp.dev/)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
- [ReqRes](https://reqres.in/) (for sample API endpoints)
