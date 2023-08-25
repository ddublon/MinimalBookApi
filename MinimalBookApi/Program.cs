using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book>
{
    new Book{ Id = 1, Title = "1" , Author = "1"},
    new Book{ Id = 2, Title = "2" , Author = "2"},
    new Book{ Id = 3, Title = "3" , Author = "3"}
};


app.MapGet("/book", ()=>{
    return books;
});


app.MapGet("/book/{id}", (int id) => {
    var book = books.Find(b => b.Id == id);

    if ( book is null)
    {
        return Results.NotFound("sorry ");
    }

    return Results.Ok(book);
});

app.MapPost("/book", (Book book) =>
{
    books.Add(book);
    return Results.Ok(book);
});


app.MapPut("/book", (Book updatedBook , int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
    {
        return Results.NotFound("soorry ");
    }
    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    return Results.Ok(book);
});


app.MapDelete("/book", ( int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
    {
        return Results.NotFound("soorry ");
    }
    books.Remove(book);
    
    return Results.Ok(book);
});


app.Run();


class Book
{
    public int Id { get; set;}
    public required string Title { get; set;}
    public required string Author { get; set;}
}

