sudo docker build -t s3g-image -f Dockerfile.test .

sudo docker run --rm --name s3g-container s3g-image
