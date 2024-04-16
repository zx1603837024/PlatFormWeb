var map;
var parkName = [];
var berthsUseness = [];
var berthsUse = [];
var empolyeePoint = [];
var count;
var updataRow;



$(function () {
    var whdef = 100 / 916;// 表示1920的设计图,使用100PX的默认值
    var wH = window.innerHeight;// 当前窗口的高度
    var rem = wH * whdef;// 以默认比例值乘以当前窗口宽度,得到该宽度下的相应FONT-SIZE值
    $('html').css('font-size', rem + "px");
    //创建地图
    initMap();
    findInfo();
    pointEmpolyee();

    var myDrag = new BMapLib.RectangleZoom(map, {
        followText: "拖拽鼠标进行操作"
    });
    //map.Dragging();
    //myDrag.open();  //开启拉框放大
    myDrag.close();  //关闭拉框放大
});

//地图添加标记点
function findInfo() {
    //泊位段点
    var points = [];
    $.ajax({
        type: "post",
        async: false,
        url: '../BigScreen/GetBerthsecsPoint',
        dataType: 'json',
        success: function (result) {
            points = result;
        }
    });
    var icon1 = new BMap.Icon("../assets/images/03.png", new BMap.Size(36, 36));

    for (var i = 0; i < points.length; i++) {
        var point = new BMap.Point(points[i].Lat, points[i].Lng); //将标注点转化成地图上的点
        var marker = new BMap.Marker(point, { icon: icon1 }); 　　　　　 // 创建标注
        map.addOverlay(marker);      // 将标注添加到地图中

        var label = new BMap.Label(points[i].BerthsecName, { offset: new BMap.Size(15, -20), position: point });//创建标注信息
        label.setStyle({
            border: "none"
        });
        map.addOverlay(label);
    }

    
}

//人员坐标
function pointEmpolyee() {
    $.ajax({
        type: "post",
        async: false,
        url: '../BigScreen/GetEmpolyeePoint',
        dataType: 'json',
        success: function (result) {
            empolyeePoint = result;
        }
    });
    var icon = new BMap.Icon("../assets/images/employee.png", new BMap.Size(36, 36));
    BMap.Icon.prototype.name = 0;
    BMap.Icon.prototype.setName = function (name) {
        this.name = name;
    }
    for (var i = 0; i < empolyeePoint.length; i++) {
        icon.setName(empolyeePoint[i].EmpolyeeId);  
        var point = new BMap.Point(empolyeePoint[i].X, empolyeePoint[i].Y); //将标注点转化成地图上的点
        var marker = new BMap.Marker(point, { icon: icon }); 　　　　　 // 创建标注
        map.addOverlay(marker);      // 将标注添加到地图中
        var label = new BMap.Label(empolyeePoint[i].TrueName + "<br/>实收：" + empolyeePoint[i].FactReceive + "元<br/>追缴：" + empolyeePoint[i].Repayment + "元", { offset: new BMap.Size(15, -20), position: point });//创建标注信息
        label.setStyle({
            border: "0",
        });
        map.addOverlay(label);
    }
}

