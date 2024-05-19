using JwtAuthMongoDemo.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace JwtAuthMongoDemo.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDB"));
            var database = client.GetDatabase(config.GetSection("MongoDB:DatabaseName").Value);
            _users = database.GetCollection<User>(config.GetSection("MongoDB:UsersCollectionName").Value);
        }

        public User GetUser(string username) => _users.Find(user => user.Username == username).FirstOrDefault();
        public void CreateUser(User user) => _users.InsertOne(user);
    }
}
