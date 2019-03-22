// page/recycle.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    district: ['上城区', '下城区', '江干区', '拱墅区', '西湖区', '滨江区', '萧山区', '余杭区'],
    districtIndex: 0,
    userName: '',
    phone: '',
    Addr: '',
    isagre: false,
    txtFlow:'>>服务流程<<'
  },
  bindDisChange: function (e) {
    this.setData({
      districtIndex: e.detail.value
    })
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
  UserName_input: function (e) {
    this.setData({
      userName: e.detail.value
    })
  },
  phone_input: function (e) {
    this.setData({
      phone: e.detail.value
    })
  },
  Addr_change: function (e) {
    this.setData({
      Addr: e.detail.value
    })
  },
  chkchange: function (e) {
    console.log(e);
    return;
    this.setData({
      isagre: e.detail.value
    })
  },
  service_Click: function () {
    getApp().globalData.ServiceType = 1;
    wx.navigateTo({
      url: '/page/serviceterms/serviceterms'

    })
  },
  flowtap:function(){
    getApp().globalData.ProcessType = 1;
    wx.navigateTo({
      url: '/page/ServiceProcess/ServiceProcess'

    })
  },
  btnSubmit_click: function () {
    if (!validatemobile(this.data.phone))
    { return; }
    function validatemobile(mobile) {
      if (mobile.length == 0) {

        wx.showToast({
          title: '请输入手机号！',
          image: '/image/warning.png',
          duration: 1500
        })
        return false;
      }
      if (mobile.length != 11) {
        wx.showToast({
          title: '手机号长度有误！',
          image: '/image/warning.png',
          duration: 1500
        })
        return false;
      }
      var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(17[0-9]{1}))+\d{8})$/;
      if (!myreg.test(mobile)) {
        wx.showToast({
          title: '手机号有误！',
          image: '/image/warning.png',
          duration: 1500
        })
        return false;
      }
      return true;
    }

    if (this.data.userName == '') {
      wx.showToast({
        title: '请填写姓名!',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }
    if (this.data.phone == '') {
      wx.showToast({
        title: '请填写联系方式!',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }
    if (this.data.Addr.length < 4) {
      wx.showToast({
        title: '请填写地址!',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }

    var _this = this
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType:'recycle',
        Data:{
        userName: _this.data.userName,
        phone: _this.data.phone,
        addr: _this.data.district[_this.data.districtIndex] + _this.data.Addr}
      },
      method: 'POST',
      success: function (res) {
        if (res.data == 1) {
         
          wx.request({
            url: getApp().globalData.Url + '/api/Bland',
            data: {
              FunType:'AuthCode',
              Data:{
              push:'64',
              Phone: '18605880752',
              Code: "<手机回收：" + _this.data.userName + _this.data.phone + _this.data.district[_this.data.districtIndex] + _this.data.Addr+'>'}
            },
            method: 'POST',
            success: function (res) {
              console.log(res.data)
              _this.setData({

              });
            },
            fail: function (res) {
              console.log(res.data);
              console.log('is failed')
            }
          })
          getApp().globalData.msg.msgType = 1;
          getApp().globalData.msg.msgTitle = '订单提交成功';
          getApp().globalData.msg.msgInfo = '我们的工作人员将会在10分钟内与您取得联系'
          wx.navigateTo({
            url: '/page/succeed/succeed'
          })
        }
        else if(res.data==999){
          getApp().globalData.msg.msgType = 0;
          getApp().globalData.msg.msgTitle = '订单提交失败';
          getApp().globalData.msg.msgInfo = '一小时内重复提交订单'
          wx.navigateTo({
            url: '/page/succeed/succeed'
          })
        }
        else{
          getApp().globalData.msg.msgType = 0;
          getApp().globalData.msg.msgTitle = '订单提交失败';
          getApp().globalData.msg.msgInfo = '未知错误，请稍后再试。'
          wx.navigateTo({
            url: '/page/succeed/succeed'
          })
        }

      },
      fail: function (res) {
        console.log(res.data);
        console.log('is failed')
      }
    })
  },
  mackCall: function () {
    wx.makePhoneCall({
      phoneNumber: '4008678597',
    })
  }
})