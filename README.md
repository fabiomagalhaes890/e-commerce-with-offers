# Klir Tech Challenge

Frontend was generated with [Angular CLI](https://github.com/angular/angular-cli) version 15.2.4.
Backend was generated using .NET 7 and SQL Server 2022 and docker

## Front end Run

At path `./angular/` run `npm start` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Fake Products and Users Server

At path `./angular/` run `json-server --watch fake-server/db.json` for a fake-db server to list products.

## Back end Run

At path `./` run `docker-compose up --build` for a backend server to create, update, list and delete promotions and to calculate discounts.