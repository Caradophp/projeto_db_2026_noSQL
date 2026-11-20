using MongoDB.Bson;
using MongoDB.Driver;
using projeto.Models;
using projeto.Service;

namespace projeto.Repository;

public class UserRepository(EncryptionService encryptionService)
{
    private readonly IMongoCollection<User> _collection = new MongoClient("mongodb://admin:123@localhost:27017/?authSource=admin")
        .GetDatabase("cultiva")
        .GetCollection<User>("users");

    private readonly EncryptionService _encryptionService = encryptionService;

    public bool CreateUser(User user)
    {
        string hashedPassword = _encryptionService.HashPassword(user.Password);
        var id = ObjectId.GenerateNewId().ToString();
        user.Id = id;
        user.Password = hashedPassword;
        _collection.InsertOne(user);
        return true;
    }
    
    public User? CheckUserLogin(string email, string password)
    {
        var user = _collection.Find(u => u.Email == email).Limit(1).First();

        if (!_encryptionService.VerifyPassword(password, user.Password))
        {
            return null;
        }

        return user;
    }
    
    public User? FindById(string id)
    {
        return _collection.Find(u => u.Id == id).First();       
    }

    public User? FindByEmail(string email)
    {
        var user = _collection.Find(u => u.Email == email).Limit(1).First();
        return user;
    }

    public bool ExistsByEmail(string email)
    {
       var user = _collection.Find(u => u.Email == email).Limit(1).First();

        return user != null;
    }


    public void ChangePass(string email, string newPassword)
    {
        User? userExists = this.FindByEmail(email) ?? throw new Exception("E-mail informado é inválido");

        string hashedPassword = encryptionService.HashPassword(newPassword);

        _collection.UpdateOne(
            u => u.Email == email,
            Builders<User>.Update.Set(u => u.Password, hashedPassword));
    }

}