# Build Your Head

## Local Development
    Run docker desktop
    Raise db: ~/build-your-head/ docker-compose up db
    Run API: open BuildYourHead.sln with VS and run
    Run front: ~/build-your-head/build-your-head-ui/ yarn dev

## Useful Commands

### EF:
    ~/buid-your-head/BuildYourHead.Persistence/ dotnet-ef --startup-project ../BuildYourHead.Api/ migrations add InitialCreate


## TODO:
    - [back] add logging
    - ADD BACKEND UNIT TESTS !!!!
    - [back] Implement users storage
    - [front] fix too long blob (compress images on upload)
    - [front] set up formatter (prettier)
    - [db] create script to generate default data
