using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;

namespace HampesYatzy
{
    class DbOperations
    {
        public static List<Player> GetAllPlayers()
        {
            List<Player> plist = new List<Player>();
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM player";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Player p = new Player
                            {
                                Id = reader.GetInt32(0),
                                Firstname = reader.GetString(1),
                                Nickname = reader.GetString(2),
                                Lastname = reader.GetString(3)
                            };
                            plist.Add(p);
                        }
                    }
                }
                return plist;
            }
        }
        public static List<Player> GetInGamePlayers()
        {

            List<Player> plist = new List<Player>();
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT player.player_id, player.firstname, player.lastname, player.nickname FROM game_player JOIN game ON game.game_id = game_player.game_id JOIN player ON player.player_id = game_player.player_id WHERE game.ended_at IS NULL";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Player p = new Player
                            {
                                Id = reader.GetInt32(0),
                                Firstname = reader.GetString(1),
                                Nickname = reader.GetString(2),
                                Lastname = reader.GetString(3)
                            };
                            plist.Add(p);
                        }
                    }
                }
                return plist;
            }
        }
    }
}
