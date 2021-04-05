using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMyGameApp
{
	public static class DBExtender
	{
		/// <summary>
		/// Добавление нового объекта
		/// <para>Проверка осуществляется по наименованию <see cref="Entities.Entity.Name"/></para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbSet"></param>
		/// <param name="entities"></param>
		public static void AddIfNameNotExists<T>(this DbSet<T> dbSet, params T[] entities) where T : Entities.Entity
		{
			foreach (T entity in entities)
			{
				// Проверка на существование одноименного объекта
				if(!dbSet.Any(x => x.Name == entity.Name))
				{
					dbSet.Add(entity);
				}
			}
		}
	}
}
