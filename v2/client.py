#!/usr/bin/python
import socket
import sys

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_address = ('192.168.1.104', 10000)
print >>sys.stderr, 'connecting to %s port %s' % server_address
sock.connect(server_address)

try:
	while True:
		data = sock.recv(1000)
		print >>sys.stderr, 'received "%s"' % data
finally:
	print >>sys.stderr, 'closing socket'
	sock.close()
