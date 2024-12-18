using Eshop.Common.Exceptions;
using Eshop.Common.Helpers.AppSetting.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Eshop.Common.Helpers.AppSetting;

public class AppSettingHelper
{

    static readonly object Padlock = new object();
    private static AppSettingHelper _instance = null;

    public IConfiguration Configuration;


    private AppSettingHelper(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public static AppSettingHelper Instance(IConfiguration configuration)
    {
        lock (Padlock)
        {
            if (_instance == null)
            {
                _instance = new AppSettingHelper(configuration);
            }

            return _instance;
        }
    }

    internal List<PropertyData> GetValue<T>() where T : class
    {
        Type genericSetting = typeof(T);
        List<PropertyData> values = new List<PropertyData>();

        foreach (var prop in genericSetting.GetProperties())
        {
            var properties = prop.Name;

            if (prop.Name == "Name")
            {
                continue;
            }
            try
            {
                string objectName = genericSetting.Name;
                var propValue = Configuration.GetSection(objectName + ":" + prop.Name).Value;
                if (propValue != null)
                {
                    var result = new PropertyData { PropertyName = prop.Name, Value = Configuration.GetSection(objectName + ":" + prop.Name).Value.ToString() };
                    if (result != null)
                        values.Add(result);
                }
            }
            catch
            {
                throw new NotPropertyFoundException(prop.Name);
            }
        }
        return values;
    }
}