const os = require('os');
const http = require('http');
const https = require('https');
const url = require('url');
const fs = require('fs');
const sqlite3 = require('sqlite3').verbose();
const loki = require('lokijs');
var express = require('express');
var app = express();
var port = process.env.PORT || 3000;
var bodyParser = require('body-parser');
var ws = require("nodejs-websocket");
var exec = require('child_process').exec;
//var wlan = require('./wlan')();
var emitter = require('events').EventEmitter;
var inherits = require('util').inherits;

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

require('./api/routes')(app, {});

var db = new loki('loki.json');
var devices = db.addCollection('devices');
var values = db.addCollection('values');

for(var i = 0; i < 5; i++)
{
  devices.insert({id:i, name:'Zoro16aSensor'+i});
  for(var j = 0; j < 100000; j++)
  {
    values.insert({deviceId:i, value:10+i, timestamp:'1.1.2018'});
  }
  db.save();
}
app.listen(port);

console.log('todo list RESTful API server started on: ' + port);