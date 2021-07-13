using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Business
{
    /// <summary>
    /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class ObjectCopier
    {
        /// <summary>
        /// Copy object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyObject<T>(T obj)
        {
            string tmpStr = JsonConvert.SerializeObject(obj);
            var ret = JsonConvert.DeserializeObject<T>(tmpStr);
            return ret;
        }

        /// <summary>
        /// Copy List Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listObj"></param>
        /// <returns></returns>
        public static List<T> CopyObject<T>(List<T> listObj)
        {
            string tmpStr = JsonConvert.SerializeObject(listObj);
            var ret = JsonConvert.DeserializeObject<List<T>>(tmpStr);
            return ret;
        }
    }
}
