// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年07月16日 18:12
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/ObjectDynmic.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dev.Comm
{
    /// <summary>
    ///   http://stackoverflow.com/questions/7717281/expandoobject-to-static-object-and-back-again-spanning-the-two-domains
    /// </summary>
    internal class ObjectDynamicConvert
    {
        public static ExpandoObject ToExpando(object staticObject)
        {
            System.Dynamic.ExpandoObject expando = new ExpandoObject();
            var dict = expando as IDictionary<string, object>;
            PropertyInfo[] properties = staticObject.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                dict[property.Name] = property.GetValue(staticObject, null);
            }

            return expando;
        }
    }
}