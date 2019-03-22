Page({
  data: {
    blandDisplay: 'block',
    blandMoreLess: '/image/less.png',
    blandisMore: true,
    blandHeight: '200rpx',

    conDisplay: "none",
    moreLessImage: "/image/more_unfold.png",
    isMore: false,
    verHeight: '200rpx',

    colorDisplay: "none",
    colormoreLessImage: "/image/more_unfold.png",
    colorisMore: false,
    colorHeight: '200rpx',

    backColr: '#eee',
    _currVer: "iPhone 6s Plus",
    _currBland: "苹果",
    _currColor: "玫瑰金",
    brand: [],
    macVer: [],
    macColor: [],
    Orders: [],
    macFault: [],
    totalPrice: 0,           // 总价，初始为0
    selectAllStatus: true    // 全选状态，默认全选
  },
  onLoad: function (options) {
    // 页面初始化 options为页面跳转所带来的参数
    var _this = this
    getApp().globalData.order.hasComite = false
    // wx.showLoading({
    //   title: '加载中',
    // })
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      method: "GET",
      success: function (res) {
        console.log('bland', res)
        _this.setData({
          brand: res.data.bland,
          macVer: res.data.ver,
          macColor: res.data.color,
          macFault: res.data.fault,

          blandHeight: res.data.bland.length * 100 + 40 + "rpx",
          verHeight: res.data.ver.length * 100 + 40 + "rpx",
          colorHeight: res.data.color.length * 100 + 40 + "rpx",
        })
        if (res.data.bland.length > 0) {
          _this.setData({
            _currBland: res.data.bland[0].name,
          })

        }
        if (res.data.ver.length > 0) {
          _this.setData({
            _currVer: res.data.ver[0].name,
          })
        }
        if (res.data.color.length > 0) {
          _this.setData({
            _currColor: res.data.color[0].name,
          })
        }
        getApp().globalData.bland = res.data;

      }

    })

    //  setTimeout(function () {
    //   wx.hideLoading()
    // }, 100)
  },
  onReady: function () {
    // 页面渲染完成
  },
  onShow: function () {
    // 页面显示
  },
  onHide: function () {
    // 页面隐藏
  },
  onUnload: function () {
    // 页面关闭
  },
  onPullDownRefresh: function () {
    // Do something when pull down.
  },
  //更多品牌
  onOtherBland: function (e) {
    var _this = this
    if (!_this.data.blandisMore) {
      _this.setData({
        blandDisplay: "block",
        blandMoreLess: "/image/less.png",
        blandisMore: true,
      })
    }
    else {
      _this.setData({
        blandDisplay: "none",
        blandMoreLess: "/image/more_unfold.png",
        blandisMore: false
      })
    }
  },
  //更多型号
  onOtherMacVer: function (e) {
    var _this = this
    if (!_this.data.isMore) {
      _this.setData({
        conDisplay: "block",
        moreLessImage: "/image/less.png",
        isMore: true
      })
    }
    else {
      _this.setData({
        conDisplay: "none",
        moreLessImage: "/image/more_unfold.png",
        isMore: false
      })
    }
  },
  //更多颜色
  onOtherColor: function (e) {
    var _this = this
    if (!_this.data.colorisMore) {
      _this.setData({
        colorDisplay: "block",
        colormoreLessImage: "/image/less.png",
        colorisMore: true
      })
    }
    else {
      _this.setData({
        colorDisplay: "none",
        colormoreLessImage: "/image/more_unfold.png",
        colorisMore: false
      })
    }
  },
  onThisBland: function () {
    var _this = this
    _this.setData({
      blandDisplay: "none",
      blandMoreLess: "/image/more_unfold.png",
      blandisMore: false

    })
  },
  onblandSelected: function (e) {
    var _this = this
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType: "Bland",
        Data: {
          name: e.currentTarget.dataset.name
        }
      },
      method: 'POST',
      success: function (res) {

        _this.setData({
          macFault: [],
          _currBrand: e.currentTarget.dataset.name,
          macVer: res.data,
          _currVer: res.data[0].name,
          conHeight: res.data.length * 100 + 40 + "rpx",
          _currBland: e.currentTarget.dataset.name,

          conDisplay: "block",
          moreLessImage: "/image/less.png",
          isMore: true,
          blandDisplay: "none",
          blandMoreLess: "/image/more_unfold.png",
          blandisMore: false
        });
      },
      fail: function (res) {
        console.log(res.data);
        console.log('is failed')
      }
    })

  },
  onMacVerSelected: function (e) {
    var _this = this

    //取颜色
    wx.request({

      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType:"Colors",
        Data:{
        bland: _this.data._currBland,
        ver: e.currentTarget.dataset.name
        }
      },
      method: 'POST',
      success: function (res) {
        _this.setData({
          macFault: [],
          macColor: res.data,

          colorHeight: res.data.length * 100 + 40 + "rpx",
          _currColor: res.data[0].name,

          colorDisplay: "block",
          colormoreLessImage: "/image/less.png",
          colorisMore: true,

          conDisplay: "none",
          moreLessImage: "/image/more_unfold.png",
          isMore: false
        });
      },
      fail: function (res) {
        console.log(res.data);
        console.log('is failed')
      }
    })

    _this.setData({
      _currVer: e.currentTarget.dataset.name
    })
  },
  onMacColorSelected: function (e) {
    var _this = this
    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType:"Fault",
        Data:{
        bland: _this.data._currBland,
        ver: _this.data._currVer,
        color: _this.data._currColor}
      },
      method: 'POST',
      success: function (res) {
        _this.setData({
          totalPrice: 0,
          macFault: res.data,

          colorDisplay: "none",
          colormoreLessImage: "/image/more_unfold.png",
          colorisMore: false
        });
      },
      fail: function (res) {
        console.log(res.data);
        console.log('is failed')
      }
    })

    _this.setData({

      _currColor: e.currentTarget.dataset.name
    })
  },
  OnFaultSelected: function (e) {
    var _this = this
    var macFault = _this.data.macFault
    var price = 0
    var orders = []

    for (var k = 0, length = macFault.length; k < length; k++) {
      if (macFault[k].name == e.currentTarget.dataset.name) {
        _this.data.macFault[k].isSelect = !_this.data.macFault[k].isSelect
        price = parseInt(macFault[k].price)
        if (isNaN(price)) {
          price = 0
        }
        if (!_this.data.macFault[k].isSelect) {
          price = 0 - price

        }
        else {

        }
      }
      if (_this.data.macFault[k].isSelect) {
        var order = new Object()
        order.Bland = _this.data._currBland
        order.Ver = _this.data._currVer
        order.Color = _this.data._currColor
        order.Fault = macFault[k].name
        order.Price = macFault[k].price
        orders.push(order)
      }
    }
    _this.setData({
      macFault: _this.data.macFault,
      totalPrice: parseInt(_this.data.totalPrice) + parseInt(price)
    });
    getApp().globalData.Orders = orders
  },
  btnNext_click: function (e) {

    if (getApp().globalData.Orders.length == 0) {
      wx.showToast({
        title: '请选择故障!',
        image: '/image/warning.png',
        duration: 1000
      })
    }
    else {

      getApp().globalData.doorType = '2'
      wx.navigateTo({
        url: '/page/order/order'
      })
    }
  }
})