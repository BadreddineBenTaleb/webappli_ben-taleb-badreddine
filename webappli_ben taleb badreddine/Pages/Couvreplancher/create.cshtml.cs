using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static webappli_ben_taleb_badreddine.Pages.demandeModel;

namespace webappli_ben_taleb_badreddine.Pages.Couvreplancher
{
    public class createModel : PageModel
    {
        public Couvre_Plancher cvrcreate = new Couvre_Plancher();
        public string messageErreur = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            cvrcreate.id = Convert.ToInt32(Request.Form["id"]);
            cvrcreate.Type = Request.Form["Type"];
            cvrcreate.Materiaux = float.Parse(Request.Form["Materiaux"]);
            cvrcreate.MainOeuvre= float.Parse(Request.Form["MainOeuvre"]);

            //Sauvegarder les donn�es dans la base de donn�es
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Cr�ation de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "INSERT INTO couvrePlancher(id,Type,Materiaux,MainOeuvre)" +
                        "values(@id,@Type,@Materiaux,@MainOeuvre);";//D�claration de la requ�te
                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //pr�parer la requ�te
                    {
                        cmd.Parameters.AddWithValue("@id", cvrcreate.id);
                        cmd.Parameters.AddWithValue("@Type", cvrcreate.Type);
                        cmd.Parameters.AddWithValue("@Materiaux", Convert.ToString(cvrcreate.Materiaux));
                        cmd.Parameters.AddWithValue("@MainOeuvre", Convert.ToString(cvrcreate.MainOeuvre));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            //Reinitialiser les donn�es
            cvrcreate.id =0;
            cvrcreate.Type = "";
            cvrcreate.Materiaux = 0.00F;
            cvrcreate.MainOeuvre = 0.00F;

            Response.Redirect("/Couvreplancher/Index");
        }
    }
}
