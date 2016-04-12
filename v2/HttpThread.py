#!/usr/bin/python
# -*- coding: utf-8 -*-
__metaclass__ = type
import threading, socket, select, time
from ComThread import *

class HttpThread(threading.Thread):

	def run(self):
		connections = {}; requests = {}; responses = {}
		while True:
			events = self.__epoll.poll(1)
			for fileno, event in events:
				if fileno == self.__serversocket.fileno():
					connection, address = self.__serversocket.accept()
					connection.setblocking(0)
					self.__epoll.register(connection.fileno(), select.EPOLLIN)
					connections[connection.fileno()] = connection
					requests[connection.fileno()] = b''
					if self.__text == 'plain':
						responses[connection.fileno()] = self.__plain()
					if self.__text == 'html':
						responses[connection.fileno()] = self.__html()
				elif event & select.EPOLLIN:
					requests[fileno] += connections[fileno].recv(1024)
					EOL1 = b'\n\n'
					EOL2 = b'\n\r\n'
					if EOL1 in requests[fileno] or EOL2 in requests[fileno]:
						self.__epoll.modify(fileno, select.EPOLLOUT)
				elif event & select.EPOLLOUT:
					byteswritten = connections[fileno].send(responses[fileno])
					responses[fileno] = responses[fileno][byteswritten:]
					if len(responses[fileno]) == 0:
						self.__epoll.modify(fileno, 0)
						connections[fileno].shutdown(socket.SHUT_RDWR)
				elif event & select.EPOLLHUP:
					self.__epoll.unregister(fileno)
					connections[fileno].close()
					del connections[fileno]
		return

	def __init__(self, port=8080, text='plain'):
		threading.Thread.__init__(self)
		self.__serversocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
		self.__serversocket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
		self.__serversocket.bind(('0.0.0.0', port))
		self.__serversocket.listen(1)
		self.__serversocket.setblocking(0)
		self.__epoll = select.epoll()
		self.__epoll.register(self.__serversocket.fileno(), select.EPOLLIN)
		self.__text = text
		if text != 'plain':
			self.__text = 'html'

	def __del__(self):
		self.__epoll.unregister(self.__serversocket.fileno())
		self.__epoll.close()
		self.__serversocket.close()
		threading.Thread.__del__(self)

	def __plain(self):
		text  = b'HTTP/1.0 200 OK\r\n'
		text += b'Content-Type: text/plain\r\n\r\n'
		for key in ComThread.NodeList:
			text += ComThread.NodeList[key].csv()
		return text

	def __html(self):
		text  = 'HTTP/1.0 200 OK\r\n'
		text += 'Content-Type: text/html\r\n\r\n'
		text += '''
<!DOCTYPE HTML>
<html>
<head>
<title>Overview</title>
<style type="text/css">
.datalist{
	border:1px solid #0058a3;	/* 表格边框 */
	font-family:Arial;
	border-collapse:collapse;	/* 边框重叠 */
	background-color:#eaf5ff;	/* 表格背景色 */
	font-size:14px;
}
.datalist caption{
	padding-bottom:5px;
	font:bold 1.4em;
	text-align:left;
}
.datalist th{
	border:1px solid #0058a3;	/* 行名称边框 */
	background-color:#4bacff;	/* 行名称背景色 */
	color:#FFFFFF;				/* 行名称颜色 */
	font-weight:bold;
	padding-top:4px; padding-bottom:4px;
	padding-left:12px; padding-right:12px;
	text-align:center;
}
.datalist td{
	border:1px solid #0058a3;	/* 单元格边框 */
	text-align:center;
	padding-top:4px; padding-bottom:4px;
	padding-left:10px; padding-right:10px;
}
.datalist tr.altrow{
	background-color:#c7e5ff;	/* 隔行变色 */
}
</style>
</head>
<body>
<center>
<table class="datalist">
<caption>Overview</caption>
<tr>
<th scope="col">Time</th>
<th scope="col">ID</th>
<th scope="col">pID</th>
<th scope="col">Temp</th>
<th scope="col">Hum</th>
<th scope="col">Photo</th>
<th scope="col">Voltage</th>
<th scope="col">Current</th>
<th scope="col">Power</th>
<th scope="col">Energy</th>
<th scope="col">Count</th>
</tr>
		'''
		i = 1
		for key in ComThread.NodeList:
			if i % 2 == 0:
				text += '<tr class="altrow">'
			else:
				text += '<tr>'
			a = ComThread.NodeList[key]
			t = time.strptime(a.getTime(), '%Y%m%d%H%M%S')
			text += '<td>' + time.strftime('%Y-%m-%d %H:%M:%S', t) + '</td>'
			text += '<td>' + a.getID() + '</td>'
			text += '<td>' + a.getPID() + '</td>'
			text += '<td>' + a.getTemp() + '</td>'
			text += '<td>' + a.getHum() + '</td>'
			text += '<td>' + a.getPhoto() + '</td>'
			text += '<td>' + a.getVoltage() + '</td>'
			text += '<td>' + a.getCurrent() + '</td>'
			text += '<td>' + a.getPower() + '</td>'
			text += '<td>' + a.getEnergy() + '</td>'
			text += '<td>' + a.getCount() + '</td></tr>'
			i += 1
		text += '''
</table>
</center>
</body>
</html>
		'''
		return text

if __name__ == '__main__':
	t = ComThread()
	v = HttpThread(8081, 'plain')
	u = HttpThread(8080, 'html')
	t.start()
	v.start()
	u.start()
