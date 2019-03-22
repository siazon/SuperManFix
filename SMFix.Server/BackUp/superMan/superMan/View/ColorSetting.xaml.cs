using superMan.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace superMan.View
{
    /// <summary>
    /// ColorSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ColorSetting : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        private ObservableCollection<PushUser> _PushUser = new ObservableCollection<PushUser>();

        public ObservableCollection<PushUser> PushUser
        {
            get { return _PushUser; }
            set
            {
                _PushUser = value;
                OnPropertyChanged("PushUser");
            }
        }
        private PushUser _selectPush;

        public PushUser SelectPush
        {
            get { return _selectPush; }
            set
            {
                if (value != null)
                {
                    RefreshRules(value.ruleID);
                }
                _selectPush = value;
                OnPropertyChanged("SelectPush");
            }
        }
        private bool _chk1;

        public bool Chk1
        {
            get { return _chk1; }
            set
            {

                _chk1 = value;
                OnPropertyChanged("Chk1");
            }
        }
        private bool _chk2;

        public bool Chk2
        {
            get { return _chk2; }
            set
            {

                _chk2 = value;
                OnPropertyChanged("Chk2");
            }
        }
        private bool _chk4;

        public bool Chk4
        {
            get { return _chk4; }
            set
            {

                _chk4 = value;
                OnPropertyChanged("Chk4");
            }
        }
        private bool _chk8;

        public bool Chk8
        {
            get { return _chk8; }
            set
            {

                _chk8 = value;
                OnPropertyChanged("Chk8");
            }
        }
        private bool _chk16;

        public bool Chk16
        {
            get { return _chk16; }
            set
            {

                _chk16 = value;
                OnPropertyChanged("Chk16");
            }
        }
        private bool _chk32;

        public bool Chk32
        {
            get { return _chk32; }
            set
            {

                _chk32 = value;
                OnPropertyChanged("Chk32");
            }
        }
        private bool _chk64;

        public bool Chk64
        {
            get { return _chk64; }
            set
            {

                _chk64 = value;
                OnPropertyChanged("Chk64");
            }
        }
        private ObservableCollection<tb_user> _User = new ObservableCollection<tb_user>();

        public ObservableCollection<tb_user> User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged("User");
            }
        }
        private tb_user _SelectUser;

        public tb_user SelectUser
        {
            get { return _SelectUser; }
            set
            {
                _SelectUser = value;
                OnPropertyChanged("SelectUser");
            }
        }
        public ColorSetting()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += ColorSetting_Loaded;
        }

        private void ColorSetting_Loaded(object sender, RoutedEventArgs e)
        {
            DoQuery();
            DoQueryUser();
            DoQueryPushRule();
        }

        private void DoQuery()
        {
            try
            {
                DataTable dt = DbManager.Ins.ExcuteDataTable("SELECT `VALUE` from tb_systemConfig where code ='pwd'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtPWD.Text = dt.Rows[0]["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void DoQueryPushRule()
        {
            try
            {
                PushUser.Clear();
                int ruleid = 0;
                List<PushUser> list = MySqlUitity.Ins.Query<PushUser>("SELECT `id`,`code` Rule,`name`,`value` Phone from tb_systemConfig where code like 'Rule%'");
                foreach (var item in list)
                {
                    int.TryParse(item.rule.Substring(4, item.rule.Length - 4), out ruleid);
                    item.ruleID = ruleid;
                    PushUser.Add(item);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void DoQueryUser()
        {
            try
            {
                User.Clear();
                List<tb_user> list = MySqlUitity.Ins.Query<tb_user>("select * from tb_user");
                foreach (var item in list)
                {
                    User.Add(item);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            DoQuery();
        }
        private void btnQueryUser_Click(object sender, RoutedEventArgs e)
        {
            DoQueryUser();
        }
        private void btnEdti_Click(object sender, RoutedEventArgs e)
        {
            int i = DbManager.Ins.ExecuteNonquery(string.Format("UPDATE tb_systemConfig set `value`='{0}' where code='pwd'", txtPWD.Text.Trim().ToLower()));
            if (i > 0)
                systemGlobal.Ins.MainWin.Tips("修改成功");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MySqlUitity.Ins.DeleteAsync(SelectUser);
                User.Remove(SelectUser);
                systemGlobal.Ins.MainWin.Tips("删除成功");
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }
        private void RefreshRules(int ruleID)
        {
            try
            {
                Byte[] Bytes = BitConverter.GetBytes(ruleID);
                BitArray btt = new BitArray(Bytes);
                for (int i = 0; i < btt.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Chk1 = btt[i];
                            break;
                        case 1:
                            Chk2 = btt[i];
                            break;
                        case 2:
                            Chk4 = btt[i];
                            break;
                        case 3:
                            Chk8 = btt[i];
                            break;
                        case 4:
                            Chk16 = btt[i];
                            break;
                        case 5:
                            Chk32 = btt[i];
                            break;
                        case 6:
                            Chk64 = btt[i];
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }
        private int GetRuleID()
        {
            int rule = 0;
            if (Chk1)
                rule += 1;
            if (Chk2)
                rule += 2;
            if (Chk4)
                rule += 4;
            if (Chk8)
                rule += 8;
            if (Chk16)
                rule += 16;
            if (Chk32)
                rule += 32;
            if (Chk64)
                rule += 64;
            return rule;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Rule = "Rule" + GetRuleID();
                int i = DbManager.Ins.ExecuteNonquery(string.Format("insert into tb_systemConfig (`code`,`name`,`value`,active,cType) VALUES ('{0}','{1}','{2}',{3},'{4}')",
                      Rule, txtName.Text, txtPhone.Text, 1, "1"));
                if (i > 0)
                { DoQueryPushRule();
                    systemGlobal.Ins.MainWin.Tips("新增成功");
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }

        private void btnEditPush_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rule = GetRuleID();
                SelectPush.ruleID = rule;
                RefreshRules(SelectPush.ruleID);
                SelectPush.rule = "Rule" + rule;
                int i = DbManager.Ins.ExecuteNonquery(string.Format(@"UPDATE tb_systemConfig set 
            `name`='{0}',`VALUE`='{1}',`CODE`='{2}' where id ={3}",
                 SelectPush.name, SelectPush.Phone, SelectPush.rule, SelectPush.id));
                if (i > 0)
                {
                    systemGlobal.Ins.MainWin.Tips("修改成功");
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int i = DbManager.Ins.ExecuteNonquery(string.Format(@"DELETE from tb_systemConfig where id={0}", SelectPush.id));
            if (i > 0)
            {
                PushUser.Remove(SelectPush);
                systemGlobal.Ins.MainWin.Tips("删除成功");
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DoQueryPushRule();
        }
    }
}
