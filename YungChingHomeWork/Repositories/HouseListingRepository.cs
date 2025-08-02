using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;
using YungChingHomeWork.Models;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace YungChingHomeWork.Repositories
{
    public class HouseListingRepository : IHouseListingRepository
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string connectionString = "Data Source=houseListings.db";

        public HouseListingRepository()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS HouseListings (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Address TEXT NOT NULL,
                    Price REAL NOT NULL
                );";
                db.Execute(createTableQuery);
            }
        }

        public HouseListing Create(HouseListing houseListing)
        {
            try
            {
                using (IDbConnection db = new SqliteConnection(connectionString))
                {
                    string insertQuery = "INSERT INTO HouseListings (Name, Address, Price) VALUES (@Name, @Address, @Price); SELECT last_insert_rowid();";
                    houseListing.Id = db.ExecuteScalar<int>(insertQuery, houseListing);
                    return houseListing;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in Create");
                throw;
            }
        }

        public bool Update(int id, HouseListing houseListing)
        {
            try
            {
                using (IDbConnection db = new SqliteConnection(connectionString))
                {
                    string updateQuery = "UPDATE HouseListings SET Name = @Name, Address = @Address, Price = @Price WHERE Id = @Id;";
                    int rowsAffected = db.Execute(updateQuery, new { houseListing.Name, houseListing.Address, houseListing.Price, Id = id });
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in Update");
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (IDbConnection db = new SqliteConnection(connectionString))
                {
                    string deleteQuery = "DELETE FROM HouseListings WHERE Id = @Id;";
                    int rowsAffected = db.Execute(deleteQuery, new { Id = id });
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in Delete");
                throw;
            }
        }

        public List<HouseListing> GetAll()
        {
            try
            {
                using (IDbConnection db = new SqliteConnection(connectionString))
                {
                    string selectQuery = "SELECT * FROM HouseListings;";
                    return db.Query<HouseListing>(selectQuery).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetAll");
                throw;
            }
        }

        public HouseListing? GetById(int id)
        {
            try
            {
                using (IDbConnection db = new SqliteConnection(connectionString))
                {
                    string selectQuery = "SELECT * FROM HouseListings WHERE Id = @Id;";
                    return db.QueryFirstOrDefault<HouseListing>(selectQuery, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetById");
                throw;
            }
        }
    }
}
