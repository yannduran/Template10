using System;

namespace Template10.Services.SerializationService
{
    public static class SerializationService
    {
        private static volatile ISerializationService instance = new ToStringSerializationService();
        private static volatile Tuple<object, string> lastCache = new Tuple<object, string>(null, null);

        /// <summary>
        /// Gets or sets the instance that should be used to serialize/deserialize.
        /// </summary>
        public static ISerializationService Instance
        {
            get { return instance; }
            set
            {
                instance = value;
                lastCache = new Tuple<object, string>(null, null);
            }
        }

        /// <summary>
        /// Serializes the value.
        /// </summary>
        public static string Serialize(object value)
        {
            var lastCacheValue = lastCache;
            if (ReferenceEquals(lastCacheValue.Item1, value))
            {
                return lastCacheValue.Item2;
            }
            else
            {
                var result = instance.Serialize(value);
                lastCache = new Tuple<object, string>(value, result);
                return result;
            }
        }

        /// <summary>
        /// Serializes the value.
        /// </summary>
        public static object Deserialize(object value)
        {
            return Deserialize(value?.ToString());
        }

        /// <summary>
        /// Deserializes the value.
        /// </summary>
        public static object Deserialize(string value)
        {
            var lastCacheValue = lastCache;
            if (ReferenceEquals(lastCacheValue.Item2, value))
            {
                return lastCacheValue.Item1;
            }
            else
            {
                var result = instance.Deserialize(value);
                lastCache = new Tuple<object, string>(result, value);
                return result;
            }
        }

        /// <summary>
        /// Serializes the value.
        /// </summary>
        public static T Deserialize<T>(object value)
        {
            return Deserialize<T>(value?.ToString());
        }

        /// <summary>
        /// Deserializes the value.
        /// </summary>
        public static T Deserialize<T>(string value)
        {
            var lastCacheValue = lastCache;
            if (ReferenceEquals(lastCacheValue.Item2, value))
            {
                if (lastCacheValue.Item1 != null)
                {
                    return (T)lastCacheValue.Item1;
                }
                return default(T);
            }
            else
            {
                var result = instance.Deserialize<T>(value);
                lastCache = new Tuple<object, string>(result, value);
                return result;
            }
        }
    }
}