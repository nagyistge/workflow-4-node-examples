var ActivityMarkup = require("workflow-4-node").activities.ActivityMarkup;

module.exports = new ActivityMarkup().parse(
    {
        workflow: {
            name: "calculator",
            running: true,
            inputArgs: null,
            currentValue: 0,
            args: [
                {
                    while: {
                        condition: "{ this.running }",
                        body: {
                            pick: [
                                {
                                    block: {
                                        displayName: "Add block",
                                        args: [
                                            {
                                                method: {
                                                    displayName: "Add method",
                                                    methodName: "add",
                                                    instanceIdPath: "[0].id",
                                                    canCreateInstance: true,
                                                    "@to": "inputArgs"
                                                }
                                            },
                                            {
                                                assign: {
                                                    value: "{ this.currentValue + this.inputArgs[0].value }",
                                                    to: "currentValue"
                                                }
                                            }
                                        ]
                                    }
                                },
                                {
                                    block: {
                                        displayName: "Subtract block",
                                        args: [
                                            {
                                                method: {
                                                    displayName: "Subtract method",
                                                    methodName: "subtract",
                                                    instanceIdPath: "[0].id",
                                                    canCreateInstance: true,
                                                    "@to": "inputArgs"
                                                }
                                            },
                                            {
                                                assign: {
                                                    value: "{ this.currentValue - this.inputArgs[0].value }",
                                                    to: "currentValue"
                                                }
                                            }
                                        ]
                                    }
                                },
                                {
                                    block: {
                                        displayName: "Multiply block",
                                        args: [
                                            {
                                                method: {
                                                    displayName: "Multiply method",
                                                    methodName: "multiply",
                                                    instanceIdPath: "[0].id",
                                                    canCreateInstance: true,
                                                    "@to": "inputArgs"
                                                }
                                            },
                                            {
                                                assign: {
                                                    value: "{ this.currentValue * this.inputArgs[0].value }",
                                                    to: "currentValue"
                                                }
                                            }
                                        ]
                                    }
                                },
                                {
                                    block: {
                                        displayName: "Divide block",
                                        args: [
                                            {
                                                method: {
                                                    displayName: "Divide method",
                                                    methodName: "divide",
                                                    instanceIdPath: "[0].id",
                                                    canCreateInstance: true,
                                                    "@to": "inputArgs"
                                                }
                                            },
                                            {
                                                assign: {
                                                    value: "{ this.currentValue / this.inputArgs[0].value }",
                                                    to: "currentValue"
                                                }
                                            }
                                        ]
                                    }
                                },
                                {
                                    method: {
                                        displayName: "Equals method",
                                        methodName: "equals",
                                        instanceIdPath: "[0].id",
                                        canCreateInstance: true,
                                        result: "{ this.currentValue }"
                                    }
                                },
                                {
                                    block: {
                                        displayName: "Reset block",
                                        args: [
                                            {
                                                method: {
                                                    displayName: "Reset method",
                                                    methodName: "reset",
                                                    instanceIdPath: "[0].id"
                                                }
                                            },
                                            {
                                                assign: {
                                                    value: false,
                                                    to: "running"
                                                }
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                }
            ]
        }
    });