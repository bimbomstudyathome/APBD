using System.Data.Common;
using System.Data.SqlClient;
using TestExample.Models;

namespace TestExample.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    private IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> AnimalExists(int Id)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        await con.OpenAsync();

        cmd.CommandText = "SELECT 1 FROM Animal WHERE ID = @Id";
        cmd.Parameters.AddWithValue("@Id", Id);

        var animal = await cmd.ExecuteScalarAsync();
        return animal is not null;
    }

    public async Task<AnimalsDTOs> AnimalById(int Id)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        

        var query = @"SELECT 
							Animal.ID AS AnimalID,
							Animal.Name AS AnimalName,
							Type,
							AdmissionDate,
							Owner.ID as OwnerID,
							FirstName,
							LastName,
							Date,
							[Procedure].Name AS ProcedureName,
							Description
							FROM Animal
							JOIN Owner ON Owner.ID = Animal.Owner_ID
							JOIN Procedure_Animal ON Procedure_Animal.Animal_ID = Animal.ID
							JOIN [Procedure] ON [Procedure].ID = Procedure_Animal.Procedure_ID
							WHERE Animal.ID = @Id";
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@Id", Id);
        
        await con.OpenAsync();
        DbTransaction tran = await con.BeginTransactionAsync();
        cmd.Transaction = (SqlTransaction)tran;
        AnimalsDTOs animalsDtOs = null;
        try
        {
	        var reader = await cmd.ExecuteReaderAsync();
	        while (await reader.ReadAsync())
	        {
		        if (animalsDtOs == null)
		        {
			        animalsDtOs = new AnimalsDTOs()
			        {
				        ID = Convert.ToInt32(reader["AnimalID"]),
				        Name = reader["AnimalName"].ToString(),
				        Type = reader["Type"].ToString(),
				        AdmissionDate = Convert.ToDateTime(reader["AdmissionDate"]),
				        Owner = new Owner()
				        {
					        ID = Convert.ToInt32(reader["OwnerID"]),
					        FirstName = reader["FirstName"].ToString(),
					        LastName = reader["LastName"].ToString()
				        },
				        Procedures = new List<Procedure>()
			        };
		        }

		        animalsDtOs.Procedures.Add(new Procedure()
		        {
			        Name = reader["ProcedureName"].ToString(),
			        Description = reader["ProcedureName"].ToString(),
			        Date = Convert.ToDateTime(reader["Date"])
		        });
	        }
        }
        catch (Exception e)
        {
	        await tran.RollbackAsync();
        }

        return animalsDtOs;
    }
}