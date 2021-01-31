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


        public bool AddPersonRecord(Persons personEntities)
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
                var result = cmd.ExecuteNonQuery();
                con.Close();
                return result == 1;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                Console.WriteLine(ex.Message);// büyük uygulamalarda hata loglanır.
                return false;
            }
        }

        public bool AddReportRecord(AddReportDTO reportEntities)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("pr_AddReport", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportDate", reportEntities.ReportDate);
                cmd.Parameters.AddWithValue("@Status", reportEntities.Status);
                cmd.Parameters.AddWithValue("@PersonID", reportEntities.PersonID);
                con.Open();
                var result = cmd.ExecuteNonQuery();
                con.Close();
                return result == 1;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return false;
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
                        Email = dr["Email"].ToString(),
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
                throw;
            }
        }
        public Persons GetPersonsByID(int id)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from persons where PersonID=@PersonID");
                cmd.Parameters.AddWithValue("@PersonID", id);
                cmd.Connection = con;
                dr = cmd.ExecuteReader();
                Persons p = new Persons();
                while (dr.Read())
                {
                    p.PersonID = Convert.ToInt32(dr["PersonID"]);
                    p.FirstName = dr["FirstName"].ToString();
                    p.LastName = dr["LastName"].ToString();
                    p.Email = dr["Email"].ToString();
                    p.Telephone = dr["Telephone"].ToString();
                    p.Location = dr["Location"].ToString();
                    p.Company = dr["Company"].ToString();

                }
                dr.Close();
                con.Close();
                return p;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Report GetReportByID(int id)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Report where ID=@ID");
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Connection = con;
                dr = cmd.ExecuteReader();
                Report r = new Report();
                while (dr.Read())
                {
                    r.ID = Convert.ToInt32(dr["ID"]);
                    r.Status = dr["Status"].ToString();
                    r.ReportDate = DBNull.Value == dr["ReportDate"] ? null : Convert.ToDateTime(dr["ReportDate"]);
                    r.PersonID = Convert.ToInt32(dr["PersonID"]);
                }
                dr.Close();
                con.Close();
                return r;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Location ID olarak ayrı tabloda tutulabilir
        //Status ID olarak ayrı tabloda tutulabilir
        //Business katmanında da 
        //inputlarda email,telefon kontrolleri yapılabilir (bu kontroller hem fd tarafında hemde back end tarafında yapılmalıdır.Aksi takdirde algoritma patlar)
        //Database'de istenilen değerlere kısıtlama yapılabilir bu kısıtlama sonucunda program daha fazla performanlı çalışıp daha az yer kaplar.

        public List<Report> GetReport()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT r.ID, p.FirstName, p.LastName,p.Company, p.Location ,r.ReportDate,r.Status FROM Persons p Inner JOIN Report r ON p.PersonID = r.PersonID", con);
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                List<Report> List = new List<Report>();



                while (dr.Read())
                {

                    Report report = new Report
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Company = dr["Company"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        ReportDate = DBNull.Value == dr["ReportDate"] ? null : Convert.ToDateTime(dr["ReportDate"]),
                        Status = DBNull.Value == dr["Status"] ? null : (dr["Status"].ToString()),
                        Location = dr["Location"].ToString(),
                    };
                    List.Add(report);
                }

                dr.Close();
                con.Close();
                return List;
            }
            catch (Exception ex)
            {
                dr.Close();
                con.Close();
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool DeletePerson(int id)
        {
            try
            {
                string reportDeleteSql = "DELETE FROM [PhoneBook].[dbo].[Report] WHERE [PersonID]=@PersonID";
                string personDeleteSql = $"Delete From Persons Where PersonID = @PersonID";



                SqlCommand command = new SqlCommand(reportDeleteSql);
                command.Parameters.AddWithValue("@PersonID", id);
                con.Open();
                command.Connection = con;
                command.ExecuteNonQuery();
                command.CommandText = personDeleteSql;
                var result = command.ExecuteNonQuery();
                return result == 1;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DeleteReport(int id)
        {
            try
            {
                string reportDeleteSql = "DELETE FROM [PhoneBook].[dbo].[Report] WHERE [ID]=@ID";

                SqlCommand command = new SqlCommand(reportDeleteSql);
                command.Parameters.AddWithValue("@ID", id);
                con.Open();
                command.Connection = con;
                command.CommandText = reportDeleteSql;
                var result = command.ExecuteNonQuery();
                return result == 1;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public void EditPerson(Persons person)
        {

            try
            {
                string editPersons = "Update Persons  set FirstName = @FirstName,LastName = @LastName,Email = @Email,Telephone = @Telephone, Location = @Location WHERE PersonID = @PersonID";
                SqlCommand cmd = new SqlCommand(editPersons);

                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Telephone", person.Telephone);
                cmd.Parameters.AddWithValue("@Location", person.Location);
                cmd.Parameters.AddWithValue("@Company", person.Company);

                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

        }



    }
}

