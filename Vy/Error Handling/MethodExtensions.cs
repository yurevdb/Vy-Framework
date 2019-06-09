using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Vy
{
    /// <summary>
    /// 
    /// </summary>
    public static class MethodExtensions
    {
        /// <summary>
        /// Handles a given <see cref="Action"/>
        /// </summary>
        /// <param name="action"></param>
        public static void Handle(this Delegate action)
        {
            try
            {
                //action.in;
            }
            catch (Exception ex)
            {
                throw new Exception($"{action.Method.Name} failed at runtime", ex);
            }
        }
    }
}
