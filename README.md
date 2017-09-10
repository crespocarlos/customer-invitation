# customer-invitation

We have some customer records in a text file (customers.json) -- one customer per line, JSON-encoded. We want to invite any customer within 100km of our Dublin office for some food and drinks on us. Write a program that will read the full list of customers and output the names and user ids of matching customers (within 100km), sorted by User ID (ascending).

### Development

clone this repo

```
cd CustomerInvitation

dotnet restore

cd CustomerInvitation.Test

dotnet test

```

### Usage

```
cd CustomerInvitation

dotnet run

```