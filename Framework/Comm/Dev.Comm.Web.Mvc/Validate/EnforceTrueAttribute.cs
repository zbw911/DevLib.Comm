// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年03月06日 14:52
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/EnforceTrueAttribute.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Comm.Web.Mvc.Validate
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using ModelMetadata = System.Web.Http.Metadata.ModelMetadata;

    /// <summary>
    /// </summary>
    public class EnforceTrueAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof (bool))
                throw new InvalidOperationException("can only be used on boolean properties.");
            return (bool) value == true;
        }

        /// <summary>
        /// </summary>
        /// <param name="name"> </param>
        /// <returns> </returns>
        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field must be checked in order to continue.";
        }

        /// <summary>
        ///   When implemented in a class, returns client validation rules for that class.
        /// </summary>
        /// <returns> The client validation rules for this validator. </returns>
        /// <param name="metadata"> The model metadata. </param>
        /// <param name="context"> The controller context. </param>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(System.Web.Mvc.ModelMetadata metadata,
                                                                               ControllerContext context)
        {
            yield return new ModelClientValidationRule
                             {
                                 ErrorMessage =
                                     String.IsNullOrEmpty(ErrorMessage)
                                         ? FormatErrorMessage(metadata.DisplayName)
                                         : ErrorMessage,
                                 ValidationType = "enforcetrue"
                             };
        }
    }
}