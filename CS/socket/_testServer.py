#!/usr/bin/python
import socket
import MySQLdb

s = socket.socket()
host = '10.170.34.211'
port = 14108
s.connect((host, port))
print s.recv(1024)
s.close

try:
	conn = MySQLdb.Connect(host='localhost',user='root',passwd='123456',port=3306)
	conn.select_db('zigbee')
	cursor = conn.cursor()
	cursor.execute("SELECT * FROM Overload")
	result = cursor.fetchall()
	for record in result:
		print "%s\t%s\t%s\t%s\t%s\t%s\t%s\t%s\t%s\t%s\t%s\t%s\t%s" % record
	cursor.close()
	conn.close()
except MySQLdb.Error,e:
     print "Mysql Error %d: %s" % (e.args[0], e.args[1])
