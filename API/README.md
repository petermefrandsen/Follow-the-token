# API

## Quickstart

Add secret(s)

Example for local appsettings.Local.json:

``` json
{
    "BscScanApiKey": "Your-BscScan-Api-Key"
}
```

## Run

docker build ./API -t follow-the-token-api-image

docker run -d -p 8080:80 --name follow-the-token-api follow-the-token-api-image --environment="development"

Where the environment variable is optional

View the swagger documentation at: http://localhost:8080/swagger/index.html