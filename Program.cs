
namespace transaction_tracker{
    class Program {
        public static void Main(){
            Auth();
        }
        public static void Auth(){
            string inp;
            int inp_validation_count = 0;
            Console.WriteLine("\n==Welcome to Transaction Tracker==");
            Console.WriteLine("(1) Register\n(2) Login \n(3) Exit");
            do{
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("Enter options: (1) Register (2) Login (3)Exit: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp){
                    case "1": Register();
                    break;
                    case "2": Login();
                    break;
                    case "3": 
                    Console.WriteLine("\nThanks for using Transaction_Tracker!!");
                    Environment.Exit(1);
                    break;
                    default: Console.WriteLine("Invalid Input!!");break; 
                }
                inp_validation_count++;  
            } while(inp != "1" && inp != "2" && inp != "3");
        }

        public static void Register() {
            string inp;
            int register_validation_count = 0;
            int inp_validation_count = 0;
            string? result;
            string? username;
            string? password; 
                do{
                    if(inp_validation_count > 4) {
                        Console.WriteLine("\nYou crashed the app!!");
                        Environment.Exit(1);
                    }
                    Console.Write("\nEnter options: (1) Go back (2) Continue register: ");
                    inp = Convert.ToString(Console.ReadLine());
                    switch (inp) {
                        case "1": Auth();break;
                        case "2":
                        do {
                            if(register_validation_count > 4) {
                                Console.WriteLine("\nYou crushed the app!!");
                                Environment.Exit(1);
                            }
                            Console.WriteLine("\n==Register==");
                            Console.Write("Username: ");
                            username = Convert.ToString(Console.ReadLine());
                            Console.Write("Password: ");
                            password = Convert.ToString(Console.ReadLine());  
                            Auth user = new Auth(username, password);                                               
                            result = user.Register(); 
                            if(result == "Username or Password cannot be empty!!" || result == "Username already exists!!"){
                                Console.WriteLine(result);
                            } 
                            register_validation_count++;
                        }while(result == "Username or Password cannot be empty!!" || result == "Username already exists!!"); 
                        if(result == "Registered Successfully!!"){
                            Console.WriteLine(result);
                            Login();
                        } else {
                            Console.WriteLine(result);
                            Environment.Exit(1);
                        }
                        break;
                        default: Console.WriteLine("Invalid input!!");break;
                    }
                    inp_validation_count++;
                }while (inp != "1" && inp!= "2");
            }
     
        public static void Login() {
            string inp;
            int login_validation_count = 0;
            int inp_validation_count = 0;
            string? result;
            string? username;
            string? password; 
            do{
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crushed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("\nEnter options: (1) Go back (2) Continue login: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp) {
                    case "1" : Auth();break;
                    case "2" : 
                    do{
                        if(login_validation_count > 4) {
                            Console.WriteLine("\nYou crushed the app!!");
                            Environment.Exit(1);
                        }
                        Console.WriteLine("\n==Login==");
                        Console.Write("Username: ");
                        username = Convert.ToString(Console.ReadLine());
                        Console.Write("Password: ");
                        password = Convert.ToString(Console.ReadLine());  
                        Auth user = new Auth(username, password);
                        result = user.Login();
                        if(result == "Username not found!!" || result == "Wrong Password!!"){
                            Console.WriteLine(result);
                        }
                        login_validation_count++;
                    }while(result == "Username not found!!" || result == "Wrong Password!!");
                    if(result == username){
                        Console.WriteLine("Logged In Successfully!!");
                        Home(username);
                    } else {
                        Console.WriteLine(result);
                        Environment.Exit(1);
                    } 
                    break;
                    default: Console.WriteLine("Invalid input!!");break;      
                }
                inp_validation_count++;
            }while(inp != "1" && inp != "2");
        }
        public static void Home(string username) {
            string inp;
            int inp_validation_count = 0;
            Console.WriteLine("\nWelcome " + username);
            do{
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("\n(1)Transactions (2)Accounts (3)Log out\nEnter options: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp){
                    case "1": Manage_Transactions(username); break;
                    case "2": Manage_Accounts(username); break;
                    case "3": Logout(); break;
                    default: Console.WriteLine("Invalid Input!!"); break;
                };
                inp_validation_count++;
            }while(inp != "1" && inp != "2" && inp != "3");

          
        }
        public static string Accounts(string username){
            Dictionary<int, string> result= new Dictionary<int, string>();
            Account accounts = new Account();
            Console.WriteLine("\n==Accounts=="); 
            result = accounts.Show_Accounts(username);
            if(result.ContainsKey(0)){
                if(result[0] == "No Accounts to show!!"){
                    Console.WriteLine(result[0]);
                    return result[0];
                } else{
                    Console.WriteLine(result[0]);
                    Environment.Exit(1);                
                } 
            }else {
                Console.WriteLine("Id   Account Name");
                foreach(KeyValuePair<int, string> kvp in result){
                Console.WriteLine("{0}     {1}", kvp.Key, kvp.Value);
                }
            }
            return "";
        }

