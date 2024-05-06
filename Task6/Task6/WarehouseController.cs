using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Task6.Models;

namespace Task6;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public WarehouseController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> addProductWarehouseRequest(ProductWarehouseRequest productWarehouseRequest)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT(*) FROM Product where IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", productWarehouseRequest.IdProduct);
        if ((int) await cmd.ExecuteScalarAsync() == 0)
        {
            return NotFound("Product with a given id not found");
        }
        cmd.CommandText = "SELECT COUNT(*) FROM Warehouse where IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", productWarehouseRequest.IdWarehouse);
        if ((int) await cmd.ExecuteScalarAsync() == 0)
        {
            return NotFound("Warehouse with a given id not found");
        }
        if (productWarehouseRequest.Amount < 0)
        {
            return BadRequest("Amount must be > 0");
        }

        cmd.CommandText = "SELECT COUNT(*) FROM [Order] where IdProduct = @IdProduct and Amount = @Amount and CreatedAt < @CreatedAt";
        cmd.Parameters.AddWithValue("@Amount", productWarehouseRequest.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", productWarehouseRequest.CreatedAt);
        if ((int) await cmd.ExecuteScalarAsync() == 0)
        {
            return NotFound("Order with following fields not found");
        }
        
        cmd.CommandText = "SELECT * FROM [Order] where IdProduct = @IdProduct and Amount = @Amount";
        int IdOrder = (int) await cmd.ExecuteScalarAsync();
        
        cmd.CommandText = "SELECT COUNT(*) FROM Product_Warehouse where IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@IdOrder", IdOrder);
        if ((int) await cmd.ExecuteScalarAsync() > 0)
        {
            return BadRequest("Product_Warehouse row is already exist");
        }
        cmd.CommandText = "UPDATE [Order] SET FulfilledAt = @FulfilledAt where IdOrder = @IdOrder ";
        cmd.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
        cmd.ExecuteNonQueryAsync();
        
        cmd.CommandText = "SELECT Price from Product where IdProduct = @IdProduct";
        double ProductPrice = Convert.ToDouble(cmd.ExecuteScalar());
        cmd.Parameters.AddWithValue("@Price", ProductPrice);
        
        cmd.DisposeAsync();
        
        cmd.CommandText =
            "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)" +
            "VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @FulfilledAt)";
        
        cmd.ExecuteNonQueryAsync();

        cmd.CommandText = "SELECT IdProductWarehouse FROM Product_Warehouse Where IdWarehouse = @IdWarehouse " +
                          "and IdProduct = @IdProduct " +
                          "and IdOrder = @IdOrder " +
                          "and Amount = @Amount " +
                          "and Price = @Price " +
                          "and CreatedAt = @FulfilledAt ";
        int Id = (int) await cmd.ExecuteScalarAsync();
        return Ok(Id);
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product";
        
        using var ProductData = cmd.ExecuteReader();
        
        List<Product> products = new List<Product>();
        while (ProductData.Read())
        {
            var product = new Product
            {
                IdProduct = Convert.ToInt32(ProductData["IdProduct"]),
                Price = Convert.ToDouble(ProductData["Price"]),
                Description = ProductData["Description"].ToString(),
                Name = ProductData["Name"].ToString()
            };
            products.Add(product);
        }

        return Ok(products);
    }
}