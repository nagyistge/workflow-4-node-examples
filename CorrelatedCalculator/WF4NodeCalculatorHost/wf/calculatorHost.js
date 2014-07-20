// MongoDb connection string:
var connStr = "mongodb://localhost/calculator";

var WorkflowHost = require("workflow-4-node").hosting.WorkflowHost;
var MongoDDPersistence = require("workflow-4-node").hosting.mongoDB.MongoDDPersistence;

var persistence = new MongoDDPersistence({
    connection: connStr
});

//var persistence = null;

var host = new WorkflowHost(
    {
        persistence: persistence
    });

host.registerWorkflow(require("./calculatorWorkflow"));

module.exports = host;
