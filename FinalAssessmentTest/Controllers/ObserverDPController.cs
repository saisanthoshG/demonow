using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using FinalAssessmentTest.Models;

namespace FinalAssessmentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObserverDPController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ObserverDPController(IConfiguration configuration)
        {
            _configuration = configuration;

        }



        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Message from
                            dbo.ObserverDP";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);

        }

        [HttpPut]
        public JsonResult Put(ObserverDP obs)
        {
            string query = @"
                            update dbo.ObserverDP
                            set Message = @str
                            where Id = 1";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@str", obs.Message);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Sucessfully");

        }

    }
}
