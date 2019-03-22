// page/recyclePro/recyclePro.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    queryType: 1,
    sdate: '',
    edate: '',
    Phone: '',
    listTitle: '维修订单',
    orders: [],
    Reorders: [],

  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.setData({
      //sdate: new Date().format("yyyy-MM-dd"),
      //time: new Date().toLocaleTimeString(),
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  },
  phone_input: function (e) {
    this.setData({
      Phone: e.detail.value
    })
  },
  btnQuery_Click: function () {
    var _this = this
    if (_this.data.queryType == 1)
      _this.DoQeury();
    if (_this.data.queryType == 2)
      _this.DoFQeury('0');
    if (_this.data.queryType == 3)
      _this.DoFQeury('1');
  },
  radioChange: function (e) {
    var _this = this
    _this.setData({
      Reorders: [],

    });
    var title = '维修订单';
    if (e.detail.value == 2) {
      title = '回收订单';
    }
    if (e.detail.value == 3) {
      title = '用户反馈';
    }
    this.setData({
      queryType: e.detail.value,
      listTitle: title
    })
  },
  DoQeury() {
    var _this = this
    //订单查询
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType: "GetOrders",
        Data: {
          sdate: this.data.sdate,
          edate: this.data.edate,
          bland: this.data.Phone,
          qtype: 1
        }
      },
      method: 'POST',
      success: function (res) {
        console.log(res)
        _this.setData({
          orders: res.data,

        });
        if (res.data.length == 0) {
          wx.showToast({
            title: '没有数据',
            image: '/image/warning.png',
            duration: 1000
          })
        }
      },
      fail: function (res) {
        console.log(res.data);
        console.log('is failed')
      }
    })
  },

  DoFQeury(e) {
    var _this = this
    //订单查询
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType:'feedbacks',
        Data:{
        sdate: this.data.sdate,
        edate: this.data.edate,
        phone: this.data.Phone,
        qtype: e}
      },
      method: 'POST',
      success: function (res) {
        console.log(res)
        _this.setData({
          Reorders: res.data,

        });
        if (res.data.length == 0) {
          wx.showToast({
            title: '没有数据',
            image: '/image/warning.png',
            duration: 1000
          })
        }
      },
      fail: function (res) {
        console.log(res.data);
        console.log('is failed')
      }
    })
  },
  sbindDateChange: function (e) {
    this.setData({
      sdate: e.detail.value
    })
  },
  ebindDateChange: function (e) {
    this.setData({
      edate: e.detail.value
    })
  },
  service_Click: function () {
    wx.navigateTo({
      url: '/page/serviceterms/serviceterms?para=1'

    })
  },
  makePhone: function (e) {
    console.log(e)
    wx.makePhoneCall({
      phoneNumber: e.currentTarget.dataset.phone,
    })
  },
  payOrders: function (e) {
    console.log(e.currentTarget.dataset.phone)
    wx.makePhoneCall({
      phoneNumber: e.currentTarget.dataset.phone,
    })
  }
})