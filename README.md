# 🚀 Hướng Dẫn Cài Đặt và Khởi Chạy Dự Án Human Resource System (HRM)

Hệ thống Quản lý Nhân sự (**Human Resource Management - HRM**) được xây dựng trên nền tảng **.NET 8**, kiến trúc Full-stack với **Blazor WebAssembly** (Client) và **ASP.NET Core Web API** (Server), kết nối cơ sở dữ liệu **Microsoft SQL Server**.

---

## 📋 Mục lục
1. [Yêu cầu hệ thống (Prerequisites)](#-yêu-cầu-hệ-thống-prerequisites)
2. [Cấu trúc thư mục dự án](#-cấu-trúc-thư-mục-dự-án)
3. [Bước 1: Cài đặt và cấu hình Cơ sở dữ liệu (SQL Server)](#-bước-1-cài-đặt-và-cấu-hình-cơ-sở-dữ-liệu-sql-server)
4. [Bước 2: Cấu hình Connection String](#-bước-2-cấu-hình-connection-string)
5. [Bước 3: Hướng dẫn chạy dự án](#-bước-3-hướng-dẫn-chạy-dự-án)
   - [Cách 1: Sử dụng Visual Studio (Khuyên dùng)](#cách-1-sử-dụng-visual-studio-khuyên-dùng)
   - [Cách 2: Sử dụng Command Line (Terminal / PowerShell / VS Code)](#cách-2-sử-dụng-command-line-terminal--powershell--vs-code)
6. [Tài khoản đăng nhập & Phân quyền](#-tài-khoản-đăng-nhập--phân-quyền)
7. [Các lỗi thường gặp và cách xử lý (Troubleshooting)](#-các-lỗi-thường-gặp-và-cách-xử-lý-troubleshooting)

---

## 🛠 Yêu cầu hệ thống (Prerequisites)

Trước khi bắt đầu, hãy đảm bảo máy tính đã cài đặt các công cụ sau:

1. **.NET SDK 8.0** trở lên:
   - Kiểm tra bằng lệnh: `dotnet --version` (kết quả hiển thị `8.0.x`).
   - Tải về tại: [Download .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **Microsoft SQL Server**:
   - SQL Server 2019/2022 (hoặc SQL Server Express / LocalDB).
3. **SQL Server Management Studio (SSMS)**:
   - Dùng để import file cơ sở dữ liệu `.bacpac`.
   - Tải về tại: [Download SSMS](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)
4. **IDE / Trình soạn thảo**:
   - Visual Studio 2022 (Workload: *ASP.NET and web development*) hoặc Visual Studio Code / JetBrains Rider.

---

## 📁 Cấu trúc thư mục dự án

```text
HRM/
├── Database_HRM.bacpac             # File sao lưu dữ liệu SQL Server có sẵn dữ liệu mẫu
├── README.md                       # Tài liệu hướng dẫn cài đặt và chạy dự án
└── HumanResourceSystem-master/
    ├── HumanResourceSystem.sln     # File Solution tổng hợp
    ├── Server/                     # Web API Project (ASP.NET Core .NET 8)
    │   ├── appsettings.json        # Cấu hình Connection String & JWT
    │   └── Controllers/            # API Endpoints
    ├── Client/                     # Frontend Project (Blazor WebAssembly .NET 8)
    │   ├── Pages/                  # Giao diện các trang chức năng
    │   └── wwwroot/                # Tài nguyên tĩnh (CSS, JS, Fonts)
    ├── BaseLibrary/                # Entities, DTOs, Models dùng chung
    ├── ServerLibrary/              # DbContext, Repositories, Migrations
    └── ClientLibrary/              # Services gọi API, Authentication State
```

---

## 🗄 Bước 1: Cài đặt và cấu hình Cơ sở dữ liệu (SQL Server)

Trong thư mục `HRM/` đã chuẩn bị sẵn file `Database_HRM.bacpac`. Hãy làm theo các bước sau để nạp dữ liệu:

### Nhập cơ sở dữ liệu qua SSMS:
1. Mở **SQL Server Management Studio (SSMS)** và đăng nhập vào SQL Server của bạn.
2. Tại cửa sổ **Object Explorer** bên trái:
   - Nhấp chuột phải vào thư mục **Databases**.
   - Chọn **Import Data-tier Application...**.
3. Tại giao diện Wizard:
   - Nhấn **Next**.
   - Tại mục *Import Settings*, nhấn **Browse...** và chọn file:
     ```
     Database_HRM.bacpac
     ```
   - Nhấn **Next**.
   - Đặt tên Database Settings: `DemoEmployeeDb` (đây là tên mặc định được cấu hình trong `appsettings.json`).
   - Nhấn **Next** rồi nhấn **Finish**.
4. Chờ quá trình import hoàn tất (thường mất 1 - 2 phút). Sau khi hoàn tất, cơ sở dữ liệu `DemoEmployeeDb` đã sẵn sàng với đầy đủ bảng và dữ liệu mẫu.

> 💡 **Lưu ý phụ:** Nếu bạn muốn tạo cơ sở dữ liệu rỗng thông qua EF Core Migrations thay vì dùng file `.bacpac`:
> ```powershell
> cd HumanResourceSystem-master
> dotnet ef database update --project ServerLibrary --startup-project Server
> ```

---

## ⚙ Bước 2: Cấu hình Connection String

Mở file cấu hình của Server tại:
`HumanResourceSystem-master/Server/appsettings.json`

Kiểm tra chuỗi kết nối tại dòng `ConnectionStrings`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(local); Database=DemoEmployeeDb; Trusted_Connection=True; Trust Server Certificate=True; Connection Timeout=30;"
  },
  "AllowedHosts": "*",
  "JWTSection": {
    "Key": "Rn2OgfPmhEAWovOX4vHHPsTCHStTOas0",
    "Issuer": "https://localhost:3000",
    "Audience": "https://localhost:3000"
  }
}
```

- Nếu SQL Server của bạn là bản **SQL Server mặc định**: giữ nguyên `Server=(local)` hoặc `Server=.`.
- Nếu bạn sử dụng **SQL Server Express**: đổi thành `Server=.\\SQLEXPRESS`.
- Nếu dùng tài khoản SQL Server Authentication (sa): đổi thành:
  ```json
  "DefaultConnection": "Server=(local); Database=DemoEmployeeDb; User Id=sa; Password=YOUR_PASSWORD; Trust Server Certificate=True; Connection Timeout=30;"
  ```

---

## 🚀 Bước 3: Hướng dẫn chạy dự án

Dự án gồm **2 phần** cần chạy đồng thời:
1. **Server (Backend API)**: Chạy trên cổng `https://localhost:3000` (Swagger: `https://localhost:3000/swagger`).
2. **Client (Blazor WebAssembly)**: Chạy trên cổng `https://localhost:3001` (hoặc `http://localhost:5133`).

---

### Cách 1: Sử dụng Visual Studio (Khuyên dùng)

1. Mở file `HumanResourceSystem-master\HumanResourceSystem.sln` bằng Visual Studio 2022.
2. Thiết lập chạy đồng thời cả Server và Client:
   - Trong cửa sổ **Solution Explorer**, nhấp chuột phải vào Solution `HumanResourceSystem` -> Chọn **Configure Startup Projects...** (hoặc **Properties**).
   - Chọn mục **Multiple startup projects**.
   - Tại danh sách các project:
     - Chọn **Server** -> Action: **Start**
     - Chọn **Client** -> Action: **Start**
     - Các project còn lại: để *None*.
   - Nhấn **Apply** -> **OK**.
3. Nhấn **F5** (chế độ Debug) hoặc **Ctrl + F5** (chạy không Debug) để khởi động.
4. Trình duyệt sẽ tự động mở:
   - Giao diện Client: `https://localhost:3001`
   - Giao diện Swagger API Server: `https://localhost:3000/swagger`

---

### Cách 2: Sử dụng Command Line (Terminal / PowerShell / VS Code)

Mở 2 cửa sổ Terminal (PowerShell hoặc Command Prompt) riêng biệt:

#### 🔹 Terminal 1: Khởi chạy Server (Web API)
```powershell
cd "HumanResourceSystem-master\Server"
dotnet run --launch-profile https
```
> Khi hiển thị thông báo: `Now listening on: https://localhost:3000` là Server đã chạy thành công.  
> Bạn có thể truy cập `https://localhost:3000/swagger` để kiểm tra các API endpoints.

#### 🔹 Terminal 2: Khởi chạy Client (Blazor Web)
```powershell
cd "HumanResourceSystem-master\Client"
dotnet run --launch-profile https
```
> Khi hiển thị thông báo: `Now listening on: https://localhost:3001` là Client đã sẵn sàng.  
> Mở trình duyệt và truy cập: **`https://localhost:3001`**.

---

## 🔐 Tài khoản đăng nhập & Phân quyền

### Cơ chế tài khoản trong hệ thống:
1. **Tài khoản có sẵn trong cơ sở dữ liệu**:
   - Nếu bạn đã import file `Database_HRM.bacpac`, các dữ liệu mẫu và tài khoản người dùng đã có sẵn trong bảng `ApplicationUsers`.
2. **Đăng ký tài khoản mới**:
   - Trên giao diện `https://localhost:3001`, nhấn vào **Register** để tạo tài khoản mới.
   - **Quy tắc phân quyền tự động**:
     - Tài khoản đăng ký đầu tiên trong hệ thống sẽ tự động được gán quyền **Admin**.
     - Các tài khoản đăng ký tiếp theo sẽ được gán quyền **Staff**.
   - Sau khi đăng ký, tiến hành đăng nhập với Email và Mật khẩu vừa tạo.

---

## ❓ Các lỗi thường gặp và cách xử lý (Troubleshooting)

### 1. Lỗi chứng chỉ SSL / HTTPS (`Untrusted Certificate` / `Connection not private`)
Blazor WebAssembly gọi API qua giao diện HTTPS `localhost`. Nếu gặp lỗi chứng chỉ:
- Mở PowerShell và chạy lệnh sau để thiết lập chứng chỉ phát triển:
  ```powershell
  dotnet dev-certs https --clean
  dotnet dev-certs https --trust
  ```
- Chọn **Yes** khi có hộp thoại xác nhận hiện ra.

### 2. Lỗi kết nối Cơ sở dữ liệu (`Cannot open database "DemoEmployeeDb"`)
- Kiểm tra Service SQL Server đã ở trạng thái **Running** hay chưa (`services.msc` -> tìm `SQL Server (MSSQLSERVER)` hoặc `SQL Server (SQLEXPRESS)`).
- Đảm bảo bạn đã import thành công file `Database_HRM.bacpac` với đúng tên database là `DemoEmployeeDb`.
- Kiểm tra lại `Server` trong file `appsettings.json`.

### 3. Lỗi xung đột cổng (Port 3000 hoặc 3001 đang bị chiếm dụng)
Nếu gặp lỗi `Address already in use` hoặc file `.dll` bị khóa (`The process cannot access the file because it is being used by another process`):
- Tìm và đóng tiến trình đang chiếm cổng:
  ```powershell
  # Tìm tiến trình đang lắng nghe cổng 3000 hoặc 3001
  Get-Process -Id (Get-NetTCPConnection -LocalPort 3000).OwningProcess -ErrorAction SilentlyContinue | Stop-Process -Force
  Get-Process -Id (Get-NetTCPConnection -LocalPort 3001).OwningProcess -ErrorAction SilentlyContinue | Stop-Process -Force
  ```
- Hoặc mở Task Manager và dừng tiến trình `Server.exe` / `dotnet.exe`.

### 4. Lỗi CORS khi gọi API
- Cấu hình CORS đã được cài đặt sẵn trong `Server/Program.cs` hỗ trợ `https://localhost:3001` và `http://localhost:5133`.
- Hãy đảm bảo bạn khởi chạy Client đúng cổng `3001` hoặc `5133`.

---

🎉 **Chúc bạn cài đặt và trải nghiệm dự án thành công!**
