function loadMap() {
    // 初始化地图
    var first_locate = new BMap.Point(121.553000, 31.279000);
    map.centerAndZoom(first_locate, 19);
    map.enableScrollWheelZoom();
    // 设置右键菜单
    map.addEventListener("rightclick", rightMap);
    // 载入已知节点
    var i = window.external.leftNodesNum();
    while ( i-- > 0 ) {
        // 读取ID及坐标
        var id = window.external.popID();
        var point = new BMap.Point(window.external.popLng(), window.external.popLat());
        var marker = new BMap.Marker(point);
        marker.setTitle("ID:" + id);
        map.addOverlay(marker);
        marker.addEventListener("dblclick", removeMarker);
    }
}
function rightMap(e) {
    var menu = new BMap.ContextMenu();
    map.addContextMenu(menu);
    var menuItem_NewNode = new BMap.MenuItem("新建节点", function () {
        putAndSend(e);
    });
    var menuItem_MapType = (map.getMapType() == BMAP_NORMAL_MAP) ?
          new BMap.MenuItem("卫星地图", function () {
              map.setMapType(BMAP_SATELLITE_MAP);
          })
        : new BMap.MenuItem("普通视图", function () {
            map.setMapType(BMAP_NORMAL_MAP);
        });
    menu.addItem(menuItem_NewNode);
    menu.addItem(menuItem_MapType);
}
function putAndSend(e) {
    // 新建Marker
    var point = new BMap.Point(e.point.lng, e.point.lat);
    var marker = new BMap.Marker(point);
    map.addOverlay(marker);
    // 标记编辑状态
    var label = new BMap.Label("编辑中", { offset: new BMap.Size(20, -10) });
    marker.setLabel(label);
    // 双击：删除节点
    marker.setTitle("删除：双击节点");
    marker.addEventListener("dblclick", removeMarker);
    // 单击：信息显示
    marker.addEventListener("click", genWindow);
    // 拖动：编辑节点
    marker.enableDragging();
    marker.addEventListener("dragend", function () {
        e.target.closeInfoWindow();
    });
}
function genWindow(e) {
    var htmlTitle = '节点ID：<input type="text" name="NodeID" size=3 />&nbsp&nbsp'
        + '<input type="button" value="提交" onClick="handleClick()" /><br />'
        + '坐标：(<span id="NodeLng">';
    var infoWindow = new BMap.InfoWindow(htmlTitle
        + e.target.getPosition().lng + '</span>，<span id="NodeLat">' + e.target.getPosition().lat + '</span>)<br />'
        + '');
    e.target.openInfoWindow(infoWindow);
}
function handleClick(e) {
    var id = document.getElementById("NodeID").value;
    var lng = document.getElementById("NodeLng").innerHTML;
    var lat = document.getElementById("NodeLat").innerHTML;
    if (!window.external.validID(id))
        alert("节点ID不合法！");
    else {
        var result = confirm("<录入信息>\nID:\t" + id
            + "\n经度:\t" + lng
            + "\n纬度:\t" + lat);
        if (result = true) {
            window.external.updateLocation(id, lng, lat);//refresh map
        }
    }
}
function removeMarker(e) {
    map.removeOverlay(e.target);
}
