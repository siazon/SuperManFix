// order.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    IsRead: true,
    BlandVer: '苹果 iPhone 5s',
    FaultCount: 0,
    Amount: 0,
    ReAmount: 0,
    TotalAmount: 0,
    orders: [],
    thumb: '',
    nickname: '',
    state: '参考价为预测报价，实际以工程师上门检测为准。以上报价为零件交换价格，损坏零件需被工程师收回，如需保留，另补差价。'
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var _this = this
   
    var temo = getApp().globalData.doorType
    if (temo == "1") {
      this.setData({
        state: '由于扩容需要大型精密设备不便搬运的原因，我们无法提供上门现场扩容服务，建议您选择到店扩容，同时提供邮寄和上门取件的服务。'

      })
    }
    else {
      this.setData({
        state: '参考价为预测报价，实际以工程师上门检测为准。以上报价为零件交换价格，损坏零件需被工程师收回，如需保留，另补差价。'
      })
    }

    var BlandVer = ''
    var Amount = 0
    var orders = []
    var Globalorders = getApp().globalData.Orders;
    if (Globalorders.length > 0) {
      BlandVer = Globalorders[0].Bland + ' ' + Globalorders[0].Ver + '-' + Globalorders[0].Color
    }
    for (var k = 0, length = Globalorders.length; k < length; k++) {
      var order = new Object()
      order.Index = k + 1
      order.Fault = Globalorders[k].Fault
      order.Price = Globalorders[k].Price
      orders.push(order)
      Amount = parseInt(Amount) + parseInt(Globalorders[k].Price)
    }
    _this.setData({
      FaultCount: orders.length,
      Amount: Amount,
      TotalAmount: Amount,
      BlandVer: BlandVer,
      orders: orders
    });

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

  }, Chk_Click:function(){
    if (this.data.IsRead==true){
      this.setData({
        IsRead: false
      })
    }
    else{
      this.setData({
        IsRead: true
      })
    }
  },
  btnSubmit_click: function () {
    if (!this.data.IsRead)
    {
      wx.showToast({
        title: '请阅读条款。',
        image: '/image/warning.png',
        duration: 1500
      })
      return;}
     
    wx.navigateTo({
      url: '/page/userinfo/userinfo'

    })
  },
  service_Click: function () {
    this.setData({
      IsRead:true
    })
    getApp().globalData.ServiceType = 0;
    wx.navigateTo({
      url: '/page/serviceterms/serviceterms'

    })
  }
})