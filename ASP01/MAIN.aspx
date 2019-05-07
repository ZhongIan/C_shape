<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MAIN.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>


    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"/>

    <title></title>
    <style>
        /*.MyPager a{
            text-decoration:none;
        }
        .PageBtn{
            font-family:Webdings;
            text-decoration:none;
        }
        .MyFont1 {
            font-weight: 900;
            font-family: 微軟正黑體;
        }*/

        /*與Nav間隔*/
        #Mybody{
            padding-top: 100px;
        }

        body {
          position: relative;
        }

    </style>
    
</head>
<%--滾動監聽對象--%>
<body id="Mybody" data-spy="scroll" data-target="#MyNavScroll" data-offset="100">
    <div class="container">
        <%--Nav--%>
        <nav id="MyNavScroll" class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#">風險值計算</a>
                        <%--螢幕小時顯示功能按鈕--%>
                        <button class="navbar-toggle" type="button" data-toggle="collapse" data-target=".bs-js-navbar-scrollspy">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    </div>
                    <%--Modal 呼叫--%>
                    <div>
                        <button type="button" class="btn btn-default navbar-btn navbar-left glyphicon glyphicon-plus" data-toggle="modal" data-target="#MyModal"> | 資料輸入 |</button>
                    </div>
                    <%--滾動監聽--%>
                    <div class="collapse navbar-collapse bs-js-navbar-scrollspy">
                        <ul class="nav navbar-nav">
                            <li><a id="clickHist" href="#Hist">歷史模擬法</a></li>
                            <li><a id="clicksigma" href="#sigma">變異數的估算</a></li>
                            <li><a id="clickCm" href="#Cm">變異數共變異數法</a></li>
                            <li><a id="clickMote" href="#Mote">蒙地卡羅法</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </nav>
        <%--Modal 模態框 子框--%>
        <div class="modal" id="MyModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="modalclose" class="close" data-dismiss="modal">&times;</button>
                        <h3>請選擇</h3>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="stock">股票代號</label>
                            <%--由jquery取得資料AjAX到JSON.asp--%>
                            <select id="stock" name="stock" class="form-control">
                                <option>1201</option>
                                <option>2409</option>
                                <option>2884</option>
                                </select>
                        </div>
                        <div class="form-group">
                            <label  for="alpha">alpha值</label>
                            <select id="alpha" name="alpha" class="form-control">
                                <option>0.01</option>
                                <option>0.05</option>
                                </select>
                        </div>
                        <div class="form-group">
                            <label  for="alpha">變異數計算</label>
                            <select id="SigmaMethod" name="SigmaMethod" class="form-control">
                                <option>簡單(Sample)</option>
                                <option>加權(Weight)</option>
                                </select>
                        </div>
                        <div class="form-group">
                            <label  for="alpha">風險值計算方法</label>
                            <select id="method" name="method" class="form-control">
                                <option>歷史模擬法</option>
                                <option>變異數共變異數法</option>
                                <option>蒙地卡羅模擬法</option>
                                </select>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="form-group">
                            <input id="Submit1" type="submit" value="送出" class="btn btn-primary" />
                            <input id="Button3" type="button" value="取消" class="btn btn-default" data-dismiss="modal" />
                        </div>
                    </div>

                    </div>
                </div>
        </div>
        <div id="test" class="row text-center jumbotron">
            <h1>風險值方法</h1>
            <div>
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#MyModal">繪圖資料輸入</button>
            </div>
        </div>
        <div id="Hist" class="panel panel-primary">
            <div class="panel-heading">歷史模擬法</div>
            <div class="panel-body">
                <p>分析之資產需有大量歷史資料，才有辦法精確地敘述極端狀況下的風險值</p>
                <p>將可能報酬由小至大排序，依據不同$\alpha$取值。以100筆損益資料為例，若$\alpha$=0.05，</p>
                <p>則第5筆資料(100*0.05=5)即為估算之風險值。</p>
                <p class="alert alert-info">**條件風險值**</p>
                <p>以100筆損益資料為例，若$\alpha$=0.05，則獎第5筆前的資料加總後平均</p>
            </div>
        </div>
        <div id="plotHist" class="row"></div>
        <div id="sigma" class="panel panel-primary">
            <div class="panel-heading">變異數的估算</div>
            <div class="panel-body">
                <p>傳統上通常利用移動平均的觀念來估算變異數，並且可進一步分為等權移動平均與指數加權移動平均兩種方式。</p>
                <p class="alert alert-info">* 等權移動平均(簡單)</p>
                <p>$$ \sigma_{t} = \sqrt{\sum\frac{(R_{t}-\bar{R})^2}{T-1}} $$</p>
                <p class="alert alert-info">* 加權移動平均(指數)</p>
                <p>$$ \sigma_{t} = \sqrt{\lambda\sigma^2_{t-1} + (1-\lambda) R_{t}^2} $$</p>
                <p class="alert alert-danger">($\lambda$≤1 表示衰退因子, 目的為在使早期的變異數對於當期波動性影響程度隨$\lambda$值愈小而降低)</p>
                <p>$\lambda$：衰退因子<span class="alert alert-danger">(0.94，因使用日資料，月資料為0.97)</span></p>
            </div>
        </div>
        <div id="Cm" class="panel panel-primary">
            <div class="panel-heading">變異數共變異數法</div>
            <div class="panel-body">
                <p>假設個別資產報酬率符合聯合常態分配，而且具有序列獨立的特性，藉由常態分配的性質，可快速估算評估期間的風險值</p>
                <p>W(價值)設為1，僅考慮變化</p>
                <p>$$ VaR = -Z\sigma W $$</p>
                <p>$\alpha$=0.05 -> Z=1.645</p>
                <p>$\alpha$=0.01 -> Z=2.33</p>
                <p class="alert alert-info">**條件風險值**</p>
                <p>$$ ES = \frac{1}{\alpha}[-\frac{\sigma}{\sqrt{2\pi}}exp(-\frac{(-VaR_{\alpha})^2}{2\sigma^2})] $$</p>
            </div>
        </div>
        <div id="plotCm" class="row"></div>
        <div id="Mote" class="panel panel-primary">
            <div class="panel-heading">蒙地卡羅法</div>
            <div class="panel-body">
                <p>是一種隨機模擬方法，以概率和統計理論方法為基礎的一種計算方法，</p>
                <p>是使用隨機數（或更常見的偽隨機數）來解決很多計算問題的方法，基於大數法則的實證方法，</p>
                <p>當實驗的次數越多，平均值會越接近理論值。</p>
                <p class="alert alert-danger">分成t期 $S_{t-1}$到$S_{t}$的關係式</p>
                <p>$$ S_{t} = S_{t-1}exp\left[(\mu-\frac{\sigma^2}{2})\Delta t+\sigma\sqrt{\Delta t}\epsilon\right] $$</p>
                <p>其中$\mu$是股價平均數，$\epsilon$是符合常態N(0,1)的隨機數</p>
                <p class="alert alert-danger">移項取$\ln$得到$S_{0}$到$S_{t}$的$return$:</p>
                <p>$$ return \simeq \ln\frac{S_{t}}{S_{0}} = \sum_{n=1}^{t}\left[(\mu-\frac{\sigma^2}{2})\Delta t+\sigma\sqrt{\Delta t}\epsilon\right] $$</p>
                <p>基於大數法則，計算N條不同隨機數，所得到的$return$，排序後選擇第N*$\alpha$筆(和歷史模擬法類似)</p>
                <p class="alert alert-info">**條件風險值**</p>
                <p>排序後選擇前T*$\alpha$筆<span class="alert alert-danger">(和歷史模擬法類似)</span>加總後平均</p>
            </div>
        </div>
        <div id="plotMote" class="row"></div>
        <form id="form1" runat="server" >
            <%--<asp:Button ID="Button1" runat="server" Text="Button" />--%>
        </form>

    </div>

    <!-- plotly -->
    <script src="https://cdn.plot.ly/plotly-latest.min.js"></script>

    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

    <script>
        //click監聽 執行滾動 
        var idname1 = "Hist";
        $('#click'+idname1).click(function () {
            FunScroll(idname1)
        });
        var idname2 = "sigma";
        $('#click'+idname2).click(function () {
            FunScroll(idname2)
        });
        var idname3 = "Cm";
        $('#click'+idname3).click(function () {
            FunScroll(idname3)
        });
        var idname4 = "Mote";
        $('#click'+idname4).click(function () {
            FunScroll(idname4)
        });
        //由於Nav佔有空間 需往下90
        function FunScroll(idname) {
            event.preventDefault();
            var top = $('#'+idname).offset().top - 90;
            $('html,body').animate({scrollTop:top},800);
        }
        // 歷史模擬法 不需考慮 SigmaMethod
        $("#MyModal").click(function () {
            if ($("#method").val() == "歷史模擬法") {
                $("#SigmaMethod").attr("disabled", "disabled");
            }
            else if($("#method").val() != "歷史模擬法") {
                $("#SigmaMethod").removeAttr("disabled");
            }

        });
        
        $("#Submit1").click(function () {
            $("#SigmaMethod").removeAttr("disabled");

            var RequestData = {
                "stock": $("#stock").val(),
                "alpha": $("#alpha").val(),
                "sigma": $("#SigmaMethod").val(),
                "method": $("#method").val()
            };
            //console.log(RequestData)//測試用

            var plotdivID;
            switch (RequestData.method)
            {
                case "歷史模擬法":
                    plotdivID = "Hist"
                    break;
                case "變異數共變異數法":
                    plotdivID = "Cm"
                    break;
                case "蒙地卡羅模擬法":
                    plotdivID = "Mote"
                    break;
            }

            $.ajax({
                type: 'POST',
                url: 'JSON.aspx',
                //發送參數
                data: RequestData,
                success: function (data) {
                    console.log(data)//測試用
                    console.log(plotdivID)
                    Result_Plot(data,plotdivID);
                    $("#MyModal .close").click();
                    FunScroll(plotdivID);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert("Ajax發生錯誤!!");
                }
            })
        });
        // Plotly 繪圖
        function Result_Plot(data,divID) {
            //alert("Ajax!!"); //測試用
            var JS_data = $.parseJSON(data)[0];
            var trace_return = {
                x: JS_data["Date"],
                y: JS_data["Rt"],
                mode: 'lines',
                name: 'return',
                type: 'scatter'
            };
            var trace_VaR = {
                x: JS_data["Date"],
                y: JS_data["Rv"],
                mode: 'lines',
                name: 'VaR',
                type: 'scatter'
            };
            var trace_CVaR = {
                x: JS_data["Date"],
                y: JS_data["RCv"],
                mode: 'lines',
                name: 'CVaR',
                type: 'scatter'
            };
            var Ply_data = [trace_return, trace_VaR, trace_CVaR];
            Plotly.newPlot("plot"+divID, Ply_data);
        }
    </script>
    <%--數學函式 Latex 支持  需放在Jquery之下(位置，猜測$號衝突)，否則會失敗--%>
    <script type="text/x-mathjax-config">
      MathJax.Hub.Config({tex2jax: {inlineMath: [['$','$'], ['\\(','\\)']]}});
    </script>
    <script type="text/javascript"
      src="http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML">
    </script>

</body>
</html>
