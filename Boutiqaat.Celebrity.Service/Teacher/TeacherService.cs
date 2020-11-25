using Boutiqaat.Celebrity.Core.Response;
using Boutiqaat.Celebrity.Repository.Teacher;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using Boutiqaat.Celebrity.Core.Helpers;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Boutiqaat.Celebrity.Service.Teacher
{
    public class TeacherService : ITeacherRepostory
    {
        public async Task<ResponseMessage> AddTeacherAsync(string json)
        {
            using (IDbConnection _cn = new SqlConnection(Global.ConnectionString))
            {
                ResponseMessage rm = new ResponseMessage() { Code = 0, Message = "" };
                if (_cn.State == ConnectionState.Closed) _cn.Open();
                var dc = new DynamicParameters();
                dc.Add("@JSON", json);
                IDataReader rdr = await _cn.ExecuteReaderAsync("SP_CRUD_TEACHER", dc, commandType: CommandType.StoredProcedure);
                while (rdr.Read())
                {
                    rm.Code = Convert.ToInt32(rdr[0]);
                    rm.Message = rdr[1].ToString();
                };
                _cn.Close();
                return rm;
            }
        }

        public async Task<ResponseMessage> DeleteTeacherAsync(string json)
        {
            using (IDbConnection _cn = new SqlConnection(Global.ConnectionString))
            {
                ResponseMessage rm = new ResponseMessage() { Code = 0, Message = "" };
                if (_cn.State == ConnectionState.Closed) _cn.Open();
                var dc = new DynamicParameters();
                dc.Add("@JSON", json);
                IDataReader rdr = await _cn.ExecuteReaderAsync("SP_CRUD_TEACHER", dc, commandType: CommandType.StoredProcedure);
                while (rdr.Read())
                {
                    rm.Code = Convert.ToInt32(rdr[0]);
                    rm.Message = rdr[1].ToString();
                };
                _cn.Close();
                return rm;
            }
        }

        public async Task<ResponseMessage> EditTeacherAsync(string json)
        {
            using (IDbConnection _cn = new SqlConnection(Global.ConnectionString))
            {
                ResponseMessage rm = new ResponseMessage() { Code = 0, Message = "" };
                if (_cn.State == ConnectionState.Closed) _cn.Open();
                var dc = new DynamicParameters();
                dc.Add("@JSON", json);
                IDataReader rdr = await _cn.ExecuteReaderAsync("SP_CRUD_TEACHER", dc, commandType: CommandType.StoredProcedure);
                while (rdr.Read())
                {
                    rm.Code = Convert.ToInt32(rdr[0]);
                    rm.Message = rdr[1].ToString();
                };
                _cn.Close();
                return rm;
            }
        }

        public async Task<ResponseMessage> GetTeacherByIdAsync(string json)
        {
            using (IDbConnection _cn = new SqlConnection(Global.ConnectionString))
            {

                ResponseMessage rm = new ResponseMessage() { Code = 0, Message = "" };
                if (_cn.State == ConnectionState.Closed) _cn.Open();
                var dc = new DynamicParameters();
                dc.Add("@JSON", json);
                IDataReader rdr = await _cn.ExecuteReaderAsync("SP_CRUD_TEACHER", dc, commandType: CommandType.StoredProcedure);
                while (rdr.Read())
                {
                    TeacherResponse _teacher = new TeacherResponse
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Gender = rdr["Gender"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"]),
                        InsertedOn = rdr["InsertedOn"].ToString()
                    };
                    rm.Code = 1;
                    rm.Message = JsonConvert.SerializeObject(_teacher);
                };
                _cn.Close();
                return rm;
            }
        }

        public async Task<ResponseMessage> GetTeachersAsync(string json)
        {
            using (IDbConnection _cn = new SqlConnection(Global.ConnectionString))
            {
                List<TeacherResponse> _teachers = new List<TeacherResponse>();
                ResponseMessage rm = new ResponseMessage() { Code = 0, Message = "" };
                if (_cn.State == ConnectionState.Closed) _cn.Open();
                var dc = new DynamicParameters();
                dc.Add("@JSON", json);
                IDataReader rdr = await _cn.ExecuteReaderAsync("SP_CRUD_TEACHER", dc, commandType: CommandType.StoredProcedure);
                while (rdr.Read())
                {
                    rm.Code = 1;
                    _teachers.Add(new TeacherResponse
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Gender = rdr["Gender"].ToString(),
                        Salary = Convert.ToDecimal(rdr["Salary"]),
                        InsertedOn = rdr["InsertedOn"].ToString()
                    });
                };
                _cn.Close();
                rm.Message = JsonConvert.SerializeObject(_teachers);
                return rm;
            }
        }
    }
}