        public static void Manage_Accounts(string username) {
            string inp;
            int inp_validation_count = 0;
            string no_acc = Accounts (username);
            do {
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("\nEnter options: (1)Create Account (2)Select Account to view (3) Back to Home: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp) {
                    case "1": Create_Account(username); break;
                    case "2": 
                    if (no_acc == "No Accounts to show!!"){
                        Console.WriteLine("No account yet to select!!Create account first!!");
                        Console.Write("Press any key to go back: ");
                        Console.ReadKey();
                        Console.WriteLine();
                        Manage_Accounts(username);
                    }else{
                        Account(username); 
                    }
                    break;
                    case "3": Home(username);break;
                    default: Console.WriteLine("Invalid Input!!"); break;
                }
                inp_validation_count++;  
            } while(inp != "1" && inp != "2" && inp != "3");
        }

        public static void Create_Account(string username){
            string? result;
            int create_validation_count = 0;
            string? inp;
            int inp_validation_count = 0;
            int num_string_validation = 0;
            bool num_string_result;
            Account created = new Account();
            do {
                if(inp_validation_count > 4) {
                Console.WriteLine("\nYou crashed the app!!");
                Environment.Exit(1);
                }      
                Console.Write("\nEnter options: (1)Go Back to Accounts (2)Continue create account: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp){
                    case "1": Manage_Accounts(username);break;
                    case "2":
                    do{
                    if(create_validation_count > 4) {
                        Console.WriteLine("\nYou crashed the app!!");
                        Environment.Exit(1);
                    } 
                    Console.WriteLine("\n==Account Creation==");
                    Console.Write("Account Name: ");
                    created.Name = Convert.ToString(Console.ReadLine());
                    Console.Write("Opening Balance: ");
                    created.Opening_Balance= Convert.ToString(Console.ReadLine());
                    num_string_result = Num_Validation(created.Opening_Balance);
                    while(!num_string_result){
                        Console.WriteLine("Invalid Input!!");
                        if(num_string_validation > 3) {
                            Console.WriteLine("\nYou crashed the app!!");
                            Environment.Exit(1);
                        }
                        Console.Write("Opening Balance: ");
                        created.Opening_Balance = Convert.ToString(Console.ReadLine());
                        num_string_result = Num_Validation(created.Opening_Balance);
                        num_string_validation++;
                    }
                    result = created.Create_Account(username);
                    if(result == "Account Name and Opening Balance cannot be empty!!"){
                        Console.WriteLine(result);
                    }
                    create_validation_count++;
                }while(result == "Account Name and Opening Balance cannot be empty!!");
                if(result== "Account Created Successfully!!"){
                    Console.WriteLine(result);
                    Manage_Accounts(username);
                }else{
                    Console.WriteLine(result);
                    Environment.Exit(1);
                }
                break;
                default: Console.WriteLine("Invalid Input!!");break; 
                }
                inp_validation_count++;
            } while(inp != "1" && inp!= "2");
            
        }

