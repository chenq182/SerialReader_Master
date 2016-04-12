#!/usr/bin/python
# -*- coding: utf-8 -*-
__metaclass__ = type
import threading, socket, SocketServer
from ComThread import *

class ThreadedTCPRequestHandler(SocketServer.BaseRequestHandler):

	def handle(self):
		while True:
			response = ""
			l = len(ComThread.Buff)
			if l > 0:
				for i in range(l):
					response += ComThread.Buff[i]
				del ComThread.Buff[0:l]
				self.request.sendall(response)

class ThreadedTCPServer(SocketServer.ThreadingMixIn, SocketServer.TCPServer):
	pass

if __name__ == '__main__':
	t = ComThread()
	t.start()
	server = ThreadedTCPServer(('0.0.0.0', 10000), ThreadedTCPRequestHandler)
	server_thread = threading.Thread(target=server.serve_forever)
	server_thread.start()
