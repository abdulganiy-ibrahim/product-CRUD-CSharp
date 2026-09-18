using System.ComponentModel;

namespace ProductApi.Model;

public record Product(int Id, string Name, decimal Price, string Description, int Stock);

public record CreateProduct(string Name, decimal Price, string Description, int Stock);