# Do you need other service’s data in microservice architecture: Event-Driven Architecture

## Development

```bash

# To run elastic and rabbitmq
docker compose up -d

```

Then run the applications.

## Ports

| Port            | Service             | Credentials   |
|-----------------|---------------------|---------------|
| localhost:5051  | Organizations API   |               |
| localhost:5052  | Products API        |               |
| localhost:5601  | Kibana              |               |
| localhost:1433  | SQL Server          | SA / Passw0rd |
| localhost:15672 | RabbitMQ            | guest / guest |