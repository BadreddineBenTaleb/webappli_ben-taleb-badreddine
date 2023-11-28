using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using static webappli_ben_taleb_badreddine.Pages.demandeModel;

namespace webappli_ben_taleb_badreddine.Pages
{
    public class demandeModel : PageModel
    {
        public string nom { get; set; } = "";
        public string email { get; set; } = "";
        public double Longueur { get; set; }
        public double Largeur { get; set; }
        public int NbType { get; set; }
        public bool HasData { get; set; }
        public int nbType { get; set; }
        public double superficie { get; set; }
        public double taux_installation, taux_materiaux;
        public double cout_installation, cout_materiaux;
        public double total_non_inclus, taxe_materiaux, taxe_installation, total_taxe_inclus;
        public List<Couvre_Plancher> ListCouvrePlancher = new List<Couvre_Plancher>();

        public demande clsdemande = new demande();
      
        public string messageErreur = "";
        
        public void OnGet()
        {
            
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PlancherExpert;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                { //Création de la connexion{
                    connection.Open();//Ouvrir La connexion
                    String sql = "SELECT * FROM couvrePlancher";
                    //Déclaration de la requête
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {   //Exécuter la requête{
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //Obtenir le « reader SQL »{
                            while (reader.Read())
                            {  //Lire les données (non vides){
                                Couvre_Plancher cvrp = new Couvre_Plancher(); //Sauvegarder les données dans un objet
                                cvrp.id = reader.GetInt32(0); //Ajouter les propriétés
                                cvrp.Type = reader.GetString(1);
                                cvrp.Materiaux = (float)reader.GetDouble(2);
                                cvrp.MainOeuvre = (float)reader.GetDouble(3);
                                ListCouvrePlancher.Add(cvrp);
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
        

        public void OnPost()
        {
            if (int.TryParse(Request.Form["NbType"], out int nbType))
            {
                // Utilisez nbType dans votre logique de commutation
                foreach (var type in ListCouvrePlancher)
                {
                    if (nbType == type.id)
                    {
                        // Logique de commutation ici
                        break;
                    }
                }
            }
            else
            {
                // Gestion de l'erreur si la conversion échoue
                Console.WriteLine("La conversion de la chaîne en int a échoué.");
            }

            string userId = HttpContext.Session.GetString("userId");
       
                int userIdValue = int.Parse(userId);
            clsdemande.Id_user = userIdValue;
            clsdemande.id = nbType;






            if (float.TryParse(Request.Form["Larg"], out float valeurlargueur))
            {
                clsdemande.Largeur = valeurlargueur;
            }
            else
            {
                // Gestion de l'erreur de conversion si nécessaire
                Console.WriteLine("La conversion de la chaîne en float a échoué.");
            }

            if (float.TryParse(Request.Form["Long"], out float valeurlong))
            {
                clsdemande.Longueur = valeurlong;
            }
            else
            {
                // Gestion de l'erreur de conversion si nécessaire
                Console.WriteLine("La conversion de la chaîne en float a échoué.");
            }

            //Sauvegarder les données dans la base de données
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PlancherExpert;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Création de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "INSERT INTO demande (Id_user , Largeur, Longeur,id)" +
                        "Values(@id_user,@Larg,@Long,@id)"  ;//Déclaration de la requête
                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //préparer la requête
                    {
                        cmd.Parameters.AddWithValue("@id", clsdemande.id);
                        cmd.Parameters.AddWithValue("@id_user", clsdemande.Id_user);
                        cmd.Parameters.AddWithValue("@Larg", clsdemande.Largeur);
                        cmd.Parameters.AddWithValue("@Long", clsdemande.Longueur);

                        cmd.ExecuteNonQuery(); //Exécuter la requête
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            










            


            HasData = true;
           

            if (double.TryParse(Request.Form["Long"], out double longueur))
            {
                Longueur = longueur;
            }
            else
            {
            }

            if (double.TryParse(Request.Form["larg"], out double largeur))
            {
                Largeur = largeur;
            }
            else
            {
            }
            superficie = largeur * longueur;

           

            Console.WriteLine(nom);
            Console.WriteLine(email);
            Console.WriteLine(Longueur);
            Console.WriteLine(Largeur);
            Console.WriteLine(nbType);


             switch (nbType)
            {
                case 1:
                    taux_installation = 2;
                    break;
                case 2:
                    taux_installation = 2.25D;
                    break;
                case 3:
                    taux_installation = 3.25D;
                    break;
                case 4:
                    taux_installation = 2.25D;
                    break;
                case 5:
                    taux_installation = 3.25D;
                    break;
                default:
                    System.Console.WriteLine("erreur donne une valeur entre 1 et 5");
                    break;

            }
            cout_installation = taux_installation * superficie;

            switch (nbType)
            {
                case 1:
                    taux_materiaux = 1.29D;
                    break;
                case 2:
                    taux_materiaux = 3.29D;
                    break;
                case 3:
                    taux_materiaux = 3.49D;
                    break;
                case 4:
                    taux_materiaux = 1.99D;
                    break;
                case 5:
                    taux_materiaux = 5.66D;
                    break;
                default:
                    System.Console.WriteLine("erreur donne une valeur entre 1 et 5");
                    break;


            }
            cout_materiaux = taux_materiaux * superficie;
            total_non_inclus = cout_materiaux + cout_installation;
            taxe_materiaux = cout_materiaux * 15 / 100;
            taxe_installation = cout_installation * 15 / 100;
            total_taxe_inclus = total_non_inclus + taxe_installation + taxe_materiaux;

        }
        public class Couvre_Plancher
        {
            public int id { get; set; }
            public string Type { get; set; } = "";
            public float Materiaux { get; set; }
            public float MainOeuvre { get; set; }
        }
        public class demande
        {
            public int Id_demande { get; set; }
            public int Id_user { get; set; }
            public float Largeur { get; set; }
            public float Longueur { get; set; }
            public int id { get; set; }


        }
      
    }
}