const os = require('os')
module.exports = {
  apps: [{
    name: 'colyseus',
    script: './dist/main.js', // your entrypoint file
    watch: true,           // optional
    instances: os.cpus().length,
    exec_mode: 'fork',         // IMPORTANT: do not use cluster mode.
    env: {
      DEBUG: 'colyseus:errors',
      NODE_ENV: 'production',
    }
  }]
}
