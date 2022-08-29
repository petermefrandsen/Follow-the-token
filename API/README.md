# API

A C# API that provides a way for exploring the transactions made by a specific blockchain address.

## Quickstart - local :wrench:

### Add cors

In program.cs add or change the cors settings.

``` csharp
 builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
```

### Build & Run

```go
docker build ./API -t follow-the-token-api-image
```

```go
docker run -d -p 5000:80 --name follow-the-token-api follow-the-token-api-image -e ENVIRONMENT='development' -e BSC_SCAN_API_KEY='##Your-BscScan-Api-Key##'
```

If environment variable is omitted then it defaults to production.

### Endpoint

View the swagger documentation at: http://localhost:5000/swagger/index.html

## Limitations :warning:

The API speed and transaction requests are limited by the API-key plan that is chosen at [BscScan][bsc-scan-api].

## License :page_facing_up:

The project is under [MIT license][file-license].

[bsc-scan-api]: https://bscscan.com/apis
[file-license]: https://www.apache.org/licenses/LICENSE-2.0