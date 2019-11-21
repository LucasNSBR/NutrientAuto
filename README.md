# What is NutrientAuto?
A simple community/social network focused on health, diet and training. It was originally aimed to be a nutritionists sotware but i gave up on it. Then i turned it into a "self-service, auto" user community.

## Prerequisites
You need to install RabbitMQ and ELK stack in order to properly run this solution.
 
## Core Technologies
* ASP.NET Core
* Entity Framework Core
* ASP.NET Identity Core
* SQL Server
* RabbitMQ
* MassTransit
* LilValidation
* SendGrid
* Swagger
* SonarQube
* Dapper
* ElasticSearch
* Logstash
* Kibana
* MSTest

## Architecture Overview
Every Bounded Context is 100% autonomous and the interactions of them are handled by a event bus. 

#### Community Context
Base of the community, contains profiles, diets, meals, meal foods, etc and etc. 
 
#### Shared Kernel
Contains all building blocks like events, notifications, base entities and base value object interfaces.
 
#### CrossCutting
Application infrastructure services. Dependency Injection resolving, Email Services, Service Bus configurations and Unit Of Work definitons. Basically this layer wrapps all infrastructure dirty dependencies. Also responsible for resolve the User Identity by using HttpContext.
 
#### Identity Context
Wrapps the Identity logic. Users, roles, logins, claims, etc. It uses ASP.NET Identity Core. 
 
#### Web Apps
Web API entry point and front-end.
 
