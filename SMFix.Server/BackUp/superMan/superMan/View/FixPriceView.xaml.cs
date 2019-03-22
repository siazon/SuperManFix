using superMan.Model;
using System;
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
    /// FixPriceView.xaml 的交互逻辑
    /// </summary>
    public partial class FixPriceView : UserControl, INotifyPropertyChanged
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
        public FixPriceView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += FixPriceView_Loaded;
        }
        private List<wxjm> _Prices;

        public List<wxjm> Prices
        {
            get { return _Prices; }
            set
            {
                _Prices = value;
                OnPropertyChanged("Prices");
            }
        }
        private wxjm wXJM;

        public wxjm WXJM
        {
            get { return wXJM; }
            set
            {
                wXJM = value;
                OnPropertyChanged("WXJM");
            }
        }

        private ObservableCollection<string> _blands = new ObservableCollection<string>();

        public ObservableCollection<string> Blands
        {
            get { return _blands; }
            set
            {
                _blands = value;
                OnPropertyChanged("Blands");
            }
        }
        private ObservableCollection<string> _Vers = new ObservableCollection<string>();

        public ObservableCollection<string> Vers
        {
            get { return _Vers; }
            set
            {
                _Vers = value;
                OnPropertyChanged("Vers");
            }
        }
        private ObservableCollection<string> _FixTypes = new ObservableCollection<string>();

        public ObservableCollection<string> FixTypes
        {
            get { return _FixTypes; }
            set
            {
                _FixTypes = value;
                OnPropertyChanged("FixTypes");
            }
        }
        private ObservableCollection<string> _Fixs = new ObservableCollection<string>();

        public ObservableCollection<string> Fixs
        {
            get { return _Fixs; }
            set
            {
                _Fixs = value;
                OnPropertyChanged("Fixs");
            }
        }
        List<wxjm> prices = new List<wxjm>();
        private void FixPriceView_Loaded(object sender, RoutedEventArgs e)
        {

            DoQuery();
            prices = Prices;

            DataTable dt = DbManager.Ins.ExcuteDataTable(" SELECT DISTINCT sjpp from wxjm");
            foreach (DataRow item in dt.Rows)
            {

                Blands.Add(item["sjpp"] == null ? "" : item["sjpp"].ToString());//SELECT DISTINCT yanse from sjxh
            }
            pColors.Children.Clear();
            dt = DbManager.Ins.ExcuteDataTable(" SELECT DISTINCT yanse from sjxh");
            foreach (DataRow item in dt.Rows)
            {
                string ColorName = item["yanse"] == null ? "" : item["yanse"].ToString();
                CreateColorControl(ColorName);

            }
        }

        private void ClearSelect_Click(object sender, RoutedEventArgs e)
        {
            cbBland.SelectedIndex = -1;
            cbVer.SelectedIndex = -1;
            cbfixType.SelectedIndex = -1;
            cbfix.SelectedIndex = -1;
            DoQuery();
        }

        private void btnQueyr_Click(object sender, RoutedEventArgs e)
        {
            DoQuery();

        }
        public void DoQuery()
        {
            try
            {


                //Blands.Clear(); Vers.Clear(); FixTypes.Clear(); Fixs.Clear();
                string bland = cbBland.SelectedItem == null ? "" : cbBland.SelectedItem.ToString();
                string ver = cbVer.SelectedItem == null ? "" : cbVer.SelectedItem.ToString();
                string fisType = cbfixType.SelectedItem == null ? "" : cbfixType.SelectedItem.ToString();
                string fix = cbfix.SelectedItem == null ? "" : cbfix.SelectedItem.ToString();
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.Append("SELECT * from wxjm where 1=1");
                if (bland != "")
                {
                    sqlstr.AppendFormat(" and sjpp='{0}'", bland);
                }
                if (ver != "")
                {
                    sqlstr.AppendFormat(" and sjxh='{0}'", ver);
                }
                if (fisType != "")
                {
                    sqlstr.AppendFormat(" and mklx='{0}'", fisType);
                }
                if (fix != "")
                {
                    sqlstr.AppendFormat(" and gzlx='{0}'", fix);
                }
                Prices = MySqlUitity.Ins.Query<wxjm>(sqlstr.ToString());
                foreach (var item in Prices)
                {
                    if (!Blands.Contains(item.sjpp))
                    {
                        Blands.Add(item.sjpp);
                    }
                    if (!Vers.Contains(item.sjxh))
                    {
                        Vers.Add(item.sjxh);
                    }
                    if (!FixTypes.Contains(item.mklx))
                    {
                        FixTypes.Add(item.mklx);
                    }
                    if (!Fixs.Contains(item.gzlx))
                    {
                        Fixs.Add(item.gzlx);
                    }
                }
                //cbBland.ItemsSource = Blands;
                //cbVer.ItemsSource = Vers;
                //cbfixType.ItemsSource = FixTypes;
                //cbfix.ItemsSource = Fixs;
                grid.ItemsSource = Prices;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WXJM.id = Guid.NewGuid().ToString("N");
            MySqlUitity.Ins.InsertAsync(WXJM);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MySqlUitity.Ins.UpdateAsync(WXJM);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (WXJM != null)
                MySqlUitity.Ins.DeleteAsync(WXJM);
        }
        private void cbBland_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectionstr = cbBland.SelectedItem == null ? "" : cbBland.SelectedItem.ToString();
                if (selectionstr == "")
                    return;
                Vers.Clear();
                foreach (var item in Prices)
                {
                    if (item.sjpp == selectionstr)
                    {
                        if (!Vers.Contains(item.sjxh))
                        {
                            Vers.Add(item.sjxh);
                        }
                    }

                }

                var selectionstr1 = cbBland1.SelectedItem == null ? "" : cbBland1.SelectedItem.ToString();
                if (selectionstr1 == "")
                    return;
                Vers.Clear();
                DataTable dt = DbManager.Ins.ExcuteDataTable(string.Format("SELECT DISTINCT sjxh   from wxjm where sjpp ='{0}' ORDER BY sjxh DESC", selectionstr1));
                foreach (DataRow item in dt.Rows)
                {

                    Vers.Add(item["sjxh"] == null ? "" : item["sjxh"].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cbVer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectionstr = cbVer.SelectedItem == null ? "" : cbVer.SelectedItem.ToString();
                if (selectionstr == "")
                    return;
                FixTypes.Clear();
                foreach (var item in Prices)
                {
                    if (item.sjxh == selectionstr)
                    {
                        if (!FixTypes.Contains(item.mklx))
                        {
                            FixTypes.Add(item.mklx);
                        }
                    }
                }

                string bland = cbBland1.SelectedItem == null ? "" : cbBland1.SelectedItem.ToString();
                string ver = cbVer1.SelectedItem == null ? "" : cbVer1.SelectedItem.ToString();
                if (string.IsNullOrWhiteSpace(bland) || string.IsNullOrWhiteSpace(ver))
                {
                    MessageBox.Show("请选中品牌与型号");
                    return;
                }
                foreach (CheckBox Chk in pColors.Children)
                {
                    Chk.IsChecked = false;

                }
                DataTable dtf = DbManager.Ins.ExcuteDataTable(string.Format("SELECT DISTINCT mklx   from wxjm where sjpp='{0}' and sjxh='{1}'", bland, ver));
                foreach (DataRow item in dtf.Rows)
                {

                    FixTypes.Add(item["mklx"] == null ? "" : item["mklx"].ToString());
                }
                DataTable dt = DbManager.Ins.ExcuteDataTable(string.Format("SELECT yanse from sjxh where sjpp='{0}' and sjxh='{1}'", bland, ver));
                foreach (CheckBox Chk in pColors.Children)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        string ColorName = item["yanse"] == null ? "" : item["yanse"].ToString();
                        if (ColorName == Chk.Content.ToString())
                        {
                            Chk.IsChecked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cbfixType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectionstr = cbfixType.SelectedItem == null ? "" : cbfixType.SelectedItem.ToString();
                if (selectionstr == "")
                    return;
                Fixs.Clear();
                foreach (var item in Prices)
                {
                    if (item.mklx == selectionstr)
                    {
                        if (!Fixs.Contains(item.gzlx))
                        {
                            Fixs.Add(item.gzlx);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cbfix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }




        private ObservableCollection<string> _blands1 = new ObservableCollection<string>();

        public ObservableCollection<string> Blands1
        {
            get { return _blands1; }
            set
            {
                _blands1 = value;
                OnPropertyChanged("Blands");
            }
        }
        private ObservableCollection<string> _Vers1 = new ObservableCollection<string>();

        public ObservableCollection<string> Vers1
        {
            get { return _Vers1; }
            set
            {
                _Vers1 = value;
                OnPropertyChanged("Vers");
            }
        }

        private void CreateColorControl(string Name)
        {
            CheckBox Chk = new CheckBox();
            Chk.FontSize = 20;
            Chk.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            Chk.Margin = new Thickness(10);
            Chk.Content = Name;
            Chk.Click += Chk_Click;
            pColors.Children.Add(Chk);
        }

        private void Chk_Click(object sender, RoutedEventArgs e)
        {
            CheckBox Chk = sender as CheckBox;
            txtDelColor.Text = Chk.Content.ToString();
        }

        private void cbBland1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectionstr = cbBland1.SelectedItem == null ? "" : cbBland1.SelectedItem.ToString();
                if (selectionstr == "")
                    return;
                Vers.Clear();
                DataTable dt = DbManager.Ins.ExcuteDataTable(string.Format("SELECT DISTINCT sjxh   from wxjm where sjpp ='{0}' ORDER BY sjxh DESC", selectionstr));
                foreach (DataRow item in dt.Rows)
                {

                    Vers.Add(item["sjxh"] == null ? "" : item["sjxh"].ToString());
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }

        private void cbVer1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string bland = cbBland1.SelectedItem == null ? "" : cbBland1.SelectedItem.ToString();
                string ver = cbVer1.SelectedItem == null ? "" : cbVer1.SelectedItem.ToString();
                if (string.IsNullOrWhiteSpace(bland) || string.IsNullOrWhiteSpace(ver))
                {
                    MessageBox.Show("请选中品牌与型号");
                    return;
                }
                foreach (CheckBox Chk in pColors.Children)
                {
                    Chk.IsChecked = false;

                }
                DataTable dt = DbManager.Ins.ExcuteDataTable(string.Format("SELECT yanse from sjxh where sjpp='{0}' and sjxh='{1}'", bland, ver));
                foreach (CheckBox Chk in pColors.Children)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        string ColorName = item["yanse"] == null ? "" : item["yanse"].ToString();
                        if (ColorName == Chk.Content.ToString())
                        {
                            Chk.IsChecked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string bland = cbBland1.SelectedItem == null ? "" : cbBland1.SelectedItem.ToString();
                string ver = cbVer1.SelectedItem == null ? "" : cbVer1.SelectedItem.ToString();
                if (string.IsNullOrWhiteSpace(bland) || string.IsNullOrWhiteSpace(ver))
                {
                    MessageBox.Show("请选中品牌与型号");
                    return;
                }
                foreach (CheckBox Chk in pColors.Children)
                {
                    Chk.IsChecked = false;

                }
                DataTable dt = DbManager.Ins.ExcuteDataTable(string.Format("SELECT yanse from sjxh where sjpp='{0}' and sjxh='{1}'", bland, ver));
                foreach (CheckBox Chk in pColors.Children)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        string ColorName = item["yanse"] == null ? "" : item["yanse"].ToString();
                        if (ColorName == Chk.Content.ToString())
                        {
                            Chk.IsChecked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }

        private void btnEdit1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string bland = cbBland1.SelectedItem == null ? "" : cbBland1.SelectedItem.ToString();
                string ver = cbVer1.SelectedItem == null ? "" : cbVer1.SelectedItem.ToString();
                if (string.IsNullOrWhiteSpace(bland) || string.IsNullOrWhiteSpace(ver))
                {
                    MessageBox.Show("请选中品牌与型号");
                    return;
                }
                StringBuilder sqlstr = new StringBuilder();
                foreach (CheckBox item in pColors.Children)
                {
                    if (item.IsChecked == true)
                    {
                        sqlstr.AppendFormat(" INSERT INTO sjxh(id, sjpp, sjxh, yanse) VALUES('{0}', '{1}', '{2}', '{3}');",
                    Guid.NewGuid().ToString("N"), bland, ver, item.Content.ToString());
                    }
                }
                int i = DbManager.Ins.ExecuteNonquery(string.Format("DELETE from sjxh where sjpp='{0}' and sjxh='{1}'; {2};",
                      bland, ver, sqlstr.ToString()));
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

        private void btnDelete1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult re = MessageBox.Show("是否确定删除颜色:<" + txtDelColor.Text + ">", "提示", MessageBoxButton.OKCancel);
                if (re == MessageBoxResult.OK)
                {


                    int i = DbManager.Ins.ExecuteNonquery(string.Format("DELETE from sjxh where yanse = '{0}'", txtDelColor.Text));
                    if (i > 0)
                    {
                        CheckBox che = null;
                        foreach (CheckBox item in pColors.Children)
                        {
                            if (item.Content.ToString() == txtDelColor.Text)
                            {
                                che = item;
                            }
                        }
                        if (che != null)
                            pColors.Children.Remove(che);
                        systemGlobal.Ins.MainWin.Tips("删除成功");
                    }
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }

        private void btnAdd1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(WXJM.sjpp) ||
                    string.IsNullOrWhiteSpace(WXJM.sjxh) ||
                    string.IsNullOrWhiteSpace(WXJM.mklx) ||
                    string.IsNullOrWhiteSpace(WXJM.gzlx) ||
                    string.IsNullOrWhiteSpace(WXJM.ycjg) )
                { MessageBox.Show("输入内容不合法"); return; }
                string bland = cbBland1.SelectedItem == null ? "" : cbBland1.SelectedItem.ToString();
                string ver = cbVer1.SelectedItem == null ? "" : cbVer1.SelectedItem.ToString();
                if (string.IsNullOrWhiteSpace(bland) || string.IsNullOrWhiteSpace(ver) || string.IsNullOrWhiteSpace(txtNewColor.Text))
                {
                    MessageBox.Show("请选中品牌与型号并填写正确的颜色名称");
                    return;
                }
                int i = DbManager.Ins.ExecuteNonquery(string.Format("INSERT INTO sjxh(id, sjpp, sjxh, yanse) VALUES('{0}', '{1}', '{2}', '{3}')",
                    Guid.NewGuid().ToString("N"), bland, ver, txtNewColor.Text));
                if (i > 0)
                {
                    CreateColorControl(txtNewColor.Text);
                    systemGlobal.Ins.MainWin.Tips("新增成功");
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }
    }
}
