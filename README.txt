 chrome.exe --user-data-dir="C://Chrome dev session" --disable-web-security

ng build --prod


npm install express --save
npm install nodemon --save-dev

 public DBEntities(string ConnectionString = "DBEntities") : base("name=" + ConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }


- Deploy: ng build --prod
- Create directive: ng g directive addText


npm install karma -g --save-dev
npm install karma-jasmine karma-chrome-launcher jasmine-core --save-dev
Chạy thử server

./node_modules/karma/bin/karma start
Cài đặt Karma CommandLine Interface

sudo npm install karma-cli -g
Bây giờ muốn khởi động Karma, ta đơn giản chỉ cần

karma start