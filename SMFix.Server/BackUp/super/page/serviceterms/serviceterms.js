// serviceterms.js

var WxParse = require('../../page/wxParse/wxParse.js');
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
    console.log('aa', getApp().globalData.ServiceType)
    var that = this;
    var article = `<div class="contractContentWrap">
	    <div class="contractContent" style="line-height:35px;">
	        <p style="text-align:center"><span style="font-weight:bold;">超人修服务条款</span></p>
			<p><span style="font-weight:bold;">【导言】</span></p>
<p><span>首先欢迎您选择超人修！</span></p>
<p><span>超人修在此特别提醒您在预约维修服务之前，请认真阅读本《服务协议》（以下简称“协议”），确保您充分理解本协议中各条款。请您审慎阅读并选择接受或不接受本协议。除非您接受本协议所有条款，否则您无权使用本协议所涉服务。您的预约、查询、使用等行为将视为对本协议的接受，并同意接受本协议各项条款的约束。如果您未满18周岁，请在法定监护人的陪同下阅读本协议。</span></p>
<p><span>一、【协议范围】</span></p>
<p><span>1.1&nbsp;协议适用主体范围</span></p>
<p><span>本协议是您与超人修之间的协议。“超人修”是指官网及其相关服务可能存在的运营关联单位。“用户”是指使用超人修相关服务的使用人，在本协议中更多地称为“您”。</span></p><p><span>1.2&nbsp;本许可协议指向内容</span></p>
<p><span>本协议项下的许可内容是指超人修向用户提供的包括但不限于移动设备的维修服务（以下简称“服务”）。</span></p>
<p><span>1.3&nbsp;协议关系及冲突条款</span></p>
<p><span>本协议可由超人修随时更新，更新后的协议条款一旦公布即代替原来的协议条款，恕不再另行通知，用户可在本网站查阅最新版协议条款。在超人修修改协议条款后，如果用户不接受请立即停止使用服务，继续使用，则被视为接受修改后的协议。</span></p>
<p><span>二、【个人隐私信息保护】</span></p>
<p><span>2.1&nbsp;用户在使用本服务的过程中，可能需要填写或提交一些必要的信息，如法律法规、规章规范性文件（以下称“法律法规”）规定的需要填写的身份信息。如用户提交的信息不完整或不符合法律法规的规定，&nbsp;则用户可能无法使用本服务或在使用本服务的过程中受到限制。</span></p>
<p><span>2.2&nbsp;个人隐私信息是指涉及用户个人身份或个人隐私的信息，比如，用户真实姓名、手机号码、手机设备识别码、详细地址等等。非个人隐私信息是指用户对本服务的操作状态以及使用习惯等明确且客观反映在超人修服务器端的基本记录信息、个人隐私信息范围外的其它普通信息，以及用户同意公开的上述隐私信息。</span></p>
<p><span>2.3&nbsp;尊重用户个人隐私信息的私有性是超人修的一贯制度，超人修将采取技术措施和其他必要措施，确保用户个人隐私信息安全，防止在本服务中收集的用户个人隐私信息泄露、毁损或丢失。在发生前述情形或者超人修发现存在发生前述情形的可能时，将及时采取补救措施。</span></p>
<p><span>2.4&nbsp;超人修未经用户同意不向任何第三方公开、&nbsp;透露用户个人隐私信息。但以下特定情形除外：</span></p>
<p><span>(1)&nbsp;超人修根据法律法规规定或有权机关的指示提供用户的个人隐私信息；&nbsp;</span></p>
<p><span>(2)&nbsp;由于用户将其个人隐私信息告知他人或与他人共享个人隐私信息，由此导致的任何个人信息的泄漏，或其他非因超人修原因导致的个人隐私信息的泄露；&nbsp;</span></p>
<p><span>(3)&nbsp;用户自行向第三方公开其个人隐私信息；&nbsp;</span></p>
<p><span>(4)&nbsp;用户与超人修及合作维修单位之间就用户个人隐私信息的使用公开达成约定，超人修因此向合作维修单位公开用户个人隐私信息；&nbsp;</span></p>
<p><span>(5)&nbsp;任何由于黑客攻击、电脑病毒侵入及其他不可抗力事件导致用户个人隐私信息的泄露。</span></p>
<p><span>2.5&nbsp;用户同意超人修可在以下事项中使用用户的个人隐私信息：</span></p>
<p><span>(1)&nbsp;超人修向用户及时发送重要通知，如个人服务信息、本协议条款的变更；&nbsp;
</span></p><p><span>(2)&nbsp;超人修内部进行审计、数据分析和研究等，以改进超人修的产品、服务和与用户之间的沟通；</span></p>
<p><span>(3)&nbsp;依本协议约定，超人修管理、审查用户信息及进行处理措施；&nbsp;</span></p>
<p><span>(4)&nbsp;适用法律法规规定的其他事项。&nbsp;</span></p>
<p><span>2.6&nbsp;用户确认，其地理位置包含的省份、直辖市、自治区信息（以下简称“地理位置信息”）为非个人隐私信息，用户成功预约超人修手机维修服务视为确认授权超人修提取、公开及使用用户的地理位置信息。用户地理位置信息将作为用户公开资料之一，由超人修向其他用户公开。如用户需要终止向其他用户公开其地理位置信息，可随时向超人修说明。</span></p>
<p><span>2.7&nbsp;为了改善超人修的服务，向用户提供更好的服务体验，超人修或可会自行收集使用或向第三方提供用户的非个人隐私信息。</span></p>
<p><span>三、【关于数据保护的权责】</span></p>
<p><span>3.1&nbsp;超人修有着严格的流程监管制度，工程师不得以维修或回收以外的任何理由转移、查阅、调用客户设备里的任何数据，但超人修不排除因不可控因素而造成检修设备数据泄露的任何可能。</span></p>
<p><span style="color:red;font-size:24px;"><strong>3.2&nbsp;请您务必于下单前备份并清除待检修设备的敏感数据。超人修不会承担该设备于维修或回收过程中因数据丢失、泄露等因素而造成的任何损失及责任。</strong></span></p>
&nbsp;<br>
<p><span>四、【关于邮寄业务的权责】</span></p>
<p><span>4.1.请您在寄送待检修设备时选择优质物流商，超人修不会承担您在邮寄过程中因丢失、损坏等因素对设备造成的任何损失及责任。如若待检修设备于寄送过程中受损或丢失，超人修会且仅会提供相关资料配合您向物流商进行维权。</span></p>
<p><span>4.2超人修会选择优质的物流商将检修后的设备寄还予您并配套相应的包邮保价服务措施，如若此过程中出现意外（需物流工作人员提供相关证明），超人修会且仅会承担与设备硬件相同型号（不保证地区版本）的赔偿责任。</span></p>
<p><span>五、【关于维修流程的权责】</span></p>
<p><span style="color:red;font-size:24px;">5.1&nbsp;<strong>手机进液或(摔过、压过、挤压过、剧烈震动过、变型)而损坏的手机，还会存在一些隐藏故障，因此检测或维修过程可能会发生手机彻底不能正常开机、显示或者手机彻底不能修复等一系列问题，请客户在维修前认真考虑，一旦维修或者检测造成以上故障的，由客户自行承担，超人修不承担任何责任。</strong></span></p>
<p><span>5.2工程师确认故障报价后并不保证可完全修复，若确认已无法修复，超人修将取消报价将手机退还客户。</span></p>
<p><span>5.3对于手机进液、摔过、压过、挤压过、剧烈震动过、变型等这些问题所引起的故障，经由超人修修复在保修期内非人为损坏的，超人修予以免费维修。但在维修过程中，不能修复的，超人修只承担退还上次的维修费用责任。
</span></p><p><span>5.4&nbsp;维修过程中，若由于个人原因，工程师和用户之间出现的任何纠纷问题，超人修 会积极主动为用户协商解决。</span></p>
<p><span>5.5&nbsp;设备经超人修检测后，如果您自愿放弃维修，需要承担回寄给您的快递费和快递物品保价费。</span></p>
<p><span>5.6&nbsp;对于因跌落、磕碰、挤压或进液面积达10%以上而丧失部分或全部功能的设备，超人修仅对您指定需修复的功能负维修责任。对于设备其他已知或未知的故障不承担任何责任。</span></p>
<p><span>5.7&nbsp;请您务必于下单时认真填写待检修设备的相关信息。超人修不会承担因您所提交信息的错误或缺漏而导致设备于维修过程中出现任何故障的责任。</span></p>
<p><span>5.8&nbsp;如果检修设备于超人修维修期间丢失，超人修会且仅会承担检修设备硬件相同型号（不保证地区版本）的赔偿责任，超人修不会承担因此产生的任何其它后果及责任。</span></p>
<p><span>5.9设备维修完成后，如在30天内不在线支付（支付宝、维修支付）维修费用，超人修视您放弃设备的处置权，超人修将获得设备处理权。</span></p>
<p><span>5.10&nbsp;超人修承诺采用原厂品质零配件进行设备维修。</span></p>
<p><span>六、【关于内存升级的权责】</span></p>
<p><span style="color:red;font-size:24px;"><strong>6.1超人修手机内存升级服务，在保修期内会且仅会只对硬盘的硬件故障进行保修，数据问题由用户自行负责。</strong></span></p>
<p><span>6.2超人修手机内存升级服务，对所更换的硬盘保修一个月。同时，在客户收到机器的72小时内，超人修承担手机发生的一切硬件问题（除人为损坏和不可抗力因素）。72小时之后，超人修只承担对于更换硬盘的硬件保修责任。</span></p>
<p><span>6.3鉴于手机内存升级服务的特殊性，用户同意超人修有权根据业务发展情况随时变更、中断或终止部分或全部的服务而无需通知客户，也无需对任何客户或任何第三方承担任何责任。</span></p>
<p><span>6.4手机内存升级服务存在一定的风险性(如手机生产或使用过程中存在虚焊或摔碰导致零器件松动等)，在手机升级服务过程中可能会出现手机功能性障碍或者手机无法使用等一系列问题。用户自愿承担此责任，超人修概不负责 &nbsp;</span></p>
<p><span>七、【关于保修服务的权责】</span></p>
<p><span>7.1超人修会且仅会对在超人修所更换的零件进行6个月的保修；对维修主板类故障，实行30天的有效保修时间。</span></p>
<p><span>7.2超人修对内存升级的机器会且仅会对硬盘提供30天保修期。</span></p>
<p><span>7.3超人修对以下情况不进行保修&nbsp;：</span></p>
<p><span>(1)&nbsp;人为损坏、不可抗力造成的损坏和非正常损坏（如火灾，水灾，地震等）； (2)&nbsp;超人修的专属标签被破坏； (3)出现跟之前维修无关的故障。 &nbsp;</span></p>
<p><span>八、【关于维修费用】</span></p>
<p><span>您支付的维修费用仅包含：零配件费用。</span></p>
<p><span>九、【关于回收服务的权责】</span></p>
<p><span>9.1&nbsp;超人修是根据您的描叙情况所评估的准确回收价格，请您如实描述自己电子产品的相应情况。若您的电子产品与描叙有差异或不符，超人修将安排客服与您说明情况，并根据实际情况，重新评价。您也可以取消本次交易，申请退货，退货邮费将由用户承担；</span></p>
<p><span>9.2&nbsp;本网站价格会有实时调整和变化，订单提交3天内（72小时内）发货保护价格不变动，超过72小时则按照最新的网站价格进行评估；
</span></p><p><span>9.3&nbsp;24小时承诺不包括以下情况：手机软件故障需刷机、需解密码、无法联系上用户、已申请进一步检测、法定节假日顺延、用户本身原因等，此类情况将根据实际情况确定汇款时间；</span></p>
<p><span>9.4&nbsp;官方价格调整不做通知，按照系统评估价格，提交订单时为准；</span></p>
<p><span>9.5&nbsp;交易完成后，超人修将统一将数据格式化，请您邮寄前删除或备份重要资料。如因未备份导致的资料损失，超人修不予承担责任；</span></p>
<p><span>9.6&nbsp;物品回收交易成功完成之后，超人修有权对物品进行处理，对物品不做保留和预留；</span></p>
<p><span>9.7&nbsp;回收价格大于或等于100元，超人修承担限额内邮费（顺丰承担22元以内，其他快递承担15元以内）；回收价格小于100元超人修承担一半邮费（限额内的一半）；（包装费和保价费由客户自主选择，超人修不承担相应费用）；</span></p>
<p><span>9.8&nbsp;本交易秉承合法、公平、透明、合理的原则进行；</span></p>
<p><span>9.9&nbsp;交易物品需为正规渠道物品，反之，超人修有权退回或予警处理，超人修不承担其责任；</span></p>
<p><span>9.10&nbsp;山寨机、高仿机、非法渠道物品，超人修拒绝回收，并有权退回或予警处理，超人修不承担其责任；</span></p>
<p><span>十、【法律责任】</span></p>
<p><span>10.1&nbsp;用户理解并同意，超人修有权依合理判断对违反有关法律法规或本协议规定的行为进行处罚，对违法违规的任何用户采取适当的法律行动，并依据法律法规保存有关信息向有关部门报告等，用户应承担由此而产生的一切法律责任。</span></p>
<p><span>10.2&nbsp;用户理解并同意，因用户违反本协议约定，导致或产生的任何第三方主张的任何索赔、要求或损失，包括合理的律师费，用户应当赔偿超人修与合作维修工程师、关联单位，并使之免受损害。</span></p>
<p><span>十一、【不可抗力及其他负责事由】</span></p>
<p><span>11.1&nbsp;用户理解并确认，在使用本服务的过程中，可能会遇到不可抗力等风险因素，使本服务发生中断。不可抗力是指不能预见、不能克服并不能避免且对一方或双方造成重大影响的客观事件，包括但不限于自然灾害如洪水、地震、瘟疫流行和风暴等以及社会事件如战争、动乱、政府行为等。出现上述情况时，超人修将努力在第一时间与相关单位配合，及时进行恢复，但是由此给用户造成的损失，超人修及合作维修单位在法律允许的范围内免责。</span></p>
<p><span>11.2&nbsp;本服务同大多数互联网服务一样，受包括但不限于用户原因、网络服务质量、社会环境等因素的差异影响，可能受到各种安全问题的侵扰，如他人利用用户的资料，造成现实生活中的骚扰；用户访问的其他网站中含有“特洛伊木马”等病毒，威胁到用户的计算机信息和数据的安全，继而影响本服务的正常使用等等。用户应加强信息安全及资料的保护意识，要注意加强密码保护，以免遭致损失和骚扰。</span></p>
<p><span>11.3&nbsp;用户理解并确认，本服务存在因不可抗力、计算机病毒或黑客攻击、系统不稳定、用户所在位置、用户关机以及其他任何技术、互联网络、通信线路原因等造成的服务中断或不能满足用户要求的风险，因此导致的任何损失，超人修不承担任何责任。</span></p>
<p><span>11.4&nbsp;用户理解并确认，在使用本服务过程中存在来自任何他人的包括误导性的、欺骗性的、威胁性的、诽谤性的、令人反感的或非法的信息，或侵犯他人权利的匿名或冒名的信息，以及伴随该等信息的行为，因此导致的用户或第三方的任何损失，超人修不承担任何责任。</span></p>
<p><span>11.5&nbsp;用户理解并确认，超人修需要定期或不定期地对“超人修”平台或相关的设备进行检修或者维护，如因此类情况而造成服务在合理时间内的中断，超人修无需为此承担任何责任，但超人修应事先进行通告。</span></p>
<p><span>11.6&nbsp;超人修依据法律法规、本协议约定获得处理违法违规或违约内容的权利，该权利不构成超人修的义务或承诺，超人修不能保证及时发现违法违规或违约行为或进行相应处理。</span></p>
<p><span>11.7&nbsp;用户理解并确认，对于超人修向用户提供的下列产品或者服务的质量缺陷及其引发的任何损失，超人修无需承担任何责任：</span></p>
<p><span>(1)&nbsp;超人修向用户免费提供的服务</span></p>
<p><span>(2)&nbsp;超人修向用户赠送的任何产品或者服务</span></p>
<p><span>十二、【其他】</span></p>
<p><span>12.1&nbsp;协议的生效与变更</span></p>
<p><span>您使用本服务即视为您已阅读并同意受本协议的约束。超人修有权在必要时修改本协议条款。您可以在相关页面中查阅最新的协议条款。本协议条款变更后，如果您继续使用本服务，即视为您已接受修改后的协议。如果您不接受修改后的协议，应当停止使用本服务。</span></p>
<p><span>12.2&nbsp;单方解除权</span></p>
<p><span>若您违反本协议或有关法律法规，超人修有权立刻解除本协议，您需承担由此产生的一切损失及责任。</span></p>
<p><span>12.3&nbsp;协议签订地</span></p>
<p><span>本协议签订地为中华人民共和国杭州市西湖区。</span></p>
<p><span>12.4&nbsp;适用法律</span></p>
<p><span>本协议的成立、生效、履行、解释及纠纷解决，适用中华人民共和国大陆地区法律（不包括冲突法）。</span></p>
<p><span>12.5&nbsp;争议解决</span></p>
<p><span>若您和超人修之间发生任何纠纷或争议，首先应友好协商解决；协商不成的，您同意将纠纷或争议提交本协议签订地有管辖权的人民法院管辖。</span></p>
<p><span>12.6&nbsp;条款标题</span></p>
<p><span>本协议所有条款的标题仅为阅读方便，本身并无实际涵义，不能作为本协议涵义解释的依据。</span></p>
<p><span>12.7&nbsp;条款效力</span></p>
<p><span>本协议条款无论因何种原因部分无效或不可执行，其余条款仍有效，对双方具有约束力。</span></p>
<p><span>12.8&nbsp;超人修对本协议服务有最终解释权。</span></p>
			
		</div>
	</div>
    `;
    var recycleArticle = `<div class="contractContentWrap">
        <p style="text-align:center" > <span style="font-weight:bold;" > 超人修回收服务条款 </p >
          <p><span style="font-weight:bold;" >【重要须知】</span></p>
            <p><span>超人修在此特别提醒，您使用超人修二手数码回收服务，必须事先认真阅读本服务条款全部内容，特别是免责条款及权益限制条款。请您阅读并选择接受或不接受本服务条款（未成年人审阅时应得到法定监护人的陪同）。如您不同意本服务条款 / 或随时对其的修改，您应不使用或主动取消与超人修的二手数码回收服务。您的使用行为将被视为对本服务条款的完全接受，包括接受超人修对服务条款随时所做的任何修改。</span></p>
              <p><span>一、【服务细则】</span></p>
                <p><span>1  用户使用数码产品回收服务前，您需确认已取消所有连接该数码的相关网络 / 服务合约；已移除该数码产品内的全部数据（包括并不限于个人资料、通讯录、短信、照片、视频、音频、游戏、歌曲、各软件的账户数据、记录及密码或其他资料），若未移除，超人修有权彻底删除该二手数码内的资料，若有数据未删除，超人修不提供提取 / 发送数据的服务。</span></p>
                  <p><span>2  用户使用数码产品回收服务前，您需确认已经将数码产品恢复出厂设置并做还原处理。若未提前恢复出厂设置，在交易过程中因恢复出厂设置或是还原导致的任何问题由您承担。</span></p>
                    <p><span>3  在发起苹果系列数码产品前，请您检查屏锁 / iCloud账户是否解除，未解除屏锁 / iCloud账户的苹果系列数码产品超人修不支持回收。</span></p>
                      <p><span>4  超人修为了检测机器是否存在进水和拆修等情况，检测工程师需要打开一体机系列数码产品后盖进行检测，超人修承诺绝不拆解您机器的内部零件；如您的机器已贴屏幕保护膜，超人修将拆卸屏幕保护贴膜进行检测，如在拆卸钢化膜时造成钢化膜损坏，需您自行承担该损失。</span></p>
                        <p><span>5  在上门回收前，超人修根据您对数码产品的描述给出报价。用户需理解并同意，您需如实对数码产品进行描述，超人修的报价是超人修根据您的描述情况参考与其情况相符的选项价格初步评估出来的回收价格（简称“估价”），估价并非最终成交价格。最终成交价格以上门检测后的报价为准。</span></p>
                          <p><span>6  一旦您已与超人修确认交易并且订单完成，将无法取消订单并退还机器。（包含一起邮寄的相关所有配件）</span></p>
                            <p><span>二、【免责申明】</span></p>
                              <p><span>1  本交易秉承合法、公平、透明、合理的原则进行。您同意超人修因相关法规实名制的需要，搜集并验证您的身份证信息及联系方式。</span></p>
                                <p><span>2  您确认所交付的二手数码产品为正规渠道物品，承诺所交易物品不是山寨机、高仿机、非原装产品、非法渠道产品（如失、窃物品等），且未就该数码产品进行改机 / 私拆 / 伪造机板 / 零件等不法行为，若因此产生任何争议，超人修有权依法向公安机关报案，将物品转交公安机关，并可依法追究您的民事责任和刑事责任，您应赔偿超人修因此所受的损失。</span></p>
                                  <p><span>3  您应保证自己描述的数码产品参数信息准确、真实、有效。因您描述的数码产品参数信息错误导致物品估价和 / 或最终成交价格低于评估价格的，您应自行承担后果。</span></p>
                                    <p><span>4  您坚持按照出现明显错误或依据常识不可能被认为是真实价格的价格出售物品。超人修有权取消或终止此类价格错误的订单。</span></p>
                                      <p><span>5  您了解并同意，若因您未以上条款施，导致产生任何争议及损害，包括但不限于因此产生的一切费用(例如:电信账单、服务费用等) 、资料泄露或损失，以及因此产生的所有求偿、损失、损害等，概由您自行承担，超人修无需承担任何责任。</span></p>
                                        <p><span>6  如超人修遭遇不可抗力事件，所造成的损失超人修拥有豁免权，“不可抗力”是指本协议双方不能合理控制、不可预见或即使遇见亦无法避免的事件，该事件妨碍、影响或延误任何一方根据协议履行其全部或部分义务。该事件包括但不限于政府行为、自然灾害、战争、电脑病毒、黑客攻击、网络故障、带宽或其他网络设备或技术提供商的服务延迟、服务障碍或任何其他类似事件。</span></p>
                                          </div>
                                          </div>`;

  //  WxParse.wxParse('article', 'html', article, that, 5);
    if (getApp().globalData.ServiceType==1)
      article = recycleArticle;
    var temp = WxParse.wxParse('article', 'html', article, that, 5);
    that.setData({
      article: temp
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