using superMan.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// systemConfigs.xaml 的交互逻辑
    /// </summary>
    public partial class systemConfigs : UserControl, INotifyPropertyChanged
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
        private ObservableCollection<tb_RAMfix> _dataitems = new ObservableCollection<tb_RAMfix>();

        public ObservableCollection<tb_RAMfix> DataItems
        {
            get { return _dataitems; }
            set
            {
                _dataitems = value;
                OnPropertyChanged("DataItems");
            }
        }
        private tb_RAMfix _Ramfix;

        public tb_RAMfix RAMFix
        {
            get { return _Ramfix; }
            set
            {
                _Ramfix = value;
                OnPropertyChanged("RAMFix");
            }
        }

        private ObservableCollection<tb_postAddr> _PostAddr = new ObservableCollection<tb_postAddr>();

        public ObservableCollection<tb_postAddr> PostAddr
        {
            get { return _PostAddr; }
            set
            {
                _PostAddr = value;
                OnPropertyChanged("PostAddr");
            }
        }
        private tb_postAddr _SelectAddr;

        public tb_postAddr SelectAddr
        {
            get { return _SelectAddr; }
            set
            {
                _SelectAddr = value;
                OnPropertyChanged("SelectAddr");
            }
        }

        public systemConfigs()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += SystemConfigs_Loaded;
        }

        private void SystemConfigs_Loaded(object sender, RoutedEventArgs e)
        {
            DoQuery();
            DoQueryAddr();
        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void DoQuery()
        {
            try
            {
                DataItems.Clear();
                List<tb_RAMfix> list = MySqlUitity.Ins.Query<tb_RAMfix>("select * from tb_RAMfix");
                foreach (var item in list)
                {
                    DataItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }
        private void DoQueryAddr()
        {
            try
            {
                PostAddr.Clear();
                List<tb_postAddr> list = MySqlUitity.Ins.Query<tb_postAddr>("select * from tb_postAddr");
                foreach (var item in list)
                {
                    PostAddr.Add(item);
                }
            }
            catch (Exception ex)
            {
                systemGlobal.Ins.MainWin.MessageTips(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MySqlUitity.Ins.InsertAsync(RAMFix);
            DoQuery();
            systemGlobal.Ins.MainWin.Tips("新增成功");
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            MySqlUitity.Ins.UpdateAsync(RAMFix);
            systemGlobal.Ins.MainWin.Tips("修改成功");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MySqlUitity.Ins.DeleteAsync(RAMFix);
            DataItems.Remove(RAMFix);
            systemGlobal.Ins.MainWin.Tips("删除成功");
        }

        private void btnAddAddr_lick(object sender, RoutedEventArgs e)
        {
            MySqlUitity.Ins.InsertAsync(SelectAddr);
            DoQueryAddr();
            systemGlobal.Ins.MainWin.Tips("新增成功");
        }

        private void btnEditAddr_Click(object sender, RoutedEventArgs e)
        {
            MySqlUitity.Ins.UpdateAsync(SelectAddr);
            systemGlobal.Ins.MainWin.Tips("修改成功");
        }

        private void btnDeleteAddr_Click(object sender, RoutedEventArgs e)
        {
            MySqlUitity.Ins.DeleteAsync(SelectAddr);
            PostAddr.Remove(SelectAddr);
            systemGlobal.Ins.MainWin.Tips("删除成功");
        }


    }
}
