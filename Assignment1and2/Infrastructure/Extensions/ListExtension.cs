using System;
namespace Infrastructure.Extensions
{
	public static class ListExtension
	{
        public static bool find<T>(this List<T> list, T target)
        {
            return list.Contains(target);
        }
    }
}

