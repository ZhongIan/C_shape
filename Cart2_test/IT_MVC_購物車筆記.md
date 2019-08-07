
# 參考來源

[使用ASP.NET MVC 實作購物網站](https://ithelp.ithome.com.tw/users/20091762/ironman/971)

# 執行結果

## 商品管理

**商品管理[商品列表]**

<div align=center><img width="50%" height="50%" src="./商品管理--商品列表.jpg"/></div>

**商品管理[新增]**

<div align=center><img width="50%" height="50%" src="./商品管理--新增.jpg"/></div>

**商品細節**

<div align=center><img width="50%" height="50%" src="./商品細節.jpg"/></div>

## 購物車

**購物車[首頁]**

<div align=center><img width="50%" height="50%" src="./購物車--首頁.jpg"/></div>

**購物車[ajax]**

<div align=center><img width="50%" height="50%" src="./購物車--ajax.jpg"/></div>

**購物車[結帳]**

<div align=center><img width="50%" height="50%" src="./購物車--結帳.jpg"/></div>

## 會員管理

**會員管理**

<div align=center><img width="50%" height="50%" src="./會員管理.jpg"/></div>

## 訂單管理

**訂單管理[訂單列表]**

<div align=center><img width="50%" height="50%" src="./訂單管理--訂單列表.jpg"/></div>

**訂單管理[訂單細節]**

<div align=center><img width="50%" height="50%" src="./訂單管理--訂單細節.jpg"/></div>





# 使用ASP.NET MVC 實作購物網站 (三) - MVC概念

https://ithelp.ithome.com.tw/articles/10157908

MVC是模型(Model) 檢視(View) 控制器(Controller) 的縮寫，概念是把整個完整的程式邏輯區分為三塊：

* 控制器(Controller) ：負責處理及轉發要求(Request)，可以視情況呼叫Model拿資料，也視情況呼叫View來回應，而控制器中會包含多個動作(Action)。
* 模型(Model) ：專門處理資料的相關邏輯。
* 檢視(View) ：專門展示處理結果給使用者，提供UI。

以ASP MVC舉例，一個正常的Http Request會完成以下動作：
* 某個Web使用者點選了網頁連結為[http://localhost.test.com/Home/Index]
* 路由(route)決定交付名稱為Home的控制器中的Index動作(Action)來進行
* Index動作先跟Model拿了資料
* Index動作將取得的資料交給View
* View根據資料來顯示畫面給Web使用者觀看

* App_Data - 預設ASP.NET User的本機資料庫檔案
* AppStart - 網站起始設定
* Content - 存放css檔案 (*.css)
* Controller - 所有控制器的原始碼(*.cs)
* fonts- 字型
* Models - 與模型相關的原始碼
* Scripts - 存放JavaScript檔案(*.js)
* View - 所有檢視的原始碼，依據不同的Controller名稱會有對應名稱的目錄

# 流程

到 [使用ASP.NET MVC 實作購物網站 (十二) - Product的刪除功能](https://ithelp.ithome.com.tw/articles/10159469) 為止是 Product 功能

[使用ASP.NET MVC 實作購物網站 (十三) - 會員功能](https://ithelp.ithome.com.tw/articles/10159607) 此篇有一些預設(會員功能)在本機沒有，因此手動填入，造成後面的連動問題 如登入，ID(原為HASH，手動填1)

[使用ASP.NET MVC 實作購物網站 (十六) - 購物車與Session](https://ithelp.ithome.com.tw/articles/10160180) 到 [使用ASP.NET MVC 實作購物網站 (二十三) - 使用Ajax清空購物車](https://ithelp.ithome.com.tw/articles/10161099) 完成購物車的添加及刪除 Session--Ajax 連動

[使用ASP.NET MVC 實作購物網站 (二十四) - 購物流程(1)](https://ithelp.ithome.com.tw/articles/10161215) 收貨人資訊--Models/OrderModel/Ship.cs

[使用ASP.NET MVC 實作購物網站 (二十五) - 購物流程(2)](https://ithelp.ithome.com.tw/articles/10161327) Order與OrderDetail

[使用ASP.NET MVC 實作購物網站 (二十六) - 後台訂單列表功能](https://ithelp.ithome.com.tw/articles/10161407) ManageOrderController，其中Index()是顯示目前網站所有訂單，Details()是針對每筆訂單顯示購買商品清單


[使用ASP.NET MVC 實作購物網站 (二十七) - 使用者"我的訂單"功能](https://ithelp.ithome.com.tw/articles/10161482) 在Models中新增PartialClass擴充Order類別 定義GetUserName()方法，此方法主要是透過原本儲存在Order類別中的UserId去AspNetUsers表取得UrseName(使用者暱稱)

[使用ASP.NET MVC 實作購物網站 (二十八) - 後台訂單列表搜尋功能](https://ithelp.ithome.com.tw/articles/10161571) ManageOrderController中新增SearchByUserName()

[使用ASP.NET MVC 實作購物網站 (二十九) - 商品留言功能](https://ithelp.ithome.com.tw/articles/10161678)


## 紀錄

Product[Models 資料型態] -> Product[Controller url] -> VIEW

\Views\Product\Index.cshtml

    Create
    Edit
    Index

Cart[Models\Cart]

    Cart -> 引用List<CartItem> 功能{新增Product,計數...}
    CartItem -> 資料型態
    Operation -> Sessi啟用 新增 Cart

-> CartController

    GetCart
    AddToCart
    RemoveFromCart
    ClearCart

-> _CartPartial.cshtml

    onclick="RemoveFromCart('@cartitem.Id')"
    ...

-> _Layout.cshtml

    //移除購物車內商品
        function RemoveFromCart(productId) {
            $.ajax({
                type: 'POST',
                url: '@Url.Action("RemoveFromCart", "Cart")',
                data: { id: productId }
            })
                .done(function (msg) {
                    //將回傳的購物車頁面 填入 li#Cart
                    $('li#Cart').html(msg);
                });
        }
    ...

收貨人資訊

    Models/OrderModel/Ship.cs


Order與OrderDetail [model]

ManageOrderController [訂單管理]




# 商品資訊

鉛筆	30	  https://tshop.r10s.com/e71/2ae/198d/175d/20ef/498e/47be/1124e8aab8c4544489145b.jpg?_ex=330x330

鉛筆盒    350.00    https://s.yimg.com/ut/api/res/1.2/yS21jFbVxrr6rMbcdiyXJA--~B/YXBwaWQ9eXR3bWFsbDtjYz0zMTUzNjAwMDtoPTYwMDtxPTgxO3c9NjAw/https://s.yimg.com/fy/e78b/item/p017352338124-item-14a3xf4x0950x0713-m.jpg

相機	3000	https://e.ecimg.tw/items/DGAD73A9008KCFQ/000001_1516676071.jpg






