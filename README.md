# Swachify_Cleaning_Service_API

# install EF CLI (if not already)
dotnet tool install --global dotnet-ef

# project path of Infrastructure

# add EF Core design + Npgsql provider
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
# useful (optional) packages: 
dotnet add package Swashbuckle.AspNetCore       # Swagger UI
dotnet add package Dapper                       # lightweight high-perf reads (optional)
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis  # if using Redis cache


dotnet ef dbcontext scaffold "Host=127.0.0.1;Port=5432;Database=swachify_dev;Username=bhaskarchinta
" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir Models --context-dir Data --context MyDbContext \
  --schema public \
  --use-database-names \
  --force
