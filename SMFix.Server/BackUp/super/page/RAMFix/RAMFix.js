// page/RAMFix.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    fixs: [],
    infos: [],
    _currVer:'',
    radinfo:'',
    fixType:'',
    totalPrice:0
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var _this = this
    getApp().globalData.order.hasComite = false
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      method: 'POST',
      data:{
        FunType:"Fixs",
        Data:{

        }
      },
      success: function (res) {
        _this.setData({
          fixs: res.data
        });
      },
      fail: function (res) {
        console.log(res.data);
        console.log('is failed')
      }
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
  checkboxChange: function (e) {
    var _this = this

    for (var i = 0; i < this.data.fixs.length; i++) {
      if (e.detail.value == this.data.fixs[i].phoneCode) {
        _this.setData({
          _currVer: e.detail.value,
          infos: this.data.fixs[i].info,
          radinfo:'padding: 15rpx 0;'
        });
      }
    }
  },
  infoChange: function (e) {
    var _this = this
  var str = e.detail.value;
  var list = str.split('+'); 
  _this.setData({
    fixType: list[0],
    totalPrice: list[1],
  });
  },
  btnNext_click: function (e) {
    var _this = this
    if (this.data.totalPrice==0){return}
    var orders = []
    var order = new Object()
    order.Bland = "苹果"
    order.Ver = _this.data._currVer
    order.Color = '白色'
    order.Fault = '内存升级' + _this.data.fixType
    order.Price = _this.data.totalPrice
    orders.push(order)
    getApp().globalData.Orders = orders
    if (getApp().globalData.Orders.length == 0) {
      wx.showToast({
        title: '请选择故障!',
        image: '/image/warning.png',
        duration: 1000
      })
    }
    else {
      getApp().globalData.doorType = '1'
      wx.navigateTo({
        url: '/page/order/order'
      })
    }
  }
})