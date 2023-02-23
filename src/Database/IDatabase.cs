using AutoMapper;
using AutoMapper.QueryableExtensions;
using src.DTO;
using src.Model;

namespace src.Database;

public interface IDatabase
{
    Task<IList<UserOutput>> GetAllAsync();
    Task<UserOutput> GetByIdAsync(Guid id);
    Task CreateAsync(UserInput input);
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
        => await Task.Run(() => Db.Add(_mapper.Map<UserModel>(input)));

    public async Task<IList<UserOutput>> GetAllAsync()
        => await Task.Run(() => _mapper.ProjectTo<UserOutput>(Db.AsQueryable()).ToList());

    public async Task<UserOutput> GetByIdAsync(Guid id)
        => await Task.Run(() => _mapper.Map<UserOutput>(Db.FirstOrDefault(w => w.Id == id)));
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