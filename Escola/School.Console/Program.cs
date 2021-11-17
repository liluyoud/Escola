using System.Net.Http.Json;

HttpClient cliente = new HttpClient();

var response = await cliente.GetAsync("https://localhost:7163/School");
if (response.IsSuccessStatusCode)
{
    var cities = await response.Content.ReadFromJsonAsync<List<School.Core.Entities.City>>();
    foreach (var city in cities)
    {
        Console.WriteLine($"{city.Name}");
    }
}


