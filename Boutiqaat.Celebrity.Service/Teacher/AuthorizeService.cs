using Boutiqaat.Celebrity.Core.Helpers;
using Boutiqaat.Celebrity.Core.Response;
using Boutiqaat.Celebrity.Repository.Teacher;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Boutiqaat.Celebrity.Core.Request;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Boutiqaat.Celebrity.Service.Teacher
{
    public class AuthorizeService : IAuthRepository
    {
        private readonly AppSettings _appSettings;

        public AuthorizeService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<AuthorizeResponse> Authenticate(string email, string password)
        {
            AuthorizeResponse _user = new AuthorizeResponse();
            string json = JsonConvert.SerializeObject(new TeacherRequest()
            {
                Email = email,
                Password = password,
                Status = "LOGIN"
            });

            using (IDbConnection _cn = new SqlConnection(Global.ConnectionString))
            {
                if (_cn.State == ConnectionState.Closed) _cn.Open();
                var dc = new DynamicParameters();

                dc.Add("@JSON", json);
                IDataReader rdr = await _cn.ExecuteReaderAsync("SP_CRUD_TEACHER", dc, commandType: CommandType.StoredProcedure);
                while (rdr.Read())
                {
                    _user.Id = Convert.ToInt32(rdr["Id"]);
                    _user.Name = rdr["Name"].ToString();
                    _user.Role = rdr["Role"].ToString();
                    _user.Email = rdr["Email"].ToString();
                };

                if (_user.Id == 0)
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
                        new Claim(ClaimTypes.Name, _user.Name.ToString()),
                        new Claim(ClaimTypes.Role, _user.Role),
                        new Claim(ClaimTypes.Email, _user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                _user.Token = tokenHandler.WriteToken(token);

                _cn.Close();
                return _user;
            }
        }
    }
}
