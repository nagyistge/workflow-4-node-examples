var cluster = require('cluster');
var numCPUs = require('os').cpus().length;
var express = require('express');
var routes = require('./routes');
var calc = require('./routes/calc');
var http = require('http');
var path = require('path');

http.globalAgent.maxSockets = 10000;

if (cluster.isMaster)
{
    // Fork workers.
    for (var i = 0; i < numCPUs * 2; i++)
    {
        cluster.fork();
    }

    cluster.on('exit', function (worker, code, signal)
    {
        console.log('worker ' + worker.process.pid + ' died');
    });
}
else
{
    var app = express();

    // all environments
    app.set('port', process.env.PORT || 6667);
    app.set('views', path.join(__dirname, 'views'));
    app.set('view engine', 'jade');
    app.use(express.json());
    app.use(app.router);
    app.use(require('stylus').middleware(path.join(__dirname, 'public')));
    app.use(express.static(path.join(__dirname, 'public')));

    // development only
    if ('development' == app.get('env'))
    {
        app.use(express.errorHandler());
    }

    app.get('/', routes.index);

    for (var method in calc)
    {
        if (calc.hasOwnProperty(method))
        {
            app.get('/calc/' + method, calc[method]);
        }
    }

    http.createServer(app).listen(app.get('port'), function ()
    {
        console.log('Express server listening on port ' + app.get('port'));
    });
}
