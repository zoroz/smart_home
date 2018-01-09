'use strict';

var fs = require('fs');
var ws = require("nodejs-websocket");

var options = {
    secure: true,
    key: fs.readFileSync('./tools/ipsum-key.pem'),
    cert: fs.readFileSync('./tools/ipsum-cert.pem'),
};

var server = ws.createServer(options, function (conn) {
    console.log("WS | Server is up %s:%s to %s:%s", ip, port, conn.socket.remoteAddress, conn.socket.remotePort);
    self._connected = true;
    self.on('push', a => {
        var rq = {
            "apikey": "111111111-1111-1111-1111-111111111111",
            "action": a.action,
            "deviceid": a.target,
            "params": a.value
        };
        var r = JSON.stringify(rq);
        console.log('REQ | WS | APP | ' + r);
        conn.sendText(r);
    });
    conn.on("text", function (str) {
        var data = JSON.parse(str);
        console.log('REQ | WS | DEV | %s', JSON.stringify(data));
        res = {
            "error": 0,
            "deviceid": data.deviceid,
            "apikey": "111111111-1111-1111-1111-111111111111"
        };
        if (data.action) {
            switch (data.action) {
                case 'date':
                    res.date = new Date().toISOString();
                    break;
                case 'query':
                    //device wants information
                    var device = self._knownDevices.find(d => d.id == data.deviceid);
                    if (!device) {
                        console.log('ERR | WS | Unknown device ', data.deviceid);
                    } else {
                        /*if(data.params.includes('timers')){
                         console.log('INFO | WS | Device %s asks for timers',device.id);
                         if(device.timers){
                          res.params = [{timers : device.timers}];
                         }
                        }*/
                        res.params = {};
                        data.params.forEach(p => {
                            res.params[p] = device[p];
                        });
                    }
                    break;
                case 'update':
                    //device wants to update its state
                    var device = self._knownDevices.find(d => d.id == data.deviceid);
                    if (!device) {
                        console.log('ERR | WS | Unknown device ', data.deviceid);
                    } else {
                        device.state = data.params.switch;
                        self._updateKnownDevice(self, device);
                    }
                    break;
                case 'register':
                    var device = {
                        id: data.deviceid
                    };
                    var type = data.deviceid.substr(0, 2);
                    if (type == '01') device.kind = 'switch';
                    else if (type == '02') device.kind = 'light';
                    else if (type == '03') device.kind = 'sensor'; //temperature and humidity. No timers here;
                    device.version = data.romVersion;
                    device.model = data.model;
                    self._updateKnownDevice(self, device);
                    console.log('INFO | WS | Device %s registered', device.id);
                    break;
                default: console.log('TODO | Unknown action "%s"', data.action); break;
            }
        } else {
            console.log('TODO | WS | Not data action frame');
        }
        var r = JSON.stringify(res);
        console.log('RES | WS | DEV | ' + r);
        conn.sendText(r);
        var td = self._knownDevices.find(d => d.id == res.deviceid);
        self.emit('msg', { device: td });
    });
    conn.on("close", function (code, reason) {
        console.log("Connection closed");
    });
}).listen(port, ip);

exports.module = server;