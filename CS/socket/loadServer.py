#!/usr/bin/python
import socket
import os, re, string

s = socket.socket()

host = '10.170.34.211'
port = 14108
s.bind((host, port))

s.listen(5)
while True:
	c, addr = s.accept()
#Time
	time = os.popen('date "+%Y%m%d%H%M%S"').read().strip()
#CPU
	output = os.popen('iostat').read()
	line = output.split("\n")
	m = re.findall(r'(\d+\.\d+)$', line[3])
	cpu = "CPU:"+str(100.0-string.atof(m[0]))+"%"
#Memory
	output = os.popen('free -m').read()
	line = output.split("\n")
	m = re.findall(r'Mem.\s+(\d+)\s+(\d+)\s+\d+', line[1])
	a, b = m[0]
	mem = 'Mem:{:.2f}%'.format(100.0*float(b)/float(a))+"("+a+"M)"
#Disk
	output = os.popen('df -H |grep /dev/sd').read()
	line = output.split("\n")
	m = re.findall(r'(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+(\S+)\s+', line[0])
	name,total,used,unuse,percent = m[0]
	disk = "Disk:"+percent+"("+total+")"
#Uptime
	output = os.popen('uptime').read()
	m = re.findall(r'\s+(.*)\n+',output)
	a = m[0]
	overall = a

	c.send(time+":\n"+cpu+"\t"+mem+"\t"+disk+"\n"+overall)

	c.close()
