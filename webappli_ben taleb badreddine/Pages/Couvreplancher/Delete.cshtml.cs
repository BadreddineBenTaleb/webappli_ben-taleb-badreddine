using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static webappli_ben_taleb_badreddine.Pages.demandeModel;


namespace webappli_ben_taleb_badreddine.Pages.Couvreplancher
{
    public class DeleteModel : PageModel
    {
        public string messageErreur = "";
        public Couvre_Plancher cvrdelete = new Couvre_Plancher();
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                //connection string
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Cr�ation de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "DELETE FROM couvrePlancher  WHERE id=@id;"; //D�claration de la requ�te

                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //Ex�cuter la requ�te
                    {
                        cmd.Parameters.AddWithValue("@id", id); //Avoir le produit avec l'id pass� par la page Index
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            Response.Redirect("/Couvreplancher/Index");
        }
        public void OnPost()
        {

        }
    }
}
