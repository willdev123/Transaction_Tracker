using System;
using MySql.Data;
using MySql.Data.MySqlClient;

public class Database {
    public MySqlConnection connectdb;

    public Database(){
        string connection = "server=localhost; port=3306; uid=root; pwd=lonelone2022; database=transaction_tracker; sslMode=none;";
        connectdb = new MySqlConnection(connection);
    }
}