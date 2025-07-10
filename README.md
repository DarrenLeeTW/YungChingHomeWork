# Yu## 功能特色

- ✅ RESTful API 設計
- ✅ SQLite 資料庫整合 (使用 Dapper ORM)
- ✅ Swagger/OpenAPI 文件
- ✅ NLog 日誌記錄
- ✅ 依賴注入 (Dependency Injection)
- ✅ 三層架構 (Controller + Service + Repository)
- ✅ Repository 模式實現資料存取層omeWork - 售屋資料管理 API

這是一個使用 ASP.NET Core 8.0 開發的 RESTful Web API，用於管理售屋資料。提供新增、修改、刪除、查詢售屋資料的功能。

## 功能特色

-  RESTful API 設計
-  SQLite 資料庫整合 (使用 Dapper ORM)
-  Swagger/OpenAPI 文件
-  NLog 日誌記錄
-  依賴注入 (Dependency Injection)
-  分層架構 (Controller + Service)

## 技術

- **框架**: ASP.NET Core 8.0
- **資料庫**: SQLite
- **ORM**: Dapper
- **日誌**: NLog
- **API 文件**: Swagger/OpenAPI

## 專案結構

```
YungChingHomeWork/
├── Controllers/
│   └── HouseListingController.cs    # API 控制器
├── Models/
│   └── HouseListing.cs              # 資料模型
├── Services/
│   └── HouseListingService.cs       # 業務邏輯服務
├── Repositories/
│   ├── IHouseListingRepository.cs   # Repository 介面
│   └── HouseListingRepository.cs    # 資料存取層實作
├── Program.cs                       # 應用程式進入點
├── appsettings.json                 # 配置文件
├── nlog.config                      # NLog 配置
└── houseListings.db                 # SQLite 資料庫檔案
```

## 快速開始

### 前置需求

- .NET 8.0 SDK
- Visual Studio 2022 或 Visual Studio Code

### 安裝與執行

1. **複製專案**
   ```bash
   git clone <repository-url>
   cd YungChingHomeWork
   ```

2. **還原套件**
   ```bash
   dotnet restore
   ```

3. **建置專案**
   ```bash
   dotnet build
   ```

4. **執行應用程式**
   ```bash
   dotnet run
   ```

5. **開啟瀏覽器**
   - 應用程式預設會在 `http://localhost:5156` 或 `https://localhost:7156` 運行
   - Swagger UI: `http://localhost:5156/swagger`

## API 端點

### 售屋資料管理

| 方法 | 端點 | 描述 | 請求體 |
|------|------|------|--------|
| `GET` | `/api/HouseListing` | 查詢所有售屋資料 | 無 |
| `GET` | `/api/HouseListing/{id}` | 根據 ID 查詢特定售屋資料 | 無 |
| `POST` | `/api/HouseListing` | 新增售屋資料 | HouseListing JSON |
| `PUT` | `/api/HouseListing/{id}` | 修改售屋資料 | HouseListing JSON |
| `DELETE` | `/api/HouseListing/{id}` | 刪除售屋資料 | 無 |

### 資料模型

```json
{
  "id": 1,
  "name": "美麗的房子",
  "address": "台北市信義區信義路五段7號",
  "price": 5000000
}
```

## Swagger 使用說明

### 1. 訪問 Swagger UI

啟動應用程式後，開啟瀏覽器並訪問：
```
http://localhost:5156/swagger
```

### 2. 使用 Swagger UI 測試 API

#### 新增售屋資料 (POST)
1. 點擊 `POST /api/HouseListing` 端點
2. 點擊 "Try it out" 按鈕
3. 在 Request body 中輸入 JSON 資料：
   ```json
   {
     "name": "豪華別墅",
     "address": "台北市大安區敦化南路二段123號",
     "price": 8000000
   }
   ```
4. 點擊 "Execute" 執行請求
5. 查看 Response body 中返回的資料（包含自動生成的 ID）

#### 查詢所有售屋資料 (GET)
1. 點擊 `GET /api/HouseListing` 端點
2. 點擊 "Try it out" 按鈕
3. 點擊 "Execute" 執行請求
4. 查看所有售屋資料列表

#### 查詢特定售屋資料 (GET by ID)
1. 點擊 `GET /api/HouseListing/{id}` 端點
2. 點擊 "Try it out" 按鈕
3. 在 id 參數中輸入要查詢的 ID（例如：1）
4. 點擊 "Execute" 執行請求

#### 修改售屋資料 (PUT)
1. 點擊 `PUT /api/HouseListing/{id}` 端點
2. 點擊 "Try it out" 按鈕
3. 在 id 參數中輸入要修改的 ID
4. 在 Request body 中輸入更新後的 JSON 資料：
   ```json
   {
     "name": "更新後的房屋名稱",
     "address": "更新後的地址",
     "price": 6000000
   }
   ```
5. 點擊 "Execute" 執行請求

#### 刪除售屋資料 (DELETE)
1. 點擊 `DELETE /api/HouseListing/{id}` 端點
2. 點擊 "Try it out" 按鈕
3. 在 id 參數中輸入要刪除的 ID
4. 點擊 "Execute" 執行請求

### 3. 回應狀態碼

- `200 OK`: 請求成功
- `201 Created`: 資源建立成功
- `204 No Content`: 請求成功但無回應內容（通常用於 PUT、DELETE）
- `404 Not Found`: 找不到指定的資源
- `400 Bad Request`: 請求格式錯誤

## 使用 Postman 測試

### 環境設定
- Base URL: `http://localhost:5156`

### 測試案例

#### 1. 新增售屋資料
```
POST http://localhost:5156/api/HouseListing
Content-Type: application/json

{
  "name": "現代公寓",
  "address": "新北市板橋區文化路一段188號",
  "price": 3500000
}
```

#### 2. 查詢所有資料
```
GET http://localhost:5156/api/HouseListing
```

#### 3. 查詢特定資料
```
GET http://localhost:5156/api/HouseListing/1
```

#### 4. 修改資料
```
PUT http://localhost:5156/api/HouseListing/1
Content-Type: application/json

{
  "name": "精裝修公寓",
  "address": "新北市板橋區文化路一段188號",
  "price": 3800000
}
```

#### 5. 刪除資料
```
DELETE http://localhost:5156/api/HouseListing/1
```

## 日誌記錄

應用程式使用 NLog 進行日誌記錄：

- **檔案日誌**: 儲存在 `logs/logfile.log`
- **控制台日誌**: 在開發環境中顯示於終端

## 資料庫

- **類型**: SQLite
- **檔案**: `houseListings.db`
- **位置**: 專案根目錄

資料表會在應用程式首次啟動時自動建立。

## 開發說明

### 新增功能
1. 在 `Services/HouseListingService.cs` 中新增業務邏輯
2. 在 `Controllers/HouseListingController.cs` 中新增 API 端點
3. 更新 Swagger 註解以產生正確的 API 文件

### 依賴注入
在 `Program.cs` 中註冊服務：
```csharp
builder.Services.AddSingleton<IHouseListingRepository, HouseListingRepository>();
builder.Services.AddSingleton<HouseListingService>();
```

### 架構說明
- **Controller 層**: 處理 HTTP 請求和回應
- **Service 層**: 包含業務邏輯
- **Repository 層**: 負責資料存取，與資料庫進行交互
- **Model 層**: 定義資料結構