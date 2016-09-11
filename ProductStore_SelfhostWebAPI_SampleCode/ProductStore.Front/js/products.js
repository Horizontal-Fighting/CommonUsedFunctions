var backEndUrl = "http://localhost:9000/";//后端地址

(function () {
    function Products() {
        var self = this;
        //self.products = ko.observableArray([
        //        { Id: 1, Name: "Tomato soup", Category: "Groceries", Price: 1.39 },
        //        { Id: 2, Name: "Yo-yo", Category: "Toys", Price: 3.75 },
        //        { Id: 3, Name: "Hammer", Category: "Hardware", Price: 16.99 }
        //]);

        //双向绑定的前提bidirectional bindings
        self.products = ko.observableArray();
        self.checkedProducts = ko.observableArray([]);


        //Search
        self.btnSearch = function () {
            var searchInfo= $("#searchInfo").val();
            self.products.remove(function (item) { return item.Id != searchInfo; })    
        }

        //添加商品
        self.addProduct = function () {
            var product = { Id: $("#id").val(), Name: $("#name").val(), Category: $("#category").val(), Price: $("#price").val() };
            self.products.push(product);
        };

        //保存
        self.btnSave = function () {
            var array = new Array();
            for (var i = 0; i < self.products().length; i++) {
                console.log(self.products[i]);
            }
        };

        self.btnDelete = function () {
            for (var i = 0; i < self.checkedProducts().length; i++) {
                self.products.remove(function (item) { return item.Id == self.checkedProducts()[i]; })
                console.log("delete checked ID:" + self.checkedProducts()[i]); 
            }
            for (var i = 0; i < self.products.length; i++)
                console.log(self.products[i]);
        }

        //初始化
        self.init = function () {
            $.ajax({
                url: backEndUrl + "api/products",
                type: "GET",
                success: function (data) {
                    self.products.removeAll();
                    for (var i = 0; i < data.length; i++) {
                        self.products.push(data[i]);
                        console.log(self.products.length);
                    }
                }
            });
        }
    }


    var viewModel = new Products();
    ko.applyBindings(viewModel);//双向绑定
    viewModel.init();//执行初始化
})()