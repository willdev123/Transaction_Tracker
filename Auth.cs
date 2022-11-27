using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;

public class Auth{
    private string? username;
    private string? password;
    private const string key = "mynameisWilliam";

    public Auth(string? username, string? password) {
        this.username = username;   
        this.password = password;
    }
    public string EncryptorDecrypt(string? text, string key) {
        var op = new StringBuilder();
        for(int c=0; c< text.Length; c++) {
            op.Append((char)((uint)(text[c]) ^ (uint)(key[c%key.Length])));
        }
        return op.ToString();
    }
    public string Register() {
        string? result;
        Database con = new Database();
        if(this.username == "" || this.password == "") {
            result = "Username or Password cannot be empty!!";
        }else{
            using(con.connectdb){
                try{
                    con.connectdb.Open();
                    string select_query = "select * from profile where username =\'"+this.username+"\'";
                    MySqlCommand select_cmd = new MySqlCommand(select_query, con.connectdb);
                    MySqlDataReader select_reader = select_cmd.ExecuteReader();
                    if(select_reader.HasRows && select_reader.Read()){
                        result = "Username already exists!!";
                    }else {
                        con.connectdb.Close();
                        con.connectdb.Open();
                        string encrypted_password = EncryptorDecrypt(this.password, key);
                        string insert_query = "insert into profile values(\'"+this.username+"\', \'"+encrypted_password+"\')";
                        MySqlCommand insert_cmd = new MySqlCommand(insert_query, con.connectdb);
                        insert_cmd.ExecuteReader();
                        result = "Registered Successfully!!";
                    }
                    con.connectdb.Close();
                }catch(MySqlException ex){
                    Console.WriteLine(ex.Message.ToString());
                    result = "Server Error!!";
                }
            }
        }
        return result;
    }

    public string Login() {
        string? result;
        Database con = new Database();
        using(con.connectdb){
            try{
                con.connectdb.Open();
                string select_query = "select * from profile where username=\'"+this.username+"\'";
                MySqlCommand select_cmd = new MySqlCommand(select_query, con.connectdb);
                MySqlDataReader select_reader = select_cmd.ExecuteReader();
                if(select_reader.HasRows && select_reader.Read()){
                    string decrypted_password = EncryptorDecrypt(select_reader["password"].ToString(), key);
                    if(decrypted_password != this.password){
                        result = "Wrong Password!!";
                    }else{
                        result = select_reader["username"].ToString();
                    }
                }else{
                    result = "Username not found!!";
                }
                con.connectdb.Close();
            }catch(MySqlException ex){
                Console.WriteLine(ex.Message.ToString());
                result = "Server Error!!";
            }
        }
        return result;
    }
}