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

        SqlDataReader dr;

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
                cmd.Parameters.AddWithValue("@Location", personEntities.Location);
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

        public List<Persons> GetPersons()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from persons");
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                List<Persons> list = new List<Persons>();

                while (dr.Read())
                {
                    Persons person = new Persons
                    {
                        PersonID = Convert.ToInt32(dr["PersonID"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email  = dr["Email"].ToString(),
                        Telephone = dr["Telephone"].ToString(),
                        Location = dr["Location"].ToString(),
                        Company = dr["Company"].ToString(),
                    };
                    list.Add(person);
                }
                dr.Close();
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                dr.Close();
                con.Close();           
                throw ex;
            }
        }

        //public string AddReportRecord(Report reportEntities)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@FirstName", personEntities.FirstName);
        //        cmd.Parameters.AddWithValue("@LastName", personEntities.LastName);
        //        cmd.Parameters.AddWithValue("@Email", personEntities.Email);
        //        cmd.Parameters.AddWithValue("@Telephone", personEntities.Telephone);
        //        cmd.Parameters.AddWithValue("@Company", personEntities.Company);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        return ("Data save Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return (ex.Message.ToString());
        //    }
        //}
    }
}
