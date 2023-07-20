﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.EntityFrameworkCore;
using Snap.Hutao.Core.Database;
using Snap.Hutao.Model.Entity;
using Snap.Hutao.Model.Entity.Database;

namespace Snap.Hutao.Service.Cultivation;

[ConstructorGenerated]
[Injection(InjectAs.Singleton, typeof(ICultivationDbService))]
internal sealed partial class CultivationDbService : ICultivationDbService
{
    private readonly IServiceProvider serviceProvider;

    public List<InventoryItem> GetInventoryItemListByProjectId(Guid projectId)
    {
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return appDbContext.InventoryItems
                .AsNoTracking()
                .Where(a => a.ProjectId == projectId)
                .ToList();
        }
    }

    public async ValueTask<List<InventoryItem>> GetInventoryItemListByProjectIdAsync(Guid projectId)
    {
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return await appDbContext.InventoryItems
                .AsNoTracking()
                .Where(a => a.ProjectId == projectId)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }

    public async ValueTask<List<CultivateEntry>> GetCultivateEntryListByProjectIdAsync(Guid projectId)
    {
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return await appDbContext.CultivateEntries
                .AsNoTracking()
                .Where(e => e.ProjectId == projectId)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }

    public async ValueTask<List<CultivateItem>> GetCultivateItemListByEntryIdAsync(Guid entryId)
    {
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return await appDbContext.CultivateItems
                .Where(i => i.EntryId == entryId)
                .OrderBy(i => i.ItemId)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }

    public async ValueTask DeleteCultivateEntryByIdAsync(Guid entryId)
    {
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await appDbContext.CultivateEntries
                .ExecuteDeleteWhereAsync(i => i.InnerId == entryId)
                .ConfigureAwait(false);
        }
    }

    public void UpdateInventoryItem(InventoryItem item)
    {
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            appDbContext.InventoryItems.UpdateAndSave(item);
        }
    }

    public void UpdateCultivateItem(CultivateItem item)
    {
        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            appDbContext.CultivateItems.UpdateAndSave(item);
        }
    }
}