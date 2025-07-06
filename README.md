# DiscountServiceGrpc

## System description and requirements
The system is designed to generate and use DISCOUNT codes. The system consists of a server-side and a client-side. Communication depends on the protocol specified below.

To persist the data the method used is record in a text file, that is generates in case do not exist and can be find at the project root path.

## Protocol
The protocol used for the project is gRPC

## Solutions
The project cosnistes in tree solutions: 
* DiscountServiceClient(Console application) 
* DiscountServiceGrpc(Service) 
* DiscountServiceGrpc.Test

![DiscountService screenshot](https://github.com/VitorLucas/DiscountServiceGrpc/blob/main/Images/projects.png)

## Project starter
Was configured a startup of the project as the image
![startup screenshot](https://github.com/VitorLucas/DiscountServiceGrpc/blob/main/Images/SolutionsStarter.png)

## Postman
![postman screenshot](https://github.com/VitorLucas/DiscountServiceGrpc/blob/main/Images/images/postmanCall.png)
