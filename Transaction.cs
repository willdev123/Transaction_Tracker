using MySql.Data.MySqlClient;
public class Transaction {
    public string? Account_Id {get; set;}
    public string? Transaction_Desc {get; set;}
    public string? Transaction_Type {get; set;}
    public string? Amount {get; set;}
    public string? From {get; set;}
    public string? To {get; set;}

    public List<string[]> trn(string username, int key) {
        List<string[]> result;
        string trns_query; 
        string filtered_trns_query;
        if ( key == 0 ) {
            trns_query = "select * from transaction where created_at > current_date - interval 1 month and username=\'" + username +"\' order by created_at desc";
            result = Show_Transactions(username, trns_query);
            return result;
        } else {
            filtered_trns_query  = "select * from transaction where username = \'" + username + "\'";
            if (Account_Id != "") {
                filtered_trns_query += " and account = " + Account_Id;
            }
            if (Transaction_Desc != ""){
                filtered_trns_query += " and transaction_desc like \'%" + Transaction_Desc + "%\'";
            }
            if (Transaction_Type != "") {
                filtered_trns_query += " and transaction_type = \'" + Transaction_Type +"\'";
            }
            if (From != "") {
                filtered_trns_query += " and created_at between \'"+ From + " 00:00:00\' and \'" + To + " 23:59:59\'"; 
            }
            filtered_trns_query += " order by created_at desc";
            result = Show_Transactions(username, filtered_trns_query);
            return result;
        }
    }
    public List<string[]> Show_Transactions(string username, string q) {
        List<string[]> result = new List<string[]>();
        string[] data = new string[6];
        string[] columns = {"id", "account", "transaction_desc", "transaction_type", "amount", "created_at"};
        Database con = new Database();

        using(con.connectdb) {
            try{
                con.connectdb.Open();
                MySqlCommand trn_cmd = new MySqlCommand(q, con.connectdb);
                MySqlDataReader trn_reader = trn_cmd.ExecuteReader();
                if (trn_reader.HasRows){
                    while(trn_reader.Read()){
                       for(int i=0; i<data.Length; i++) {
                            data[i] = trn_reader[columns[i]].ToString();
                    }
                       result.Add(data);
                       data = new string[6];
                    }
                }else {
                    result.Add(data);
                    result[0][0] = "No transactions to show!!";
                }
                con.connectdb.Close();
                
                if (result[0][0] != "No transactions to show!!"){
                    
                    foreach(string[] s in result) {
                        con.connectdb.Open();
                        string acc_query = "select name from account where username =\'" + username + "\' and id =\'"+ s[1] + "\'";
                        MySqlCommand acc_cmd = new MySqlCommand(acc_query, con.connectdb);
                        MySqlDataReader acc_reader = acc_cmd.ExecuteReader();
                        if (acc_reader.HasRows && acc_reader.Read()) {
                            s[1] = acc_reader["name"].ToString();
                        }
                       con.connectdb.Close();
                }
            }
            }catch(MySqlException ex) {
                Console.WriteLine(ex.Message.ToString());
                result.Add(data);
                result[0][0] = "Server Error!!";
            }
        }        
        return result;
    }

    public string Create_Transaction(string username) {
        string? result;
        string? update_acc_create_trn;
        Database con = new Database();
        if (Transaction_Desc == "" || Transaction_Type == "" || Amount == "" ) {
            result = "Description or Type or Amount cannot be empty!!";
        } else if (Transaction_Type != "0" && Transaction_Type != "1") {
            result = "Transaction Type only accept O or 1!!";
        } else {
            using(con.connectdb) {
                try{
                    con.connectdb.Open();
                    string insert_query = "insert into transaction (username, account, transaction_desc, transaction_type, amount) values (\'" + username + "\', " + Account_Id + ", \'" + Transaction_Desc + "\', " +  Transaction_Type +", " + Amount + ")";
                    MySqlCommand insert_cmd = new MySqlCommand(insert_query, con.connectdb);
                    insert_cmd.ExecuteReader();
                    con.connectdb.Close();
                    Account updated = new Account();
                    update_acc_create_trn = updated.Update_Data(Convert.ToInt32(Account_Id), Transaction_Type, Amount, "create_trn");
                    if(update_acc_create_trn == "Updated Successfully!!") {
                        result = "Transaction Created Successfully!!";
                    } else {
                        result = "Server Error!!";
                    }               
                }catch(MySqlException ex) {
                    Console.WriteLine(ex.Message.ToString());
                    result = "Server Error!!";
                }
            }
        }
        return result;
    }

