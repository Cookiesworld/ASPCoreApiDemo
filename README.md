# ASPCoreApiDemo

Demo Project to show .net app using DI

* Swagger to document controller actions
* Health check to show microservice health /health
* Continuous integration with github actions
* Containerisation using docker

## Installation

* Requires dotnet core 8.x
* Rquires docker desktop to build and run docker image

Clone the repository

```
git clone https://github.com/Cookiesworld/ASPCoreApiDemo
cd ASPCoreApiDemo
dotnet restore
dotnet build
```

## Test

```
dotnet test
```

## Docker 

To build the image run
```
docker build -t aspnetapidemo
```

To run the image
```
docker run --rm -it -p 5001:8080 aspnetapidemo
```

## Useage 

Run the solution navigate to the [swagger page](http://localhost:5000) or in [docker](http://localhost:5001)

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.