## Introduction

This is an online platform for auctioning used cars. Sellers can list vehicle information, and buyers can place bids. The project was inspired by TryCatchLearn's course. This is my hands-on app for learning microservices.

## Architecture
![Carties2](https://github.com/user-attachments/assets/0e56db04-26e7-449a-814b-bad7cef70ff5)


## Main Tech Stack

1. Next.js 13
2. ASP.NET Core 7.0
3. Docker
4. MongoDB
5. PostgreSQL
6. MassTransit
7. RabbitMQ
8. SignalR
9. NextAuth
10. Nginx

## How to run the app locally

1. Clone the repo

```shell
git clone https://github.com/Foolish-Wang/Carsties.git
```

2. Navigate to the Project Directory

```shell
cd Carsties
```

3. Build the services
   You should make sure that you have installed docker on your computer before excuting this step.

```shell
docker compose build
```

4. Run the services

```shell
docker compose up -d
```

5. Provide the app with an SSL certificate.

To do this please install 'mkcert' onto your computer which you can get from [here](https://github.com/FiloSottile/mkcert). Once you have this you will need to install the local Certificate Authority by using:

```
mkcert -install
```

6. Create the certificate and key file on your computer

```shell
cd devcerts

mkcert -key-file carsties.local.key -cert-file carsties.local.crt app.carsties.local api.carsties.local id.carsties.local
```

7. Create an entry in your host file so you can reach the app by its domain name.

Please use this [guide](https://www.hostinger.com/tutorials/how-to-edit-hosts-file) if you do not know how to do this. Create the following entry:

```shell
127.0.0.1 id.carsties.local app.carsties.local api.carsties.local
```

8. You should now be able to browse to the app on https://app.carsties.local
