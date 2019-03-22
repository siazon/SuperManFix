// userinfo.js
var address = require('../../util/city.js')
var animation
var WxParse = require('../../page/wxParse/wxParse.js');
Page({

  /**
   * 页面的初始数据
   */
  data: {

    servers: ['上门维修', '到店维修', '邮寄维修'],
    index: 0,

    servicecity: ['杭州市'],
    cityIndex: 0,

    district: ['上城区', '下城区', '江干区', '拱墅区', '西湖区', '滨江区', '萧山区', '余杭区'],
    districtIndex: 0,
    //邮寄地址
    postAddrDatas: [],
    postaddrs: [],
    postaddrIndex: 0,
    postaddr: '',
    postBackAddr: '',
    //到店地址
    shopAddrData: [],
    shopAddrs: [],
    shopaddrindex: 0,
    shopaddr: '',
    selectAddr: null,
    date: '', //服务日期
    time: '', //服务时间
    Addr: '', //表单地址
    Phone: '', //表单手机
    Code: '', //验证码
    AuthCode: '', //验证码
    UserName: '', //表单姓名
    PostAddres: '', //表单邮寄地址
    Remark: '', //表单备注
    backPostAddr: '',
    CodeCaption: '获取验证码',
    second: 59,
    doorimg: '/image/door1.png',
    postimg: '/image/post0.png',
    shopimg: '/image/shop0.png',
    isdoor: true,
    ispost: false,
    isshop: false,

    showMap: false,
    latitude: null,
    longitude: null,
    markers: [],
    //地址选择控件
    animationAddressMenu: {},
    addressMenuIsShow: false,
    value: [0, 0, 0],
    provinces: [],
    citys: [],
    areas: [],
    province: '',
    city: '',
    area: '',
    areaInfo: '',
    doorFix: '上门'
    //地址选择控件
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function(options) {

    var _this = this
    var temo = getApp().globalData.doorType
    if (temo == "1") {
      this.setData({
        doorFix: '上门取件',
        isdoor: false,
        ispost: false,
        isshop: true,
        doorimg: '/image/door0.png',
        postimg: '/image/post0.png',
        shopimg: '/image/shop1.png',

      })
      getMap();
    } else {
      this.setData({
        doorFix: '上门',
      })
    }
    var animation = wx.createAnimation({
      duration: 500,
      transformOrigin: "50% 50%",
      timingFunction: 'ease',
    })
    this.animation = animation;
    // 默认联动显示北京
    var id = address.provinces[0].id
    this.setData({
      provinces: address.provinces,
      citys: address.citys[id],
      areas: address.areas[address.citys[id][0].id],
    })
    wx.request({
      url: getApp().globalData.Url + 'api/Bland',
        method: "POST",
        data:{
          FunType:"addrType",
          Data: { addrType:1}
        },
        success: function(res) {
          var names = new Array(res.data);
          for (var i = 0; i < res.data.length; i++) {
            names[i] = res.data[i].name
          }
          _this.setData({
            postaddrs: names,
            postAddrDatas: res.data,
            postaddr: res.data[0].addr + ' ' + res.data[0].postName
          })

        }

      }),
      this.setData({
        // date: new Date().toLocaleDateString(),
        //time: new Date().toLocaleTimeString(),
      })

    function getMap() {
      wx.request({
        url: getApp().globalData.Url + 'api/Bland',
        method: "POST",
        data: {
          FunType: "addrType",
          Data: { addrType: 2 }
        },
        success: function(res) {
          var names = new Array(res.data);

          for (var i = 0; i < res.data.length; i++) {
            names[i] = res.data[i].name
          }
          _this.setData({
            selectAddr: res.data[0],
            shopAddrs: names,
            shopAddrData: res.data,
            shopAddr: res.data[0].addr,
            latitude: parseFloat(res.data[0].latitude),
            longitude: parseFloat(res.data[0].longitude),
            markers: [{
              latitude: parseFloat(res.data[0].latitude),
              longitude: parseFloat(res.data[0].longitude),
              callout: {
                content: res.data[0].name,
                color: "#ff0000",
                fontSize: "16",
                borderRadius: "5",
                bgColor: "#ffffff",
                padding: "10",
                display: "ALWAYS"
              }
            }],
            showMap: true
          })

        }

      })
    }
  },
  selectDistrict: function(e) {
    var that = this
    if (that.data.addressMenuIsShow) {
      return
    }
    that.startAddressAnimation(true)
  },
  // 执行动画
  startAddressAnimation: function(isShow) {
    console.log(isShow)
    var that = this
    if (isShow) {
      that.animation.translateY(0 + 'vh').step()
    } else {
      that.animation.translateY(40 + 'vh').step()
    }
    that.setData({
      animationAddressMenu: that.animation.export(),
      addressMenuIsShow: isShow,
    })
  },
  mapclick: function(e) {
    var _this = this
    console.log(_this.data.selectAddr)
    wx.openLocation({
      latitude: parseFloat(_this.data.selectAddr.latitude),
      longitude: parseFloat(_this.data.selectAddr.longitude),
      scale: 18,
      name: _this.data.selectAddr.name,
      address: _this.data.selectAddr.addr
    })
  },
  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function() {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function() {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function() {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function() {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function() {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function() {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function() {

  },

  // 点击地区选择取消按钮
  cityCancel: function(e) {
    this.startAddressAnimation(false)
  },
  // 点击地区选择确定按钮
  citySure: function(e) {
    var that = this
    var city = that.data.city
    var value = that.data.value
    that.startAddressAnimation(false)
    // 将选择的城市信息显示到输入框
    var areaInfo = that.data.provinces[value[0]].name + ' ' + that.data.citys[value[1]].name + ' ' + that.data.areas[value[2]].name
    that.setData({
      areaInfo: areaInfo,
    })
  },
  hideCitySelected: function(e) {
    console.log(e)
    this.startAddressAnimation(false)
  },
  bindServerChange: function(e) {
    this.setData({
      index: e.detail.value
    })
  },
  bindcityChange: function(e) {
    this.setData({
      cityIndex: e.detail.value
    })
  },
  // 处理省市县联动逻辑
  cityChange: function(e) {
    console.log(e)
    var value = e.detail.value
    var provinces = this.data.provinces
    var citys = this.data.citys
    var areas = this.data.areas
    var provinceNum = value[0]
    var cityNum = value[1]
    var countyNum = value[2]
    if (this.data.value[0] != provinceNum) {
      var id = provinces[provinceNum].id
      this.setData({
        value: [provinceNum, 0, 0],
        citys: address.citys[id],
        areas: address.areas[address.citys[id][0].id],
      })
    } else if (this.data.value[1] != cityNum) {
      var id = citys[cityNum].id
      this.setData({
        value: [provinceNum, cityNum, 0],
        areas: address.areas[citys[cityNum].id],
      })
    } else {
      this.setData({
        value: [provinceNum, cityNum, countyNum]
      })
    }
    console.log(this.data)
  },
  bindDisChange: function(e) {
    this.setData({
      districtIndex: e.detail.value
    })
  },
  bindDateChange: function(e) {
    this.setData({
      date: e.detail.value
    })
  },
  bindTimeChange: function(e) {
    this.setData({
      time: e.detail.value
    })
  },
  bindaddrnameChange: function(e) {
    var _this = this
    var add;
    for (var i = 0; i < _this.data.postAddrDatas.length; i++) {
      if (i == e.detail.value) {
        add = _this.data.postAddrDatas[i];
      }
    }
    this.setData({
      postaddrIndex: e.detail.value,
      postaddr: add.addr + ' ' + add.postName,
    })
  },
  bindshopaddrChange: function(e) {
    var _this = this
    var add;
    var addr;
    for (var i = 0; i < _this.data.shopAddrData.length; i++) {
      if (i == e.detail.value) {
        addr = _this.data.shopAddrData[i]
        add = _this.data.shopAddrData[i].addr
      }
    }
    this.setData({
      shopaddrindex: e.detail.value,
      shopaddr: add,
      selectAddr: addr,
      shopAddr: addr.addr,
      latitude: parseFloat(addr.latitude),
      longitude: parseFloat(addr.longitude),
      markers: [{
        latitude: parseFloat(addr.latitude),
        longitude: parseFloat(addr.longitude),
        callout: {
          content: addr.name,
          color: "#ff0000",
          fontSize: "16",
          borderRadius: "5",
          bgColor: "#ffffff",
          padding: "10",
          display: "ALWAYS"
        }
      }],
    })

  },
  UserName_input: function(e) {
    this.setData({
      UserName: e.detail.value
    })
  },
  phone_input: function(e) {
    if (e.detail.value.length == 11) {
      this.setData({
        Phone: e.detail.value
      })
    }
  },
  Code_input: function(e) {
    this.setData({
      Code: e.detail.value
    })
  },
  Addr_change: function(e) {
    this.setData({
      Addr: e.detail.value
    })
  },
  Remark_change: function(e) {
    this.setData({
      Remark: e.detail.value
    })
  },
  backaddrchange: function(e) {
    this.setData({
      postBackAddr: this.data.areaInfo + e.detail.value
    })
    console.log(this.data.areaInfo)
  },
  doorclick: function(e) {
    this.setData({
      isdoor: true,
      ispost: false,
      isshop: false,
      doorimg: '/image/door1.png',
      postimg: '/image/post0.png',
      shopimg: '/image/shop0.png',
    })
  },
  postclick: function(e) {
    this.setData({
      isdoor: false,
      ispost: true,
      isshop: false,
      doorimg: '/image/door0.png',
      postimg: '/image/post1.png',
      shopimg: '/image/shop0.png',
    })
  },
  shopclick: function(e) {
    var _this = this
    this.setData({
      isdoor: false,
      ispost: false,
      isshop: true,
      doorimg: '/image/door0.png',
      postimg: '/image/post0.png',
      shopimg: '/image/shop1.png',
    })
    wx.request({
      url: getApp().globalData.Url + 'api/Bland',
      method: "POST",
      data: {
        FunType: "addrType",
        Data: { addrType: 2 }
      },
      success: function(res) {
        var names = new Array(res.data);

        for (var i = 0; i < res.data.length; i++) {
          names[i] = res.data[i].name
        }
        _this.setData({
          selectAddr: res.data[0],
          shopAddrs: names,
          shopAddrData: res.data,
          shopAddr: res.data[0].addr,
          latitude: parseFloat(res.data[0].latitude),
          longitude: parseFloat(res.data[0].longitude),
          markers: [{
            latitude: parseFloat(res.data[0].latitude),
            longitude: parseFloat(res.data[0].longitude),
            callout: {
              content: res.data[0].name,
              color: "#ff0000",
              fontSize: "16",
              borderRadius: "5",
              bgColor: "#ffffff",
              padding: "10",
              display: "ALWAYS"
            }
          }],
          showMap: true
        })

      }

    })
  },

  GetCode: function() {
    var _this = this
    if (this.data.CodeCaption != '获取验证码') {
      return;
    }
    if (!validatemobile(this.data.Phone)) {
      return;
    }

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
      if (!myreg.test(mobile.replace(/\s+/g, ""))) {
        wx.showToast({
          title: '手机号有误！',
          image: '/image/warning.png',
          duration: 1500
        })
        return false;
      }
      return true;
    }

    _this.setData({
      CodeCaption: "重新发送(60)",
      second: 59,
      AuthCode: GetCode()
    });


    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType:"AuthCode",
        Data:{
        push: 'custmer',
        Phone: this.data.Phone,
        Code: this.data.AuthCode}
      },
      method: 'POST',
      success: function(res) {
        console.log(res.data)
        _this.setData({

        });
      },
      fail: function(res) {
        console.log(res.data);
        console.log('is failed')
      }
    })

    countdown(this);

    function countdown(that) {
      var second = that.data.second

      if (second == 0) {
        that.setData({
          CodeCaption: "获取验证码"
        });
        return;
      }
      var time = setTimeout(function() {
        that.setData({
          second: second - 1,
          CodeCaption: "重新发送(" + that.data.second + ")"
        });
        countdown(that);
      }, 1000)
    }



    function GetCode() {
      var Code = '';
      for (var i = 0; i < 4; i++) {
        Code += GetRandomNum(0, 9);
      }
      return Code;
    }

    function GetRandomNum(Min, Max) {
      var Range = Max - Min;
      var Rand = Math.random();
      return (Min + Math.round(Rand * Range));
    }
  },
  GetRandomNum: function(Min, Max) {
    var Range = Max - Min;
    var Rand = Math.random();
    return (Min + Math.round(Rand * Range));
  },
  formSubmit: function(e) {

    this.setData({
      UserName: e.detail.value.UserName,
      Phone: e.detail.value.Phone,
      Code: e.detail.value.Code,
      Remark: e.detail.value.Remark
    })
    if (this.data.isdoor) {
      this.setData({
        Addr: e.detail.value.addrDetail
      })
    }
    if (this.data.ispost) {
      this.setData({
        Addr: this.data.postAddrDatas[this.data.postaddrIndex].addr,
        backPostAddr: this.data.areaInfo + e.detail.value.backPostAddr,
      })
    }
    if (this.data.isshop) {
      this.setData({
        Addr: "到店店址：" + this.data.shopAddrData[this.data.shopaddrindex].addr,
      })
    }

  },
  btnSubmit_click: function(e) {
    var _this = this
    if (getApp().globalData.order.hasComite != undefined && getApp().globalData.order.hasComite == true) {
      wx.showToast({
        title: '请勿重复提交',
        image: '/image/warning.png',
        duration: 2000
      })
      return;
    }
    if (this.data.isdoor && this.data.date == '') {
      wx.showToast({
        title: '请选择服务日期',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }
    if (this.data.isdoor && this.data.time == '') {
      wx.showToast({
        title: '请选择服务时间',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }
    if (this.data.Code != this.data.AuthCode) {
      wx.showToast({
        title: '手机验证码有误',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }
    if (this.data.AuthCode == '') {
      wx.showToast({
        title: '验证码不能为空!',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }
    if (this.data.UserName == '') {
      wx.showToast({
        title: '姓名有误!',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }
    if (this.data.Phone == '') {
      wx.showToast({
        title: '手机号有误!',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }
    if (this.data.Addr == '') {
      wx.showToast({
        title: '地址不完整!',
        image: '/image/warning.png',
        duration: 2000
      })
      return
    }

    if (this.data.isdoor) {

      getApp().globalData.order.ServerType = "上门维修"
      getApp().globalData.order.Addr = this.data.servicecity[this.data.cityIndex] + this.data.district[this.data.districtIndex] + this.data.Addr

      getApp().globalData.order.Time = this.data.date + ' ' + this.data.time
    }
    if (this.data.ispost) {
      getApp().globalData.order.ServerType = "邮寄维修"
      getApp().globalData.order.Addr = this.data.backPostAddr

      getApp().globalData.order.Time = ''
    }
    if (this.data.isshop) {
      getApp().globalData.order.ServerType = "到店维修"
      getApp().globalData.order.Addr = this.data.Addr
      getApp().globalData.order.Time = this.data.date + ' ' + this.data.time
    }

    getApp().globalData.order.Phone = this.data.Phone
    getApp().globalData.order.UserName = this.data.UserName

    getApp().globalData.order.Bland = ''
    getApp().globalData.order.Ver = ''
    getApp().globalData.order.Color = ''
    getApp().globalData.order.Fault = ''
    getApp().globalData.order.Amount = 0
    getApp().globalData.order.Remark = ''
    getApp().globalData.order.Fault = ''
    getApp().globalData.order.Amount = ''
    if (getApp().globalData.Orders.length > 0) {
      getApp().globalData.order.Bland = getApp().globalData.Orders[0].Bland
      getApp().globalData.order.Ver = getApp().globalData.Orders[0].Ver
      getApp().globalData.order.Color = getApp().globalData.Orders[0].Color
      getApp().globalData.order.Fault = ''
      getApp().globalData.order.Amount = 0

      getApp().globalData.order.Remark = this.data.Remark
      for (var i = 0; i < getApp().globalData.Orders.length; i++) {
        getApp().globalData.order.Fault = getApp().globalData.order.Fault + getApp().globalData.Orders[i].Fault + ','
        getApp().globalData.order.Amount = parseInt(getApp().globalData.order.Amount) + parseInt(getApp().globalData.Orders[i].Price)

      }
    }
    var datas = getApp().globalData.order
    console.log(datas)

    wx.request({
      url: getApp().globalData.Url + '/api/Bland',
      data: {
        FunType:"Submit",
        Data:{
        datas
        }
      },
      method: 'POST',
      success: function(res) {
        getApp().globalData.OrderNO = res.data
        var serverType = '';
        var push = '';
        var userType = getApp().globalData.doorType;

        if (_this.data.isdoor) {
          serverType = '<上门：';
          if (userType == '1')
            push = '1';
          else
            push = '8';
        }
        if (_this.data.ispost) {
          serverType = '<邮寄：';
          if (userType == '1')
            push = '2';
          else
            push = '16';
        }
        if (_this.data.isshop) {
          serverType = '<到店：';
          if (userType == '1')
            push = '4';
          else
            push = '32';
        }

        getApp().globalData.order.hasComite = true;
        wx.request({
          url: getApp().globalData.Url + '/api/Bland',
          data: {
            FunType:"AuthCode",
            Data:{
            push: push,
            Phone: '18605880752',
            Code: serverType + getApp().globalData.order.UserName + getApp().globalData.order.Phone + '>'
          }},
          method: 'POST',
          success: function(res) {
            _this.setData({

            });
          },
          fail: function(res) {
            console.log(res.data);
            console.log('is failed')
          }
        })

      },
      fail: function(res) {
        console.log(res.data);
        console.log('is failed')
      }
    })
    wx.showToast({
      title: '提交成功，请保持手机通畅!',
      image: '/image/warning.png',
      duration: 1000
    })
    wx.navigateTo({
      url: '/page/succeed/succeed'
    })
  }
})