using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class ContactDBAccessLayer
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-8FK8A22; Initial Catalog = PhoneBook; Integrated Security = True");
        public string AddPersonRecord(Persons personEntities)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("pr_AddPerson", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", personEntities.FirstName);
                cmd.Parameters.AddWithValue("@LastName", personEntities.LastName);
                cmd.Parameters.AddWithValue("@Email", personEntities.Email);
                cmd.Parameters.AddWithValue("@Telephone", personEntities.Telephone);
                cmd.Parameters.AddWithValue("@Company", personEntities.Company);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Data save Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }

    }
}
