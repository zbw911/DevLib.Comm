using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test
{
    [TestClass]
    public class TestJson
    {
        string str =
                  @"{""About"":"""",""ActionNum"":4,""BbsNum"":2,""Contact"":""啊"",""CreateDate"":""2013-04-25T14:45:17.653"",""GameId"":""896"",""GameName"":""梦幻魔兽"",""GroupIco"":""http://192.168.0.247:8001/2013/04/26/31/5/e/315e1ff61a101e44fd8df573278ed72a.jpg"",""GroupId"":10517,""GroupName"":""郑莉杰测试第一团产品团"",""GroupState"":1,""GroupTab"":"",联运平台,网络游戏团,移动游戏团,掌机游戏团,单机游戏团,"",""GroupTypeId"":1,""GroupTypeName"":""网络游戏"",""IdCard"":""阿斯蒂芬"",""IdentIco"":"""",""IsReview"":false,""LikeNum"":0,""MemberNum"":2,""Notice"":""ww"",""ProGroupId"":"""",""ProGroupName"":"""",""UserId"":null,""UserName"":""地方法"",""IdentState"":""""}";

        [TestMethod]
        public void TestMethod1()
        {

            var obj = Dev.Comm.JsonConvert.ToJsonObject(str);

            var t = obj.CreateDate;


        }

        [TestMethod]
        public void MyTestMethod()
        {
            var obj = Dev.Comm.JsonConvert.ToJsonObject<ViewModel>(str);

            Console.WriteLine(obj.CreateDate);
        }


        [TestMethod]
        public void MyTestMethod2()
        {
            var jsonString = @"""CreateDate"":""2013-04-25T14:45:17.653"",";
            string p = @"\d{4}-\d{2}-\d{2}[T\s]\d{1,2}:\d{1,2}:\d{1,2}[\.]\d{0,3}";

            var result = JsonString(jsonString, p);

            Console.WriteLine(result);
            p = @"\d{4}-\d{2}-\d{2}[T\s]\d{1,2}:\d{1,2}:\d{1,2}";
            result = JsonString(jsonString, p);

            Console.WriteLine(result);

        }

        [TestMethod]
        public void MyTestMethod3()
        {
            var jsonString = @"""CreateDate"":""2013-04-25 14:45:17"",";
            string p = @"\d{4}-\d{2}-\d{2}[T\s]\d{1,2}:\d{1,2}:\d{1,2}[\.]?\d{0,3}";

            var result = JsonString(jsonString, p);

            Console.WriteLine(result);
            //p = @"\d{4}-\d{2}-\d{2}[T\s]\d{1,2}:\d{1,2}:\d{1,2}";
            //result = JsonString(jsonString, p);

            //Console.WriteLine(result);

        }

        private static string JsonString(string jsonString, string p)
        {

            MatchEvaluator matchEvaluator = ConvertDateStringToJsonDate;
            var reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }

        /// <summary> 
        /// 将时间字符串转为Json时间 
        /// </summary>  
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

        public class ViewModel
        {
            #region Instance Properties

            public string About { get; set; }
            public Nullable<int> ActionNum { get; set; }
            public Nullable<int> BbsNum { get; set; }
            public string Contact { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public string GameId { get; set; }
            public string GameName { get; set; }
            public string GroupIco { get; set; }
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public Nullable<int> GroupState { get; set; }
            public string GroupTab { get; set; }
            public Nullable<int> GroupTypeId { get; set; }
            public string GroupTypeName { get; set; }
            public string IdCard { get; set; }
            public string IdentIco { get; set; }
            public Nullable<bool> IsReview { get; set; }
            public Nullable<int> LikeNum { get; set; }
            public Nullable<int> MemberNum { get; set; }
            public string Notice { get; set; }
            public string ProGroupId { get; set; }
            public string ProGroupName { get; set; }
            public Nullable<decimal> UserId { get; set; }
            public string UserName { get; set; }
            public string IdentState { get; set; }
            #endregion

            //public Nullable<decimal> UserId { get; set; }
        }


        [TestMethod]
        public void TestDynKeyJson()
        {
            var jsonstr = @"{""code"":0,""msg"":""OK"",
""res"":{""list"":{

""129522493072295"":{""order_id"":""129522493072295"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104112295"",""uname"":"""",""phone"":""13634172260"",""type"":2,""create_time"":1365493441,""pay_time"":0,""coupon"":[]}

,""695933696469568"":{""order_id"":""695933696469568"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104109568"",""uname"":""lijin0564335"",""phone"":""13581546381"",""type"":2,""create_time"":1365491005,""pay_time"":0,""coupon"":[]}

,""453700092842285"":{""order_id"":""453700092842285"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104112285"",""uname"":"""",""phone"":"""",""type"":2,""create_time"":1365480853,""pay_time"":1365485507,""coupon"":[]}

,""127581718422283"":{""order_id"":""127581718422283"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104112283"",""uname"":"""",""phone"":""13634172260"",""type"":2,""create_time"":1365476723,""pay_time"":0,""coupon"":[]}

,""707903022721141"":{""order_id"":""707903022721141"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104041141"",""uname"":""local1_3"",""phone"":""13818954419"",""type"":2,""create_time"":1365071013,""coupon"":{""2837468618"":{""code"":2837468618,""status"":1},""9280443850"":{""code"":9280443850,""status"":1}},""pay_time"":1365418320}

,""677376105781576"":{""order_id"":""677376105781576"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104111576"",""uname"":""passcnm"",""phone"":""18611520561"",""type"":2,""create_time"":1364884883,""coupon"":{""4985542090"":{""code"":4985542090,""status"":1}},""pay_time"":1365418320}

,""314217842351139"":{""order_id"":""314217842351139"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104041139"",""uname"":""local1_2"",""phone"":""13699246163"",""type"":2,""create_time"":1364469749,""pay_time"":0,""coupon"":[]}

,""747318099251139"":{""order_id"":""747318099251139"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104041139"",""uname"":""local1_2"",""phone"":""13699246163"",""type"":2,""create_time"":1364460676,""pay_time"":0,""coupon"":[]}

,""530326116951139"":{""order_id"":""530326116951139"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":0,""amount"":99,""count"":1,""unused_count"":1,""uid"":""104041139"",""uname"":""local1_2"",""phone"":""13699246163"",""type"":2,""create_time"":1364458601,""pay_time"":0,""coupon"":[]}

,""509945328201139"":{""order_id"":""509945328201139"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":2900,""amount"":2999,""count"":1,""unused_count"":1,""uid"":""104041139"",""uname"":""local1_2"",""phone"":""13699246163"",""type"":2,""create_time"":1364394640,""pay_time"":0,""coupon"":[]}

,""427083338661139"":{""order_id"":""427083338661139"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":2,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":2900,""amount"":2999,""count"":1,""unused_count"":1,""uid"":""104041139"",""uname"":""local1_2"",""phone"":""13699246163"",""type"":2,""create_time"":1364394428,""pay_time"":0,""coupon"":[]}

,""566873045721139"":{""order_id"":""566873045721139"",""title"":""\u9999\u4e30\u767e\u5408"",""status"":1,""provider"":""\u767e\u5ea6\u56e2\u8d2d"",""total_amount"":2900,""amount"":2999,""count"":1,""unused_count"":1,""uid"":""104041139"",""uname"":""local1_2"",""phone"":""13699246163"",""type"":1,""create_time"":1364394403,""pay_time"":0,""coupon"":[]}},""total"":12}}";




            var jss = new JavaScriptSerializer();

            dynamic data = jss.Deserialize<dynamic>(jsonstr);


            Console.WriteLine(data["code"]);

            var diclist = data["res"]["list"];
            var total = data["res"]["total"];

            if (total > 0)
            {
                foreach (var item in diclist.Keys)
                {

                    var value = diclist[item];

                    Console.WriteLine(item);
                    Console.WriteLine("--");
                    Console.WriteLine(value["title"]);
                    Console.WriteLine("--");
                    Console.WriteLine(value["order_id"]);
                }
            }
            else
            {
                Console.WriteLine("none");
            }

        }

        [TestMethod]
        public void MyDynObject2Str()
        {
            dynamic d = new { a = 1, b = 2, c = "111" };

            var str = Dev.Comm.JsonConvert.ToJsonStrDyn(d);

            Console.WriteLine(str);
        }


        [TestMethod]
        public void ExpandoObjectTestMethod()
        {
            dynamic d = new ExpandoObject();

            d.a = 1;
            d.b = new Dictionary<string, string> { { "1", "1" }, { "2", "2" } };

            var str = Dev.Comm.JsonConvert.ToJsonStrDyn(d);

            Console.WriteLine(str);
        }

    }
}
