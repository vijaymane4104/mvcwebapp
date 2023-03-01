using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace WebAPP1.Models
{
    public class EmployeeContext
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-CLDOE1G\\SQLEXPRESS;Initial Catalog=Employee;Integrated Security=true");
        public List<Employee> GetEmployees()
        {

            List<Employee> emplist = new List<Employee>();
            SqlCommand cmd = new SqlCommand("sp_getEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Employee emp = new Employee();
                emp.EmpID = Convert.ToInt32(dr[0]);
                emp.EmpName = Convert.ToString(dr[1]);
                emp.EmpSalary = Convert.ToString(dr[2]);
                emplist.Add(emp);


            }

            return emplist;
        }
        public List<Employee> GetEmployees2()
        {

            List<Employee> emplist = new List<Employee>();
            using (var connection = con)
            {
                connection.Open();
                //const string sql = @"select * from Employeee where EmpID in (2,9)";

                var p = new DynamicParameters();
               
                using (var multi = connection.QueryMultiple("sp_getEmployees",p,commandType:CommandType.StoredProcedure ))
                {
                    emplist = multi.Read<Employee>().AsList();

                }


            }

            return emplist;
        }
        public int CreateEmployee(Employee emp)
        {
            List<Employee> emplist = new List<Employee>();
            SqlCommand cmd = new SqlCommand("SP_ADDEMPLOYEE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EMPNAME", emp.EmpName);
            cmd.Parameters.AddWithValue("@EMPSALARY", emp.EmpSalary);
            int i = cmd.ExecuteNonQuery();
            return i;


        }
        public Employee GetEmployeeByID(string emp)
        {
            List<Employee> emplist = new List<Employee>();
            SqlCommand cmd = new SqlCommand("sp_GetemployeeById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", emp);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Employee emp1 = new Employee();
            foreach (DataRow dr in dt.Rows)
            {

                emp1.EmpID = Convert.ToInt32(dr[0]);
                emp1.EmpName = Convert.ToString(dr[1]);
                emp1.EmpSalary = Convert.ToString(dr[2]);

            }


            return emp1;


        }
        public int UpdateEmployee(Employee emp)
        {


            SqlCommand cmd = new SqlCommand("SP_UPDATEEMPLOYEE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EMPID", emp.EmpID);
            cmd.Parameters.AddWithValue("@EMPNAME", emp.EmpName);
            cmd.Parameters.AddWithValue("@EMPSALARY", emp.EmpSalary);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public int DeleteEmployee(string id)
        {


            SqlCommand cmd = new SqlCommand("sp_DeleteById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EMPID", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }

    }
}