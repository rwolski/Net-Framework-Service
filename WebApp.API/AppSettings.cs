using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApp.API
{
    /// <summary>
    /// Internal app settings dictionary
    /// </summary>
    internal class AppSettings
    {
        const string RedisHostnameKey = "RedisHostname";
        const string RedisPortKey = "RedisPort";
        const string RabbitMQHostnameKey = "RabbitMQHostname";
        const string RabbitMQPortKey = "RabbitMQPort";
        const string MongoDbHostnameKey = "MongoDbHostname";
        const string MongoDbPortKey = "MongoDbPort";
        const string MongoDbDatabaseKey = "MongoDbDatabase";

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
        public static int RedisPort
        {
            get
            {
                var str = ConfigurationManager.AppSettings[RedisPortKey];
                int val;
                if (string.IsNullOrWhiteSpace(str) || !Int32.TryParse(str, out val))
                    throw new ArgumentNullException(RedisPortKey);
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
        public static int RabbitMQPort
        {
            get
            {
                var str = ConfigurationManager.AppSettings[RabbitMQPortKey];
                int val;
                if (string.IsNullOrWhiteSpace(str) || !Int32.TryParse(str, out val))
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
        public static int MongoDbPort
        {
            get
            {
                var str = ConfigurationManager.AppSettings[MongoDbPortKey];
                int val;
                if (string.IsNullOrWhiteSpace(str) || !Int32.TryParse(str, out val))
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
    }
}