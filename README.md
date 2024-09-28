# rs2-PraisePoint

## Overview

PraisePoint is an Employee Recognition application designed to enhance workplace morale and engagement. By leveraging a microservices architecture built with ASP.NET and a dynamic front end using Angular, PraisePoint enables organizations to recognize and reward their employees.

## Features

- Real-Time Recognition: Allow employees to give and receive praise instantly.
- Customizable Rewards: Integrate reward system tailored to organizational needs.
  
## Technologies Used

- Backend: ASP.NET Core (Microservices Architecture)
- Frontend: Angular

## Architecture

PraisePoint follows a microservices architecture, allowing each component to function independently and scale efficiently. The system consists of the following services:

![image](https://github.com/user-attachments/assets/8e5a7d44-cc11-44d8-bb36-cec92a160e23)

## Build

For building the backend part, position into the `PraisePoint` directory and run the following command.

```
docker compose -f ./docker-compose.yml -f ./docker-compose.override.yml up -d --build
docker compose down
```

For building the frontend part, position into the `PraisePoint/WebApps/PraisePointSPA, and run the following commands:

```
npm install
ng serve
```
