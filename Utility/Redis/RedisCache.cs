#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0  || NETCOREAPP3_1 || NETSTANDARD2_0 || NETSTANDARD2_1
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// StackExchange 实现redis  数据库公共类
    /// </summary>
    public class RedisCache : IDisposable
    {

        private int _db = 0;//数据库
        /// <summary>
        /// 注入连接地址
        /// </summary>
        /// <param name="host">连接地址</param>
        public RedisCache(string host)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(host);
        }
        /// <summary>
        /// 设置数据库
        /// </summary>
        /// <param name="db">数据库</param>
        public RedisCache Set(int db)
        {
            this._db = db;
            return this;
        }
        private ConnectionMultiplexer _connectionMultiplexer;//声明redis ConnectionMultiplexer 对象
        /// <summary>
        /// redis ConnectionMultiplexer 连接对象
        /// </summary>
        public ConnectionMultiplexer Connection => _connectionMultiplexer;
        /// <summary>
        /// 添加 set
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool Add(string key, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return _connectionMultiplexer.GetDatabase(_db).SetAdd((RedisKey)key, (RedisValue)value, commandFlags);
        }
        /// <summary>
        /// 添加 string
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="when"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool AddString(string key, string value, TimeSpan? expiry = null, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            return _connectionMultiplexer.GetDatabase(_db).StringSet((RedisKey)key, (RedisValue)value, expiry, when, commandFlags);
        }
        /// <summary>
        /// 获取 string
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public string GetString(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            return _connectionMultiplexer.GetDatabase(_db).StringGet(key, commandFlags);
        }
        /// <summary>
        /// 移除键
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool Remove(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            return _connectionMultiplexer.GetDatabase(_db).KeyDelete(key, commandFlags);
        }
        /// <summary>
        /// 获取数据库对象
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IDatabase Database(int db = -1)
        {
            return this._connectionMultiplexer.GetDatabase(db);
        }
        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="handler"></param>
        public void Subscribe(string channelName, Action<RedisChannel, RedisValue> handler)
        {
            ISubscriber subscriber = this._connectionMultiplexer.GetSubscriber();
            subscriber.Subscribe(new RedisChannel(channelName, RedisChannel.PatternMode.Auto),(it,msg)=> handler?.Invoke(it,msg));
        }
        /// <summary>
        /// ISubscriber 发布信息
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public long Publish(string channelName, string msg)
        {
            ISubscriber subscriber = this._connectionMultiplexer.GetSubscriber();
            return subscriber.Publish(new RedisChannel(channelName, RedisChannel.PatternMode.Auto), msg);
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="database"></param>
        /// <param name="channelName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public long Publish(IDatabase database, string channelName, string msg)
        {
            return database.Publish(new RedisChannel(channelName, RedisChannel.PatternMode.Auto), msg);
        }
        /// <summary>
        /// 添加 list
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public long AddList(IDatabase database, string key, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return database.ListRightPush(key, value, When.NotExists, commandFlags);
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<RedisValue> Get(IDatabase database, string key)
        {
            return database.ListRange(key).ToList<RedisValue>();
        }
        /// <summary>
        /// 添加 set 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool Add(IDatabase database, string key, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return database.SetAdd((RedisKey)key, (RedisValue)value, commandFlags);
        }
        /// <summary>
        /// 添加 set 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool SetAdd(ConnectionMultiplexer connection, int db, string key, string value, CommandFlags commandFlags = CommandFlags.None)
        {
            return connection.GetDatabase(db).SetAdd((RedisKey)key, (RedisValue)value, commandFlags);
        }
        /// <summary>
        /// 添加string
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="expiry"></param>
        /// <param name="when"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool AddString(ConnectionMultiplexer connection, string key, string value, int db = 0, TimeSpan? expiry = null, When when = When.Always, CommandFlags commandFlags = CommandFlags.None)
        {
            return connection.GetDatabase(db).StringSet((RedisKey)key, (RedisValue)value, expiry, when, commandFlags);
        }
        /// <summary>
        /// 获取string
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public string GetString(ConnectionMultiplexer connection, string key, int db = 0, CommandFlags commandFlags = CommandFlags.None)
        {
            return connection.GetDatabase(db).StringGet(key, commandFlags);
        }
        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool Remove(ConnectionMultiplexer connection, string key, int db = 0, CommandFlags commandFlags = CommandFlags.None)
        {
            return connection.GetDatabase(db).KeyDelete(key, commandFlags);
        }
        /// <summary>
        /// 是否存在key
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool KeyExists(ConnectionMultiplexer connection, string key, int db = 0, CommandFlags commandFlags = CommandFlags.None)
        {
            return connection.GetDatabase(db).KeyExists(key, commandFlags);
        }  /// <summary>
           /// 是否存在key
           /// </summary>
           /// <param name="key"></param>
           /// <param name="db"></param>
           /// <param name="commandFlags"></param>
           /// <returns></returns>
        public bool KeyExists(string key, int db = 0, CommandFlags commandFlags = CommandFlags.None)
        {
            return Connection.GetDatabase(db).KeyExists(key, commandFlags);
        }
        /// <summary>
        /// hashset 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool HashSet(IDatabase database, string key, HashEntry[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            database.HashSet(key, values, commandFlags);
            return true;
        }
        /// <summary>
        /// hashset 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool HashSet( string key, HashEntry[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            Connection.GetDatabase(_db).HashSet(key, values, commandFlags);
            return true;
        }
        /// <summary>
        /// HashExists 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="hashKey"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool HashExists( string key, string hashKey, CommandFlags commandFlags = CommandFlags.None)
        {
           return Connection.GetDatabase(_db).HashExists(key, hashKey, commandFlags);
        }
        /// <summary>
        /// HashDelete 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="hashKey"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool HashDelete( string key, string hashKey, CommandFlags commandFlags = CommandFlags.None)
        {
           return Connection.GetDatabase(_db).HashDelete(key, hashKey, commandFlags);
        }  
        /// <summary>
        /// HashGet 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="hashKey"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public string HashGet( string key, string hashKey, CommandFlags commandFlags = CommandFlags.None)
        {
           return Connection.GetDatabase(_db).HashGet(key, hashKey, commandFlags);
        }
        /// <summary>
        /// hashset
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public bool HashSet(ConnectionMultiplexer connection, int db, string key, HashEntry[] values, CommandFlags commandFlags = CommandFlags.None)
        {
            connection.GetDatabase(db).HashSet((RedisKey)key, values, commandFlags);
            return true;
        }
        /// <summary>
        /// 释放资源
        /// </summary>

        public void Dispose()
        {
            if (_connectionMultiplexer != null) _connectionMultiplexer.Dispose();
        }
    }
}
#endif