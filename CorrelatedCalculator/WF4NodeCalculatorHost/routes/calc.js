var host = require("../wf/calculatorHost");
var _ = require("lodash");

var callCalcMethod = function(methodName, req, res, inArg, outValue)
{
    var id = req.param("id");
    var value = inArg ? parseFloat(req.param("value")) : 0.0;

    if (_.isUndefined(id) || _.isNaN(value))
    {
        res.send(400, "Invalid args.");
        return;
    }

    //console.log("Calling '" + methodName + "' on instance '" + id + "'" + (inArg ? (" with value: " + value) : "."));
    try
    {
        host.invokeMethod("calculator", methodName, inArg ? { id: id, value: value } : { id: id })
            .then(
            function (result)
            {
                if (outValue)
                {
                    //console.log("Sending: " + result);
                    res.json(result);
                }
                else
                {
                    //console.log("Done");
                    res.send();
                }
            },
            function (e)
            {
                console.error(e.stack);
                res.send(500, "Worlflow crashed.");
            });
    }
    catch (e)
    {
        console.error(e.stack);
        res.send(500, "Worlflow crashed.");
    }
}

module.exports = {
    add: function (req, res)
    {
        callCalcMethod("add", req, res, true, false);
    },
    divide: function (req, res)
    {
        callCalcMethod("divide", req, res, true, false);
    },
    subtract: function (req, res)
    {
        callCalcMethod("subtract", req, res, true, false);
    },
    multiply: function (req, res)
    {
        callCalcMethod("multiply", req, res, true, false);
    },
    equals: function (req, res)
    {
        callCalcMethod("equals", req, res, false, true);
    },
    reset: function (req, res)
    {
        callCalcMethod("reset", req, res, false, false);
    }
};