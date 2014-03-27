// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月21日 17:34
// 
// 修改于：2013年02月18日 18:24
// 文件名：Objcontext2DbContext.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Kt.Framework.Repository.Data.EntityFramework5
{
    internal class Objcontext2DbContext
    {
        /// <summary>
        ///     通过 DB EntityEntry 取得数据库表名的方法
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public static string GetTableName(DbEntityEntry ent)
        {
            ObjectContext objectContext = ((IObjectContextAdapter) ent).ObjectContext;
            Type entityType = ent.Entity.GetType();

            if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                entityType = entityType.BaseType;

            string entityTypeName = entityType.Name;

            EntityContainer container =
                objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = (from meta in container.BaseEntitySets
                                    where meta.ElementType.Name == entityTypeName
                                    select meta.Name).First();
            return entitySetName;
        }
    }
}