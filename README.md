# Rocket_Elevators_API

This repo serves as the Rocket Elevators REST API developed for CodeBoxx's week 9 of the Odyssey program. We were tasked with developing a REST API to interact with the MYSQL database that already exists, and provide the appropriate requests for queries.

The queries for the REST API are found in a public PostMan workspace at: https://app.getpostman.com/join-team?invite_code=fa798e97463863bf0ae7706c4d727181

The REST API URL is: https://rocket-api-burst.azurewebsites.net/interventions


Each request works as follows:

1. GET Interventions - Returns the information for a specific intervention, and different interventions can be returned by changing the number at the end of the API request.

2. PUT Interventions - Returns the information for interventions that are either in progress or completed
