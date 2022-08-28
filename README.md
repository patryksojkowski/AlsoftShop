# AlsoftShop
Solution consists of three projects:
- `AlsoftShop` - ASP NET CORE application targetting `netcoreapp3.1` with simple view and logic behind splitted into smaller classes
- `AlsoftShop.Test` - unit tests for above logic
- `AlsoftShopDatabase` - db project that can be deployed to server and seeded with `Seed.sql`

Remarks:
- connection string to database is hardcoded inside `DbConnectionFactory` class, and it points to localhost server, db = "AlsoftShopDatabase"
- I added bunch of todos, but I got short on time to complete them :)
