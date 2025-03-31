using Eshop.Common.Enum;
using Eshop.Entity.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Rasam.Data.ModelConfiguration.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Rasam.Data.DBContext;

public static class ModelExtentions
{
    public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var filter = (Expression<Func<IBaseEntity, bool>>)(e => !e.Deleted);

        IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                                            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c) && c != typeof(BaseTrackedModel));
        foreach (Type type in types)
        {
            /// <summary>
            /// تاریخ و زمان ایجاد
            /// </summary>
            modelBuilder.Entity(type).Property<DateTime>("CreateDate").HasColumnType(DataTypes.Datetime.ToString());

            /// <summary>
            /// حذف منطقی
            /// </summary>
            modelBuilder.Entity(type).Property<bool>("Deleted").HasDefaultValue(false);

            ///// <summary>
            ///// شناسه ویرایش کننده
            ///// </summary>
            modelBuilder.Entity(type).Property<Guid?>("ModifiedById").IsRequired(false);

            /// <summary>
            /// تاریخ و زمان ویرایش 
            /// </summary>
            modelBuilder.Entity(type).Property<DateTime?>("ModifiedDate").IsRequired(false).HasColumnType(DataTypes.Datetime.ToString());

            /// <summary>
            /// فیلتر Deleted و Activate 
            /// </summary>
            var filters = new List<LambdaExpression>();

            if (typeof(BaseTrackedModel).IsAssignableFrom(type))
                filters.Add(filter);

            var queryFilter = CombineQueryFilters(type, filter, filters);
            modelBuilder.Entity(type).HasQueryFilter(queryFilter);
        }
    }

    public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Identity_UserConfiguration).GetTypeInfo().Assembly);
    }

    public static void IgnorePropertyOfEntity(this ModelBuilder modelBuilder, string propertyName, Type propertyType)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IMutableProperty property = entityType.GetProperties().SingleOrDefault(p => p.Name.Equals(propertyName));
            if (property != null && property.ClrType == propertyType)
                modelBuilder.Entity(property.Name).Ignore(property.Name);
        }
    }

    private static LambdaExpression CombineQueryFilters(Type entityType, LambdaExpression baseFilter, IEnumerable<LambdaExpression> andAlsoExpressions)
    {
        var newParam = Expression.Parameter(entityType);

        var andAlsoExprBase = (Expression<Func<BaseTrackedModel, bool>>)(_ => true);
        var andAlsoExpr = ReplacingExpressionVisitor.Replace(andAlsoExprBase.Parameters.Single(), newParam, andAlsoExprBase.Body);
        foreach (var expressionBase in andAlsoExpressions)
        {
            var expression = ReplacingExpressionVisitor.Replace(expressionBase.Parameters.Single(), newParam, expressionBase.Body);
            andAlsoExpr = Expression.AndAlso(andAlsoExpr, expression);
        }
        var baseExp = ReplacingExpressionVisitor.Replace(baseFilter.Parameters.Single(), newParam, baseFilter.Body);
        var exp = Expression.OrElse(baseExp, andAlsoExpr);

        return Expression.Lambda(exp, newParam);
    }
}