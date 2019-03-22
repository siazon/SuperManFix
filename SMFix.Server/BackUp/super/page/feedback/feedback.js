// feedback.js
Page({

  /**
   * 页面的初始数据
   */
  data: {

  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

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
  btnSubmin_Click: function () {

  },

  bindFormSubmit: function (e) {
    function isNull(str) {
      if (str == "") return true;
      var regu = "^[ ]+$";
      var re = new RegExp(regu);
      return re.test(str);
    }
    if (isNull(e.detail.value.textarea))
      return
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType: "feedback",
        Data: {
          feedback: e.detail.value.Phone + ':' + e.detail.value.textarea,
        }
      },
      method: 'POST',
      success: function (res) {
        wx.showToast({
          title: '提交成功，感谢您的反馈！',
          icon: 'success',
          duration: 2000
        })
        setTimeout(function () {
          wx.navigateBack({
            delta: 1
          })
        }, 2000);
      },
      fail: function (res) {
        console.log(res.data);
        console.log('is failed')
      }
    })

  },
})