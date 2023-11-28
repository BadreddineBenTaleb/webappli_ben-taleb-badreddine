using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static webappli_ben_taleb_badreddine.Pages.demandeModel;

namespace webappli_ben_taleb_badreddine.Pages.Couvreplancher
{
    public class editModel : PageModel
    {
        public Couvre_Plancher cvrmodifi = new Couvre_Plancher();
        public string messageErreur = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                //connection string
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Création de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "SELECT * FROM couvrePlancher WHERE id=@id;"; //Déclaration de la requête

                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //Exécuter la requête
                    {
                        cmd.Parameters.AddWithValue("@id", id); //Avoir le produit avec l'id passé par la page Index
                        using (SqlDataReader reader = cmd.ExecuteReader()) //Obtenir le reader SQL aprés exécution
                        {
                            if (reader.Read()) //Lire les données (non vides)
                            {
                                cvrmodifi.id = reader.GetInt32(0);
                                cvrmodifi.Type = reader.GetString(1);
                                cvrmodifi.Materiaux = (float)reader.GetDouble(2);
                                cvrmodifi.MainOeuvre = (float)reader.GetDouble(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
		public void OnPost()
		{
			cvrmodifi.id = Convert.ToInt32(Request.Form["id"]);
			cvrmodifi.Type = Request.Form["Type"];
			cvrmodifi.Materiaux = float.Parse(Request.Form["Materiaux"]);
			cvrmodifi.MainOeuvre = float.Parse(Request.Form["MainOeuvre"]);

			if (string.IsNullOrEmpty(cvrmodifi.Type) || cvrmodifi.Materiaux == 0 || cvrmodifi.MainOeuvre == 0)
			{
				messageErreur = "Veuillez saisir le type de couvre et les valeurs de couvre plancher valide";
				return;
			}

			try
			{
				// connection string
				string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PlancherExpert;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString)) // Création de la connection
				{
					connection.Open(); // Ouvrir La connection
					String sql = "UPDATE couvrePlancher SET id=@id,  Type=@Type, Materiaux=@Materiaux, MainOeuvre=@MainOeuvre WHERE id=@id;"; // Déclaration de la requête

					using (SqlCommand cmd = new SqlCommand(sql, connection)) // préparer la requête
					{
						Console.WriteLine("Modifier le produit n: " + Convert.ToString(cvrmodifi.id));
						cmd.Parameters.AddWithValue("@id", Convert.ToString(cvrmodifi.id));

						// Ajoutez une vérification supplémentaire pour @Type
						if (!string.IsNullOrEmpty(cvrmodifi.Type))
						{
							cmd.Parameters.AddWithValue("@Type", cvrmodifi.Type);
						}
						else
						{
							// Gérez le cas où 'Type' est nul ou vide
							messageErreur = "La valeur de 'Type' est manquante.";
							return;
						}

						cmd.Parameters.AddWithValue("@Materiaux", Convert.ToString(cvrmodifi.Materiaux));
						cmd.Parameters.AddWithValue("@MainOeuvre", Convert.ToString(cvrmodifi.MainOeuvre));
						Console.WriteLine("preparer la requêtes");

						cmd.ExecuteNonQuery(); // Exécuter la requête
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
			Response.Redirect("/Couvreplancher/Index");
		}

	}
}
