# TrueLayer Challenge for DJ

This project is Software Engineering challenge from TrueLayer

## Docker

If you are new to Docker, then please install it using default settings
[Docker Download](https://www.docker.com/products/docker-desktop)

## Build

Navigate to the soruce root folder TrueLayerChallenge
Start command promt and run command:
`docker build -t true-layer-tag .`
**true-layer-tag** - is the container image name that will apear in the docker console.

## Running application

Start command promt and run command:
`docker run --publish 8091:80 true-layer-tag`
The port **8091** will be exposed and accessble so you can access app via 
http://localhost:8091
