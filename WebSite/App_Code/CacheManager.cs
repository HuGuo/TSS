using System;
using System.Web;
using System.Web.Caching;

public static class CacheManager
{
    /// <summary>
    /// 将对象加入到缓存中
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    public static void SetCache(string key , object value) {
        SetCache(key , value , RemovedCallback);
    }

    /// <summary>
    /// 将对象加入到缓存中
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存对象</param>
    /// <param name="callback">移除缓存时调用的方法</param>
    public static void SetCache(string key , object value , CacheItemRemovedCallback callback) {
        HttpRuntime.Cache.Insert(key , value , null , Cache.NoAbsoluteExpiration , TimeSpan.FromHours(1) , CacheItemPriority.Normal , callback);
    }

    /// <summary>
    /// 设置当前应用程序指定CacheKey的Cache值
    /// </summary>
    /// <param name="cacheKey">缓存Key</param>
    /// <param name="value">缓存对象</param>
    /// <param name="intMinute">过期时间分钟</param>
    public static void SetCache(string cacheKey , object value , int intMinute) {
        HttpRuntime.Cache.Insert(cacheKey , value , null , DateTime.Now.AddMinutes(intMinute) , TimeSpan.Zero);
    }

    /// <summary>
    /// 将对象加入到缓存中
    /// </summary>
    /// <param name="cacheKey">缓存键</param>
    /// <param name="cacheObject">缓存对象</param>
    /// <param name="dependency">缓存依赖项</param>
    public static void SetCache(string cacheKey , object cacheObject , CacheDependency dependency) {
        HttpRuntime.Cache.Insert(cacheKey , cacheObject , dependency);
    }

    /// <summary>
    /// 从缓存中取得对象，不存在则返回null
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <returns>获取的缓存对象</returns>
    public static object GetCache(string key) {
        return HttpRuntime.Cache[key];
    }

    /// <summary>
    /// 从缓存中获取一个对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="key">缓存集合中的键值</param>
    /// <returns></returns>
    public static T GetCache<T>(string key) {
        return (T)GetCache(key);
    }

    /// <summary>
    /// 移除指定CacheKey的值
    /// </summary>
    /// <param name="cacheKey">缓存Key</param>
    public static void RemoveCache(string cacheKey) {
        HttpRuntime.Cache.Remove(cacheKey);
    }
    /// <summary>
    /// 移除缓存时调用的方法
    /// </summary>
    /// <param name="k"></param>
    /// <param name="v"></param>
    /// <param name="r"></param>
    private static void RemovedCallback(string k , Object v , CacheItemRemovedReason r) {
        //
    }
}