        public static void Account(string username) {
            string inp;
            int inp_validation_count = 0;
            string[] columns = {"Id","Account Name", "Opening_balance", "Closing_balance", "Total_credit", "Total_debit","Total_transaction"};
            string[] result;
            do{
                if(inp_validation_count > 4) {
                Console.WriteLine("\nYou crashed the app!!");
                Environment.Exit(1);
                }
                Console.Write("\nEnter options: (1)Go back to Accounts (2)Account Id to see account info: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp) {
                    case "1": Manage_Accounts(username);break;
                    case "2": 
                    result = Acc_Validation(username);
                    if (result.Length == 7){
                        Console.WriteLine("\n==Account Details==");
                        for(int i=0; i<7; i++) {
                            Console.WriteLine(columns[i] + ": " + result[i]);
                        }
                        Delete_Rename_Account(username, Convert.ToInt32(result[0]));
                        } else {
                            Console.WriteLine(result[0]);
                            Environment.Exit(1);
                        }
                    break;
                    default: Console.WriteLine("Invalid Input!!");break;
                }
                inp_validation_count++; 
            }while(inp != "1" && inp != "2"); 
        }

        public static void Delete_Rename_Account(string username, int id) {
            string inp;
            int inp_validation_count = 0;
            string? result;
            string? new_name;
            int rename_validation_count = 0;
            Account deleted = new Account();
            do{
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("Enter options: (1)Delete (2)Rename (3)Go back to Accounts: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp){
                    case "1" : 
                    result = deleted.Delete_Account(id);
                    if(result == "Account Deleted Successfully!!") {
                        Console.WriteLine(result);
                        Manage_Accounts(username);
                    }else {
                        Console.WriteLine(result);
                        Environment.Exit(1);
                    }
                    break;
                    case "2": 
                    do {
                        if(rename_validation_count > 4) {
                            Console.WriteLine("\nYou crashed the app!!");
                            Environment.Exit(1);
                        }
                        Console.Write("Enter new name: ");
                        new_name = Convert.ToString(Console.ReadLine());
                        Account renamed = new Account();
                        renamed.Name = new_name;
                        result = renamed.Rename_Account(id);
                        if(result == "New name cannot be empty!!"){
                            Console.WriteLine(result);
                        }
                        rename_validation_count++;
                    } while( result == "New name cannot be empty!!");
                    if(result== "Account Renamed Successfully!!"){
                        Console.WriteLine(result);
                        Manage_Accounts(username);
                    }else{
                        Console.WriteLine(result);
                        Environment.Exit(1);
                    }
                    break;
                    case "3": Manage_Accounts(username);break;
                    default: Console.WriteLine("Invalid Input!!"); break;
                }
                inp_validation_count++; 
            }while(inp != "1" && inp != "2" && inp != "3");
           
        }
        public static string Transactions(string username, Func<string, int, List<string[]>> fn, int key){  
            List<string[]> result = new List<string[]>();
            int total_credit = 0;
            int total_debit = 0;
            int net_spend = 0;
            int total_transaction = 0;
            Console.WriteLine("\n==Transactions==");
            result = fn(username, key);
            if(result[0][0] == "Server Error!!"){
                Console.WriteLine(result[0][0]);
                Environment.Exit(1);
            } else if(result[0][0] == "No transactions to show!!"){
                Console.WriteLine(result[0][0]);
                return result[0][0];
            }  else {
                Console.WriteLine("Id/ Account Name/ Description/ Type/ Amount/ Created_At");
                foreach(string[] s in result){
                    if(s[3] == "0") {
                        s[3] = "expense";
                        total_debit += Convert.ToInt32(s[4]);
                    }else {
                        s[3] = "income";
                        total_credit += Convert.ToInt32(s[4]);
                    }
                    Console.WriteLine("\n{0}    {1}     {2}     {3}     {4}     {5}", s[0],s[1],s[2],s[3],s[4],s[5]);
                    total_transaction++;
                }
                net_spend = total_credit - total_debit;
                Console.WriteLine("\ntotal_transaction: " + total_transaction + "/   total_credit: " + total_credit + "/    total_debit: " + total_debit +"/    net_spend:  " + net_spend);
                return "";
            }   
            return "";
        }

