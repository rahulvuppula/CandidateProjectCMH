# CandidateProjectCMH

# Interview Scheduler API

## API Requirements

- Developed using .NET (any version).
- No authorization required.
- Endpoint: POST /api/CheckInterviews
  - Accepts application/json content.
  - Request format:
    ```json
    {
        "dateOfInterview": "<Date>"
    }
    ```
- Endpoint: GET https://cmricandidates.azurewebsites.net/api/getcandidates
  - Response format:
    ```json
    [
        {
            "name": "Mickey Mouse", 
            "dateOfInterview": "2024-03-01T00:00:00+00:00" 
        }, 
        {
            "name": "Jane Doe", 
            "dateOfInterview": "2024-03-04T00:00:00+00:00" 
        },
        ...
    ]
    ```
  - Note: Dates are randomly generated within the range from today to 14 days from today.
- Return a SUCCESS code (with response data) on success.
  - Response data: Total number of scheduled interviews on the given date.
  - Response format:
    ```json
    {
        "numberOfInterviews": <number>
    }
    ```
- Return an appropriate ERROR code if an issue is encountered.

## Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/rahulvuppula/CandidateProjectCMH
   Navigate to the project directory:
   cd your-repo
   Open the project in your preferred IDE.
   Build and run the project.

## Usage

- Make a POST request to /api/CheckInterviews with a JSON payload containing the dateOfInterview.
- Make a GET request to https://cmricandidates.azurewebsites.net/api/getcandidates to retrieve candidate interview data.
## Dependencies
.NET SDK
Newtonsoft.Json (for JSON serialization/deserialization)
## Contributors
- Rahul Vuppula
