using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.AspNetCore.Mvc;

using StudentApi.Models;
namespace StudentApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase {
        //declare Sql Connection and CommandObjects
        private SqlConnection _conn;
        private SqlDataAdapter _adapter;
        // GET api/values
        //Author : Mohamed Arsath
        [HttpGet]
        public IEnumerable<Student> Get () {
            //for sql server authentication
            //   _conn = new SqlConnection ("data source=LAPTOP-JP98TU1S\\MSSQLSERVER2019;Initial catalog=practicedb;user id=LAPTOP-JP98TU1S\\mohamed arsath s;password=;");
            //for Windows authentication
            _conn = new SqlConnection ("data source=LAPTOP-JP98TU1S\\MSSQLSERVER2019;Initial catalog=practicedb;Integrated Security=True;");
            DataTable _dt = new DataTable ();
            var query = "select * from dbo.StudentTable";
            _adapter = new SqlDataAdapter (query, _conn);
            // or use this type of declaration of adapter like below
            // _adapter = new SqlDataAdapter {
            //     SelectCommand = new SqlCommand (query, _conn)
            // };
            _adapter.Fill (_dt);
            List<Student> students = new List<Models.Student> (_dt.Rows.Count);
            if (_dt.Rows.Count > 0) {
                foreach (DataRow studentrecord in _dt.Rows) {
                    students.Add (new ReadStudent (studentrecord));
                }
            }
            return students;
        }
        //Author : Mohamed Arsath
        // GET api/values/5
        [HttpGet ("{id}")]
        public IEnumerable<Student> Get (int id) {
            _conn = new SqlConnection ("data source=LAPTOP-JP98TU1S\\MSSQLSERVER2019;Initial catalog=practicedb;Integrated Security=True;");
            DataTable _dt = new DataTable ();
            var query = "select * from dbo.StudentTable where student_id=" + id;
            _adapter = new SqlDataAdapter (query, _conn);
            // or use this type of declaration of adapter like below
            // _adapter = new SqlDataAdapter {
            //     SelectCommand = new SqlCommand (query, _conn)
            // };
            _adapter.Fill (_dt);
            List<Student> students = new List<Models.Student> (_dt.Rows.Count);
            if (_dt.Rows.Count > 0) {
                foreach (DataRow studentrecord in _dt.Rows) {
                    students.Add (new ReadStudent (studentrecord));
                }
            }
            return students;
        }
        //Author : Mohamed Arsath
        // POST api/values
        [HttpPost]
        public string Post ([FromBody] CreateStudent students) {
            _conn = new SqlConnection ("data source=LAPTOP-JP98TU1S\\MSSQLSERVER2019;Initial catalog=practicedb;Integrated Security=True;");
            // using Query We need to Write Query
            // var query = "insert into dbo.StudentTable(student_sysid,student_name,student_age,student_email) values(@StudentSysid,@StudentName,@StudentAge,@StudentEmail)";
            //Using Stored Procedure
            SqlCommand com = new SqlCommand ("studentinsertprocedure", _conn);
            //Using Query
            //SqlCommand com = new SqlCommand (query, _conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue ("@StudentSysid", students.student_sysid);
            com.Parameters.AddWithValue ("@StudentName", students.student_name);
            com.Parameters.AddWithValue ("@StudentAge", students.student_age);
            com.Parameters.AddWithValue ("@StudentEmail", students.student_email);
            _conn.Open ();
            // int i = com.ExecuteNonQuery ();
            // _conn.Close ();S
            int result = com.ExecuteNonQuery ();
            if (result > 0) {
                return "true";
            } else {
                return "false";
            }
        }
        //Author : Mohamed Arsath
        // PUT api/values/5
        [HttpPut ("{id}")]
        public string Put (int id, [FromBody] CreateStudent students) {
            _conn = new SqlConnection ("data source=LAPTOP-JP98TU1S\\MSSQLSERVER2019;Initial catalog=practicedb;Integrated Security=True;");
            SqlCommand com = new SqlCommand ("studentupdateprocedure", _conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue ("@Studentid", id);
            com.Parameters.AddWithValue ("@StudentName", students.student_name);
            com.Parameters.AddWithValue ("@StudentAge", students.student_age);
            com.Parameters.AddWithValue ("@StudentEmail", students.student_email);
            _conn.Open ();
            // int i = com.ExecuteNonQuery ();
            // _conn.Close ();S
            int result = com.ExecuteNonQuery ();
            if (result > 0) {
                return "true";
            } else {
                return "false";
            }
        }
        //Author : Mohamed Arsath
        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public string Delete (int id) {
            _conn = new SqlConnection ("data source=LAPTOP-JP98TU1S\\MSSQLSERVER2019;Initial catalog=practicedb;Integrated Security=True;");
            SqlCommand com = new SqlCommand ("studentdeleteprocedure", _conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue ("@student_id", id);
            _conn.Open ();
            // int i = com.ExecuteNonQuery ();
            // _conn.Close ();S
            int result = com.ExecuteNonQuery ();
            if (result > 0) {
                return "true";
            } else {
                return "false";
            }
        }
    }
}
//Author : Mohamed Arsath