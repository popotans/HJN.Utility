using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJN.Dapper
{
    public interface ISqlBuilder
    {
        /// <summary>
        /// 生成插入实体的语句
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        string Insert<T>(T t) where T : BaseEntity;
        /// <summary>
        /// 生成更新语句
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        string Updatet<T>(T t) where T : BaseEntity;
        /// <summary>
        /// 生成删除语句
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        string Delete<T>(T t) where T : BaseEntity;
    }
}
