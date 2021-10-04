# Cloud Infrastructor Provider

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
