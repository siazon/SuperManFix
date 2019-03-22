// about.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    pwd: ""
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this
    if (wx.getStorageSync('userInfo')) {
      var user = wx.getStorageSync('userInfo');
      that.chkLogin('0', user.openId, user.nickName);
    }

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
  pwd_input: function (e) {
    this.setData({
      pwd: e.detail.value
    })
  },
  chkLogin(OPType, openId, nickName) {
    if (openId == undefined)
      return
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType: "ChkLogin",
        Data: {
          OPType: OPType,
          openId: openId,
          nickName: nickName
        }
      },
      method: 'POST',
      success: function (result) {
        if (OPType == '0' && result.data == 1) {
          wx.navigateTo({
            url: '/page/recyclePro/recyclePro'
          })
        }
        if (OPType == '1' && result.data == 1) {
          wx.navigateTo({
            url: '/page/recyclePro/recyclePro'
          })
        }

      }
    })
  },
  onGotUserInfo: function (e) {
    var that = this;
    var pws = 'null';
    for (var i = 0; i < getApp().globalData.systemConfig.length; i++) {
      if (getApp().globalData.systemConfig[i].code == 'pwd')
        pws = getApp().globalData.systemConfig[i].value
    }
    if (pws == 'null' || that.data.pwd != pws) {
      wx.showToast({
        title: '口令错误!',
        image: '/image/warning.png',
        duration: 2000
      })
      return;
    }
    that.DoLogin(e.detail);
  },
  DoLogin(userRes) {
    var that = this;
    wx.login({
      success: function (res) {
        wx.getSetting({
          success(setRes) {
            // 判断是否已授权  
            if (!setRes.authSetting['scope.userInfo']) {
              // 授权访问  
              wx.authorize({
                scope: 'scope.userInfo',
                success(e) {
                  console.log('scope', e)
                  //发起网络请求  
                  wx.request({
                    url: getApp().globalData.Url + '/api/Bland',
                    data: {
                      FunType:'Login',
                      Data:{
                      code: res.code,
                      encryptedData: userRes.encryptedData,
                      iv: userRes.iv,
                      rawData: userRes.rawData,
                      signature: userRes.signature,
                      authCode: that.data.pwd,
                      OptType: 1}
                    },
                    method: 'POST',
                    //服务端的回掉  
                    success: function (result) {
                      console.log('lk', result.data)
                      that.chkLogin('0', result.data.openId, result.data.nickName);
                      wx.setStorageSync("userInfo", result.data);
                    }
                  })
                },
                fail: function (res) {
                  console.log(res);
                  console.log('authorizeis failed')
                }

              })
            } else {
              //发起网络请求  
              wx.request({
                url: getApp().globalData.Url + '/api/Bland',
                data: {
                  FunType:'Login',
                  Data:{
                  code: res.code,
                  encryptedData: userRes.encryptedData,
                  iv: userRes.iv,
                  rawData: userRes.rawData,
                  signature: userRes.signature,
                  authCode: that.data.pwd,
                  OptType: 0}
                },
                method: 'POST',
                success: function (result) {
                  if (result.data == 'invalid')
                    return;
                  that.chkLogin('0', result.data.openId, result.data.nickName);
                  wx.setStorageSync("userInfo", result.data);
                }
              })
            }
          }
        })
      }
    })
  },
  btn_click: function () {
    var that = this
    var pws = null;
    for (var i = 0; i < getApp().globalData.systemConfig.length; i++) {
      if (getApp().globalData.systemConfig[i].code == 'pwd')
        pws = getApp().globalData.systemConfig.value
    }
    console.log(pws, '+', that.data.pwd)
    if (pws == null || that.data.pwd != pws)
      return;
    else {
      console.log(pws, '+', that.data.pwd)
    }
    var user = wx.getStorageSync('userInfo');
    that.chkLogin('1', user.openId, user.nickName);

  },

  btnmsg_click: function () {
    var userInfo = wx.getStorageSync("userInfo");
    wx.request({
      url: 'https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token=' + userInfo.accessToken,
      data: {
        "touser": userInfo.openId,
        "template_id": "dm-x-0nKm8j1Fe75QEpC3LY2ikxKWwqX6I75O56XdEc",
        "page": "index",
        "form_id": "test1",
        "data": {
          "keyword1": {
            "value": "换屏幕",
            "color": "#173177"
          },
          "keyword2": {
            "value": "上门维修",
            "color": "#173177"
          },
          "keyword3": {
            "value": "黄先生",
            "color": "#173177"
          },
          "keyword4": {
            "value": "13928459050",
            "color": "#173177"
          },
          "keyword4": {
            "value": "2018-2-13 14:14:14",
            "color": "#173177"
          },
          "keyword4": {
            "value": "深圳南山常兴新村",
            "color": "#173177"
          },
          "keyword4": {
            "value": "暂无",
            "color": "#173177"
          }
        },
        "emphasis_keyword": "keyword1.DATA"
      },
      method: 'POST',
      success: function (result) {

        console.log("success")
        console.log(result)

      }
    })
  }
})