function delEmpolyeePoint(employeeid, message, type) {
    if (type == null)
        return false;
    var TrueName;
    var FactReceive;
    var Repayment;
    for (var i = 0; i < empolyeePoint.length; i++) {
        if (employeeid == empolyeePoint[i].EmpolyeeId) {      
            TrueName = empolyeePoint[i].TrueName;
            FactReceive = empolyeePoint[i].FactReceive;
            Repayment = empolyeePoint[i].Repayment;
            break;
        }
    }

    var allOverlay = map.getOverlays();
    var NewResult;
    for (var i = 0; i < allOverlay.length - 1; i++) {
       // if (allOverlay[i].toString() == "[object Marker]") {
            if (employeeid == allOverlay[i].getIcon().name) {
                map.removeOverlay(allOverlay[i]);
                var icon = new BMap.Icon("../assets/images/employee.png", new BMap.Size(36, 36));
                BMap.Icon.prototype.name = 0;
                BMap.Icon.prototype.setName = function (name) {
                    this.name = name;
                }
                $.ajax({
                    type: "post",
                    async: false,
                    url: '../BigScreen/GetEmpolyeePointModel',
                    data: { Employeeid: employeeid },
                    dataType: 'json',
                    success: function (result) {
                        NewResult = result;
                    }
                });
                Lat = NewResult.X;
                Lng = NewResult.Y;
                icon.setName(NewResult.EmpolyeeId);
                var point = new BMap.Point(NewResult.X, NewResult.Y); //将标注点转化成地图上的点
                var marker = new BMap.Marker(point, { icon: icon }); 　　　　　 // 创建标注
                map.addOverlay(marker);      // 将标注添加到地图中       
                break;
            }
      //  }     
    }

    for (var i = 0; i < allOverlay.length - 1; i++) {
    var str = TrueName + "<br/>实收：" + FactReceive + "元<br/>追缴：" + Repayment + "元";
        if (allOverlay[i].content == str) {
            map.removeOverlay(allOverlay[i]);
            var point = new BMap.Point(Lat, Lng); //将标注点转化成地图上的点
            if (type == 2)
                FactReceive = FactReceive + message;
            if (type == 4)
                Repayment = Repayment + message;
            var label = new BMap.Label(TrueName + "<br/>实收：" + FactReceive + "元<br/>追缴：" + Repayment + "元", { offset: new BMap.Size(15, -20), position: point });//创建标注信息
            map.addOverlay(label);
            break;
        }
    }
}

//加载地图
function initMap() {
    map = bmap('map', new BMap.Point(118.77807441, 32.0572355), 12, false, false, false);// new BMap.Map("container");    // 创建Map实例
    //地图自定义样式
    map.setMapStyle({
        styleJson: [
            {
                "featureType": "land",
                "elementType": "geometry.fill",
                "stylers": {
                    "color": "#20254c",
                    "hue": "#20254c",
                    "visibility": "on"
                }
            },
            {
                "featureType": "water",
                "elementType": "geometry.fill",
                "stylers": {
                    "color": "#2e5eb0"
                }
            },
            {
                "featureType": "green",
                "elementType": "geometry.fill",
                "stylers": {
                    "color": "#079f3a"
                }
            },
            {
                "featureType": "road",
                "elementType": "all",
                "stylers": {
                    "color": "#1892dd"
                }
            },
            {
                "featureType": "all",
                "elementType": "labels.text.fill",
                "stylers": {
                    "color": "#857f7f"
                }
            },
            {
                "featureType": "all",
                "elementType": "labels.text.stroke",
                "stylers": {
                    "color": "#182c5f"
                }
            },
            {
                "featureType": "poi",
                "elementType": "all",
                "stylers": {
                    "visibility": "off"
                }
            },
            {
                "featureType": "all",
                "elementType": "labels.icon",
                "stylers": {
                    "visibility": "off"
                }
            },
            {
                "featureType": "all",
                "elementType": "labels.text.fill",
                "stylers": {
                    "color": "#2da0c6",
                    "visibility": "on"
                }
            },
            {
                "featureType": "manmade",
                "elementType": "geometry.fill",
                "stylers": {
                    "color": "#6a6666ff"
                }
            },
            {
                "featureType": "building",
                "elementType": "geometry.fill",
                "stylers": {
                    "color": "#857f7fff"
                }
            }
        ]
    });
}

// 加载获取环比金额数据
function loadMoneyChain(totalmoney, dayTime) {
    myChart3.clear();
   var option3 = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            }
        },
        grid: {
            top: 0,
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            axisLabel: {
                show: true,
                textStyle: {
                    color: '#fff'
                }
            }
        },
        yAxis: {
            show: true,
            type: 'category',
            data: dayTime,

            axisLabel: {
                show: true,
                textStyle: {
                    color: '#fff'
                }
            }
        },
        series: [
            {
                name: '实收总额',
                type: 'bar',
                stack: '金额',
                itemStyle: {
                    normal: { color: '#9BCA63' }
                },
                label: {
                    normal: {
                        show: true,
                        position: 'insideRight'
                    }
                },
                data: totalmoney
            }
        ]
    };
    myChart3.setOption(option3);
}

