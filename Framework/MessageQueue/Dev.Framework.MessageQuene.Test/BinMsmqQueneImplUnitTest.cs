using System;
using System.Dynamic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.MessageQuene.Test
{
    [TestClass]
    public class BinMsmqQueneImplUnitTest
    {



        [TestMethod]
        public void Send()
        {
            data mp = new data();

            BinaryFormatter bformatter = new BinaryFormatter();

            Stream stream = new MemoryStream();

            Console.WriteLine("Writing Employee Information");
            bformatter.Serialize(stream, mp);

            stream.Seek(0, SeekOrigin.Begin);

            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            var str = BitConverter.ToString(bytes);

            var strb = Encoding.UTF8.GetBytes(str.Replace("-", ""));


            var len = strb.Length;

            var stream2 = new MemoryStream(bytes);
            stream2.Seek(0, SeekOrigin.Begin);

            //stream = new MemoryStream(bytes);

            //stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bformatte2 = new BinaryFormatter();
            var objects = bformatte2.Deserialize(stream2);


        }


        [TestMethod]
        public void SendMethod()
        {
            data mp = new data();

            BinaryFormatter bformatter = new BinaryFormatter();

            Stream stream = new MemoryStream();

            Console.WriteLine("Writing Employee Information");
            bformatter.Serialize(stream, mp);

            stream.Seek(0, SeekOrigin.Begin);

            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);


            TransData td = new TransData
            {
                //Type = mp.GetType(),
                SerializeData = bytes
            };


            foreach (var b in bytes)
            {
                MsgQueueProvider<TransData>.Provider.Send(td);
            }


        }

        [TestMethod]
        public void GetDataF()
        {
            var data = MsgQueueProvider<TransData>.Provider.Receive();

            var bytes = data.SerializeData;

            var stream2 = new MemoryStream(bytes);
            stream2.Seek(0, SeekOrigin.Begin);

            //stream = new MemoryStream(bytes);

            //stream.Seek(0, SeekOrigin.Begin);
            BinaryFormatter bformatte2 = new BinaryFormatter();
            var objects = bformatte2.Deserialize(stream2);
        }
    }


    public class TransData
    {
        //public Type Type { get; set; }

        public byte[] SerializeData { get; set; }
    }


    [Serializable]
    public class data
    {
        public DateTime DateTime { get; set; }

    }


    public class Mydata : data
    {
        public int Mydataint { get; set; }
    }

}
