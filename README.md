# Personal Finance API

ASP.NET Core Web API for tracking personal income/expenses with categories and summary totals.

## Tech stack
- .NET 10
- ASP.NET Core
- Entity Framework Core
- SQLite
- Swagger (Swashbuckle)

## Current implemented features
- Category CRUD:
  - `POST /api/categories`
  - `GET /api/categories`
  - `GET /api/categories/{id}`
  - `PUT /api/categories/{id}`
  - `DELETE /api/categories/{id}`
- Transaction endpoints:
  - `POST /api/transactions`
  - `GET /api/transactions`
- Summary endpoint:
  - `GET /api/summaries/financial`

## Step-by-step test guide

### 1. Go to project root
```powershell
cd "c:\Users\Olz PC\CodeProjects\Personal_finance_api"
```

### 2. Build the solution
```powershell
dotnet build src/PersonalFinance.Api/PersonalFinance.Api.csproj
```

### 3. Run the API
```powershell
dotnet run --project src/PersonalFinance.Api/PersonalFinance.Api.csproj
```

The app will print listening URLs in the console, for example:
- `https://localhost:7xxx`
- `http://localhost:5xxx`

Use one of those as `<BASE_URL>` in the steps below.

### 4. Open Swagger
Open:
`<BASE_URL>/swagger`

You can test all endpoints directly from the Swagger UI.

### 5. Test categories

#### 5.1 Get seeded categories
```powershell
Invoke-RestMethod -Method Get -Uri "<BASE_URL>/api/categories"
```

Expected: initial categories are seeded (`Food`, `Transportation`, `Rent`, `Salary`, `Entertainment`).

#### 5.2 Create a category
```powershell
$body = @{
  name = "Health"
} | ConvertTo-Json

Invoke-RestMethod -Method Post -Uri "<BASE_URL>/api/categories" -ContentType "application/json" -Body $body
```

#### 5.3 Get one category by id
```powershell
Invoke-RestMethod -Method Get -Uri "<BASE_URL>/api/categories/1"
```

#### 5.4 Update a category
```powershell
$body = @{
  name = "Healthcare"
} | ConvertTo-Json

Invoke-RestMethod -Method Put -Uri "<BASE_URL>/api/categories/1" -ContentType "application/json" -Body $body
```

#### 5.5 Delete a category
```powershell
Invoke-RestMethod -Method Delete -Uri "<BASE_URL>/api/categories/6"
```

Note: deleting a category used by transactions returns conflict.

### 6. Test transactions

#### 6.1 Create an income transaction
```powershell
$income = @{
  amount = 3000.00
  description = "Monthly salary"
  date = "2026-03-12T00:00:00Z"
  transactionType = 1
  categoryId = 4
} | ConvertTo-Json

Invoke-RestMethod -Method Post -Uri "<BASE_URL>/api/transactions" -ContentType "application/json" -Body $income
```

#### 6.2 Create an expense transaction
```powershell
$expense = @{
  amount = 120.50
  description = "Groceries"
  date = "2026-03-12T00:00:00Z"
  transactionType = 2
  categoryId = 1
} | ConvertTo-Json

Invoke-RestMethod -Method Post -Uri "<BASE_URL>/api/transactions" -ContentType "application/json" -Body $expense
```

`transactionType` values:
- `1` = Income
- `2` = Expense

#### 6.3 List transactions
```powershell
Invoke-RestMethod -Method Get -Uri "<BASE_URL>/api/transactions"
```

### 7. Test financial summary
```powershell
Invoke-RestMethod -Method Get -Uri "<BASE_URL>/api/summaries/financial"
```

Expected JSON shape:
```json
{
  "totalIncome": 3000.00,
  "totalExpense": 120.50,
  "balance": 2879.50
}
```

### 8. Reset local database (optional)
The SQLite file is created at:
- `src/PersonalFinance.Api/personal_finance.db` (typical)

Stop the API, delete the DB file, and run the API again to recreate schema + seeded categories.
