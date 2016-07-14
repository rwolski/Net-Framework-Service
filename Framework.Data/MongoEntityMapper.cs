using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Data
{
    public class MongoEntityMapper
    {
        public static void Map<T>()
        {
            var hasDefinitions = typeof(T).GetProperties().Any(x => x.IsDefined(typeof(EntityFieldAttribute), true));

            if (!hasDefinitions)
            {
                BsonClassMap.RegisterClassMap<T>();
                return;
            }

            var props = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            BsonClassMap.RegisterClassMap<T>(cm =>
            {
                RegisterClassMap(cm, props);
            });
        }

        public static void Map(Type type)
        {
            var hasDefinitions = type.GetProperties().Any(x => x.IsDefined(typeof(EntityFieldAttribute), true));

            if (!hasDefinitions)
            {
                BsonClassMap.RegisterClassMap(new BsonClassMap(type));
                return;
            }

            var props = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            var cm = new BsonClassMap(type);
            RegisterClassMap(cm, props);

            BsonClassMap.RegisterClassMap(cm);
        }

        private static void RegisterClassMap(BsonClassMap cm, IEnumerable<PropertyInfo> props)
        {
            foreach (var prop in props)
            {
                var ignored = prop.GetCustomAttributes(typeof(FieldIgnoreAttribute), true).Cast<FieldIgnoreAttribute>().FirstOrDefault();
                if (ignored != null)
                    return;

                var idAttr = prop.GetCustomAttributes(typeof(IdFieldAttribute), true).Cast<IdFieldAttribute>().FirstOrDefault();
                if (idAttr != null)
                {
                    var id = cm.MapIdMember(prop).SetIdGenerator(CombGuidGenerator.Instance);
                    id.SetElementName(idAttr.FieldName ?? "_id");
                    return;
                }

                //var versionAttr = prop.GetCustomAttributes(typeof(VersionFieldAttribute), true).Cast<VersionFieldAttribute>().FirstOrDefault();
                //if (versionAttr != null)
                //{
                //    var id = cm.(prop).SetIdGenerator(CombGuidGenerator.Instance);
                //    id.SetElementName(idAttr.FieldName ?? "_id");
                //    continue;
                //}

                var propAttr = prop.GetCustomAttributes(typeof(EntityFieldAttribute), true).Cast<EntityFieldAttribute>().FirstOrDefault();
                var map = cm.MapMember(prop);
                if (propAttr != null)
                {
                    if (!string.IsNullOrEmpty(propAttr.FieldName))
                        map.SetElementName(propAttr.FieldName);
                    //if (propAttr.StringRepresentation)
                        //map.SetSerializer(new MongoDB.Bson.Serialization.Serializers.EnumSerializer((BsonType.String));
                }
            }
        }
    }
}
