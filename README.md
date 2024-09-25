# Order-Microservices-Project
Test microservices task
## Getting Started

### Docker Compose build
* docker-compose
```sh
docker-compose up
```

### Configuring broker connection and broker exchange name

* Order Service ENV configuration

```yaml
 environment:
      - ORDER_S_RABBITMQ_HOSTNAME="Your RabbitService Hostname"
      - ORDER_S_RABBITMQ_ORDER_ACCEPTED_EXCHANGE="Your Exchange Name"
```
* Notification Service ENV configuration

```yaml
environment:
     - NOTIF_S_CONSUMER_HOSTNAME="Your RabbitService Hostname"
     - NOTIF_S_CONSUMER_EXCHANGE="Your Exchange Name"
```

