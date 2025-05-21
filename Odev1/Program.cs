using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var products = new List<Product>
{
    new Product { Id = 1, Name = "Bilgisayar", Category = "Elektronik", Price = 700m },
    new Product { Id = 2, Name = "Etek", Category = "Giyim", Price = 500m },
    new Product { Id = 3, Name = "Buzdolabý", Category = "Elektronik", Price = 600m },
    new Product { Id = 4, Name = "Bilim Kurgu", Category = "Kitap", Price = 400m },
    new Product { Id = 5, Name = "Elbise", Category = "Giyim", Price = 300m },
    new Product { Id = 6, Name = "Bilezik", Category = "Aksesuar", Price = 550m }
};

app.MapGet("/products", (HttpRequest request) =>
{
    var filter = request.Query["filter"].ToString().Trim().ToLower();

    if (string.IsNullOrEmpty(filter))
    {
        return Results.Ok(products);
    }
    else
    {
        var matched = products
            .Where(p =>
                p.Name.ToLower().Contains(filter) ||
                p.Category.ToLower().Contains(filter))
            .ToList();

        return Results.Ok(matched);
    }
});

app.MapGet("/", () =>
    Results.Content(
        "Ürün API çalýþýyor. /products?filter=elektronik gibi endpoint’i deneyin.",
        "text/plain; charset=utf-8"
    )
);

app.Run();

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
//Bu proje, ASP.NET Core Minimal API mimarisi kullanýlarak geliþtirilmiþ,
//basit ve hýzlý bir þekilde çalýþabilen bir ürün listeleme ve filtreleme uygulamasýdýr.
//Sabit verilerle (mock data) çalýþtýðý için herhangi bir veritabaný baðýmlýlýðý olmadan doðrudan çalýþtýrýlabilir.
//Kullanýcýlar, /products endpoint’ine GET isteði göndererek tüm ürünleri listeleyebilir veya ?filter= parametresiyle ürün adý ya da kategorisine göre filtreleme yapabilir.