        public static void Manage_Transactions(string username) {
            string inp;
            int inp_validation_count = 0;
            Transaction fn = new Transaction();
            string no_trn = Transactions(username, fn.trn, 0);
            do {
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("\nEnter options:(1)Create Transaction (2)Filter Transactions to manage (3)Back to Home: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp) {
                    case "1":Create_Transaction(username); break;
                    case "2": Filtered_Transactions(username); break;
                    case "3": Home(username);break;
                    default: Console.WriteLine("Invalid Input!!"); break;
                }
                inp_validation_count++;  
            } while(inp != "1" && inp != "2" && inp != "3");
        }

        public static void Create_Transaction(string username) {
            string? result;
            int create_validation_count = 0;
            int num_string_validation = 0;
            bool num_string_result;
            string? inp;
            int inp_validation_count = 0;
            string? _acc;
            Transaction created = new Transaction();
            Account acc = new Account();
            string[] has_acc;
            
            do {
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("\nEnter options:(1)Go Back to Transactions (2)Continue create transaction: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp) {
                    case "1": Manage_Transactions(username);break;
                    case "2": 
                    do {
                    if(create_validation_count > 4) {
                        Console.WriteLine("\nYou crashed the app!!");
                        Environment.Exit(1);
                    }
                    Console.WriteLine("\n==Transaction Creation=="); 
                    Console.WriteLine("\nSelect your accounts");
                    _acc = Accounts(username);
                    if(_acc == "No Accounts to show!!"){
                        Console.Write("Press any key to go back home: ");
                        Console.ReadKey();
                        Console.WriteLine();
                        Home(username);
                    }
                    has_acc = Acc_Validation(username);
                    if (has_acc.Length == 7){
                        created.Account_Id = has_acc[0];
                    } else {
                        Console.WriteLine(has_acc[0]);
                        Environment.Exit(1);
                    }
                    Console.Write("Description: ");
                    created.Transaction_Desc = Convert.ToString(Console.ReadLine());
                    Console.Write("Transaction Type(1 for credit and 0 for debit): ");
                    created.Transaction_Type = Convert.ToString(Console.ReadLine());
                    Console.Write("Amount: ");
                    string? amount = Convert.ToString(Console.ReadLine());
                    if(amount != ""){
                        num_string_result = Num_Validation(amount);
                        while (!num_string_result) {
                            Console.WriteLine("Invalid Input!!");
                            if(num_string_validation > 3){
                                Console.WriteLine("\nYou crashed the app!!");
                                Environment.Exit(1);
                            }
                            Console.Write("Amount: ");
                            amount = Convert.ToString(Console.ReadLine());
                            num_string_result = Num_Validation(amount);
                            num_string_validation++;
                        }
                    }
                    created.Amount = amount;
                    result = created.Create_Transaction(username);
                    if(result == "Description or Tranaction Type or Amount cannot be empty!!" || result == "Transaction Type only accept O or 1!!"){
                        Console.WriteLine(result);
                    }
                    create_validation_count++;
                    } while (result == "Description or Transaction Type or Amount cannot be empty!!" || result == "Transaction Type only accept O or 1!!");
                    if(result== "Transaction Created Successfully!!"){
                        Console.WriteLine(result);
                        Manage_Transactions(username);
                    }else{
                        Console.WriteLine(result);
                        Environment.Exit(1);
                    }
                    break;
                    default: Console.WriteLine("Invalid Input!!");break;
                }
                inp_validation_count++;
            } while (inp != "1" && inp != "2");    
        }

