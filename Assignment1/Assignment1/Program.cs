var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapGet("/books", () =>
{
    return "Here are all the books.";
});
app.MapGet("/books/{id}", (HttpContext context) =>
{
    var BookId = context.Request.RouteValues["id"];
    return $"The book id is {BookId}";
});
app.MapPost("/books", () =>
{
    return "This is post requesdt!";
});

app.Run();
