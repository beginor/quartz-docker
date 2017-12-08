msbuild
docker build --rm -t quartz-test:latest .
docker run --rm --name quartz-test quartz-test:latest
docker rmi quartz-test:latest
