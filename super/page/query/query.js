// query.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    orders: [],
    Phone: '',
    thumb: '',
    nickname: '',
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var _this = this
    wx.getUserInfo({
      success: function (res) {
        _this.setData({
          thumb: res.userInfo.avatarUrl,
          nickname: res.userInfo.nickName
        })
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
  phone_input: function (e) {
    this.setData({
      Phone: e.detail.value
    })
  },
  btnQuery_Click: function () {
    var _this = this
    //订单查询
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType: "GetOrders",
        Data: {
          bland: this.data.Phone,
          qtype: 0
        }
      },
      method: 'POST',
      success: function (res) {
        _this.setData({
          orders: res.data,

        });
        if (res.data.length == 0) {
          wx.showToast({
            title: '该手机号码下没有订单！',
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
  payOrders: function (e) {
    console.log(e.currentTarget.dataset.phone)
    wx.makePhoneCall({
      phoneNumber: e.currentTarget.dataset.phone,
    })
  }
})