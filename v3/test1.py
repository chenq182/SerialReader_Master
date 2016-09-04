#!/usr/bin/python
__metaclass__ = type
import sys, time, threading, SocketServer

class ThreadedRequestHandler(SocketServer.BaseRequestHandler):
    def handle(self):
        while True:
            h = "%x" % (0x26)
            self.request.send(h.decode('hex'))
            data = self.request.recv(8).encode('hex')
            cur_thread = threading.currentThread()
            ip, port = self.client_address
            print >>sys.stderr, '%s:[%s][%s] %s' % (cur_thread.getName(), ip, port, data)
            time.sleep(5)

class ThreadedServer(SocketServer.ThreadingMixIn, SocketServer.TCPServer,):
    pass


if __name__ == '__main__':
    import socket, threading

    address = ('', 8899)
    server = ThreadedServer(address, ThreadedRequestHandler)
    ip, port = server.server_address

    t = threading.Thread(target=server.serve_forever)
    t.setDaemon(False)
    t.start()
    print >>sys.stderr, '%s: Server[%s][%s]' % (t.getName(), ip, port)
