using MySql.Data.MySqlClient;
public class Account {

    public string? Name{get; set;}
    public string? Opening_Balance{get; set;} 

    public Dictionary<int, string> Show_Accounts(string username){
        Dictionary<int, string> result = new Dictionary<int, string>();
        Database con = new Database();

        using(con.connectdb){
            try{
                con.connectdb.Open();
                string select_query = "select * from account where username =\'" + username + "\' and status=1";
                MySqlCommand select_cmd = new MySqlCommand(select_query, con.connectdb);
                MySqlDataReader select_reader = select_cmd.ExecuteReader();
                if(select_reader.HasRows){
                    while(select_reader.Read()){
                        result.Add((int)select_reader["id"], select_reader["name"].ToString());
                    }
                }else {
                    result.Add(0, "No Accounts to show!!");
                }
                con.connectdb.Close();
            }catch(MySqlException ex){
                Console.WriteLine(ex.Message.ToString());
                result.Add(0, "Server Error!!");
            }
        }
        return result;
    }

    public string Create_Account(string username){
        string? result;
        Database con = new Database();
        if(Name == "" || Opening_Balance == "") {
            result = "Account Name and Opening Balance cannot be empty!!";
        } else{
            using(con.connectdb){
                try{
                    con.connectdb.Open();
                    string insert_query = "insert into account(name, opening_balance,  closing_balance, username) values (\'"+ Name +"\', \'" + Opening_Balance +"\', \'"+ Opening_Balance +"\', \'"+ username +"\')";
                    MySqlCommand insert_cmd = new MySqlCommand(insert_query, con.connectdb);
                    insert_cmd.ExecuteReader();
                    result = "Account Created Successfully!!";
                    con.connectdb.Close();
                }catch(MySqlException ex){
                    Console.WriteLine(ex.Message.ToString());
                    result = "Server Error!!";
                }
            }
        }
        return result;
    }

    public string[] Account_Detail(string username, int id) {
        string?[] result = new string[7];
        string[] columns = {"id","name", "opening_balance", "closing_balance", "total_credit", "total_debit","total_transaction"};
        Database con = new Database();
        using(con.connectdb){
            try{
                con.connectdb.Open();
                string select_query = "select * from account where username=\'"+ username +"\' and id=" + id +" and status=1";
                MySqlCommand select_cmd = new MySqlCommand(select_query, con.connectdb);
                MySqlDataReader select_reader = select_cmd.ExecuteReader();
                if(select_reader.HasRows && select_reader.Read()) {
                    for (int i=0; i<7; i++) {
                        result[i] = Convert.ToString(select_reader[columns[i]]);
                    }
                }else {
                    result[0] = "Invalid Id!!";
                }
                con.connectdb.Close();
            }catch(MySqlException ex){
                Console.WriteLine(ex.Message.ToString());
                result[0] = "Server Error!!";
            }
        }
        return result;
    }

    public string Delete_Account(int id) {
        string result;
        Database con = new Database();
        using(con.connectdb) {
            try{
                con.connectdb.Open();
                string update_query = "update account set status=0 where id = " + id;
                MySqlCommand update_cmd = new MySqlCommand(update_query, con.connectdb);
                update_cmd.ExecuteReader();
                result = "Account Deleted Successfully!!";
                con.connectdb.Close();
            }catch(MySqlException ex) {
                Console.WriteLine(ex.Message.ToString());
                result= "Server Error!!";
            }
        }
        return result;
    }
        public string Rename_Account(int id) {
        string result;
        Database con = new Database();
        if( Name == "") {
            result = "New name cannot be empty!!";
        } else {
            using(con.connectdb) {
                try{
                    con.connectdb.Open();
                    string update_query = "update account set name =\'" + Name + "\' where id = " + id + " and status = 1";
                    MySqlCommand update_cmd = new MySqlCommand(update_query, con.connectdb);
                    update_cmd.ExecuteReader();
                    result = "Account Renamed Successfully!!";
                    con.connectdb.Close();
                }catch(MySqlException ex) {
                    Console.WriteLine(ex.Message.ToString());
                    result= "Server Error!!";
                }
            }
        }   
        return result;
    }

    public string Update_Data(int acc_id, string? trn_type, string? amount, string source) {
        string result;
        string update_query = "";
        Database con = new Database();
        if(source == "create_trn" || source == "after_update_trn"){
            if (trn_type == "1") {
                update_query = "update account set total_credit = total_credit + " + amount + ", closing_balance = opening_balance + (total_credit - total_debit), total_transaction = total_transaction + 1 where status = 1 and id = " + acc_id;
            }
            if(trn_type == "0") {
                update_query = "update account set total_debit = total_debit + " + amount + ", closing_balance = opening_balance + (total_credit - total_debit), total_transaction = total_transaction + 1 where status = 1 and id = " + acc_id;
            }
        }
        if (source == "delete_trn" || source == "before_update_trn"){
            if (trn_type == "1") {
                update_query = "update account set total_credit = total_credit - " + amount + ", closing_balance = opening_balance + (total_credit - total_debit), total_transaction = total_transaction - 1 where status = 1 and id = " + acc_id;
            }
            if(trn_type == "0") {
                update_query = "update account set total_debit = total_debit - " + amount + ", closing_balance = opening_balance + (total_credit - total_debit), total_transaction = total_transaction - 1 where status = 1 and id = " + acc_id;
            }
        }
        Console.WriteLine(update_query);
        using(con.connectdb) {
            try{
                con.connectdb.Open();
                MySqlCommand update_cmd= new MySqlCommand(update_query, con.connectdb);
                update_cmd.ExecuteReader();
                result = "Updated Successfully!!"; 
                con.connectdb.Close();
            } catch(MySqlException ex) {
                Console.WriteLine(ex.Message.ToString());
                result = "Server Error!!";
            }
        }
        return result;
    }
}

