using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static webappli_ben_taleb_badreddine.Pages.demandeModel;
using System.Data.SqlClient;

namespace webappli_ben_taleb_badreddine.Pages
{
    public class demandeclientsModel : PageModel
    {
        public List<demandeClients> demandeClient = new List<demandeClients>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PlancherExpert;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                { //Création de la connexion{
                    connection.Open();//Ouvrir La connexion
                    String sql = "SELECT " +
     "u.firstName, " +
     "u.lastName, " +
     "d.Largeur, " +
     "d.Longeur, " +
     "cp.Type " +
     "FROM users u " +
     "JOIN demande d ON u.id_user = d.Id_user " +
     "JOIN couvrePlancher cp ON d.id = cp.id;";
                    //Déclaration de la requête
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {   //Exécuter la requête{
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //Obtenir le « reader SQL »{
                            while (reader.Read())
                            {  //Lire les données (non vides){
                                demandeClients cvrp = new demandeClients(); //Sauvegarder les données dans un objet
                                cvrp.firstName = reader.GetString(0); //Ajouter les propriétés
                                cvrp.lastName = reader.GetString(1);
                                cvrp.Largeur = (float)reader.GetDouble(2);
                                cvrp.Longueur = (float)reader.GetDouble(3);
                                cvrp.Type = reader.GetString(4);
                                demandeClient.Add(cvrp);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    public class demandeClients
    {
        public String firstName { get; set; } = "";
        public String lastName { get; set; } = "";
        public float Largeur { get; set; }
        public float Longueur { get; set; }
        public String Type { get; set; } = "";


    }
}
