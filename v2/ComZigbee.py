#!/usr/bin/python
__metaclass__ = type
import serial, time, sys

class Message:
	def __init__(self, raw):
		self.__raw = self.__decode(raw)
		self.v = False
		if self.__valid():
			self.__parse()
			self.v = True

	def sql(self):
		sql = ''
		if self.v:
			sql = "INSERT INTO Msg (ID,MTime,pID,MTemp,MHum,MPhoto,"+\
				"MVoltage,MCurrent,MPower,MEnergy,MCount) VALUES "+\
				"('%s','%s','%s','%.2f','%.2f','%.2f','%.2f','%.2f','%.2f','%.4f','%s');" % \
				(self.__nid,self.__now,self.__pid,self.__temp,self.__hum,self.__photo,\
				self.__voltage,self.__current,self.__power,self.__energy,self.__count)
		return sql

	def csv(self):
		csv = ''
		if self.v:
			csv = "%s,%s,%s,%.2f,%.2f,%.2f,%.2f,%.2f,%.2f,%.4f,%s;" % \
				(self.__nid,self.__now,self.__pid,self.__temp,self.__hum,self.__photo, \
				self.__voltage,self.__current,self.__power,self.__energy,self.__count)
		return csv

	def getID(self):
		return "%s" % self.__nid
	def getTime(self):
		return "%s" % self.__now
	def getPID(self):
		return "%s" % self.__pid
	def getTemp(self):
		return "%.2f" % self.__temp
	def getHum(self):
		return "%.2f" % self.__hum
	def getPhoto(self):
		return "%.2f" % self.__photo
	def getVoltage(self):
		return "%.2f" % self.__voltage
	def getCurrent(self):
		return "%.2f" % self.__current
	def getPower(self):
		return "%.2f" % self.__power
	def getEnergy(self):
		return "%.4f" % self.__energy
	def getCount(self):
		return "%s" % self.__count

	def __decode(self, raw):
		del raw[0:raw.index('7e')]
		while len(raw)>1 and raw[1]=='7e':
			del raw[0]
		if len(raw) < 55:
			return raw
		i = raw.index('7e',54)
		j = 1
		while j < i:
			if raw[j]=='7d' and raw[j+1]=='5e':
				raw[j]='7e'
				del raw[j+1]
				i = i-1
			if raw[j]=='7d' and raw[j+1]=='5d':
				del raw[j+1]
				i = i-1
			j = j+1
		return raw[0:i+1]


	def __valid(self):
		s = self.__raw
		if len(s) != 55:
			return False
		if s[0]!='7e' or s[54]!='7e':
			return False
		if s[1]!='45' or s[2]!='00':
			return False
		if s[26]!='ca' or s[27]!='fe':
			return False
		if s[20]=='00' and s[21]=='00':
			return False
		return True

	def __parse(self):
		s = self.__raw
		for i in range(len(s)):
			s[i] = int(s[i],16)
		self.__nid = s[18]*256+s[19]
		self.__now = time.strftime('%Y%m%d%H%M%S',time.localtime(time.time()))
		self.__pid = s[22]*256+s[23]
		self.__temp = (s[33]*256+s[34])*0.01-39.6
		tmp = s[37]*256+s[38]
		self.__hum = -4+tmp*0.0405-tmp*tmp*0.00000028 + (self.__temp-25)*(0.01+0.00008*tmp)
		self.__photo = s[35]*256+s[36]*0.085
		self.__voltage = (s[39]*65536+s[40]*256+s[41])*214.8/1805143
		self.__current = (s[42]*65536+s[43]*256+s[44])*1234.0/18976
		self.__power = ((255-s[45])*16777216+(255-s[46])*65536+(255-s[47])*256+(255-s[48]))*2.5258/10000
		if self.__power<0:
			self.__power = 0
		self.__energy = (s[49]*65536+s[50]*256+s[51])/3200.0
		self.__count = s[20]*256+s[21]


if __name__ == '__main__':
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
				print msg.csv()
