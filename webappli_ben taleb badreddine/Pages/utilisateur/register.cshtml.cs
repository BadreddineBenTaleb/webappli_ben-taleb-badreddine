using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace webappli_ben_taleb_badreddine.Pages.utilisateur
{
    public class registerModel : PageModel
    {
        public Utilisateur utilisateurData = new Utilisateur();
        public string msgError = "";
        public void OnGet()
        { }
        public void OnPost()
        {
            utilisateurData.firstName = Request.Form["firstName"];
            utilisateurData.lastName = Request.Form["lastName"];
            utilisateurData.userLogin = Request.Form["email"];
            utilisateurData.userPw = Request.Form["pw"];
            utilisateurData.userType = "client";
            if (utilisateurData.firstName == "" || utilisateurData.lastName == "" ||
                utilisateurData.userLogin == "" || utilisateurData.userPw == "")
            {
                msgError = "Veuillez saisir vos données";
                return;
            }
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Création de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "INSERT INTO users(firstName,lastName,userLogin,userPw,userType)" +
                        "values(@firstName,@lastName,@userLogin,@userPw,@userType);";//Déclaration de la requête
                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //préparer la requête
                    {
                        cmd.Parameters.AddWithValue("@firstName", utilisateurData.firstName);
                        cmd.Parameters.AddWithValue("@lastName", utilisateurData.lastName);
                        cmd.Parameters.AddWithValue("@userLogin", utilisateurData.userLogin);
                        cmd.Parameters.AddWithValue("@userPw", utilisateurData.userPw);
                        cmd.Parameters.AddWithValue("@userType", utilisateurData.userType);



                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            
           

            Response.Redirect("/utilisateur/Login");
        }
            

    }
}
