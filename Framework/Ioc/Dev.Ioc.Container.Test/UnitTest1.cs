using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Ioc.Container.Test
{
    [TestClass]
    public class UnitTest1
    {
        private Dev.Ioc.Container.IContainer _container;
        private Dev.Ioc.ServiceLocator.IServiceLocator _serviceLocator;
        [TestInitialize]
        public void init()
        {
            Ninject.IKernel kernel = new Ninject.StandardKernel();
            _container = new Dev.Ioc.Container.NinjectAdapter.NInjectContainer(kernel);


            _serviceLocator =
                new Dev.Ioc.ServiceLocator.NinjectAdapter.NinjectServiceLocator(kernel);
        }
        [TestMethod]
        public void TestMethod1()
        {
            //_container.Register<II, CI>();

            //var str = _serviceLocator.GetInstance<II>();

            //Console.WriteLine(str.test());




            _container.RegisterWithConstructor<II, CI>(new Parameter
            {
                Name = "str",
                Value = "inmyssssssss"
            });

            var str = _serviceLocator.GetInstance<II>();

            Console.WriteLine(str.test());


            _container.Register<II, CI>("aaaaa");


            str = _serviceLocator.GetInstance<II>("aaaaa");

            Console.WriteLine(str.test());
        }


        [TestMethod]
        public void MyTestMethodConstructor()
        {
            _container.RegisterWithConstructor<II, CI>(new Parameter
            {
                Name = "str",
                Value = "inmyssssssss"
            });

            var str = _serviceLocator.GetInstance<II>();

            Console.WriteLine(str.test());
        }

        [TestMethod]
        public void MyTestMethod2Constructor()
        {
            _container.RegisterWithConstructor<II, CI>(new[]
            {
                new Parameter
                {
                    Name = "str",
                    Value = "inmyssssssss"
                },
                new Parameter
                {
                    Name = "str2",
                    Value = "this str2"
                }
            });

            var str = _serviceLocator.GetInstance<II>();

            Console.WriteLine(str.test());
            Console.WriteLine(str.getStr2());
        }


        [TestMethod]
        public void MyTestMethod2ConstructorWithNamed()
        {
            _container.RegisterWithConstructor<II, CI>("name", new[]
            {
                new Parameter
                {
                    Name = "str",
                    Value = "inmyssssssss"
                },
                new Parameter
                {
                    Name = "str2",
                    Value = "this str2"
                }
            });

            var str = _serviceLocator.GetInstance<II>("name");

            Console.WriteLine(str.test());
            Console.WriteLine(str.getStr2());
        }




        public interface II
        {
            string test();
            string getStr2();
        }

        private class CI : II
        {
            private readonly string _str;
            private readonly string _str2;

            public CI()
            {

            }

            public CI(string str)
            {
                _str = str;
            }

            public CI(string str, string str2)
            {
                if (str2 == null) throw new ArgumentNullException("str2");
                _str = str;
                _str2 = str2;
            }

            public string getStr2()
            {
                return _str2;
            }
            public string test()
            {
                if (string.IsNullOrEmpty(_str))
                    return System.DateTime.Now.ToString();
                else
                    return _str;
            }
        }
    }
}
