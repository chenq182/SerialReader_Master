using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace remoteMysql
{
    /* Author   : Quan Chen
     * Name     : Remote MySQL database manager
     * e-mail   : chenq182@sina.com
     * Date     : Mar 27th, 2015
     */
    public class Mysql
    {
        #region 状态及配置
        private MySqlConnection connection = null;
        private MySqlTransaction transaction = null;
        private Boolean available = false;

        private string server = "";
        private string database = "";
        private string user = "";
        private string password = "";
        #endregion
        #region 构造函数
        public Mysql()  // 配置文件为*.config
        {
            AppSettingsReader ar = new AppSettingsReader();
            server += ar.GetValue("server", typeof(string));
            database += ar.GetValue("database", typeof(string));
            user += ar.GetValue("user", typeof(string));
            password += ar.GetValue("password", typeof(string));
        }
        public Mysql(string server, string database, string user, string password)
        {
            this.server = server;
            this.database = database;
            this.user = user;
            this.password = password;
        }
        #endregion
        #region 公共方法
        public Boolean Active()                     //开启
        {
            try
            {
                connection = new MySqlConnection(cs());
                connection.Open();
                available = true;
                return true;
            }
            catch (MySqlException ex)
            {
                available = false;
                //Console.WriteLine("Error: {0}", ex.ToString());
                return false;
                throw new Exception(ex.Message);
            }
        }
        public MySqlDataReader Reader(string sql)   //读
        {
            if (available == false)
                Active();
            MySqlCommand command = new MySqlCommand(sql, connection);
            return command.ExecuteReader();
        }
        public int Writer(string sql)               //写
        {
            if (available == false)
                Active();
            MySqlCommand command = new MySqlCommand(sql, connection);
            return command.ExecuteNonQuery();
        }
        public void Trans(string[] sqls)            //写事务
        {
            try
            {
                if (available == false)
                    Active();
                transaction = connection.BeginTransaction();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                foreach (string s in sqls)
                {
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (MySqlException ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (MySqlException ex1)
                {
                    throw new Exception(ex1.Message);
                }
                throw new Exception(ex.Message);
            }
            finally
            {
                if (available == true)
                    Close();
            }
        }
        public void Close()                         //关闭
        {
            if (connection != null)
                connection.Close();
            available = false;
        }

        public string Server    //服务器
        {
            get { return server; }
            set { server = value; }
        }
        public string Database  //数据库
        {
            get { return database; }
            set { database = value; }
        }
        public string User      //用户名
        {
            get { return user; }
            set { user = value; }
        }
        public string Password  //密码
        {
            get { return password; }
            set { password = value; }
        }
        protected string cs()     //连接字符串
        {
            return "server=" + server + ";user id=" + user +
                ";password=" + password + ";database=" + database;
        }
        #endregion

        /* 示例
        static void Main(string[] args)
        {
            Mysql test = new Mysql();
            test.User = "user";
            string t2 = "SELECT * FROM Admin";
            MySqlDataReader reader = test.Reader(t2);
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    string t = reader["AId"].ToString() + "\t";
                    t += reader["AKey"].ToString() + "\t";
                    t += reader["APri"].ToString();
                    Console.WriteLine(t);
                }
            }
            reader.Close();

            string[] node;
            node = new string[51];
            int i = 0;
            for (i = 0; i <= 50; i++)
                node[i] = "INSERT INTO NodeGIS (NId) VALUES (" + i + ");";
            test.Trans(node);

            test.Close();
            Console.ReadLine();
        }//*/
    }
}
