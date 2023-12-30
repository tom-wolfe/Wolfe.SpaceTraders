# SpaceTraders

This project is a work in progress for automating the [SpaceTraders](https://spacetraders.io/) game. The goal is to create a bot that can play the game for you.

## Getting Started

This project uses MongoDB as its data store. There is a Docker Compose file that will start a MongoDB instance for you. To start the database, run the following command from the root of the project:

```bash
docker compose --file build/docker-compose.yml up --detach
```

If you make a mistake, you can tear down the all the containers with the following command:

```bash
docker compose --project-name space-traders down --volumes
```

## MongoDB

By default, MongoDB runs on port 27017. There's a Mongo Express Web UI that runs on port 8081. You can access it at [http://localhost:8081](http://localhost:8081).

You can also connect to the database using Mongo Compass as a UI.

```bash
