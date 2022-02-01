using Microsoft.Extensions.Caching.Memory;

namespace RestCacheService.Constants;

public static class ClientConstants
{
    public static readonly MemoryCacheEntryOptions MemoryCacheEntryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.High);
}

