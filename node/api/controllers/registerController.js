'use strict';

exports.listRegisteredDevices = function(req, res) {
  res.json("ok");
};


exports.getRegisteredDevice = function(req, res) {
      res.json(req.params.registerId + " task");
  };
