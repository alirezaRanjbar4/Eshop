using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Eshop.Common.Helpers.Utilities.Utilities.Providers
{
    public class ObjectHelper
    {

        public object CombineObjects(object item)
        {
            var ret = new ExpandoObject() as IDictionary<string, object>;
            var props = item.GetType().GetProperties();

            foreach (var property in props)
            {
                if (property.CanRead)
                {
                    ret[property.Name] = GetPropertyValue(item, property.Name);
                }
            }

            return ret;
        }

        public void CheckArgumentIsNull(object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }

        public object GetPropertyValue(object instant, string propertyName)
        {
            var instantOfType = instant;
            if (instantOfType == null)
                return null!;
            Type ObjType = instantOfType.GetType();
            PropertyInfo prop = ObjType.GetProperty(propertyName)!;
            if (prop == null)
                return null!;

            object obj = prop.GetValue(instantOfType)!;
            return obj;
        }

        public void SetPropertyValue(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, value);
            }
        }

        public object JsonDeSerializeObject(string obj)
        {
            return JsonConvert.DeserializeObject(obj);
        }

        /// <summary>
        /// سریال کردن آبجکت به جیسون
        /// </summary>
        /// <param name="obj">آبجکت</param>
        /// <returns>آبجت سریال شده به جیسون</returns>
        public object JsonSerializeObject(object obj, bool referenceLoopIgnore = false)
        {
            if (referenceLoopIgnore)
                return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            else
                return JsonConvert.SerializeObject(obj, Formatting.None);
        }
    }
}
