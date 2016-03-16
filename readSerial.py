#!/usr/bin/python
import MySQLdb
import serial
import time
import sys

def valid(s):
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

def parse(s):
	for i in range(len(s)):
		s[i] = int(s[i],16)
	nid = s[18]*256+s[19]
	now = time.strftime('%Y%m%d%H%M%S',time.localtime(time.time()))
	pid = s[22]*256+s[23]
	temp = (s[33]*256+s[34])*0.01-39.6
	tmp = s[37]*256+s[38]
	hum = -4+tmp*0.0405-tmp*tmp*0.00000028 + (temp-25)*(0.01+0.00008*tmp)
	photo = s[35]*256+s[36]*0.085
	voltage = (s[39]*65536+s[40]*256+s[41])*214.8/1805143
	current = (s[42]*65536+s[43]*256+s[44])*1234.0/18976
	power = ((255-s[45])*16777216+(255-s[46])*65536+(255-s[47])*256+(255-s[48]))*2.5258/10000
	if power<0:
		power = 0
	energy = (s[49]*65536+s[50]*256+s[51])/3200.0
	count = s[20]*256+s[21]

	sql = "INSERT INTO Msg (ID,MTime,pID,MTemp,MHum,MPhoto,MVoltage,MCurrent,MPower,MEnergy,MCount) VALUES ('%s','%s','%s','%.2f','%.2f','%.2f','%.2f','%.2f','%.2f','%.4f','%s');" % \
		(nid,now,pid,temp,hum,photo,voltage,current,power,energy,count)
	try:
		cursor.execute(sql)
		conn.commit()
	except MySQLdb.Error, e:
		#print "Mysql Error %d: %s" % (e.args[0], e.args[1])
		pass
	#print now,nid,count

def singleton():
	global buff
	del buff[0:buff.index('7e')]
	while len(buff)>1 and buff[1]=='7e':
		del buff[0]
	if len(buff)<55:
		return
	i = buff.index('7e',54)
	j = 1
	while j < i:
		if buff[j]=='7d' and buff[j+1]=='5e':
			buff[j]='7e'
			del buff[j+1]
			i = i-1
		if buff[j]=='7d' and buff[j+1]=='5d':
			del buff[j+1]
			i = i-1
		j = j+1
	s = buff[0:i+1]
	del buff[0:i+1]
	if valid(s):
		parse(s)

try:
	ser = serial.Serial('/dev/ttyUSB0', 115200)
except Exception, e:
	print 'open serial failed.'
	exit(1)

try:
	conn = MySQLdb.Connect(host='localhost',user='root',passwd='323',port=3306)
	conn.select_db('zigbee')
	cursor = conn.cursor()
except MySQLdb.Error, e:
	print "Mysql Error %d: %s" % (e.args[0], e.args[1])
	exit(2)

buff = []
while True:
	s = ser.read().encode('hex')
	buff.append(s)
	if s=='7e':
		while len(buff)>=55:
			singleton()
