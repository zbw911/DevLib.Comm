using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Dev.Comm.Web.Mvc.Model
{
    /// <summary>
    /// 模型帮助方法
    /// </summary>
    public class ModelHelper
    {
        /// <summary>
        /// 直接取得Model
        /// </summary>
        /// <param name="controller"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static TModel GetModel<TModel>(Controller controller) where TModel : class
        {
            return GetModel<TModel>(controller, false);
        }

        /// <summary>
        /// 直接取得Model
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="modeltype"></param>
        /// <returns></returns>
        public static object GetModel(Controller controller, Type modeltype)
        {
            return GetModel(controller, modeltype, false);
        }

        /// <summary>
        /// 直接取得Model
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="ignoreModelStateError"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public static TModel GetModel<TModel>(Controller controller, bool ignoreModelStateError) where TModel : class
        {
            var modeltype = typeof(TModel);
            return (TModel)GetModel(controller, modeltype, ignoreModelStateError);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="modeltype"></param>
        /// <param name="ignoreModelStateError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public static object GetModel(Controller controller, Type modeltype, bool ignoreModelStateError)
        {
            if (modeltype == null) throw new ArgumentNullException("modeltype");
            var model = DependencyResolver.Current.GetService(modeltype);

            if (model == null)
            {
                throw new ArgumentNullException("model");
            }


            var controllerContext = controller.ControllerContext;

            var valueProvider = ValueProviderFactories.Factories.GetValueProvider(controllerContext);

            IModelBinder binder = ModelBinders.Binders.GetBinder(modeltype);

            var innerModelState = new ModelStateDictionary();

            var bindingContext = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, modeltype),
                //ModelName = prefix,
                ModelState = innerModelState,
                //PropertyFilter = propertyFilter,
                ValueProvider = valueProvider
            };




            var obj = binder.BindModel(controllerContext, bindingContext);

            var error = Dev.Comm.Web.Mvc.Model.ModelStateHandler.GetAllError(innerModelState);

            if (!ignoreModelStateError && error.Any())
            {
                throw new InvalidCastException(string.Join(",", error.Select(x => x.ErrorMessage)));
            }

            return obj;

        }



    }
}
