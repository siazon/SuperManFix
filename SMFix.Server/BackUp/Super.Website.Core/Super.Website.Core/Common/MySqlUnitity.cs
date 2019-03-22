using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Super.Website.Core
{
    public class MySqlUnitity
    {
        public MySqlUnitity()
        {

        }
        private static MySqlUnitity _instance = null;
        public static MySqlUnitity Ins
        {
            get { if (_instance == null) { _instance = new MySqlUnitity(); } return _instance; }
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
            MySqlParameter[] parameters = new MySqlParameter[propertys.Length];
            foreach (PropertyInfo info in propertys)
            {
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
            DataTable dt= DbManager.Ins.ExcuteDataTable(sql);
            return DataSetToList<T>(dt).ToList();
        }


        public IList<T> DataSetToList<T>(DataTable dt)
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
