# 1nn0va-NetConf2023
Example for 1nn0va-NetConf 2023 conference


# Scenario
When we have to deal with distributed systems we need to balance three forces 
> - Communication (sync vs async)
> - Persistence (strong vs eventually)
> - Workflow (orchestrator vs choreography)


# What is a Saga?
When your business workflow depends by more than one microservice, you have to manage it.
A Saga is a Long Running Process that involves all the microservices requested. There are two kind of Sagas
> - Orchestrator  
> - Choreography  
As you know, in Software Architecture, all is a Trade-Off, and which kind of Saga you choose is a trade-off.
For this example I choose 
> - Asynchronous communication
> - Eventually Consistency
> - Orchestrator workflow

# The solution
In this solution you can find:  
> - `BrewApp` example of Blazor Modular Architecture with lazy loading in FrontEnd  
A series of microservices: 
> - `BrewUpSagas` to manage the Saga  
> - `BrewUpSales` delegate to receive the orders from pubs  
> - `BrewUpWarehouses` delegate to prepare the beers  
> - `BrewUpLogistics` delegate to send the beers to the pubs
You need .NET 8 installed to run the examples.

# CQRS
Is a pattern used to split the process of writing from the process of reading the data.
It was introduced by Greg Young, and, as every pattern, is not a "one size fit all" solution, so please, handle with carefully!!!

# EventStore
It's a State-Transition Databases that stores not just your data, but the different transitions your data goes through over time. State transitions are events that record not just what’s changed, but why it changed in the order those changes occurred. This creates a permanent, unchangeable log of all the transitions the data has gone through.

# Muflone
It's an open-source project to help you implementing Domain-Driven Design with Event-Driven approach.  
[You can find more details here](https://github.com/cqrs-muflone)  
[You can find more examples here](https://github.com/brewup)  


# `Microservices` are not `Distributed Monolithic`
Each microservice in this solution has its own ReadModel (MongoDb) and its own EventStore.

# Run Solution
> - Prepare Infrastructure: docker-compose up -d (inside docker folder)  
> - BrewUpSagas:
> > - docker build -t brewupsagas .
> > - docker run --rm -p 5000:8080 --name brewupsagas brewupsagas
> - BrewUpSales:
> > - docker build -t brewupsales .
> > - docker run --rm -p 5100:8080 --name brewupsales brewupsales
> - BrewUpWarehouses:
> > - docker build -t brewupwarehouses .
> > - docker run --rm -p 5200:8080 --name brewupwarehouses brewupwarehouses
> - BrewUpLogistics:
> > - docker build -t brewuplogistics .
> > - docker run --rm -p 5300:8080 --name brewuplogistics brewuplogistics

You can also use the docker-compose file for each microservice, instead of docker run
