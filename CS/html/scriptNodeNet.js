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
        var marker = new BMap.Marker(point);
        marker.setTitle("ID:" + id);
        map.addOverlay(marker);
        marker.disableMassClear();
        var markerMenu = new BMap.ContextMenu();
        marker.addContextMenu(markerMenu);
        var menuItem_Del = new BMap.MenuItem("隐藏", marker.Remove(marker));
        markerMenu.addItem(menuItem_Del);
    }
    // 设置右键菜单
    menu.addItem(menuItem_Now);
    menu.addSeparator();
    menu.addItem(menuItem_Offline);
    menu.addSeparator();
    menu.addItem(menuItem_MapType);
}
BMap.Marker.prototype.Remove = function (o) {
    function a(){
        map.removeOverlay(o);
    }
    return a;
}
function shiftMapType() {
    if (map.getMapType() == BMAP_NORMAL_MAP) {
        map.setMapType(BMAP_SATELLITE_MAP);
        menuItem_MapType.setText("卫星地图");
        lineColor = "white";
    }
    else {
        map.setMapType(BMAP_NORMAL_MAP);
        menuItem_MapType.setText("普通视图");
        lineColor = "blue";
    }
}
function shiftOffline() {
    var text = "30";
    var response = prompt("请输入断线阈值(秒)：", text);
    var rgExp = /^([0-9]+)$/
    var time_str = response.match(rgExp);
    if (time_str != null) {
        offLine = time_str[1];
        menuItem_Offline.setText("断线阈值: " + offLine + "s");
        ///////
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
        history = new Date(time_str[1], time_str[2] - 1, time_str[3],
            time_str[4], time_str[5], time_str[6]);
        timerOff(history);
    }
}
function timerOn(newInterval) {
    if (interval > 0)
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
function watchNow() {
    history = new Date();
    watchBack();
}
function watchBack() {
    map.clearOverlays();
    var n = window.external.initHisNodeCount(history.Format("yyyyMMddhhmmss"));
    var i = 0;
    var lid, ltime, lngA, latA, lngB, latB;
    for (i = 0; i < n; i++) {
        lid = window.external.lid(i);
        lpid = window.external.lpid(i);
        if (lid == lpid)
            continue;
        var ltime_str = window.external.ltime(i);
        var rgExp = /^([0-9]+)\/([0-9]+)\/([0-9]+)\s([0-9]+):([0-9]+):([0-9]+)$/
        var time_str = ltime_str.match(rgExp);
        ltime = new Date(time_str[1], time_str[2] - 1, time_str[3],
            time_str[4], time_str[5], time_str[6]);
        lngA = window.external.lngA(i);
        latA = window.external.latA(i);
        lngB = window.external.lngB(i);
        latB = window.external.latB(i);
        var polyline = new BMap.Polyline([
            new BMap.Point(lngA, latA),
            new BMap.Point(lngB, latB)
        ], { strokeColor: lineColor, strokeWeight: 2, strokeOpacity: 0.5 });
        var deadline = new Date();
        deadline.setTime(ltime.getTime() + offLine * 1000);
        if (deadline < history)
            polyline.setStrokeStyle("dashed");//solid
        map.addOverlay(polyline);
        polyline.addEventListener("click", polyline.Info(ltime, lid, lpid));
    }
}
BMap.Polyline.prototype.Info = function (t,a,b) {
    function info() {
        alert("节点" + a + " 至 节点" + b + " 传输时间:\n"
            + t.Format("yyyy年MM月dd日 hh时mm分ss秒"));
    }
    return info;
}
