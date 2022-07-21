using System.Data.SqlClient;
using System.Configuration;

using EmpSystemApp.Models;
using static EmpSystemApp.Services.EmpService;
using System.Data;

namespace EmpSystemApp.Services
{
    public class EmpService : IEmpService
    {
        public string Add(Employees employee)
        {
            SqlConnection connection = new SqlConnection("Data source = (localdb)\\MSSQLLocalDB; Initial Catalog =JoinADO ");
            connection.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[sp_Add]";
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Surname", employee.Surname);
            cmd.Parameters.AddWithValue("@Age", employee.Age);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@DepartmentName", employee.DepartmentName);
            cmd.Parameters.AddWithValue("@Description", employee.Description);



            cmd.ExecuteNonQuery();
            connection.Close();

            return "Added";
        }

        public List<Employees> getAllEmp()
        {
            List<Employees> productList = new List<Employees>();



            using (SqlConnection connection = new SqlConnection("Data source = (localdb)\\MSSQLLocalDB; Initial Catalog =JoinADO "))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_JoinThreeTables";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtProducts = new DataTable();

                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Employees

                    {

                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        Name = dr["Name"].ToString(),
                        Surname = dr["Surname"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Address = dr["Address"].ToString(),
                        DepartmentName = dr["DepartmentName"].ToString(),
                        Description = dr["Description"].ToString(),






                    });
                }


            }
            return productList;




        }

        void IEmpService.DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        List<Employees> IEmpService.GetById(int EmployeeId)
        {
            List<Employees> employees = new List<Employees>();
            using (SqlConnection connection = new SqlConnection("Data source = (localdb)\\MSSQLLocalDB; Initial Catalog =JoinADO "))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_GetById";
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dtProducts = new DataTable();

                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();

                foreach (DataRow dr in dtProducts.Rows)
                {
                    employees.Add(new Employees

                    {


                        Name = dr["Name"].ToString(),
                        Surname = dr["Surname"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Address = dr["Address"].ToString(),
                        DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                        PositionId = Convert.ToInt32(dr["PositionId"]),






                    });
                }

            }
            return employees;
        }
        bool IEmpService.Update(Employees employee)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection("Data source = (localdb)\\MSSQLLocalDB; Initial Catalog =JoinADO "))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_Update ";
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Surname", employee.Surname);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                cmd.Parameters.AddWithValue("@PositionId", employee.PositionId);
                id = cmd.ExecuteNonQuery();
                connection.Close();




            }
            if (id > 0)
            {
                return true;

            }
            else { return false; }

        }

        public interface IEmpService
        {
            public List<Employees> getAllEmp();

            public string Add(Employees employee);

            public List<Employees> GetById(int id);

            void DeleteById(int id);

            public bool Update(Employees employee);



        }
    }
}

