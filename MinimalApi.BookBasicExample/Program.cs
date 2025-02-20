using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MinimalApi.BookBasicExample.Context;
using MinimalApi.BookBasicExample.Entities;
using MinimalApi.BookBasicExample.Service;
using MinimalApi.BookBasicExample.Validators;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{ 

    options.UseInMemoryDatabase("BookStore");

});
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//CreateBook
app.MapPost("addbook", async (Book book ,IBookService bookService ,CancellationToken cancellationToken) =>
{   
    BookValidator bookValidator = new();
    ValidationResult resultValidation = bookValidator.Validate(book);
    if (!resultValidation.IsValid) 
    { 
    return Results.BadRequest(resultValidation.Errors.Select(s=>s.ErrorMessage.ToString()));

    }

    var result = await bookService.CreateAsync(book);
    if (!result) return Results.BadRequest("Someting went wrong!");
    return Results.Ok(new { Message = "book create is succsessfull" });

});

//GetAllBooks
app.MapGet("getallbooks", async (IBookService bookService, CancellationToken cancellationToken) =>
{
    var result = await bookService.GetAllBooksAsync(cancellationToken);
    return Results.Ok(result);
});

//Getbookwithid
app.MapGet("getbyýd", async (int id, Book book, IBookService bookService, CancellationToken cancellationToken) =>
{
    BookValidator validator = new();
    ValidationResult validationResult = validator.Validate(book);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));
    }

    var result = await bookService.GetByBookIdAsync(id, cancellationToken);
    if (result==null) return Results.BadRequest("Something went wrong!");

    return Results.Ok(new { Message = "Book" });
});

//Getbookwithtitle
app.MapGet("get-books-by-title/{title}", async (string title, IBookService bookService, CancellationToken cancellationToke) =>
{
    var books = await bookService.SearchBooksNameAsync(title, cancellationToke);
    return Results.Ok(books);
});

//bookUpdate
app.MapPut("booksupdate", async (Book book, IBookService bookService, CancellationToken cancellationToken) =>
{
    BookValidator validator = new();
    ValidationResult validationResult = validator.Validate(book);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));
    }

    var result = await bookService.UpdateAsync(book, cancellationToken);
    if (!result) return Results.BadRequest("Something went wrong!");

    return Results.Ok(new { Message = "Book update is successful" });
});

//bookDelete
app.MapDelete("booksdelete/{id}", async (int id, Book book, IBookService bookService, CancellationToken cancellationToken) =>
{
    BookValidator validator = new();
    ValidationResult validationResult = validator.Validate(book);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));
    }

    var result = await bookService.DeleteAsync(id, cancellationToken);
    if (!result) return Results.BadRequest("Something went wrong!");

    return Results.Ok(new { Message = "Book update is successful" });
});


app.Run();

