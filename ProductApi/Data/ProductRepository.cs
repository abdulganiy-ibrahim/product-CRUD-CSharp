namespace ProductApi.Data;

using Npgsql;
using ProductApi.Model;

public class ProductRepository
{
  private readonly string _connectionString;

  public ProductRepository(string connectionString)
  {
    _connectionString = connectionString;
  }

  // Create new product
  public Product Create(CreateProduct newProduct)
  {
    using var conn = new NpgsqlConnection(_connectionString);
    conn.Open();

    using var cmd = new NpgsqlCommand(
      "INSERT INTO products (name, price, description, stock) VALUES (@name, @price, @description, @stock) RETURNING id",
      conn
    );

    cmd.Parameters.AddWithValue("name", newProduct.Name);
    cmd.Parameters.AddWithValue("price", newProduct.Price);
    cmd.Parameters.AddWithValue("description", newProduct.Description);
    cmd.Parameters.AddWithValue("stock", newProduct.Stock);

    var newId = (int)cmd.ExecuteScalar()!;

    return new Product(newId, newProduct.Name, newProduct.Price, newProduct.Description, newProduct.Stock);
  }

  // first method: Get all products from database
  public List<Product> GetAll()
  {
    var products = new List<Product>();

    using var conn = new NpgsqlConnection(_connectionString);
    conn.Open();

    using var cmd = new NpgsqlCommand("SELECT id, name, price, description, stock FROM products", conn);
    using var reader = cmd.ExecuteReader();

    while (reader.Read())
    {
      products.Add(new Product(
        reader.GetInt32(0),
        reader.GetString(1),
        reader.GetDecimal(2),
        reader.GetString(3),
        reader.GetInt32(4)
      ));
    }

    return products;
  }

  // Get Product By ID
  public Product? GetById(int id)
  {

    using var conn = new NpgsqlConnection(_connectionString);
    conn.Open();

    using var cmd = new NpgsqlCommand("SELECT id, name, price, description, stock FROM products WHERE id = @id", conn);
    cmd.Parameters.AddWithValue("id", id);

    using var reader = cmd.ExecuteReader();

    if (reader.Read())
    {
      return new Product(
        reader.GetInt32(0),
        reader.GetString(1),
        reader.GetDecimal(2),
        reader.GetString(3),
        reader.GetInt32(4)
      );
    }

    return null;
  }

  // Delete Product By ID method
  public void DeleteById(int id)
  {
    using var conn = new NpgsqlConnection(_connectionString);
    conn.Open();

    using var cmd = new NpgsqlCommand("DELETE FROM products WHERE id = @id", conn);
    cmd.Parameters.AddWithValue("id", id);

    var rowsAffected = cmd.ExecuteNonQuery();

    return;
  }
}