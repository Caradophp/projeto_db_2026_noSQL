
using MongoDB.Bson;
using MongoDB.Driver;
using projeto.Models;
using projeto.Service;

namespace projeto.Repository;

public class UserRepository(EncryptionService encryptionService)
{
    private readonly IMongoCollection<User> _collection =
        new MongoClient(DotNetEnv.Env.GetString("MONGO_CONNECTION_STRING"))
        .GetDatabase("cultiva")
        .GetCollection<User>("users");

    private readonly EncryptionService _encryptionService = encryptionService;

    public bool CreateUser(User user)
    {
        string hashedPassword = _encryptionService.HashPassword(user.Password);
        user.Id = ObjectId.GenerateNewId().ToString();
        user.Password = hashedPassword;

        _collection.InsertOne(user);

        return true;
    }

    public User? CheckUserLogin(string email, string password)
    {
        var user = _collection.Find(u => u.Email == email)
            .Limit(1).FirstOrDefault();

        if (user == null)
        {
            return null;
        }

        if (!_encryptionService.VerifyPassword(password, user.Password))
        {
            return null;
        }

        return user;
    }

    public User? FindById(string id)
    {
        return _collection.Find(u => u.Id == id)
            .FirstOrDefault();
    }

    public User? FindByEmail(string email)
    {
        return _collection.Find(u => u.Email == email)
            .Limit(1).FirstOrDefault();
    }

    public bool ExistsByEmail(string email)
    {
        return _collection.Find(u => u.Email == email)
            .Limit(1).Any();
    }

    public void ChangePass(string email, string newPassword)
    {
        User? userExists = FindByEmail(email)
            ?? throw new Exception("E-mail informado é inválido");

        string hashedPassword =
            _encryptionService.HashPassword(newPassword);

        _collection.UpdateOne(
            u => u.Email == email,
            Builders<User>.Update.Set(u => u.Password, hashedPassword));
    }
}
