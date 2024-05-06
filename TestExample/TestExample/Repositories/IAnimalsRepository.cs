using TestExample.Models;

namespace TestExample.Repositories;

public interface IAnimalsRepository
{
    Task<bool> AnimalExists(int Id);
    Task<AnimalsDTOs> AnimalById(int Id);
}