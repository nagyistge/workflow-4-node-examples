﻿//require('nodetime').profile({
//    accountKey: '8efd999a311ceefae50cbe8896ff23c045b2db2b',
//    appName: 'Correlated Calculator'
//});

/**
 * Module dependencies.
 */

var express = require('express');
var routes = require('./routes');
var calc = require('./routes/calc');
var http = require('http');
var path = require('path');

var app = express();

// all environments
app.set('port', process.env.PORT || 6667);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');
app.use(express.favicon());
app.use(express.logger('dev'));
app.use(express.json());
app.use(express.urlencoded());
app.use(express.methodOverride());
app.use(app.router);
app.use(require('stylus').middleware(path.join(__dirname, 'public')));
app.use(express.static(path.join(__dirname, 'public')));

// development only
if ('development' == app.get('env')) {
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

http.globalAgent.maxSockets = 1000000;

http.createServer(app).listen(app.get('port'), function(){
  console.log('Express server listening on port ' + app.get('port'));
});
