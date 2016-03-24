#!/usr/bin/python
import MySQLdb
import socket
import time
import re
from mod_python import apache

timeout = 2
socket.setdefaulttimeout(timeout)

html = """
<html>
<head>
<title>Cluster Overload</title>
<link rel="stylesheet" type="text/css" href="load.css">
<script language="javascript" src="load.js"></script>
</head>
<body>
<center>
<table class="datalist">
<caption>Cluster Overload</caption>
<tr>
<th scope="col"></th>
<th scope="col">IP</th>
<th scope="col">CPU</th>
<th scope="col">Mem</th>
<th scope="col">Disk</th>
<th scope="col">Avg1</th>
<th scope="col">RunTime</th>
</tr>
"""

try:
	conn = MySQLdb.Connect(host='localhost',user='root',passwd='123456',port=3306)
	conn.select_db('zigbee')
	cursor = conn.cursor()
	cursor.execute("SELECT * FROM Overload")
	reader = cursor.fetchall()

	count = 0
	for record in reader:
		if count % 2 == 0:
			html += \
'<tr class="altrow">'
		else:
			html += \
'<tr>'
		count += 1

		s = socket.socket()
		host = record[0]
		port = 14108
		try:
			s.connect((host, port))
			overload = s.recv(1024)

			line = overload.split("\n")
			m = re.findall(r'(\d+):', line[0])
			time = m[0]
			m = re.findall(r'CPU:([\d\.]+)%\tMem:([\d\.]+)%\(([\dM]+)\)\sDisk:([\d\.]+)%\(([\dG]+)\)', line[1])
			cpu,mem,memall,disk,diskall = m[0]
			m = re.findall(r'up\s+([\d:]+[\smin]*),\s+(\d+)\s+users,\s+load\s+average:\s+([\d\.]+),\s+([\d\.]+),\s+([\d\.]+)', line[2])
			rtime,usrnum,avg1,avg5,avg15 = m[0]
			update_sql = "UPDATE Overload SET \
			Time='%s',CPU='%s',Mem='%s',MemAll='%s',\
			Disk='%s',DiskAll='%s',RunTime='%s',\
			UsrNum='%s',Avg01='%s',Avg05='%s',\
			Avg15='%s' WHERE IP='%s';" % \
			(time,cpu,mem,memall,disk,diskall,\
			rtime,usrnum,avg1,avg5,avg15,host)

			s.close
		except socket.timeout as e:
			update_sql = "UPDATE Overload SET \
			CPU='null',Mem='null',MemAll='null',\
			Disk='null',DiskAll='null',RunTime='null',\
			UsrNum='null',Avg01='null',Avg05='null',\
			Avg15='null' WHERE IP='%s';" % (host) 
			(time,cpu,mem,memall,disk,diskall,\
			rtime,usrnum,avg1,avg5,avg15) = \
			('','?.??','??.??','????M','??','??G','--','','--','','')
		cursor.execute(update_sql)
		conn.commit()
		html += """
<td><span onclick="setIP('"""+host+"""');"><input type="radio" name="write"></span></td>
<td>"""+host+"""</td>
<td>"""+cpu+"""%</td>
<td>"""+mem+"%("+memall+")"+"""</td>
<td>"""+disk+"%("+diskall+")"+"""</td>
<td>"""+avg1+"""</td>
<td>"""+rtime+"""</td>
</tr>
		"""

	html += """
</table>
</center>
</body>
</html>
	"""
	cursor.close()
	conn.close()
except MySQLdb.Error,e:
	print "Mysql Error %d: %s" % (e.args[0], e.args[1])

def index(req):
	req.log_error('handler')
	req.content_type = 'text/html'
	req.send_http_header()
	req.write(html)
