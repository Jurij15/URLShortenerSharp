# URLShortenerSharp

A simple URL shortener built with .NET. URLShortenerSharp allows you to easily shorten long URLs. Designed to be lightweight, fast, and extensible, it's suitable for both small personal projects and (possibly) production use.

## Features

- **Shorten URLs:** Convert long URLs into short, easy-to-share links.
- **Redirection:** Automatically redirect users from short URLs to their original destinations.

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2026](https://visualstudio.microsoft.com/downloads/) (optional)

### Running the Application

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/Jurij15/URLShortenerSharp.git
   cd URLShortenerSharp
   ```

2. **Build the Project:**
   ```bash
   dotnet build
   ```

3. **Run the Project:**
   ```bash
   dotnet run
   ```
### OR
Clone in Visual Studio and Build

4. **Visit the Application:**
   By default, the server will be running at [https://localhost:5000](https://localhost:5000) or as configured.

## API Usage

Below is a sample API usage for shortening URLs:

| Method | Endpoint        | Description            |
|--------|----------------|------------------------|
| POST   | /api/shorten   | Create a short URL     |
| GET    | /{alias}       | Redirect to long URL   |

### Example: Shorten a URL

```http
POST /api/shorten
Content-Type: application/json

{
  "url": "https://www.example.com"
}
```
**Response:**
```json
{
  "shortUrl": "http://localhost:5000/abcd123"
}
```

## Technologies Used

- C#
- ASP.NET Core
- Entity Framework Core
- SQLite for saving data (can be swapped with something else if neccesary)

## License

MIT License. See [LICENSE](LICENSE) file for details.

## Author

Developed by [Jurij15](https://github.com/Jurij15).
