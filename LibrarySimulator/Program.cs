public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public DateTime PublicationDate { get; set; }
    public int NumberOfPages { get; set; }

    public Book(string title, string author, string isbn, DateTime publicationDate, int numberOfPages)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationDate = publicationDate;
        NumberOfPages = numberOfPages;
    }
}

public class Library
{
    private List<Book> _books = new List<Book>();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        _books.Remove(book);
    }

    public List<Book> SearchByTitle(string title)
    {
        return _books.Where(b => b.Title.Contains(title)).ToList();
    }

    public List<Book> SearchByAuthor(string author)
    {
        return _books.Where(b => b.Author.Contains(author)).ToList();
    }
}
class Program
{
    static void Main(string[] args)
    {
        var library = new Library();

        library.AddBook(new Book("The Catcher in the Rye", "J.D. Salinger", "9780316769174", new DateTime(1951, 7, 16), 277));
        library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "9780446310789", new DateTime(1960, 7, 11), 281));
        library.AddBook(new Book("1984", "George Orwell", "9780451524935", new DateTime(1949, 6, 8), 328));

        while (true)
        {
            Console.WriteLine("Enter a command:");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Remove book");
            Console.WriteLine("3. Search by title");
            Console.WriteLine("4. Search by author");
            Console.WriteLine("5. Exit");

            var command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    Console.WriteLine("Enter book details:");
                    Console.Write("Title: ");
                    var title = Console.ReadLine();
                    Console.Write("Author: ");
                    var author = Console.ReadLine();
                    Console.Write("ISBN: ");
                    var isbn = Console.ReadLine();
                    Console.Write("Publication date (MM/DD/YYYY): ");
                    var publicationDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Number of pages: ");
                    var numberOfPages = int.Parse(Console.ReadLine());

                    var book = new Book(title, author, isbn, publicationDate, numberOfPages);
                    library.AddBook(book);
                    Console.WriteLine("Book added successfully.");
                    break;

                case "2":
                    Console.WriteLine("Enter the title of the book you want to remove:");
                    var titleToRemove = Console.ReadLine();
                    var booksToRemove = library.SearchByTitle(titleToRemove);
                    if (booksToRemove.Count == 0)
                    {
                        Console.WriteLine("No books found with that title.");
                    }
                    else if (booksToRemove.Count == 1)
                    {
                        library.RemoveBook(booksToRemove[0]);
                        Console.WriteLine("Book removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Multiple books found with that title. Please choose one to remove:");
                        for (int i = 0; i < booksToRemove.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {booksToRemove[i].Title} by {booksToRemove[i].Author}");
                        }
                        var choice = int.Parse(Console.ReadLine());
                        library.RemoveBook(booksToRemove[choice - 1]);
                        Console.WriteLine("Book removed successfully.");
                    }
                    break;

                case "3":
                    Console.WriteLine("Enter a title to search for:");
                    var searchTitle = Console.ReadLine();
                    var foundByTitle = library.SearchByTitle(searchTitle);
                    if (foundByTitle.Count == 0)
                    {
                        Console.WriteLine("No books found with that title.");
                    }
                    else
                    {
                        Console.WriteLine("Books found:");
                        foreach (var bookResult in foundByTitle)
                        {
                            Console.WriteLine($"{bookResult.Title} by {bookResult.Author}");
                        }
                    }
                    break;

                case "4":
                    Console.WriteLine("Enter an author to search for:");
                    var searchAuthor = Console.ReadLine();
                    var foundByAuthor = library.SearchByAuthor(searchAuthor);
                    if (foundByAuthor.Count == 0)
                    {
                        Console.WriteLine("No books found by this author.");
                    }
                    else
                    {
                        Console.WriteLine("Books found:");
                        foreach (var bookResult in foundByAuthor)
                        {
                            Console.WriteLine($"{bookResult.Title} by {bookResult.Author}");
                        }
                    }
                    break;

                case "5":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }

            Console.WriteLine();
        }
    }
}