    public string Update_Transaction (int id) {
        string? result;
        Database con = new Database();
        string[] columns = {"account", "transaction_type", "amount"};
        string?[] data = new string[3];
        string? update_acc_result_before_trn_update;
        string? update_acc_result_after_trn_update;
        if (Transaction_Desc == "" || Transaction_Type == "" || Amount == "" ) {
            result = "Description or Type or Amount cannot be empty!!";
        } else if (Transaction_Type != "0" && Transaction_Type != "1") {
            result = "Transaction Type only accept O or 1!!";
        } else {
            using(con.connectdb){
                try{
                    con.connectdb.Open();
                    string select_query = "select account, transaction_type, amount from transaction where id = " + id;
                    MySqlCommand select_cmd = new MySqlCommand(select_query, con.connectdb);
                    MySqlDataReader select_reader = select_cmd.ExecuteReader();
                    if(select_reader.HasRows && select_reader.Read()){
                        for(int i=0; i<columns.Length; i++) {
                            data[i] = select_reader[columns[i]].ToString();
                        }
                    }
                    con.connectdb.Close();
                    Account updated = new Account();
                    update_acc_result_before_trn_update = updated.Update_Data(Convert.ToInt32(data[0]), data[1], data[2], "before_update_trn");
                    if(update_acc_result_before_trn_update == "Updated Successfully!!") {
                        con.connectdb.Open();
                        string update_query = "update transaction set transaction_desc= \'"+ Transaction_Desc + "\', transaction_type= \'" + Transaction_Type + "\', amount= \'" + Amount+"\' where id =" + id;
                        MySqlCommand update_cmd = new MySqlCommand(update_query, con.connectdb);
                        update_cmd.ExecuteReader();
                        con.connectdb.Close();
                        update_acc_result_after_trn_update =updated.Update_Data(Convert.ToInt32(data[0]), Transaction_Type, Amount, "after_update_trn");
                        if(update_acc_result_after_trn_update == "Updated Successfully!!"){
                            result = "Transaction Updated Successfully!!";
                        }else {
                            result = "Server Error!!";
                        }
                    } else {
                        result = "Server Error!!";
                    }
                } catch (MySqlException ex) {
                    Console.WriteLine(ex.Message.ToString());
                    result = "Server Error!!";
                }
            }
        }
        return result;
    }

    public string Delete_Transaction(int id) {
        string? result;
        Database con = new Database();
        string[] columns = {"account", "transaction_type", "amount"};
        string?[] data = new string[3];
        string? update_acc_delete_trn;

        using(con.connectdb) {
            try{
                con.connectdb.Open();
                string select_query = "select account, transaction_type, amount from transaction where id = " + id;
                MySqlCommand select_cmd = new MySqlCommand(select_query, con.connectdb);
                MySqlDataReader select_reader = select_cmd.ExecuteReader();
                if(select_reader.HasRows && select_reader.Read()){
                    for(int i=0; i<columns.Length; i++) {
                        data[i] = select_reader[columns[i]].ToString();
                    }
                }
                con.connectdb.Close();
                con.connectdb.Open();
                string delete_query = "delete from transaction where id = " + id;
                MySqlCommand delete_cmd = new MySqlCommand(delete_query, con.connectdb);
                delete_cmd.ExecuteReader();
                con.connectdb.Close();
                Account updated = new Account();
                update_acc_delete_trn = updated.Update_Data(Convert.ToInt32(data[0]), data[1], data[2], "delete_trn");
                if(update_acc_delete_trn == "Updated Successfully!!") {
                    result = "Transaction Deleted Successfully!!";
                } else {
                    result = "Server Error!!";
                }
            }catch(MySqlException ex) {
                Console.WriteLine(ex.Message.ToString());
                result = "Server Error!!";
            }
        }
        return result;
    }
    public string Transaction_Detail(int id, string? username) {
        string? result;
        Database con = new Database();
        using(con.connectdb){
            try {
                con.connectdb.Open();
                string select_query = "select id from transaction where id =" + id + " and username =\'" + username + "\'";
                MySqlCommand select_cmd = new MySqlCommand(select_query, con.connectdb);
                MySqlDataReader select_reader = select_cmd.ExecuteReader();
                if(select_reader.HasRows && select_reader.Read()) {
                    result = select_reader["id"].ToString();
                } else {
                    result = "Invalid Id!!";
                }
                con.connectdb.Close();
            }catch(MySqlException ex) {
                Console.WriteLine(ex.Message.ToString());
                result = "Server Error!!";
            }
        }
        return result;
    }
}

