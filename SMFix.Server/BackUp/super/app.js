App({
  onLaunch: function () {
    console.log('App Launch')
  },
  onShow: function () {
    console.log('App Show')
  },
  onHide: function () {
    console.log('App Hide')
  },
  globalData: {
    //Url: 'https://zcdi.cn/',
    //Url: 'http://localhost:52435/',
    Url: 'http://localhost:62981/',
    hasLogin: false,
    bland:[],
    Orders:[],
    order:new Object(),
    OrderNO:'',
    doorType:'',
    systemConfig:[],
    ServiceType:0,
    ProcessType:0,
    msg:{
      msgType: '1',
      msgTitle: '订单提交成功',
      msgInfo: '我们的工作人员将会尽快与您联系确认订单安排维修，请保持通讯畅通。'
    }
  }
})
