// MongoDb connection string:
//var connStr = "mongodb://localhost/calculator";
var connStr = "mongodb://swiiis01/calculator";

var WorkflowHost = require("workflow-4-node").hosting.WorkflowHost;
var MongoDDPersistence = require("workflow-4-node").hosting.mongoDB.MongoDDPersistence;

var MemoryPersistence = require("workflow-4-node").hosting.MemoryPersistence;

var persistence = new MongoDDPersistence({
    connection: connStr
});

//var persistence = null;

//var persistence = new MemoryPersistence();

var host = new WorkflowHost(
    {
        persistence: persistence
    });

host.registerWorkflow(require("./calculatorWorkflow"));

module.exports = host;
