﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore.Update;

namespace DataAccess.Context
{
	public class SoftDeleteInterceptor : SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			if (eventData.Context is null) return result;

			foreach (var entry in eventData.Context.ChangeTracker.Entries())
			{
				if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete }) continue;
				if (delete.FINALDELETE is true) return result;
				entry.State = EntityState.Modified;
				delete.IsDeleted = true;
				delete.FINALDELETE = true;
				delete.DeletedAt = DateTime.UtcNow;
			}
			return result;
		}
	}
}
