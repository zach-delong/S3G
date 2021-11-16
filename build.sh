# If the current user is in the docker group, we don't need to use sudo.
# If they are not, then we must run docker as root =()
if groups $USER | grep "docker" ; then
    docker build -t s3g-image -f Dockerfile .
else
    sudo docker build -t s3g-image -f Dockerfile .
fi