//ajax请求
function getData(updataRow) {
    $.ajax({
        type: "post",
        async: false,
        url: '../BigScreen/GetBerthsecsUtilization',
        dataType: 'json',
        success: function (result) {
            count = result.length;
            if (6 * updataRow >= count) {
                for (var i = 6 * (updataRow - 1); i < count; i++) {
                    parkName.push(result[i].ParkName + "  ");
                    berthsUseness.push(result[i].BerthsUseness);
                    berthsUse.push(result[i].BerthsUse);
                }
            } else {
                for (var i = 6 * (updataRow - 1); i < 6 * updataRow; i++) {
                    parkName.push(result[i].ParkName + "  ");
                    berthsUseness.push(result[i].BerthsUseness);
                    berthsUse.push(result[i].BerthsUse);
                }
            }
        }
    });
}

//加载泊位使用率数据
function loadGetBerthsecsUtilization() {
    getData(1);

    setInterval(function () {
        //myChart2.clear();
        parkName.splice(0, parkName.length);
        berthsUseness.splice(0, berthsUseness.length);
        berthsUse.splice(0, berthsUse.length);
        if (updataRow == undefined)
            updataRow = 1;
        updataRow = updataRow + 1;
        if ((updataRow - 1) * 6 >= count) {
            updataRow = 1;
        }
        getData(updataRow);
        myChart2.setOption(option2);
    }, 7000);

    var option2 = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
            }
        },
        grid: {
            top: 0,
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            axisLabel: {
                show: true,
                textStyle: {
                    color: '#fff'
                }
            }
        },
        yAxis: {
            show: true,
            type: 'category',
            data: parkName,
            axisLabel: {
                show: true,
                textStyle: {
                    color: '#fff'
                }
            }
        },
        series: [
            {
                name: '已使用泊位',
                type: 'bar',
                stack: '总量',
                itemStyle: {
                    normal: { color:'#FF0000' }
                },
                label: {
                    normal: {
                        show: true,
                        position: 'insideRight'
                    }
                },
                data: berthsUse
            },
            {
                name: '未使用泊位',
                type: 'bar',
                stack: '总量',
                itemStyle: {
                    normal: { color: '#48B373' }
                },
                label: {
                    normal: {
                        show: true,
                        position: 'insideRight'
                    }
                },
                data: berthsUseness
            }
        ]
    };
    myChart2.setOption(option2);
}

//加载地磁图表数据
function loadSensorsDetails(data, total) {
    myChart1.clear();
    var option = {
        title: {
            text: total,
            subtext: '总数',
            left: 'center',
            top: '30%',
            padding: [24, 0],
            textStyle: {
                color: '#fff',
                fontSize: 18,
                align: 'center'
            }
        },
        backgroundColor: 'rgba(255,255,255,0)',
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}: {c} ({d}%)"
        },
        legend: {
            orient: 'horizontal',
            top: '2%',
            left: 'center',
            data: ['离线地磁','在线地磁'],
            textStyle: {
                fontSize: 12,
                color: '#6cbbe6'
            }
        },
        series: [{
            name: '数目',
            type: 'pie',
            selectedMode: 'single',
            center: ['50%', '60%'],
            radius: ['40%', '58%'],
            color: ['#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B', '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD'],
            label: {
                normal: {
                    position: 'outside',
                    formatter: '{b}',
                    textStyle: {
                        color: '#3db3cb',
                        fontSize: 10
                    }
                }
            },
            labelLine: {
                normal: {
                    show: true,
                    lineStyle: {
                        color: '#3db3cb'
                    }
                }
            },
            data: data
        }]
    };
    myChart1.setOption(option);
}



