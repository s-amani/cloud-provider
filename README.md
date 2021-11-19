# Cloud Infrastructor Provider

## Introduction to Problem

* The purpose of this project is to create a library for a company to create and maintain their cloud
infrastructure efficiently without needing to have deep knowledge about different cloud providers. This
library will introduce apis and abstractions that developers can use to design and implement cloud
agnostic infrastructure. The client wants to start the first phase by supporting only virtual machines and
database servers. But they would want more resources (i.e. load balancers, elastic file storage etc) at later
stages. They want to be able to create both Windows and Linux instances. For database servers they want
support for both MySQL and SQL Server. The client wants to be able to create multiple infrastructures as
well, for example they want to create a UAT infrastructure for one project and a Test infrastructure for
their internal team.
The project is about creating a class library that provides interfaces for creating infrastructure resources

## A web api sample application who covers following area:


* Analytical thinking
* Object Oriented design, naming skills, and domain-driven thinking, avoiding code duplication  
* Approach to problem-solving
* Attention to details and professionalism
* Write minimal and clean code, best practices and separation of concerns

## Simple library which introduces apis and abstractions that developers can use to design and implement cloud agnostic infrastructure.

* The output of creating an infrastructure should be a sub-directory in the provider directory with the same name as the infrastructure with a sub-directory for each resource type. For each resource a file containing the attributes of that resource should be created in the [infrastructure/resource type] directory. example:

§  IGS/
      §  UAT/
            §  VirtualMachine/
                              §  UAT_SERVER.json
                                  §  content: { property : value } 

### Important:
* The delete api should accept the name of an infrastructure and delete all the associated resources.
* The implementation of deleting a resource is to delete the resource file on disk. 
* Important: Do not delete the infrastructure folder. Dependency hierarchy should be followed, i.e. a virtual machine first should be deleted before deleting its hard disks.
