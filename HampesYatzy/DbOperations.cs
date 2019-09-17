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
        public static List<Player> GetFreePlayers()
        {
            var plist = GetAllPlayers();
            var busylist = GetInGamePlayers();
            //List<InGamePlayer> freeList = new List<InGamePlayer>();
            for (int i = 0; i < plist.Count; i++)
            {
                for (int j = 0; j < busylist.Count; j++)
                {
                    if (plist[i].Id == busylist[j].Id)
                    {
                        plist.Remove(plist[i]);
                    }
                }
            }
            return plist;
        }

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
                    cmd.CommandText = "SELECT player.player_id, player.firstname, player.lastname, player.nickname FROM game_player JOIN player ON game_player.player_id = player.player_id JOIN game ON game.game_id = game_player.game_id WHERE game.ended_at IS NULL GROUP BY player.player_id, player.firstname, player.lastname, player.nickname";
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

        public static List<Player> GetMostGamesPlayer()
        {
            List<Player> plist = new List<Player>();
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "WITH rankgamesamount AS (SELECT player.nickname, player.firstname, player.lastname, COUNT(game_player.player_id) FROM game_player JOIN player ON player.player_id = game_player.player_id GROUP BY player.nickname, player.firstname, player.lastname ORDER BY COUNT DESC) SELECT * FROM rankgamesamount";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Player p = new Player
                            {
                                Nickname = reader.GetString(0),
                                Firstname = reader.GetString(1),
                                Lastname = reader.GetString(2),
                                Stats = new PlayerStats() { GamesPlayed = reader.GetInt32(3) }
                            };
                            plist.Add(p);
                        }
                    }
                }
                return plist;
            }
        }

        public static List<Player> GetTotalScoresPlayer()
        {
            List<Player> plist = new List<Player>();
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "WITH rankscoreamount AS (SELECT player.nickname, player.firstname, player.lastname, SUM(game_player.score)FROM game_player JOIN player ON player.player_id = game_player.player_id JOIN game ON game.game_id = game_player.game_id WHERE game.started_at BETWEEN CURRENT_DATE - INTERVAL '7 days' AND CURRENT_DATE + INTERVAL '1 day' GROUP BY player.nickname, player.firstname, player.lastname ORDER BY SUM DESC) SELECT* FROM rankscoreamount";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(3))
                            {
                                Player p = new Player
                                {
                                    Nickname = reader.GetString(0),
                                    Firstname = reader.GetString(1),
                                    Lastname = reader.GetString(2),

                                    Stats = new PlayerStats() { TotalScore = reader.GetInt32(3) }
                                };
                                plist.Add(p);
                            }
                        }
                    }
                }
                return plist;
            }
        }

        public static int CreateGame(List<Player> players, int gameType)
        {
            DateTime currentDateTime = DateTime.Now;
            string stmt = "INSERT INTO game(gametype_id, started_at) VALUES(@gameType, @currentDateTime)";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("gameType", gameType);
                    cmd.Parameters.AddWithValue("currentDateTime", currentDateTime);
                    cmd.ExecuteNonQuery();
                }
            }
            return CreateGamePlayers(players, currentDateTime);
        }

        private static int CreateGamePlayers(List<Player> players, DateTime currentDateTime)
        {
            int gameId = 0;
            string stmt = "SELECT game_id FROM game WHERE started_at = @currentDateTime";
            string stmtTwo = "INSERT INTO game_player(game_id, player_id) VALUES(@gameId, @player)";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("currentDateTime", currentDateTime);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameId = reader.GetInt32(0);
                        }
                    }
                }
                for (int i = 0; i < players.Capacity; i++) //loopen kanske ska va inne i using? och sluta innan executenonquery
                {
                    using (var cmd = new NpgsqlCommand(stmtTwo, conn))
                    {
                        cmd.Parameters.AddWithValue("gameId", gameId);
                        cmd.Parameters.AddWithValue("player", players[i].Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return gameId;
        }

        public static List<Player> GetGame(int gameId)
        {
            List<Player> plist = new List<Player>();
            string stmt = "SELECT player.player_id, player.firstname, player.lastname, player.nickname FROM player JOIN game_player ON game_player.player_id = player.player_id JOIN game ON game_player.game_id = game.game_id WHERE game.game_id = @gameId";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("gameId", gameId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Player p = new Player
                            {
                                Nickname = reader.GetString(0),
                                Firstname = reader.GetString(1),
                                Lastname = reader.GetString(2),
                                ScoreSheet = new ScoreSheet()
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