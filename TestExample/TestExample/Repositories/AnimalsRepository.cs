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
        await con.OpenAsync();

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

        var reader = await cmd.ExecuteReaderAsync();

        var animalIdOrdinal = reader.GetOrdinal("AnimalID");
        var animalNameOrdinal = reader.GetOrdinal("AnimalName");
        var animalTypeOrdinal = reader.GetOrdinal("Type");
        var admissionDateOrdinal = reader.GetOrdinal("AdmissionDate");
        var ownerIdOrdinal = reader.GetOrdinal("OwnerID");
        var firstNameOrdinal = reader.GetOrdinal("FirstName");
        var lastNameOrdinal = reader.GetOrdinal("LastName");
        var dateOrdinal = reader.GetOrdinal("Date");
        var procedureNameOrdinal = reader.GetOrdinal("ProcedureName");
        var procedureDescriptionOrdinal = reader.GetOrdinal("Description");

        AnimalsDTOs animalsDtOs = null;
        
        while (await reader.ReadAsync())
        {
	        if (animalsDtOs == null)
	        {
		        animalsDtOs = new AnimalsDTOs()
		        {
			        ID = reader.GetInt32(animalIdOrdinal),
			        Name = reader.GetString(animalNameOrdinal),
			        Type = reader.GetString(animalTypeOrdinal),
			        AdmissionDate = reader.GetDateTime(admissionDateOrdinal),
			        Owner = new Owner()
			        {
				        ID = reader.GetInt32(ownerIdOrdinal),
				        FirstName = reader.GetString(firstNameOrdinal),
				        LastName = reader.GetString(lastNameOrdinal)
			        },
			        Procedures = new List<Procedure>()
		        };
	        }
	        animalsDtOs.Procedures.Add(new Procedure()
	        {
		        Name = reader.GetString(procedureNameOrdinal),
		        Description = reader.GetString(procedureDescriptionOrdinal),
		        Date = reader.GetDateTime(dateOrdinal)
	        });
        }

        return animalsDtOs;
    }
}