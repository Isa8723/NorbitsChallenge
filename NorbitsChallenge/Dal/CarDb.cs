using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
        }

        public int GetTireCount(int companyId, string licensePlate)
        {
            int result = 0;

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand {Connection = connection, CommandType = CommandType.Text})
                {
                    command.CommandText = $"select * from car where companyId = @CID and licenseplate = @LP";
                    command.Parameters.AddWithValue("@LP", licensePlate);
                    command.Parameters.AddWithValue("@CID", companyId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = (int) reader["tireCount"];
                        }
                    }
                }
            }

            return result;
        }

        public List<string> GetList(int companyId)
        {
            List<string> result = new List<string>();

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select licenseplate from car where companyId = {companyId}" ;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader["licenseplate"].ToString());
                        }
                    }
                }
            }

            return result;
        }

        public string GetDescription(int companyId, string licensePlate)
        {
            string result = "";

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader["description"].ToString();
                        }
                    }
                }
            }

            return result;
        }

        public string GetModel(int companyId, string licensePlate)
        {
            string result = "";

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader["model"].ToString();
                        }
                    }
                }
            }

            return result;
        }

        public string GetBrand(int companyId, string licensePlate)
        {
            string result = "";

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader["brand"].ToString();
                        }
                    }
                }
            }

            return result;
        }

        public string AddNewCar(int companyId, string licensePlate, string description, string carmodel, string brand, int tireCount)
        {
            string result = "";

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"insert into car (LicensePlate, Description, Model, Brand, TireCount, CompanyId) values ('{licensePlate}', '{description}', '{carmodel}', '{brand}', {tireCount}, {companyId})";

                    int rowsAffected = command.ExecuteNonQuery();

                    result = rowsAffected > 0 ? "Lagt til ny bil" : "Kunne ikke legge til bilen";
                }
            }

            return result;
        }

        public string DeleteCar(int companyId, string licensePlate)
        {
            string result = "";

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"delete from car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    int rowsAffected = command.ExecuteNonQuery();

                    result = rowsAffected > 0 ? "Slettet bil" : "Kunne ikke slette bilen";
                }
            }

            return result;
        }

        public string ModifyCar(int companyId, string licensePlate, string description, string carmodel, string brand, int tireCount)
        {
            string result = "";

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"update car set Description='{description}', Model='{carmodel}', Brand='{brand}', TireCount={tireCount} where companyId = {companyId} and licenseplate = '{licensePlate}'"; 

                    int rowsAffected = command.ExecuteNonQuery();


                    result = rowsAffected > 0 ? "Redigert bil" : "Kunne ikke redigere bilen";
                }
            }

            return result;
        }


    }
}
