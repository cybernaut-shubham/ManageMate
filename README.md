#ManageMate - A collection of inventory API

A simple CRUD API built with .NET 8 & EF Core (Code-First Approach) to manage products in an inventory.

🚀 Quick Setup

1️⃣ Clone the Repository

git clone https://github.com/cybernaut-shubham/ManageMate.git    

2️⃣ Configure Database

Update appsettings.json:

"ConnectionStrings": {  
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ManageMateDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"  
}  

3️⃣ Install Dependencies

dotnet restore

4️⃣ Apply Migrations & Update Database

dotnet ef migrations add InitialCreate  
dotnet ef database update  

5️⃣ Build and Run the Application
dotnet build
dotnet run  

⚡ Common Issues & Fixes 

1️⃣ dotnet ef command not found

Install EF Core CLI:

dotnet tool install --global dotnet-ef  

2️⃣ Migration Issues

Delete the Migrations folder and re-run:

dotnet ef migrations add InitialCreate  
dotnet ef database update  
