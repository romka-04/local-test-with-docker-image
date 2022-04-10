# About The Project
This sample demonstrates one of approach that can be used in order to perform efficient integration tests of infrastructure components on local machine.
The common issue with such tests is an obligation of manually create resources if there are no proper development environment. 
One of the solutions presented here is to roll out required external resources as a docker image.

# Steps to run unit tests

In order to use this approach Docker should be installed.

1. Open Terminal (Menu -> View -> Terminal) and run command.

``docker-compose up``

This would automatically download docker image and run it.

2. Run unit tests as usual.

# Notes

Please take a look at the file *docker-compose.yml*. 
This approach could be used with *Radis*, *RabbitMQ* and maybe some other apps. 
Keep database in docker image is a questinable but some folks do this as well. 