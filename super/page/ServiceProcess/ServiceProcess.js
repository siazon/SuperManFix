// ServiceProcess.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    ProType:null,
    process: [
      {
        count:[1,1,1,1],
        height: '200rpx',
        index: 1,
        caption: '用户预约下单',
        content: '用户通过超人修微信公众号、官网、小程序、电话等方式进行预约下单。'
      }, {
        height: '200rpx',
        count: [1, 1, 1, 1],
        index: 2,
        caption: '客服确认信息',
        content: '客服通过电话与用户确认手机故障以及用户方便安排上门的时间地点。'
      }, {
        height: '200rpx',
        count: [1, 1, 1, 1],
        index: 3,
        caption: '派单给相应的维修工程师',
        content: '确认订单后，我们会将您的订单匹配给您附近的工程师'
      }, {
        height: '240rpx',
        count: [1, 1, 1, 1],
        index: 4,
        caption: '程师上门维修',
        content: '我们的维修工程师接到订单后，将会在您指定的时间上门到您指定的地方为您进行维修服务。'
      }, {
        height: '200rpx',
        count: [1, 1, 1],
        index: 5,
        caption: '维修完毕付款',
        content: '完成维修后，在维修工程师的指引下进行线上支付（支持 :微信、支付宝扫码付款），告别离开完成维修。'
      }
    ],
    process1: [
      {
        height: '340rpx',
        count: [1, 1, 1, 1,1,1],
        index: 1,
        caption: '用户前往超人修维修体验店',
        content: '超人修维修体验店位于杭州市拱墅区丰登街373-1号（距离城西银泰丰潭路南大门不到100米），门店100米内有大型公共停车场，方便停车；公交站：政苑小区。'
      }, {
        height: '260rpx',
        count: [1, 1, 1, 1],
        index: 2,
        caption: '现场互动式维修',
        content: '超人修设计的互动式维修，维修台无隔挡、维修全过程就在您眼前且可以十分舒适的和维修工程师沟通互动。'
      }, {
        height: '200rpx',
        count: [1, 1],
        index: 3,
        caption: '维修完毕付款',
        content: '完成维修后，程师的指引下进行线上支付（支持 :微信支付宝扫码付款）。'
      },
    ],
    process2: [
      {
        height: '240rpx',
        count: [1, 1, 1, 1],
        index: 1,
        caption: '在线预约下单',
        content: '用户通过超人修微信公众号、官网、微信小程序、电话等方式进行预约下单。'
      }, {
        height: '330rpx',
        count: [1, 1, 1,1, 1,1],
        index: 2,
        caption: '联系顺丰快递上门取件',
        content: '用户联系顺丰取件将设备寄出，邮费由超人修到付。超人修维修总部位于—-杭州市西湖区文二路391号西湖国际科技大厦D南二楼星沅空间超人修（4008-678-597 )'
      }, {
        height: '240rpx',
        count: [1,1,1, 1],
        index: 3,
        caption: '接收设备并致电报价',
        content: '超人修收到设备后，工程师进行检测，客服致电用户沟通维修方案与准确报价。'
      }, {
        height: '240rpx',
        count: [1,1,1, 1],
        index: 4,
        caption: '监控环境下开始维修',
        content: '换排线屏幕等更换零配件可修复的故障一般当天修好寄出，主板维修内存升级24小时内修好寄出。'
      }, {
        height: '240rpx',
        count: [1,1,1, 1],
        index: 5,
        caption: '维修完毕用户付款',
        content: '维修完成后，用户会收到转账支付账户，用户可以使用支付宝或微信线上付款。'
      }, {
        height: '240rpx',
        count: [1, 1],
        index: 6,
        caption: '顺丰包邮寄回',
        content: '用户支付完成后，超人修会通过顺丰包邮寄回设备。'
      },
    ],
    processRe: [
      {
        count: [1, 1],
        height: '150rpx',
        index: 1,
        caption: '用户预约下单',
        content: '填写联系方式后一键下单回收'
      }, {
        height: '200rpx',
        count: [1, 1, 1, 1],
        index: 2,
        caption: '客服确认信息',
        content: '10分钟内我们的相关人员将致电与您沟通对您的手机价值进行评估'
      }, {
        height: '150rpx',
        count: [1],
        index: 3,
        caption: '派单给相应的维修工程师',
        content: '安排上门回收，当面给钱。'
      }
      ]
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.setData({
      ProType: getApp().globalData.ProcessType
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

  }
})