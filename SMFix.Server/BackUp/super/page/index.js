
//var timer = require('../../common/wxTimer.js'); 

Page({
  data: {
    imgUrls: [
      'http://zcdi.cn:808/banner1.jpg',
      'http://zcdi.cn:808/banner2.jpg',
      'http://zcdi.cn:808/banner3.jpg'
    ],
    indicatorDots: false,
    autoplay: false,
    interval: 3000,
    duration: 800,
    orderlist: [],
    systemConfig:null,
    find: {
      LB_screen: {
        LB_text: "屏幕",
        LB_color: "#ff0000",
        LB_image: "/image/screen.png"
      },
      LB_button: {
        LB_text: "按键",
        LB_color: "#ff0000",
        LB_image: "/image/button.png"
      },
      LB_camera: {
        LB_text: "摄像头",
        LB_color: "#ff0000",
        LB_image: "/image/camera.png"
      },
      LB_battery: {
        LB_text: "电池",
        LB_color: "#ff0000",
        LB_image: "/image/battery.png"
      },
      LB_speaker: {
        LB_text: "充电",
        LB_color: "#ff0000",
        LB_image: "/image/plug.png"
      },
      LB_sell: {
        LB_text: "进液",
        LB_color: "#ff0000",
        LB_image: "/image/water.png"
      },
      LB_respond: {
        LB_text: "主板",
        LB_color: "#ff0000",
        LB_image: "/image/chip.png"
      },
      LB_other: {
        LB_text: "其它",
        LB_color: "#ff0000",
        LB_image: "/image/more.png"
      },
      LB_repair: {
        LB_text: "维修",
        LB_color: "#ff0000",
        LB_image: "/image/repair.png"
      },
      LB_recycle: {
        LB_text: "回收",
        LB_color: "#ff0000",
        LB_image: "/image/recycle.png"
      },
      LB_up: {
        LB_text: "扩容",
        LB_color: "#ff0000",
        LB_image: "/image/up.png"
      }
    }
  },
  onLoad: function (options) {
    var _this = this
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      method: 'Post',
      data:{
        FunType:"SystemConfig",
        Data:{}
      },
      success: function (res) {
        getApp().globalData.systemConfig = res.data;
        _this.setData({
          systemConfig: res.data,
        });
      }
    })

    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType: "GetQuery",
        Data:{}
      },
      method: 'POST',
      success: function (res) {
        _this.setData({
          orderlist: res.data,
        });
      }
    })

    countdown(this);
    function countdown(that) {

      var time = setTimeout(function () {

        wx.request({
          url: getApp().globalData.Url + '/api/Bland',
          data: {
            FunType: "GetQuery",
            Data:{}
          },
          method: 'POST',
          success: function (res) {
            that.setData({
              orderlist: res.data,
            });
          },
          fail: function (res) {
            console.log(res.data);
            console.log('is failed')
          }
        })
        countdown(that);
      }
        , 60000)
    }
  },
  onclick: function (e) {



    if (e.target.dataset.name == "主板") {
    
    }
    if (e.target.dataset.name == "其它") {
      if (wx.getStorageSync("LoginSessionKey")) return;
      console.log(wx.getStorageSync("LoginSessionKey"))
      var that = this
      wx.checkSession({
        success: function () {
          console.log("已登录")
          //session 未过期，并且在本生命周期一直有效
        },
        fail: function () {
          console.log("未登录")
          wx.login({
            success: function (res) {

              if (res.code) {
                //发起网络请求
                wx.request({
                  url: getApp().globalData.Url + '/api/Login',
                  data: {
                    code: res.code
                  },
                  method: 'POST',
                  success: function (res) {
                    if (res.statusCode == 200) {
                      wx.setStorageSync("LoginSessionKey", res.data)
                    }
                  },
                  fail: function (res) {
                    console.log(res.data);
                    console.log('is failed')
                  }
                })

              } else {
                console.log('获取用户登录态失败！' + res.errMsg)
              }
            }
          });

        }
      })




    }
    else {
      wx.navigateTo({
        url: '/page/bland/bland'
      })
    }
  },
  doclick: function () {
    wx.getUserInfo({
      success: function (res) {
        var nickName = userInfo.nickName
        return;
      }
    })
  },
  onReclick:function(){
    wx.navigateTo({
      url: '/page/recycle/recycle'
    })
  },
  onmackcall: function () {
    wx.makePhoneCall({
      phoneNumber: '4008678597',
    })
  },
  Up_Click: function () {
    wx.navigateTo({
      url: "/page/RAMFix/RAMFix"
    })

  },
  query_Click: function () {
    wx.navigateTo({
      url: '/page/query/query'
    })
  },
  btnFeedback_Click: function () {
    wx.navigateTo({
      url: "/page/feedback/feedback"
    })
  },
  btnHelp_Click: function () {
    wx.navigateTo({
      url: "/page/about/about"
    })
  },
  QA_Click: function () {
    wx.navigateTo({
      url: "/page/QAndA/QAndA"
    })
  },
  Service_Click: function () {
    getApp().globalData.ProcessType = 0;
    wx.navigateTo({
      url: "/page/ServiceProcess/ServiceProcess"
    })

  }
})