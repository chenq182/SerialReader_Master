#!/usr/bin/python
import socket
import sys
from ZigbeeSerial import *

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_address = ('', 10000)
sock.bind(server_address)
print >>sys.stderr, 'starting up on %s port %s' % sock.getsockname()
sock.listen(1)

while True:
	print >>sys.stderr, 'waiting for a connection'
	connection, client_address = sock.accept()
	try:
		print >>sys.stderr, 'connection from', client_address
		try:
			ser = serial.Serial('/dev/ttyUSB0', 115200)
		except Exception, e:
			print >>sys.stderr, 'open serial failed.'
			exit(1)
		buff = []
		while True:
			s = ser.read().encode('hex')
			buff.append(s)
			if s=='7e':
				while len(buff)>=55:
					i = buff.index('7e',54)
					msg = Message(buff[0:i+1])
					del buff[0:i+1]
					if msg.v == True:
						connection.sendall(msg.csv())
	finally:
		connection.close()
