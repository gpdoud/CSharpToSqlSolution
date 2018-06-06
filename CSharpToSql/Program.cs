using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToSql {
	class Program {

		static List<User> users = new List<User>();

		void Run() {
			User user = new User();
			user.Id = 15;
			user.Username = "ZZZZ";
			user.Password = "Password";
			user.Firstname = "Denise";
			user.Lastname = "Bartik";
			user.Phone = "555-555-2121";
			user.Email = "denise@maxtrain.com";
			user.IsReviewer = true;
			user.IsAdmin = true;
			Update(user);
		}

		static void Main(string[] args) {
			(new Program()).Run();
		}
		void Update(User user) {
			string connStr = @"server=localhost\SQLEXPRESS;database=prssql;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if (conn.State != System.Data.ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}
			string sql = " Update [user] "
						+ " Set Username = @Username, "
						+ " Password = @Password, "
						+ " Firstname = @Firstname, "
						+ " Lastname = @Lastname, "
						+ " Phone = @Phone, "
						+ " Email = @Email, "
						+ " IsReviewer = @IsReviewer, "
						+ " IsAdmin = @IsAdmin "
						+ " Where Id = @Id; ";

			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add(new SqlParameter("@Id", user.Id));
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			int recsAffected = cmd.ExecuteNonQuery();
			if (recsAffected != 1) {
				System.Diagnostics.Debug.WriteLine("Record insert failed");
			}
			conn.Close();

		}
		void Insert(User user) {
			string connStr = @"server=localhost\SQLEXPRESS;database=prssql;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if (conn.State != System.Data.ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}
			string sql = "Insert into [user] (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin)" +
				"values (@Username, @Password, @Firstname, @Lastname, @Phone, @Email, @IsReviewer, @IsAdmin)";
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			int recsAffected = cmd.ExecuteNonQuery();
			if(recsAffected != 1) {
				System.Diagnostics.Debug.WriteLine("Record insert failed");
			}
			conn.Close();

		}
		void Select() { 

			string connStr = @"server=localhost\SQLEXPRESS;database=prssql;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if(conn.State != System.Data.ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}
			string sql = "Select * from [user]";
			SqlCommand cmd = new SqlCommand(sql, conn);
			SqlDataReader reader = cmd.ExecuteReader();
			while(reader.Read()) {
				int id = reader.GetInt32(reader.GetOrdinal("Id"));
				string username = reader.GetString(reader.GetOrdinal("Username"));
				string password = reader.GetString(reader.GetOrdinal("Password"));
				string firstname = reader.GetString(reader.GetOrdinal("Firstname"));
				string lastname = reader.GetString(reader.GetOrdinal("Lastname"));
				string phone = reader.GetString(reader.GetOrdinal("Phone"));
				string email = reader.GetString(reader.GetOrdinal("Email"));
				bool isreviewer = reader.GetBoolean(reader.GetOrdinal("IsReviewer"));
				bool isadmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"));
				bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

				User user = new User();
				user.Id = id;
				user.Username = username;
				user.Password = password;
				user.Firstname = firstname;
				user.Lastname = lastname;
				user.Phone = phone;
				user.IsReviewer = isreviewer;
				user.IsAdmin = isadmin;
				user.Active = active;

				users.Add(user);

				//System.Diagnostics.Debug.WriteLine($"{id}, {username}");
			}


			conn.Close();
		}
	}
}
