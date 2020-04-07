using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
//Author : Mohamed Arsath
namespace StudentApi.Models {
  public class Student {
    //define all the properties of the table with get and set
    public int student_sysid { get; set; }
    public int student_id { get; set; }
    public string student_name { get; set; }
    public int student_age { get; set; }
    public string student_email { get; set; }
  }
  //Author : Mohamed Arsath
  //will create new object of studentclass
  public class CreateStudent : Student { }
  public class ReadStudent : Student {
    public ReadStudent (DataRow row) {
      student_sysid = Convert.ToInt32 (row["student_sysid"]);
      student_id = Convert.ToInt32 (row["student_id"]);
      student_name = row["student_name"].ToString ();
      student_age = Convert.ToInt32 (row["student_age"]);
      student_email = row["student_email"].ToString ();
    }

  }
}
