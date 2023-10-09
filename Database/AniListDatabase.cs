using AnimedleWeb.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace AnimedleWeb.Database
{
    public class AniListDatabase
    {
        private readonly IConfiguration _configuration;
        
        public AniListDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AniListTitle> AniListGetTitles()
        {
            List<AniListTitle> titles = new List<AniListTitle>();
            string connectionString = _configuration.GetConnectionString("AnimedleDatabase");

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("AniListGetTitles", connection) { CommandType = CommandType.StoredProcedure };

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Have to create new one each time, or only the first populates entire list
                //May be an issue with assigning, "set", AniListTitle values
                AniListTitle title = new AniListTitle();

                title.Romaji = (string)reader["TitleRomaji"];
                title.English = (string)reader["TitleEnglish"];
                title.Native = (string)reader["TitleNative"];

                titles.Add(title);
            }

            return titles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="romaji"></param>
        /// <returns></returns>
        public AniListMedia AniListGetByRomaji(string romaji)
        {
            AniListMedia media;
            string mediaAsXML;
            string connectionString = _configuration.GetConnectionString("AnimedleDatabase");

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("AniListGetByRomaji", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@TitleRomaji", romaji);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            mediaAsXML = (string)reader["MediaData"];

            //Convert XML String back to AniListMedia object
            media = (AniListMedia)new XmlSerializer(typeof(AniListMedia)).Deserialize(new StringReader(mediaAsXML));

            return media;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaList"></param>
        public void AniListSaveMedia(List<AniListMedia> mediaList)
        {
            string connectionString = _configuration.GetConnectionString("AnimedleDatabase"); 
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("AniListSaveMedia", connection) { CommandType = CommandType.StoredProcedure };

            connection.Open();

            foreach(var media in mediaList)
            {
                //Serialize AniListMedia object into xml
                StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
                XmlSerializer serializer = new XmlSerializer(typeof(AniListMedia));
                serializer.Serialize(writer, media);

                command.Parameters.AddWithValue("@ID", media.ID);

                //Should be able to make SQL variables NULLABLE, but it aint working so this garbage is happening for now
                if(media.Title.Romaji != null)
                {
                    command.Parameters.AddWithValue("@TitleRomaji", media.Title.Romaji);
                }
                else
                {
                    command.Parameters.AddWithValue("@TitleRomaji", "");
                }

                if(media.Title.English != null)
                {
                    command.Parameters.AddWithValue("@TitleEnglish", media.Title.English);
                }
                else
                {
                    command.Parameters.AddWithValue("@TitleEnglish", "");
                }

                if (media.Title.Native != null)
                {
                    command.Parameters.AddWithValue("@TitleNative", media.Title.Native);
                }
                else
                {
                    command.Parameters.AddWithValue("@TitleNative", "");
                }

                command.Parameters.AddWithValue("@MediaData", writer.ToString());

                command.ExecuteNonQuery();

                //Must clear paramaters each loop since it is a single connection
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void AniListRemoveByID(int id)
        {
            string connectionString = _configuration.GetConnectionString("AnimedleDatabase");
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("AniListRemoveByID", connection) { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AniListClearDatabase()
        {
            string connectionString = _configuration.GetConnectionString("AnimedleDatabase");
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("AniListClearDatabase", connection) { CommandType = CommandType.StoredProcedure };
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
