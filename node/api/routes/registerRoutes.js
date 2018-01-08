'use strict';

module.exports = function(app) {
  var register = require('../controllers/registerController');

  // todoList Routes
  app.route('/register')
    .get(register.listRegisteredDevices);

  app.route('/register/:registerId')
    .get(register.getRegisteredDevice);
};