# TinyUrl
Small version of a TinyUrl Service via CLI in C#

This console application demonstrates a Proof-of-Concept (POC) for a Tiny URL service. It allows users to create short URLs from long URLs, retrieve long URLs from short URLs, delete short URLs, and get statistics on the usage of short URLs.

## Features

- **Create Short URL**: Generates a short URL from a provided long URL. Optionally supports custom short URLs.
  
- **Delete Short URL**: Deletes a previously created short URL.
  
- **Get Long URL**: Retrieves the original long URL from a short URL.
  
- **Get Statistics**: Retrieves the number of times a short URL has been accessed (clicked).

## Requirements

- .NET 6.0 or later

## Installation

1. Clone the repository
2. Build the project: dotnet build
3. Run the application: dotnet run

## Usage

Upon running the application, follow the on-screen instructions to interact with the Tiny URL service. You can choose between manual mode, where you provide input via the console, or automated mode, where predefined test cases are executed.

## Automated Testing

The application includes automated testing capabilities to verify functionality. Modify the `AutomatedTestCases.cs` file to add or adjust test scenarios as needed. To run automated tests, select "2. Automated" mode when prompted.

## Logging

The application uses the built-in logging feature of .NET Core. Logs are displayed in the console output and can be extended to include file logging or other providers as needed. Logging helps track errors and important events during application runtime.

## Contributing

Contributions are welcome! If you have suggestions for improvements, please fork the repository and create a pull request. For major changes, please open an issue first to discuss what you would like to change.