        public static void Filtered_Transactions(string username) {
            string? inp;
            int inp_validation_count = 0;
            do {
                if(inp_validation_count > 4) {
                    Console.WriteLine("You crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("\nEnter options: (1)Go back (2)Continue filter: ");
                inp = Convert.ToString(Console.ReadLine());
                switch (inp) {
                    case "1": Manage_Transactions(username);break;
                    case "2": Manage_Filtered_Transactions(username);break;
                    default: Console.WriteLine("Invalid Input!!");break;  
                }
                inp_validation_count++;
            } while (inp != "1" && inp != "2");
        }
        
        public static void Filtered_Transactions_Input(string username, Transaction obj) {
            string[] has_acc; 
            int typ_validation_count = 0; 
            int date_to_validation_count = 0; 
            Transaction filtered = new Transaction();
            bool date_validation_result = true;
            int date_validation_count =0;
            string? _acc;
            // Account 
            Console.WriteLine("\n==Filtering Transactions==");
            Console.Write("select your accounts(Press Enter to skip or enter any value to proceed): ");
            string? acc_val = Convert.ToString(Console.ReadLine());
            obj.Account_Id = acc_val;
            if(acc_val != "") {
                _acc = Accounts(username);
                if(_acc == "No Accounts to show!!"){
                    Console.Write("Press any key to go back home: ");
                    Console.ReadKey();
                    Console.WriteLine();
                    Home(username);
                } else {
                    has_acc = Acc_Validation(username);
                    if (has_acc.Length == 7){
                        obj.Account_Id = has_acc[0];
                    }else {
                        Console.WriteLine(has_acc[0]);
                        Environment.Exit(1);
                    }
                }
            }
            //Desc
            Console.Write("Description(Press Enter to skip): ");
            obj.Transaction_Desc = Convert.ToString(Console.ReadLine());
            //Type         
            Console.Write("Type \" 0 or 1\"(Press Enter to skip): ");
            string? type = Convert.ToString(Console.ReadLine());
            while (type != "0" && type != "1" && type != "") {
                Console.WriteLine("Type only accept 0 or 1!!");
                if(typ_validation_count > 4){
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("Type \" 0 or 1\"(Press Enter to skip): ");
                type = Convert.ToString(Console.ReadLine()); 
                typ_validation_count ++;
            }
            obj.Transaction_Type = type;
            //Date            
            do {
                if(date_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("From_Date(yyyy-mm-dd)(Enter to Skip):");
                string? from = Convert.ToString(Console.ReadLine());
                if( from != "" ) {
                    Console.Write("To_Date(yyyy-mm-dd):");
                    string? to = Convert.ToString(Console.ReadLine());
                    while(to == "") {
                        Console.WriteLine("To_Date cannot be empty!!");
                        if(date_to_validation_count > 3) {
                            Console.WriteLine("\n You crashed the app!!");
                            Environment.Exit(1);
                        }
                        Console.Write("To_Date(yyyy-mm-dd):");
                        to = Convert.ToString(Console.ReadLine());
                        date_to_validation_count++;
                }
                    date_validation_result = Date_Validation(to, from);
                    if(date_validation_result) {
                        obj.From = from;
                        obj.To = to;
                    } else {
                        Console.WriteLine("Invalid format of From_Date or To_Date!!");
                        date_validation_count++;
                    }
                } else {
                    obj.From = from;
                    date_validation_result = true;
                }
            } while(!date_validation_result);
        }

        public static void Manage_Filtered_Transactions(string username) {
            string? inp;
            int inp_validation_count = 0;
            Transaction filtered = new Transaction();
            Filtered_Transactions_Input(username, filtered);
            string no_trn = Transactions(username, filtered.trn, 1);
            do {
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                if(no_trn == "No transactions to show!!"){
                    Console.Write("\nEnter options: (3)Back to Transactions: ");
                }else {
                    Console.Write("\nEnter options: (1)Delete (2)Update (3)Back to Transactions: ");
                }
                inp = Convert.ToString(Console.ReadLine());
                switch(inp) {
                    case "1":
                    if (no_trn == "No transactions to show!!"){
                        Console.WriteLine("No transactions filtered to delete!!");
                        Console.Write("Press any key to go back home: ");
                        Console.ReadKey();
                        Console.WriteLine();
                        Manage_Transactions(username);
                    }else {
                        Delete_Transaction(username);
                    }
                    break;
                    case "2":
                    if (no_trn == "No transactions to show!!"){
                        Console.WriteLine("No transactions filtered to updated!!");
                        Console.Write("Press any key to go back home: ");
                        Console.ReadKey();
                        Console.WriteLine();
                        Manage_Transactions(username);
                    }else {
                        Update_Transaction(username);
                    }
                    break;
                    case "3": Manage_Transactions(username);break;
                    default: Console.WriteLine("Invalid Input!!"); break;
                }
                inp_validation_count++;  
            } while(inp != "1" && inp != "2" && inp != "3");

        }

        public static void Update_Transaction(string username) {
            string? inp;
            int inp_validation_count = 0;
            string? trn_validation_result;
            bool num_string_result;
            int num_string_validation = 0;
            int update_validation_count = 0;
            string? update_result;
            Transaction updated = new Transaction();
            do {
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("Enter options (1)Go back (2)Continue update: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp) {
                    case "1": Manage_Transactions(username); break;
                    case "2": 
                    do {
                        if (update_validation_count > 4){
                            Console.WriteLine("You crashed the app!!");
                            Environment.Exit(1);
                        }
                        trn_validation_result = Transaction_Validation(username);
                        if(trn_validation_result == "Server Error!!") {
                            Console.WriteLine("\nYou crashed the app!!");
                            Environment.Exit(1);
                        } 
                        Console.Write("Description: ");
                        updated.Transaction_Desc = Convert.ToString(Console.ReadLine());
                        Console.Write("Transaction Type(1 for credit and 0 for debit): ");
                        updated.Transaction_Type = Convert.ToString(Console.ReadLine());
                        Console.Write("Amount: ");
                        string? amount = Convert.ToString(Console.ReadLine());
                        if( amount != ""){
                            num_string_result = Num_Validation(amount);
                            while (!num_string_result) {
                                Console.WriteLine("Invalid Input!!");
                                if(num_string_validation > 3){
                                    Console.WriteLine("\nYou crashed the app!!");
                                    Environment.Exit(1);
                                }
                                Console.Write("Amount: ");
                                amount = Convert.ToString(Console.ReadLine());
                                num_string_result = Num_Validation(amount);
                                num_string_validation++;
                            }
                        }
                        updated.Amount = amount;
                        update_result = updated.Update_Transaction(Convert.ToInt32(trn_validation_result));
                        if (update_result == "Description or Type or Amount cannot be empty!!" || update_result == "Transaction Type only accept O or 1!!" ){
                            Console.WriteLine(update_result);
                        }
                        update_validation_count++;
                    }while (update_result == "Description or Type or Amount cannot be empty!!" || update_result == "Transaction Type only accept O or 1!!");
                    if(update_result== "Transaction Updated Successfully!!"){
                        Console.WriteLine(update_result);
                        Manage_Transactions(username);
                    }else{
                        Console.WriteLine(update_result);
                        Environment.Exit(1);
                    }
                    break;
                    default: Console.WriteLine("Invalid Input");break;
                }
            }while (inp != "1" && inp != "2");
        }

        public static void Delete_Transaction(string username) {
            string? inp;
            int inp_validation_count = 0;
            string? trn_validation_result;
            string? delete_result;
            Transaction deleted = new Transaction();
            do {
                if(inp_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("Enter options (1)Go back (2)Continue delete: ");
                inp = Convert.ToString(Console.ReadLine());
                switch(inp) {
                    case "1": Manage_Transactions(username); break;
                    case "2":
                    trn_validation_result = Transaction_Validation(username);
                    if(trn_validation_result == "Server Error!!") {
                        Console.WriteLine("\nYou crashed the app!!");
                        Environment.Exit(1);
                    } 
                    delete_result = deleted.Delete_Transaction(Convert.ToInt32(trn_validation_result));
                    if(delete_result == "Transaction Deleted Successfully!!") {
                        Console.WriteLine(delete_result);
                        Manage_Transactions(username);
                    }else {
                        Console.WriteLine(delete_result);
                        Environment.Exit(1);
                    }
                    break;
                    default: Console.WriteLine("Invalid Input!!");break; 
                }
                inp_validation_count++;
            } while (inp != "1" && inp !="2");
        }

        public static string[] Acc_Validation(string username) {
            string[] result; 
            int account_validation_count = 0;
            string? id;
            Account account = new Account();
            do {
                if(account_validation_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                id = Id_Validation("account");
                result = account.Account_Detail(username, Convert.ToInt32(id));
                if(result[0] == "Invalid Id!!" ){
                   Console.WriteLine(result[0]);
                } 
                account_validation_count++; 
            } while (result[0] == "Invalid Id!!"); 
            return result;
        }

        public static string Transaction_Validation(string username) {
            string result; 
            int trn_valdaition_count = 0;
            string? id;
            Transaction trn = new Transaction();
            do{
                if(trn_valdaition_count > 4) {
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                id = Id_Validation("transaction");
                result = trn.Transaction_Detail(Convert.ToInt32(id), username);
                if(result == "Invalid Id!!" ){
                   Console.WriteLine(result);
                }
                trn_valdaition_count++;
            } while (result == "Invalid Id!!");
            return result;
        }

        public static string Id_Validation(string? perem) {
            bool num_string_result; 
            int num_string_validation = 0; 
            Console.Write("\nEnter " + perem +  " id: ");
            string id = Convert.ToString(Console.ReadLine());
            num_string_result = Num_Validation(id);
            while (!num_string_result) {
                Console.WriteLine("Invalid Input!!");
                if(num_string_validation > 3){
                    Console.WriteLine("\nYou crashed the app!!");
                    Environment.Exit(1);
                }
                Console.Write("\nEnter " + perem +  " id: ");
                id = Convert.ToString(Console.ReadLine());
                num_string_result = Num_Validation(id);
                num_string_validation++;
            }
            return id;
        }
        public static bool Num_Validation(string? num_string) {
            bool result = true;
            if(num_string == ""){
                result = false;
            } else {
                for(int i=0; i < num_string.Length ; i++) {
                    if (!((int)num_string[i] >= 48 && (int)num_string[i] <= 57) || !((int)num_string[i] >= 48 && (int)num_string[i] <= 57)) {
                        result = false;
                    } 
                    if (i == 0) {
                        if ((int)num_string[0] == 48) {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }
        public static bool Date_Validation(string? from, string? to) {
            bool result = true; 
            if ( from.Length == 10 && to.Length == 10) {
                for (int i=0; i<10; i++){
                    if (i==4 || i==7) {
                        if(from[i] != '-' || to[i] != '-') {
                            result = false;
                        }
                    } else {
                        if(!((int)from[i] >= 48 && (int)from[i] <= 57) || !((int)to[i] >= 48 && (int)to[i] <= 57)){
                            result = false;
                        }
                    }
                }
            } else {
                result = false;
            }
            return result;
        }

        public static void Logout() {
            Auth();
        }
    }
}
