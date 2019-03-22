using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace superMan
{
    public class MySqlUitity
    {
        public MySqlUitity()
        {

        }
        private static MySqlUitity _instance = null;
        public static MySqlUitity Ins
        {
            get { if (_instance == null) { _instance = new MySqlUitity(); } return _instance; }
        }
        private MySqlDbType GetDBType(string typestr)
        {
            switch (typestr)
            {
                case "Int32":
                    return MySqlDbType.Int32;
                case "Double":
                    return MySqlDbType.Double;
                case "DateTime":
                    return MySqlDbType.DateTime;
                case "String":
                    return MySqlDbType.VarChar;
                default:
                    return MySqlDbType.Int32;
            }
        }
        public Task<int> InsertAsync(object item)
        {
            return Task.Factory.StartNew(() =>
            {
                return Insert(item, item.GetType());
            });
        }

        public int Insert(object obj, Type objType)
        {
            if (obj == null || objType == null)
            {
                return 0;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ");
            strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));
            strSql.Append(" (");

            List<string> strList = new List<string>();
            int i = 0;
            PropertyInfo[] propertys = objType.GetProperties();
            int filedNum = 0;
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
              typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                {
                    continue;
                }
                filedNum++;
            }
            MySqlParameter[] parameters = new MySqlParameter[filedNum];
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
              typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                {
                    continue;
                }
                if (info.Name == "key")
                {

                    strList.Add(string.Format("`{0}`", info.Name));
                }
                else
                    strList.Add(string.Format("{0}", info.Name));
                parameters[i] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                parameters[i].Value = info.GetValue(obj);
                i++;
            }
            strSql.Append(string.Join(",", strList));
            strSql.Append(") values (");
            strList.Clear();
            foreach (var info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
           typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                {
                    continue;
                }
                strList.Add(string.Format("@{0}", info.Name));
            }
            strSql.Append(string.Join(",", strList));
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            int count;
            try
            {
                count = DbManager.Ins.ExecuteNonquery(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }
        public Task<int> UpdateAsync(object item)
        {
            return Task.Factory.StartNew(() =>
            {
                return Update(item, item.GetType());
            });
        }


        public Task<int> UpdateBySql(string sqlStr, MySqlParameter[] parameters)
        {
            return Task.Factory.StartNew(() =>
            {
                return Updatebysql(sqlStr, parameters);
            });
        }

        private int Updatebysql(string sqlStr, MySqlParameter[] parameters)
        {

            int count;
            try
            {
                count = DbManager.Ins.ExecuteNonquery(sqlStr, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }


        public Task<int> ExecuteNonquery(string sqlStr, MySqlParameter[] parameters)
        {
            return Task.Factory.StartNew(() =>
            {
                return Executenonquery(sqlStr, parameters);
            });
        }

        private int Executenonquery(string sqlStr, MySqlParameter[] parameters)
        {

            int count;
            try
            {
                count = DbManager.Ins.ExecuteNonquery(sqlStr, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }


        public int Update(object obj, Type objType)
        {
            if (obj == null || objType == null)
            {
                return 0;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE  ");
            strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));
            strSql.Append(" set ");

            string strWhere = "";
            List<string> strList = new List<string>();
            int i = 0;
            PropertyInfo[] propertys = objType.GetProperties();
            MySqlParameter[] parameters = new MySqlParameter[propertys.Length];
            foreach (PropertyInfo info in propertys)
            {
                if (info.Name == "id")
                {
                    strWhere = string.Format(" where {0}=@{0}", info.Name);
                }
                strList.Add(string.Format("{0}=@{0}", info.Name));
                parameters[i] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                parameters[i].Value = info.GetValue(obj);
                i++;
            }
            strSql.Append(string.Join(",", strList));
            strSql.Append(strWhere);

            int count;
            try
            {
                count = DbManager.Ins.ExecuteNonquery(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }

        public Task<int> DeleteAsync(object item)
        {
            return Task.Factory.StartNew(() =>
            {
                return Delete(item, item.GetType());
            });
        }
        public int Delete(object obj, Type objType)
        {
            if (obj == null || objType == null)
            {
                return 0;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Delete from ");
            strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));

            string strWhere = "";
            PropertyInfo[] propertys = objType.GetProperties();
            MySqlParameter[] parameters = new MySqlParameter[1];
            foreach (PropertyInfo info in propertys)
            {
                if (info.Name == "id")
                {
                    strWhere = string.Format(" where {0}=@{0}", info.Name);
                    parameters[0] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                    parameters[0].Value = info.GetValue(obj);
                }
            }
            strSql.Append(strWhere);

            int count;
            try
            {
                count = DbManager.Ins.ExecuteNonquery(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }
        public Task<int> ExecuteProcAsync(object item, string procName)
        {
            return Task.Factory.StartNew(() =>
            {
                return ExecuteProc(item, item.GetType(), procName);
            });
        }
        public int ExecuteProc(object obj, Type objType, string procName)
        {
            if (obj == null || objType == null)
            {
                return 0;
            }


            List<string> strList = new List<string>();
            int i = 0;
            PropertyInfo[] propertys = objType.GetProperties();
            MySqlParameter[] parameters = new MySqlParameter[propertys.Length - 1];
            foreach (PropertyInfo info in propertys)
            {
                if (info.Name == "key")
                {
                    strList.Add(string.Format("`{0}`", info.Name));
                }
                else
                    strList.Add(string.Format("{0}", info.Name));
                parameters[i] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                parameters[i].Value = info.GetValue(obj);
                i++;
            }
            strList.Clear();
            foreach (var info in propertys)
            {
                strList.Add(string.Format("@{0}", info.Name));
            }
            int count;
            try
            {
                count = DbManager.Ins.ExecuteProcNonQuery(procName, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }
        public Task<int> ExecuteProcByRetrunAsync<T>(object item, string procName)
        {
            return Task.Factory.StartNew(() =>
            {
                return ExecuteProcByRetrun<T>(item,  procName);
            });
        }
        public int ExecuteProcByRetrun<T>(object obj,  string procName)
        {
            if (obj == null)
            {
                return 0;
            }
            int i = 0;
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            int length = 0;
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
                                           typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                    continue;
                length++;
            }
            MySqlParameter[] parameters = new MySqlParameter[length];
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
                                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                    continue;
                parameters[i] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                parameters[i].Value = info.GetValue(obj);
                i++;
            }
            try
            {
                var counta = DbManager.Ins.ExecuteProcQuery(procName, parameters);
                
            }
            catch (Exception ex)
            {
               // Log.WriteError(ex.Message,ex);
            }
            return 0;
        }
        public Task<bool> ExcuteCommandByTran(List<string> sqlStr)
        {
            return Task.Factory.StartNew(() =>
            {
                List<MySqlCommand> cmds = new List<MySqlCommand>();
                foreach (var item in sqlStr)
                {
                    cmds.Add(new MySqlCommand(item));
                }
                if (cmds.Count == 0) return false;
                return DbManager.Ins.ExcuteCommandByTran(cmds.ToArray());
            });
          
        }
        public Task<List<T>> QueryAsync<T>(string sql)
            where T : new()
        {
            return Task<List<T>>.Factory.StartNew(() =>
            {
                return Query<T>(sql);
            });
        }
        public List<T> Query<T>(string sql)
        {
            DataTable dt = DbManager.Ins.ExcuteDataTable(sql);
            return DataSetToList<T>(dt).ToList();
        }

        private T parasToList<T>(MySqlParameter[] outparameters)
        {
            if (outparameters == null || outparameters.Length <= 0)
                return default(T);
            T _t = Activator.CreateInstance<T>();
            //获取对象所有属性
            PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
            foreach (PropertyInfo info in propertyInfo)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
                                      typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                    continue;
                foreach (var item in outparameters)
                {
                    if (item.ParameterName.ToUpper().Substring(1).Equals(info.Name.ToUpper()))
                    {
                        info.SetValue(_t, item.Value);
                    }
                }

            }
            return _t;
        }
        private IList<T> DataSetToList<T>(DataTable dt)
        {

            IList<T> list = new List<T>();
            //确认参数有效
            if (dt == null || dt.Rows.Count <= 0)
                return list;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();

                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }

                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }


    }

}
