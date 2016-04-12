#!/usr/bin/python
__metaclass__ = type
import threading, sys
from ComZigbee import *

class ComThread(threading.Thread):

	NodeList = {}
	Buff = []

	def run(self):
		while True:
			s = self.ser.read().encode('hex')
			self.buff.append(s)
			if s=='7e':
				while len(self.buff)>=55:
					i = self.buff.index('7e',54)
					msg = Message(self.buff[0:i+1])
					del self.buff[0:i+1]
					if msg.v == True:
						ComThread.NodeList[msg.getID()] = msg
						if len(ComThread.Buff) > 10000:
							ComThread.Buff = []
						ComThread.Buff.append(msg.csv())
		return

	def __init__(self):
		threading.Thread.__init__(self)
		try:
			self.ser = serial.Serial('/dev/ttyUSB0', 115200)
		except Exception, e:
			print >>sys.stderr, 'open serial failed.'
			exit(1)
		self.buff = []

if __name__ == '__main__':
	t = ComThread()
	t.start()
