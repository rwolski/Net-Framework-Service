﻿using System;
using System.Configuration;

namespace WebApp.API
{
    /// <summary>
    /// Internal app settings dictionary
    /// </summary>
    internal class AppSettings
    {
        const string RedisHostnameKey = "RedisHostname";
        const string RedisPortKey = "RedisPort";
        const string RedisDatabaseIndexKey = "RedisDatabaseIndex";
        const string RabbitMQHostnameKey = "RabbitMQHostname";
        const string RabbitMQPortKey = "RabbitMQPort";
        const string MongoDbHostnameKey = "MongoDbHostname";
        const string MongoDbPortKey = "MongoDbPort";
        const string MongoDbDatabaseKey = "MongoDbDatabase";
        const string AppServiceBusNameKey = "AppServiceBusName";

        /// <summary>
        /// Gets the redis hostname.
        /// </summary>
        /// <value>
        /// The redis hostname.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string RedisHostname
        {
            get
            {
                var str = ConfigurationManager.AppSettings[RedisHostnameKey];
                if (string.IsNullOrWhiteSpace(str))
                    throw new ArgumentNullException(RedisHostnameKey);
                return str;
            }
        }

        /// <summary>
        /// Gets the redis port.
        /// </summary>
        /// <value>
        /// The redis port.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static UInt16 RedisPort
        {
            get
            {
                var str = ConfigurationManager.AppSettings[RedisPortKey];
                UInt16 val;
                if (string.IsNullOrWhiteSpace(str) || !UInt16.TryParse(str, out val))
                    throw new ArgumentNullException(RedisPortKey);
                return val;
            }
        }

        /// <summary>
        /// Gets the redis database index.
        /// </summary>
        /// <value>
        /// The redis database index.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static int RedisDatabaseIndex
        {
            get
            {
                var str = ConfigurationManager.AppSettings[RedisDatabaseIndexKey];
                int val;
                if (string.IsNullOrWhiteSpace(str) || !Int32.TryParse(str, out val))
                    throw new ArgumentNullException(RedisDatabaseIndexKey);
                return val;
            }
        }

        /// <summary>
        /// Gets the rabbit mq hostname.
        /// </summary>
        /// <value>
        /// The rabbit mq hostname.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string RabbitMQHostname
        {
            get
            {
                var str = ConfigurationManager.AppSettings[RabbitMQHostnameKey];
                if (string.IsNullOrWhiteSpace(str))
                    throw new ArgumentNullException(RabbitMQHostnameKey);
                return str;
            }
        }

        /// <summary>
        /// Gets the rabbit mq port.
        /// </summary>
        /// <value>
        /// The rabbit mq port.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static UInt16 RabbitMQPort
        {
            get
            {
                var str = ConfigurationManager.AppSettings[RabbitMQPortKey];
                UInt16 val;
                if (string.IsNullOrWhiteSpace(str) || !UInt16.TryParse(str, out val))
                    throw new ArgumentNullException(RabbitMQPortKey);
                return val;
            }
        }


        /// <summary>
        /// Gets the mongo db hostname.
        /// </summary>
        /// <value>
        /// The mongo db hostname.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string MongoDbHostname
        {
            get
            {
                var str = ConfigurationManager.AppSettings[MongoDbHostnameKey];
                if (string.IsNullOrWhiteSpace(str))
                    throw new ArgumentNullException(MongoDbHostnameKey);
                return str;
            }
        }

        /// <summary>
        /// Gets the mongo db port.
        /// </summary>
        /// <value>
        /// The mongo db port.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static UInt16 MongoDbPort
        {
            get
            {
                var str = ConfigurationManager.AppSettings[MongoDbPortKey];
                UInt16 val;
                if (string.IsNullOrWhiteSpace(str) || !UInt16.TryParse(str, out val))
                    throw new ArgumentNullException(MongoDbPortKey);
                return val;
            }
        }

        /// <summary>
        /// Gets the mongo db database name.
        /// </summary>
        /// <value>
        /// The mongo db database name.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string MongoDbDatabase
        {
            get
            {
                var str = ConfigurationManager.AppSettings[MongoDbDatabaseKey];
                if (string.IsNullOrWhiteSpace(str))
                    throw new ArgumentNullException(MongoDbDatabaseKey);
                return str;
            }
        }

        /// <summary>
        /// Gets the application service bus name.
        /// </summary>
        /// <value>
        /// The application bus name.
        /// </value>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string AppServiceBusName
        {
            get
            {
                var str = ConfigurationManager.AppSettings[AppServiceBusNameKey];
                if (string.IsNullOrWhiteSpace(str))
                    throw new ArgumentNullException(AppServiceBusNameKey);
                return str;
            }
        }
    }
}