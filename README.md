# MyProject
ASP.Net Core 6 專案
1. 參考網址與自我練習寫ASP.Net Core網站，網址內容prjAllShow.Backend/Note/參考網址.txt
2. 啟動專案是Backend，是主要網站，需要在Backend下npm指令將使用到的前端套件安裝
3. 網站是用Code First，下指令等DB表格設定好後，執行時SeedData會把基本資料新增進去
4. 有新增一個WebAPI來測試JWT驗證機制，所以啟動時Backend和WebAPI要一起啟動，當Web登入時會一併去WebAPI要Token
5. 網站登入後，上方toolBar顯示的：員工管理、商店管理、會員管理是使用一般的CRUD跟Razor做存取，商店類別管理跟測試JWT是使用Vue跟WebAPI
6. 
