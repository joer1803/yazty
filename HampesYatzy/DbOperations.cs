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
            for (int i = 0; i < busylist.Count; i++)
            {
                for (int j = 0; j < plist.Count; j++)
                {
                    if (plist[j].Id == busylist[i].Id)
                    {
                        plist.Remove(plist[j]);
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
                            if (!reader.IsDBNull(3))
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
            int gameId = 0;
            DateTime currentDateTime = DateTime.Now;
            string stmt = "INSERT INTO game(gametype_id, started_at) VALUES(@gameType, @currentDateTime) RETURNING game_id";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("gameType", gameType);
                    cmd.Parameters.AddWithValue("currentDateTime", currentDateTime);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameId = reader.GetInt32(0);
                        }
                    }
                }
            }
            CreateGamePlayers(players, currentDateTime, gameId);
            return gameId;
        }

        private static void CreateGamePlayers(List<Player> players, DateTime currentDateTime, int gameId)
        {
            string stmtTwo = "INSERT INTO game_player(game_id, player_id) VALUES(@gameId, @player)";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                for (int i = 0; i < players.Count; i++)
                {
                    using (var cmd = new NpgsqlCommand(stmtTwo, conn))
                    {
                        cmd.Parameters.AddWithValue("gameId", gameId);
                        cmd.Parameters.AddWithValue("player", players[i].Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
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
                                Id = reader.GetInt32(0),
                                Nickname = reader.GetString(3),
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

        public static void SetEndGame(YatzyGame game)
        {
            string stmt = "UPDATE game SET ended_at = @endTime WHERE game_id = @gameId";
            string stmtTwo = "UPDATE game_player SET score = @playerScore WHERE game_id = @gameId AND player_id = @playerId";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("endTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("gameId", game.GameId);
                    cmd.ExecuteNonQuery();
                }
                for (int i = 0; i < game.Players.Count; i++)
                {
                    using (var cmd = new NpgsqlCommand(stmtTwo, conn))
                    {
                        cmd.Parameters.AddWithValue("playerScore", game.Players[i].ScoreSheet.TotScore);
                        cmd.Parameters.AddWithValue("gameId", game.GameId);
                        cmd.Parameters.AddWithValue("playerId", game.Players[i].Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public static string CreatePlayer(string fName, string lName, string nickName)
        {
            string stmt = "INSERT INTO player(firstname, lastname, nickname) VALUES(@fName, @lName, @nickName)";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("fName", fName);
                    cmd.Parameters.AddWithValue("lName", lName);
                    cmd.Parameters.AddWithValue("nickName", nickName);
                    cmd.ExecuteNonQuery();
                }
            }
            return $"{nickName} är redo att spela yatzy!";
        }


        public static void DeleteGame(int gameId)
        {
            string stmt = "DELETE FROM game WHERE game_id = @gameId";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("gameId", gameId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}