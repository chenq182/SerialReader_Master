#!/usr/bin/python
# -*- coding: utf-8 -*-
__metaclass__ = type
from ComThread import *
from HttpThread import *
from TcpThread import *

if __name__ == '__main__':
	com = ComThread()
	com.start()

	plain = HttpThread(8081, 'plain')
	html = HttpThread(8080, 'html')
	plain.start()
	html.start()

	server = ThreadedTCPServer(('0.0.0.0', 10000), ThreadedTCPRequestHandler)
	server_thread = threading.Thread(target=server.serve_forever)
	server_thread.start()
