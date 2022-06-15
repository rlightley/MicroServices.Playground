# MicroServices.Playground
Getting started
Use the following command to run an instance of RabbitMQ

docker run --hostname my-rabbit --name some-rabbit -p 80:15672 -p 5672:5672 rabbitmq:3-management-alpine

run all three projects

Service one creates/updates/deletes users then sends relevent integration messages which are picked up by service two which contains endpoints to get people from its DB. Service three is a 'dumb' client which uses refit to call service 1 or service 2 depending on the request

