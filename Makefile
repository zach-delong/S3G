build:
	podman build -t docker.io/zachdelong/s3g --pull=true ./

push:
	podman push docker.io/zachdelong/s3g

