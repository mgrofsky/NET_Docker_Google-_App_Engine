## .NET Web App in Google App Engine Flexible w/ Docker Support

#### K8s only Instruction at the bottom

Microsoft has [changed](https://twitter.com/Code_Munkee/status/1172665448904548352?s=20 "changed").

### app.yaml

This is is required to deploy to GAE Flexible. Runtime will be `custom` and env will be `flex`.

You can either add your service into the `app.yaml` or specify it in the `gcloud app deploy` command.

```
runtime: custom
env: flex

# This sample incurs costs to run on the App Engine flexible environment. 
# The settings below are to reduce costs during testing and are not appropriate
# for production use. For more information, see:
# https://cloud.google.com/appengine/docs/flexible/python/configuring-your-app-with-app-yaml
manual_scaling:
  instances: 1
resources:
  cpu: 1
  memory_gb: 0.5
  disk_size_gb: 10

service: matt-test
```

**Remember that the flex environment does not scale down to 0 and could become costly if you supply the wrong resources and forget about it.**

### Dockerfile

One key item here is that when creating and deploying a custom runtime, the App Engine front end will route incoming requests to the appropriate module on port 8080. 

You must be sure that your application code is listening on 8080.

The default `EXPOSE` when adding in Docker support to a .NET web application is:

```
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
```

You will want to change this to:

```
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
```


### Using in K8s

Keep the default `EXPOSE` when adding in Docker support to a .NET web application:

```
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
```

**Build**

`docker build -f "NET_Docker_Google-_App_Engine/Dockerfile" -t net-sample-image .`

**Tag**

`docker tag net-sample-image gcr.io/<PORJECT NAME>/net-sample-image:v1`

**Push to Container Registry**

`docker push gcr.io/<PORJECT NAME>/net-sample-image:v1`

**Workloads**

Deploy your containerized application, expose the service and then set scaling and max surge as needed.
