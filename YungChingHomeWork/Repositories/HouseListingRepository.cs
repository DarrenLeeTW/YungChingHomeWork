using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;
using YungChingHomeWork.Models;
using System.Collections.Generic;
using System.Linq;

namespace YungChingHomeWork.Repositories
{
    public class HouseListingRepository : IHouseListingRepository
    {
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
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                string insertQuery = "INSERT INTO HouseListings (Name, Address, Price) VALUES (@Name, @Address, @Price); SELECT last_insert_rowid();";
                houseListing.Id = db.ExecuteScalar<int>(insertQuery, houseListing);
                return houseListing;
            }
        }

        public bool Update(int id, HouseListing houseListing)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                string updateQuery = "UPDATE HouseListings SET Name = @Name, Address = @Address, Price = @Price WHERE Id = @Id;";
                int rowsAffected = db.Execute(updateQuery, new { houseListing.Name, houseListing.Address, houseListing.Price, Id = id });
                return rowsAffected > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM HouseListings WHERE Id = @Id;";
                int rowsAffected = db.Execute(deleteQuery, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public List<HouseListing> GetAll()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                string selectQuery = "SELECT * FROM HouseListings;";
                return db.Query<HouseListing>(selectQuery).ToList();
            }
        }

        public HouseListing? GetById(int id)
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                string selectQuery = "SELECT * FROM HouseListings WHERE Id = @Id;";
                return db.QueryFirstOrDefault<HouseListing>(selectQuery, new { Id = id });
            }
        }
    }
}
