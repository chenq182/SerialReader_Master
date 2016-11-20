#!/usr/bin/python
__metaclass__ = type
import re, sys, time, socket, threading, SocketServer
import MySQLdb


class ThreadedRequestHandler(SocketServer.BaseRequestHandler):

    def handle(self):
        cur_thread = threading.currentThread()
        ip, port = self.client_address
        rematch = re.match(r'.+\.([0-9]+)$', ip)
        ipid = int(rematch.group(1))
        pID = 1####################
        ID = pID*1000+ipid
        try:
            conn = MySQLdb.Connect(host='10.170.31.119',user='root',passwd='sitemap',port=3306)
            conn.select_db('shiep')
        except Exception, e:
            print >>sys.stderr, "[WARN]('%s', %s): %s" % (ip, port, e)
            time.sleep(60)
            self.request.close()
            return
        try:
            cursor = conn.cursor()

            self.__init()
            time.sleep(5)
            ma = mA(self.__get("22"))
            val = []
            while True:
                ma = mA(self.__get("22"))
                v  =  V(self.__get("24"))
                hz = Hz(self.__get("25"))
                w  =  W(self.__get("26"))
                kwh=kWh(self.__get("29"))
                now= time.strftime('%Y%m%d%H%M%S',time.localtime(time.time()))
                val.append((ID, now, pID, hz, v, ma, w, kwh))
                if len(val) >= 12:
                    sql = "INSERT INTO Msg (ID,MTime,pID,MHum,MVoltage,MCurrent,MPower,MEnergy) VALUES " + \
                        "(%s,%s,%s,%s,%s,%s,%s,%s)"
                    cursor.executemany(sql, val)
                    val = []
                #sql = "INSERT INTO Msg (ID,MTime,pID,MHum,MVoltage,MCurrent,MPower,MEnergy) VALUES " + \
                #    "('%s','%s','%s','%s','%s','%s','%s','%s');\n" % \
                #    (ID, now, pID, hz, v, ma, w, kwh)
                #cursor.execute(sql)
                    conn.commit()
                time.sleep(5)
        except (socket.timeout, MySQLdb.Error), e:
            cursor.close()
            conn.close()
            print >>sys.stderr, "[WARN]('%s', %s): %s" % (ip, port, e)
            self.request.close()
            return

    def __get(self, s):
        self.request.send(s.decode('hex'))
        data = self.request.recv(8).encode('hex')
        return data

    def __init(self):
        self.request.send("eae530".decode('hex'))
        self.request.send("ea5abb".decode('hex'))
        self.request.send("8000007f".decode('hex'))
        self.request.send("8170010d".decode('hex'))
        self.request.send("8203a0da".decode('hex'))
        self.request.send("83001f5d".decode('hex'))
        self.request.send("85024434".decode('hex'))
        self.request.send("8a16d08f".decode('hex'))
        self.request.send("8efe165d".decode('hex'))
        self.request.send("eadc39".decode('hex'))


class ThreadedServer(SocketServer.ThreadingMixIn, SocketServer.TCPServer,):
    pass


def mA(raw):
    s = [0]*len(raw)
    for i in range(len(raw)):
        s[i] = int(raw[i],16)
    a = s[0]*16 + s[1]
    b = s[2]*16 + s[3]
    c = s[4]*16 + s[5]
    f = (a*65536+b*256+c)*1234.0/18976/15
    return "%.2f" % f

def V(raw):
    s = [0]*len(raw)
    for i in range(len(raw)):
        s[i] = int(raw[i],16)
    a = s[0]*16 + s[1]
    b = s[2]*16 + s[3]
    c = s[4]*16 + s[5]
    f = (a*65536+b*256+c)*214.8/1805143*222/229
    return "%.2f" % f

def Hz(raw):
    s = [0]*len(raw)
    for i in range(len(raw)):
        s[i] = int(raw[i],16)
    a = s[0]*16 + s[1]
    b = s[2]*16 + s[3]
    f = 3597545.0/8/(a*256+b)
    return "%.2f" % f

def W(raw):
    s = [0]*len(raw)
    for i in range(len(raw)):
        s[i] = 15 - int(raw[i],16)
    a = s[0]*16 + s[1]
    b = s[2]*16 + s[3]
    c = s[4]*16 + s[5]
    d = s[6]*16 + s[7]
    f = (a*16777216+b*65536+c*256+d)*2.5258/10000*0.065
    return "%.2f" % f

def kWh(raw):
    s = [0]*len(raw)
    for i in range(len(raw)):
        s[i] = int(raw[i],16)
    a = s[0]*16 + s[1]
    b = s[2]*16 + s[3]
    c = s[4]*16 + s[5]
    f = (a*65536+b*256+c)/3200.0/15
    return "%.4f" % f


if __name__ == '__main__':
    socket.setdefaulttimeout(20)
    address = ('', 8899)
    server = ThreadedServer(address, ThreadedRequestHandler)
    ip, port = server.server_address

    t = threading.Thread(target=server.serve_forever)
    t.setDaemon(False)
    t.start()
