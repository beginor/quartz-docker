FROM beginor/mono:5.4.1.6

COPY bin /app

WORKDIR /app

ENTRYPOINT ["mono", "QuartzDocker.exe"]
