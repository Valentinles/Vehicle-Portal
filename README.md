# Vehicle-Portal
**Vehicle Portal** is ASP.NET CORE web application where *users* can *buy/rent/rate* cars, read a specific individual information for every car and transfer virtual money in their accounts. There is an *admin* account which allows you to *add/edit/delete* cars and take a look of all users' deals. *Guests* can see the top rated cars and register to use more features.
## Getting Started

###### To run the application you need:
- .NET Core 2.2 

- If you don't have *Sql server* on your machine you should replace the configuration in *VehiclePortal.Web/appsettings.json* with this code
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=VehiclePortal;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
- In your *package manger console* type 

```
update-database
```
- **Admin account**: </br>
 *username*: admin <br>  *password*: 123456

## Used technologies
- C#
- .NET Core 2.2
- .NET Core MVC
- Entity Framework Core
- Bootstrap 4.0
- HTML
- CSS
