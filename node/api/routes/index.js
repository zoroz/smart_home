// routes/index.js
const registerRoutes = require('./registerRoutes');

module.exports = function(app, db) {
  registerRoutes(app, db);
  // Other route groups could go here, in the future
};