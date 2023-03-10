using AutoMapper;
using src.DTO;
using src.Model;
using System.Linq;

namespace src.Database;

public interface IDatabase
{
    Task<IList<UserOutput>> GetAllAsync();
    Task<UserOutput> GetByIdAsync(Guid id);
    Task CreateAsync(UserInput input);
    Task UpdateAsync(Guid id, UserInput input);
    Task UpdateAgeAsync(Guid id, int age);
    Task DeleteAsync(Guid id);
    Task<List<UserInfoOutput>> GetUsersInfoByIdAsync(Guid id);
}

public class Database : IDatabase
{
    private readonly List<UserModel> Db = new List<UserModel>();
    private readonly IMapper _mapper;

    public Database(IMapper mapper)
    {
        _mapper = mapper;
        DatabaseSeed.Execute(Db);
    }

    public async Task CreateAsync(UserInput input)
        => await Task.Run(() =>
        {
            var user = _mapper.Map<UserModel>(input);
            user.Id = Guid.NewGuid();
            Db.Add(user);
        });

    public async Task DeleteAsync(Guid id)
    {
        await Task.Run(() =>
        {
            int index = Db.FindIndex(user => user.Id == id);
            if (index == -1) return;
            Db.RemoveAt(index);
        });
    }

    public async Task<IList<UserOutput>> GetAllAsync()
        => await Task.Run(() => _mapper.ProjectTo<UserOutput>(Db.AsQueryable()).ToList());

    public async Task<UserOutput> GetByIdAsync(Guid id)
        => await Task.Run(() => _mapper.Map<UserOutput>(Db.FirstOrDefault(w => w.Id == id)));

    public async Task<List<UserInfoOutput>> GetUsersInfoByIdAsync(Guid id)
        => await Task.Run(() => _mapper.ProjectTo<UserInfoOutput>(Db.First(user => user.Id == id).Infos.AsQueryable()).ToList());

    public async Task UpdateAgeAsync(Guid id, int age)
    {
        await Task.Run(() =>
        {
            int index = Db.FindIndex(user => user.Id == id);
            UserModel user = Db[index];
            user.Age = age;
            Db[index] = user;
        });
    }

    public async Task UpdateAsync(Guid id, UserInput input)
    {
        await Task.Run(() =>
        {
            int index = Db.FindIndex(user => user.Id == id);
            var userUpdated = _mapper.Map<UserModel>(input);
            userUpdated.Id = id;
            Db[index] = userUpdated;
        });
    }
}

public class DatabaseSeed
{
    public static void Execute(List<UserModel> Db)
    {
        if (Db.Any()) return;

        Db.AddRange(new List<UserModel>()
        {
            new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = "User " + Guid.NewGuid(),
                Age = new Random().Next(1, 25),
                Infos = new List<UserInfoModel>()
                {
                    new UserInfoModel()
                    {
                        Name = "CPF",
                        Value = "123456789"
                    },
                    new UserInfoModel()
                    {
                        Name = "RG",
                        Value = "12345678900"
                    }
                }
            },
            new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = "User " + Guid.NewGuid(),
                 Age = new Random().Next(1, 25),
                Infos = new List<UserInfoModel>()
                {
                    new UserInfoModel()
                    {
                        Name = "CPF",
                        Value = "987654321"
                    },
                    new UserInfoModel()
                    {
                        Name = "RG",
                        Value = "98765432100"
                    }
                }
            }
        });
    }
}