function loadMap() {
    // 初始化地图
    var first_locate = new BMap.Point(121.553000, 31.279000);
    map.centerAndZoom(first_locate, 19);
    map.enableScrollWheelZoom();
    // 载入已知节点
    var i = window.external.leftNodesNum();
    while ( i-- > 0 ) {
        // 读取ID及坐标
        var id = window.external.popID();
        var point = new BMap.Point(window.external.popLng(), window.external.popLat());
        var html = '<label class="BMapLabel" style="font: 12px arial,simsun; color: rgb(255, 102, 0);"><span id="Value'
        + id + '"></span></label>';
        var richMarker = new BMapLib.RichMarker(html, point, { "anchor": new BMap.Size(0, -8) });
        map.addOverlay(richMarker);
        // 双击移除节点
        richMarker.addEventListener("dblclick", removeRichMarker);
        // 初始化信息：ID
        document.getElementById("Value" + id).innerText = "<<"+id;
    }
    // 设置右键菜单
    menu.addItem(menuItem_Status);
    menu.addSeparator();
    menu.addItem(menuItem_Now);
    menu.addSeparator();
    menu.addItem(menuItem_MapType);
}
function removeRichMarker(e) {
    var rgExp = /<span id="Value([0-9]*)">/
    var id_str = e.target.getContent().match(rgExp);
    if (window.external.removeNode(id_str[1]))
        alert("节点" + id_str[1] + "已删除");
    map.removeOverlay(e.target);
}
function shiftStatus() {
    switch (valueStatus) {
        case 'ID':
            valueStatus = 'MVoltage';
            menuItem_Status.setText("电压 / V");
            break;
        case 'MVoltage':
            valueStatus = 'MCurrent';
            menuItem_Status.setText("电流 / mA");
            break;
        case 'MCurrent':
            valueStatus = 'MPower';
            menuItem_Status.setText("功率 / W");
            break;
        case 'MPower':
            valueStatus = 'MEnergy';
            menuItem_Status.setText("电量 / kWh");
            break;
        case 'MEnergy':
            valueStatus = 'MTemp';
            menuItem_Status.setText("温度 / °C");
            break;
        case 'MTemp':
            valueStatus = 'MHum';
            menuItem_Status.setText("湿度 / %");
            break;
        case 'MHum':
            valueStatus = 'MPhoto';
            menuItem_Status.setText("光照 / kLux");
            break;
        case 'MPhoto':
            valueStatus = 'MTime';
            menuItem_Status.setText("时间");
            break;
        case 'MTime':
        default:
            valueStatus = 'ID';
            menuItem_Status.setText("ID");
    }
    reset();
}
function shiftMapType() {
    if (map.getMapType() == BMAP_NORMAL_MAP) {
        map.setMapType(BMAP_SATELLITE_MAP);
        menuItem_MapType.setText("卫星地图");
    }
    else {
        map.setMapType(BMAP_NORMAL_MAP);
        menuItem_MapType.setText("普通视图");
    }
}
function shiftTime() {
    var now_str = (new Date()).Format("yyyy-MM-dd hh:mm:ss");
    var response = prompt("请输入时间(yyyy-MM-dd hh:mm:ss)或实时更新周期(秒)：", now_str);
    var rgExp = /^([0-9]+)-([0-9]+)-([0-9]+)\s([0-9]+):([0-9]+):([0-9]+)$/
    var time_str = response.match(rgExp);
    if (time_str == null) {
        rgExp = /^([0-9]+)$/
        time_str = response.match(rgExp);
        if (time_str != null)
            timerOn(time_str[1]);
        else
            alert("输入格式不合法!");
    }
    else {
        history = new Date(time_str[1], time_str[2]-1, time_str[3],
            time_str[4], time_str[5], time_str[6]);
        timerOff(history);
    }
}
function watchNow() {
    var i = window.external.initNodeCount(valueStatus);
    var id, value;
    while (--i >= 0) {
        id = window.external.getID(i);
        value = window.external.getValue(i);
        document.getElementById("Value" + id).innerText = value;
    }
}
function watchBack() {
    var i = window.external.initHisNodeCount(valueStatus,
        history.Format("yyyyMMddhhmmss"));
    var id, value;
    while (--i >= 0) {
        id = window.external.getID(i);
        value = window.external.getValue(i);
        document.getElementById("Value" + id).innerText = value;
    }
}
function timerOn(newInterval) {
    if(interval > 0)
        window.clearInterval(timer);
    interval = newInterval;
    menuItem_Now.setText("实时(/" + interval + "s)");
    timer = window.setInterval(watchNow, interval * 1000);
}
function timerOff(newTime) {
    if (interval > 0) {
        window.clearInterval(timer);
        interval = 0;
    }
    history = newTime;
    menuItem_Now.setText(history.Format("hh:mm:ss"));
    watchBack();
}
function reset() {
    if (interval > 0)
        timerOn(interval);
    else
        timerOff(history);
}
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
