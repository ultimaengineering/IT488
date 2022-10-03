using System.Collections;
using Inventory_Tracker.DAL;
using Inventory_Tracker.Entities;
using Inventory_Tracker.Helpers;
using Inventory_Tracker.Models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Inventory_Tracker.Services
{
    using BCrypt.Net;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        User CreateUser(UserCreationRequest model);
        User UpdateUser(UpdateUserRequest model);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly DbContext _context;
        private readonly ILogger _logger;

        public UserService(IOptions<AppSettings> appSettings, DbContext context, ILogger<UserService> logger)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _logger = logger;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            try
            {
                User? user = _context.Users.First(x => x.Username == model.Username);
                // return null if user not found
                if (!BCrypt.EnhancedVerify(model.Password, user.Password, hashType: HashType.SHA384))
                {
                    return null;
                }

                // authentication successful so generate jwt token
                var token = generateJwtToken(user);
                return new AuthenticateResponse(user, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public IEnumerable<User> GetAll()
        {
            IEnumerable <User> users;
            List<User> table = new List<User>();
            table = _context.Users.Select(x => x).ToList();
            return table;
        }

        public User GetById(Guid id)
        {
            User user = null;
            user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public User CreateUser(UserCreationRequest model)
        {
            User user = default;
            try 
            {
                user = new User 
                {
                    Username = model.Username,
                    Password = BCryptNet.EnhancedHashPassword(model.Password, hashType: HashType.SHA384),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    account_created = DateTime.Now,
                Id = Guid.NewGuid(),
                };

                _context.Add(user);
                _context.SaveChanges();
            }
            catch(Exception exception)
            {
                _logger.LogError("Unable to continue with creation {}", exception);
                return null;
            }
            return user;
        }
        // helper methods


        public User? UpdateUser(UpdateUserRequest model)
        {
            User? user = _context.Users.FirstOrDefault(x => x.Username == model.OldUsername);
            if (user == null) // does the user exist
            {
                return default;
            }
            
            if (model.Username != model.OldUsername) // We are updating the username, so double check for conflict.
            {
                User? checkForCollisionConflict = _context.Users.FirstOrDefault(x => x.Username == model.Username);
                if (checkForCollisionConflict != default) // if there is a user, and we aren't updating ourselves. 
                {
                    return default;
                }
            }

            try
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Username = model.Username;
               
                if (model.Password != default && model.Password != "")
                {
                    user.Password = BCryptNet.EnhancedHashPassword(model.Password, hashType: HashType.SHA384);
                }
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError("Unable to continue with creation {}", exception);
                return null;
            }
            return user;
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.Username),
                    new Claim("firstName", user.FirstName),
                    new Claim("lastName", user.LastName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        
    }
}
