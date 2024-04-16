
//加载统计图1数据
function  loadoOption1(data, money){
    myChart1.clear();
    var option = {
        title: {
            text: money,
            subtext: '总应收',
            left: 'center',
            top: '50%',
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
            data: ['现金', '在线支付', '在线追缴', '现金追缴', '未付金额'],
            textStyle: {
                fontSize: 12,
                color: '#6cbbe6'
            }
        },
        series: [{
            name: '金额',
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

//加载统计图2
function loadoOption2(data, money) {
    myChart2.clear();
    var option = {
        title: {
            text: money,
            subtext: '总应收',
            left: 'center',
            top: '50%',
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
            data: ['现金', '在线支付', '在线追缴', '现金追缴', '未付金额'],
            textStyle: {
                fontSize: 12,
                color: '#6cbbe6'
            }
        },
        series: [{
            name: '金额',
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
    myChart2.setOption(option);
}

//加载统计图3数据
function loadoOption3(month, totals){
    myChart3.clear();

    var option = {
        title: {
            text: '最近6个月停车次数汇总',
            show: true,
            textStyle: {
                fontWeight: 'normal',
                fontSize: 22,
                color: '#3db3cb'
            },
            left: '2%',
            top: '2%'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                lineStyle: {
                    color: '#3db3cb'
                }
            }
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [{
            type: 'category',
            boundaryGap: false,
            axisLine: {
                lineStyle: {
                    color: 'rgba(128, 128, 128, 0.5)'
                }
            },
            axisLabel: {
                margin: 10,
                textStyle: {
                    fontSize: 14,
                    color: '#999'
                }
            },
            data: month// ['2018-03', '2018-04', '2018-05', '2018-06', '2018-07', '2018-08']
        }],
        yAxis: [{
            type: 'value',
            name: '',
            axisTick: {
                show: false
            },
            axisLine: {
                show:false,
                lineStyle: {
                    color: '#fff'
                }
            },
            axisLabel: {
                margin: 10,
                textStyle: {
                    fontSize: 14,
                    color: '#799dff'
                }
            },
            splitLine: {
                lineStyle: {
                    type: 'dashed',
                    color: 'rgba(121, 157, 255, 0.5)'
                }
            }
        }],
        series: [{
            name: '停车次数',
            type: 'line',
            smooth: true,
            symbol: 'circle',
            symbolSize: 5,
            showSymbol: false,
            lineStyle: {
                normal: {
                    width: 1
                }
            },
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(0, 136, 212, 0.2)'
                    }, {
                        offset: 1,
                        color: 'rgba(0, 136, 212, 0)'
                    }], false),
                    shadowColor: 'rgba(0, 0, 0, 0.1)',
                    shadowBlur: 10
                }
            },
            itemStyle: {
                normal: {
                    color: 'rgb(0,136,212)',
                    borderColor: 'rgba(0,136,212,0.2)',
                    borderWidth: 12

                }
            },
            data: totals// [120, 110, 145, 122, 165, 150]
        }, ]
    }
    myChart3.setOption(option);
}

//加载统计图4数据
function loadoOption4(data) {
    myChart4.clear();
    var option = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            top: '5%',
            left: '5%',
            right: '5%',
            bottom: '5%',
            containLabel: true,
        },
        xAxis: {
            type: 'value',
            position: 'top',
            splitLine: { show: false },
            boundaryGap: [0, 0.01],
            axisLabel: {
                textStyle: {
                    color: '#6cbbe6'
                }
            },
        },
        yAxis: {
            type: 'category',
            splitLine: { show: false },
            data: ['包月车辆', '今日新增', '今日到期'],
            axisLabel: {
                textStyle: {
                    fontSize: 12,
                    color: '#6cbbe6'
                }
            },
        },
        series: [
            {
                name: '数量',
                itemStyle: {
                    normal: {
                        color: function (params) {
                            var colorList = [
                                //'#C1232B','#B5C334','#FCCE10','#E87C25','#27727B',
                                '#FAD860', '#F3A43B', '#60C0DD',//'#FE8463', '#9BCA63',
                                // '#D7504B','#C6E579','#F4E001','#F0805A','#26C0C0'
                            ];
                            return colorList[params.dataIndex]
                        },
                        shadowBlur: 20,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                },
                barWidth: 10,
                barGap: 20,
                type: 'bar',
                data: data
            }
        ]
    };
    myChart4.setOption(option);
}

//加载统计图5数据
function loadoOption5(totalCount){
    myChart5.clear();
    var option = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            top:'5%',
            left: '5%',
            right: '5%',
            bottom: '5%',
            containLabel: true,
        },
        xAxis: {
            type: 'value',
            position:'top',
            splitLine: {show: false},
            boundaryGap: [0, 0.01],
            axisLabel:{
                textStyle:{
                    color: '#6cbbe6'
                }
            },
        },
        yAxis: {
            type: 'category',
            splitLine: {show: false},
            data: ['微信用户总数', '今日新增用户','活跃用户'],
            axisLabel:{
                textStyle:{
                    fontSize: 12,
                    color: '#6cbbe6'
                }
            },
        },
        series: [
            {
                name: '数量',
                itemStyle: {
                    normal: {
                        color: function(params) {
                            var colorList = [
                                //'#C1232B','#B5C334','#FCCE10','#E87C25','#27727B',
                                '#FE8463','#9BCA63','#FAD860'//,'#F3A43B','#60C0DD',
                               // '#D7504B','#C6E579','#F4E001','#F0805A','#26C0C0'
                            ];
                            return colorList[params.dataIndex]
                        },
                        shadowBlur: 20,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                },
                barWidth:10,
                barGap:20,
                type: 'bar',
                data: totalCount
            }
        ]
    };
    myChart5.setOption(option);
}



