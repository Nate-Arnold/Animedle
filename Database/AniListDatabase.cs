using AnimedleWeb.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;

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
                command.Parameters.AddWithValue("@TitleRomaji", media.Title.Romaji);
                command.Parameters.AddWithValue("@TitleEnglish", media.Title.English);
                command.Parameters.AddWithValue("@TitleNative", media.Title.Native);
                command.Parameters.AddWithValue("@MediaData", writer.ToString());

                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }
    }
}
