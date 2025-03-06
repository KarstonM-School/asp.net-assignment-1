var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/* 
    All available types:
        - book
        - reader
        - borrowing
 */

/*
    home page:                   GET     /
    get all items of type:       GET     /type + s
    get one type item:           GET     /type + s?id=1
    create new type item:        POST    /type + s
    edit a type item:            PUT     /type + s?id=1
    delete a type item:          DELETE  /type + s?id=1
*/

app.Use(async (context, next) =>
{
    //Define some vars & create methods to make my life easier haha
    var reqPathCheck = context.Request.Path;
    var methodCheck = context.Request.Method;
    
    // This just writes the text to the response body
    async Task Write(string message)
    {
        await context.Response.WriteAsync($"{message}");
    };

    // Handles GET requests: Returns a specific item if "id" is provided, otherwise lists all items.
    async Task runGet(string type)
    {
        if (reqPathCheck == $"/{type.ToLower()}s")
        {
            if (context.Request.Query.ContainsKey("id"))
            {
                await Write($"Here is {type}s with id: {context.Request.Query["id"]}.");
            }
            else
            {
                await Write($"Here are all the {type.ToLower()}s.");
            }
        }
    }

    // Handles POST requests: Creates a new item of the given type.
    async Task runPost(string type)
    {
        if (reqPathCheck == $"/{type}s")
        {
            await Write($"Create a new {type}.");
        }
    }

    // Handles PUT requests: Updates an item if "id" is provided.
    async Task runPut(string type)
    {
        if (reqPathCheck == $"/{type}s")
        {
            if (context.Request.Query.ContainsKey("id"))
            {
                await Write($"Edit {type}, id is {context.Request.Query["id"]}");
            }
        }
    }

    // Handles DELETE requests: Deletes an item if "id" is provided.
    async Task runDelete(string type)
    {
        if (reqPathCheck == $"/{type}s")
        {
            if (context.Request.Query.ContainsKey("id"))
            {
                await Write($"Delete {type}, id is {context.Request.Query["id"]}");
            }
        }
    }

    // Start of Program.
    if (reqPathCheck == "/")
    {
        await Write("Welcome to the Library Management System.");
    }

    // GET Method
    else if(methodCheck == "GET")
    {
        await runGet("Book");

        await runGet("Reader");

        await runGet("Borrowing");
    }

    // POST Method
    else if (methodCheck == "POST")
    {
        await runPost("book");

        await runPost("reader");

        await runPost("borrowing");
    }

    // PUT Method
    else if (methodCheck == "PUT")
    {
        await runPut("book");

        await runPut("reader");

        await runPut("borrowing");
    }

    // DELETE Method
    else if (methodCheck == "DELETE")
    {
        await runDelete("book");

        await runDelete("reader");

        await runDelete("borrowing");
    }
    else
    {
        await next();
    }
});

app.Run();
